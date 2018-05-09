var blogController = function () {
    this.initialize = function () {
        loadData();
        registerEvents();
        registerControls();
    }

    function registerEvents() {
        //Init validation
        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'vi',
            rules: {
                txtNameM: { required: true }
            }
        });

        $('#txt-search-keyword').keypress(function (e) {
            if (e.which === 13) {
                e.preventDefault();
                loadData();
            }
        });
        $("#btn-search").on('click', function () {
            loadData();
        });
        $("#fileInputImage").on('change', function () {
            var fileUpload = $(this).get(0);
            var files = fileUpload.files;
            var data = new FormData();
            for (var i = 0; i < files.length; i++) {
                data.append(files[i].name, files[i]);
            }
            $.ajax({
                type: "POST",
                url: "/Admin/Upload/UploadImage",
                contentType: false,
                processData: false,
                data: data,
                success: function (path) {
                    $('#txtImageM').val(path);
                    tedu.notify('Đã tải ảnh lên thành công!', 'success');

                },
                error: function () {
                    tedu.notify('There was error uploading files!', 'error');
                }
            });
        });
        $("#btnSelectImg").on('click', function () {
            $('#fileInputImage').click();
        });
        $("#ddl-show-page").on('change', function () {
            tedu.configs.pageSize = $(this).val();
            tedu.configs.pageIndex = 1;
            loadData(true);
        });

        $("#btn-create").on('click', function () {
            resetFormMaintainance();
            $('#modal-add-edit').modal('show');

        });

        $('body').on('click', '.btn-edit', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            $.ajax({
                type: "GET",
                url: "/Admin/Blog/GetById",
                data: { id: that },
                dataType: "json",
                beforeSend: function () {
                    tedu.startLoading();
                },
                success: function (response) {
                    var data = response;
                    $('#hidIdM').val(data.Id);
                    $('#txtNameM').val(data.Name);
                    $('#txtDescM').val(data.Description);


                    $('#txtImageM').val(data.Image);

                    $('#txtTagM').val(data.Tags);
                    $('#txtMetakeywordM').val(data.SeoKeywords);
                    $('#txtMetaDescriptionM').val(data.SeoDescription);
                    $('#txtSeoPageTitleM').val(data.SeoPageTitle);
                    $('#txtSeoAliasM').val(data.SeoAlias);

                    CKEDITOR.instances.txtContentM.setData(data.Content);
                    $('#ckStatusM').prop('checked', data.Status === 1);
                    $('#ckHotM').prop('checked', data.HotFlag);
                    $('#ckShowHomeM').prop('checked', data.HomeFlag);

                    $('#modal-add-edit').modal('show');
                    tedu.stopLoading();

                },
                error: function () {
                    tedu.notify('Có lỗi xảy ra', 'error');
                    tedu.stopLoading();
                }
            });
        });

        $('#btnSaveM').on('click', function (e) {
            if ($('#frmMaintainance').valid()) {
                e.preventDefault();
                var id = $('#hidIdM').val();
                var name = $('#txtNameM').val();

                var description = $('#txtDescM').val();

                var image = $('#txtImageM').val();

                var tags = $('#txtTagM').val();
                var seoKeyword = $('#txtMetakeywordM').val();
                var seoMetaDescription = $('#txtMetaDescriptionM').val();
                var seoPageTitle = $('#txtSeoPageTitleM').val();
                var seoAlias = $('#txtSeoAliasM').val();

                var content = CKEDITOR.instances.txtContentM.getData();
                var status = $('#ckStatusM').prop('checked') === true ? 1 : 0;
                var hot = $('#ckHotM').prop('checked');
                var showHome = $('#ckShowHomeM').prop('checked');

                $.ajax({
                    type: "POST",
                    url: "/Admin/Blog/SaveEntity",
                    data: {
                        Id: id,
                        Name: name,
                        Image: image,
                        Description: description,
                        Content: content,
                        HomeFlag: showHome,
                        HotFlag: hot,
                        Tags: tags,
                        Status: status,
                        SeoPageTitle: seoPageTitle,
                        SeoAlias: seoAlias,
                        SeoKeywords: seoKeyword,
                        SeoDescription: seoMetaDescription
                    },
                    dataType: "json",
                    beforeSend: function () {
                        tedu.startLoading();
                    },
                    success: function () {
                        tedu.notify('Cập nhật sản phẩm thành công', 'success');
                        $('#modal-add-edit').modal('hide');
                        resetFormMaintainance();

                        tedu.stopLoading();
                        loadData(true);
                    },
                    error: function () {
                        tedu.notify('Cập nhật sản phẩm có lỗi xảy ra', 'error');
                        tedu.stopLoading();
                    }
                });
                return false;
            }
            return false;
        });

        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            tedu.confirm('Bạn có chắc chắn muốn xoá?', function () {
                $.ajax({
                    type: "POST",
                    url: "/Admin/Blog/Delete",
                    data: { id: that },
                    dataType: "json",
                    beforeSend: function () {
                        tedu.startLoading();
                    },
                    success: function () {
                        tedu.notify('Xoá thành công', 'success');
                        tedu.stopLoading();
                        loadData();
                    },
                    error: function () {
                        tedu.notify('Xoá không thành công', 'error');
                        tedu.stopLoading();
                    }
                });
            });
        });
    };

    function resetFormMaintainance() {
        $('#hidIdM').val(0);
        $('#txtNameM').val('');

        $('#txtDescM').val('');

        $('#txtImageM').val('');

        $('#txtTagM').val('');
        $('#txtMetakeywordM').val('');
        $('#txtMetaDescriptionM').val('');
        $('#txtSeoPageTitleM').val('');
        $('#txtSeoAliasM').val('');

        CKEDITOR.instances.txtContentM.setData('');
        $('#ckStatusM').prop('checked', true);
        $('#ckHotM').prop('checked', false);
        $('#ckShowHomeM').prop('checked', false);

    }

    function registerControls() {
        var editorConfig = {
            filebrowserImageUploadUrl: '/Admin/Upload/UploadImageForCKEditor?type=Images'
        }
        CKEDITOR.replace('txtContentM', editorConfig);
        //Fix: cannot click on element ck in modal
        $.fn.modal.Constructor.prototype.enforceFocus = function () {
            $(document)
                .off('focusin.bs.modal') // guard against infinite focus loop
                .on('focusin.bs.modal', $.proxy(function (e) {
                    if (
                        this.$element[0] !== e.target && !this.$element.has(e.target).length
                        // CKEditor compatibility fix start.
                        && !$(e.target).closest('.cke_dialog, .cke').length
                        // CKEditor compatibility fix end.
                    ) {
                        this.$element.trigger('focus');
                    }
                }, this));
        };
    }



    function loadData(isPageChanged) {
        $.ajax({
            type: "GET",
            url: "/admin/blog/GetAllPaging",
            data: {
                keyword: $('#txt-search-keyword').val(),
                page: tedu.configs.pageIndex,
                pageSize: tedu.configs.pageSize
            },
            dataType: "json",
            beforeSend: function () {
                tedu.startLoading();
            },
            success: function (response) {
                var template = $('#table-template').html();
                var render = "";
                if (response.RowCount > 0) {
                    $.each(response.Results, function (i, item) {
                        render += Mustache.render(template, {
                            Name: item.Name,
                            Id: item.Id,
                            Image: item.Image == undefined ? '<img src="/admin-side/images/user.png" width=25 />' : '<img src="' + item.Image + '" width=25 />',
                            DateCreated: tedu.dateTimeFormatJson(item.DateCreated),
                            Status: tedu.getStatus(item.Status)
                        });
                    });
                    $("#lbl-total-records").text(response.RowCount);
                    if (render != undefined) {
                        $('#tbl-content').html(render);

                    }
                    wrapPaging(response.RowCount, function () {
                        loadData();
                    }, isPageChanged);


                }
                else {
                    $('#tbl-content').html('');
                }
                tedu.stopLoading();
            },
            error: function (status) {
                console.log(status);
            }
        });
    };

    function wrapPaging(recordCount, callBack, changePageSize) {
        var totalsize = Math.ceil(recordCount / tedu.configs.pageSize);
        //Unbind pagination if it existed or click change pagesize
        if ($('#paginationUL a').length === 0 || changePageSize === true) {
            $('#paginationUL').empty();
            $('#paginationUL').removeData("twbs-pagination");
            $('#paginationUL').unbind("page");
        }
        //Bind Pagination Event
        $('#paginationUL').twbsPagination({
            totalPages: totalsize,
            visiblePages: 7,
            first: 'Đầu',
            prev: 'Trước',
            next: 'Tiếp',
            last: 'Cuối',
            onPageClick: function (event, p) {
                tedu.configs.pageIndex = p;
                setTimeout(callBack(), 200);
            }
        });
    }
}