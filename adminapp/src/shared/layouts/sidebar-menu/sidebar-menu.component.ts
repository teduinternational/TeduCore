import { Component, OnInit, AfterViewChecked } from '@angular/core';
import { DataService } from './../../services/data.service';

@Component({
    selector: 'app-sidebar-menu',
    templateUrl: './sidebar-menu.component.html',
    styleUrls: ['./sidebar-menu.component.css']
})
export class SidebarMenuComponent implements OnInit, AfterViewChecked {
    public functions: any[];
    constructor(private dataService: DataService) { }

    ngOnInit() {
        this.dataService.get('/api/function').subscribe((response: any) => {
            var data = response.sort((n1, n2) => {
                if (n1.DisplayOrder > n2.DisplayOrder)
                    return 1;
                else if (n1.DisplayOrder < n2.DisplayOrder)
                    return -1;
                return 0;
            });
            this.functions = data;
        }, error => this.dataService.handleError(error));
    }

    ngAfterViewChecked() {

        // TODO: This is some kind of easy fix, maybe we can improve this
        var setContentHeight = function () {
            // reset height
            $('.right_col').css('min-height', $(window).height());

            var bodyHeight = $('body').outerHeight(),
                footerHeight = $('body').hasClass('footer_fixed') ? 0 : $('footer').height(),
                leftColHeight = $('.left_col').eq(1).height() + $('.sidebar-footer').height(),
                contentHeight = bodyHeight < leftColHeight ? leftColHeight : bodyHeight;

            // normalize content
            contentHeight -= $('.nav_menu').height() + footerHeight;

            $('.right_col').css('min-height', contentHeight);
        };

        $('#sidebar-menu').find('a').off('click').on('click', function (ev) {
            var $li = $(this).parent();

            if ($li.is('.active')) {
                $li.removeClass('active active-sm');
                $('ul:first', $li).slideUp(function () {
                    setContentHeight();
                });
            } else {
                // prevent closing menu if we are on child menu
                if (!$li.parent().is('.child_menu')) {
                    $('#sidebar-menu').find('li').removeClass('active active-sm');
                    $('#sidebar-menu').find('li ul').slideUp();
                }

                $li.addClass('active');

                $('ul:first', $li).slideDown(function () {
                    setContentHeight();
                });
            }
        });

        // toggle small or large menu
        $('#menu_toggle').off('click').on('click', function () {
            if ($('body').hasClass('nav-md')) {
                $('#sidebar-menu').find('li.active ul').hide();
                $('#sidebar-menu').find('li.active').addClass('active-sm').removeClass('active');
            } else {
                $('#sidebar-menu').find('li.active-sm ul').show();
                $('#sidebar-menu').find('li.active-sm').addClass('active').removeClass('active-sm');
            }

            $('body').toggleClass('nav-md nav-sm');

            setContentHeight();
        });

        // check active menu
        $('#sidebar-menu').find('a[href="' + window.location.href.split('?')[0] + '"]').parent('li').addClass('current-page');

        $('#sidebar-menu').find('a').filter(function () {
            return this.href == window.location.href.split('?')[0];
        }).parent('li').addClass('current-page').parents('ul').slideDown(function () {
            setContentHeight();
        }).parent().addClass('active');

        // recompute content when resizing
        $(window).smartresize(function () {
            setContentHeight();
        });

        setContentHeight();

        // fixed sidebar
        if ($.fn.mCustomScrollbar) {
            $('.menu_fixed').mCustomScrollbar({
                autoHideScrollbar: true,
                theme: 'minimal',
                mouseWheel: { preventDefault: true }
            });
        }
    }

}
