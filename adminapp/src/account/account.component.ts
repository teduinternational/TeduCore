import { Component, ViewContainerRef, OnInit, Injector, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import * as moment from 'moment';
import * as _ from 'lodash';

@Component({
    selector: 'app-account',
    templateUrl: './account.component.html',
    styleUrls: [
        './account.component.less'
    ],
    encapsulation: ViewEncapsulation.None
})
export class AccountComponent implements OnInit {

    private viewContainerRef: ViewContainerRef;

    currentYear: number = moment().year();

    public constructor(
        injector: Injector,
        private _router: Router,
        viewContainerRef: ViewContainerRef
    ) {
        this.viewContainerRef = viewContainerRef; // We need this small hack in order to catch application root view container ref for modals
    }


    ngOnInit(): void {
        $('body').attr('class', 'login');
    }
}
