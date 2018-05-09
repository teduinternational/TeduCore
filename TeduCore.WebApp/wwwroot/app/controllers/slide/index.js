var SlideController = function () {

    this.initialize = function () {
        loadData();
        registerEvents();
    }

    function registerEvents() {
        //Init validation
        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'vi',
            rules: {
                txtNameM: { required: true },
                txtImageM: { required: true },
                txtGroupAliasM: { required: true },
                txtDisplayOrderM: { required: true, number: true }

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
            getDetail(that);
        });

        $('#btnSaveM').on('click', function (e) {
            if ($('#frmMaintainance').valid()) {
                e.preventDefault();
                saveData();
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
                    url: "/Admin/Slide/Delete",
                    data: { id: that },
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
        $('#txtGroupAliasM').val('');
        $('#txtDisplayOrderM').val('');
        $('#txtUrlM').val('');
        $('#ckStatusM').prop('checked', true);

    }

    function loadData(isPageChanged) {
        $.ajax({
            type: "GET",
            url: "/Admin/Slide/GetAllPaging",
            data: {
                categoryId: $('#ddl-category-search').val(),
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
                            GroupAlias: item.GroupAlias,
                            Image: item.Image == undefined ? '<img src="/admin-side/images/user.png" width=25 />' : '<img src="' + item.Image + '" width=25 />',
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

    function saveData() {
        var id = $('#hidIdM').val();
        var name =$('#txtNameM').val();
        var desc =$('#txtDescM').val();
        var image =$('#txtImageM').val();
        var groupAlias = $('#txtGroupAliasM').val();
        var displayOrder =$('#txtDisplayOrderM').val();
        var url = $('#txtUrlM').val();
        var status = $('#ckStatusM').prop('checked');

        $.ajax({
            type: "POST",
            url: "/Admin/Slide/SaveEntity",
            data: {
                Id: id,
                Name: name,
                Description: desc,
                DisplayOrder: displayOrder,
                GroupAlias: groupAlias,
                Image: image,
                Url: url,
                Status: status
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
    }

    function getDetail(that) {
        $.ajax({
            type: "GET",
            url: "/Admin/Slide/GetById",
            data: { id: that },
            dataType: "json",
            beforeSend: function () {
                tedu.startLoading();
            },
            success: function (response) {
                var data = response;
                $('#hidIdM').val(data.Id);
                $('#txtNameM').val(data.Name);
                $('#txtGroupAliasM').val(data.GroupAlias);
                $('#txtDisplayOrderM').val(data.DisplayOrder);
                $('#txtUrlM').val(data.Url);
                $('#txtDescM').val(data.Description);
                $('#txtImageM').val(data.Image);
                $('#ckStatusM').prop('checked', data.Status === true);

                $('#modal-add-edit').modal('show');
                tedu.stopLoading();

            },
            error: function () {
                tedu.notify('Có lỗi xảy ra', 'error');
                tedu.stopLoading();
            }
        });
    }

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