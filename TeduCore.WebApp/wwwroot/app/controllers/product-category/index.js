var productCategoryController = function () {
    var self = this;
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
                txtOrderM: { number: true },
                txtCodeM: { required: true },
                txtHomeOrderM: { number: true }
            }
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

        $("#btn-create").on('click', function () {
            //resetForm();
            initTreeDropDownCategory();
            resetFormMaintainance();
            $('#modal-add-edit').modal('show');

        });

        $('body').on('click', '#btn-edit', function (e) {
            e.preventDefault();
            var that = $('#hidIdM').val();
            fillData(that);
        });

        $('#btnSaveM').on('click', function (e) {
            if ($('#frmMaintainance').valid()) {
                e.preventDefault();
                var id = parseInt($('#hidIdM').val());
                var name = $('#txtNameM').val();
                var code = $('#txtCodeM').val();
                var parentId = $('#ddlCategoryIdM').combotree('getValue');
                var description = $('#txtDescM').val();

                var image = $('#txtImageM').val();
                var order = $('#txtOrderM').val();
                var homeOrder = $('#txtHomeOrderM').val();

                var seoKeyword = $('#txtSeoKeywordM').val();
                var seoMetaDescription = $('#txtSeoDescriptionM').val();
                var seoPageTitle = $('#txtSeoPageTitleM').val();
                var seoAlias = $('#txtSeoAliasM').val();
                var status = $('#ckStatusM').prop('checked') === true ? 1 : 0;
                var showHome = $('#ckShowHomeM').prop('checked');

                $.ajax({
                    type: "POST",
                    url: "/Admin/ProductCategory/SaveEntity",
                    data: {
                        Id: id,
                        Name: name,
                        Code: code,
                        Description: description,
                        ParentId: parentId,
                        HomeOrder: homeOrder,
                        SortOrder: order,
                        HomeFlag: showHome,
                        Image: image,
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

        });

        $('body').on('click', '#btn-delete', function (e) {
            e.preventDefault();
            var that = $('#hidIdM').val();
            tedu.confirm('Bạn có chắc chắn muốn xoá?', function () {
                $.ajax({
                    type: "POST",
                    url: "/Admin/ProductCategory/Delete",
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
    function fillData(that) {
        $.ajax({
            type: "GET",
            url: "/Admin/ProductCategory/GetById",
            data: { id: that },
            dataType: "json",
            beforeSend: function () {
                tedu.startLoading();
            },
            success: function (response) {
                var data = response;
                $('#hidIdM').val(data.Id);
                $('#txtNameM').val(data.Name);
                $('#txtCodeM').val(data.Code);
                initTreeDropDownCategory(data.CategoryId);

                $('#txtDescM').val(data.Description);

                $('#txtImageM').val(data.ThumbnailImage);

                $('#txtSeoKeywordM').val(data.SeoKeywords);
                $('#txtSeoDescriptionM').val(data.SeoDescription);
                $('#txtSeoPageTitleM').val(data.SeoPageTitle);
                $('#txtSeoAliasM').val(data.SeoAlias);

                $('#ckStatusM').prop('checked', data.Status === 1);
                $('#ckShowHomeM').prop('checked', data.HomeFlag);
                $('#txtOrderM').val(data.SortOrder);
                $('#txtHomeOrderM').val(data.HomeOrder);

                $('#modal-add-edit').modal('show');
                tedu.stopLoading();

            },
            error: function () {
                tedu.notify('Có lỗi xảy ra', 'error');
                tedu.stopLoading();
            }
        });
    }
    function initTreeDropDownCategory(selectedId) {
        $.ajax({
            url: "/Admin/ProductCategory/GetAll",
            type: 'GET',
            dataType: 'json',
            async: false,
            success: function (response) {
                var data = [];
                $.each(response, function (i, item) {
                    data.push({
                        id: item.Id,
                        text: item.Name,
                        parentId: item.ParentId,
                        sortOrder: item.SortOrder
                    });
                });
                var arr = tedu.unflattern1(data);
                $('#ddlCategoryIdM').combotree({
                    data: arr
                });
                if (selectedId != undefined) {
                    $('#ddlCategoryIdM').combotree('setValue', selectedId);
                }
            }
        });
    }

    function resetFormMaintainance() {
        $('#hidIdM').val("0");
        $('#txtNameM').val('');
        $('#txtCodeM').val('');
        initTreeDropDownCategory('');

        $('#txtDescM').val('');
        $('#txtOrderM').val('');
        $('#txtHomeOrderM').val('');
        $('#txtImageM').val('');

        $('#txtSeoKeywordM').val('');
        $('#txtSeoDescriptionM').val('');
        $('#txtSeoPageTitleM').val('');
        $('#txtSeoAliasM').val('');

        $('#ckStatusM').prop('checked', true);
        $('#ckShowHomeM').prop('checked', false);

    }

    function loadData() {
        $.ajax({
            url: "/Admin/ProductCategory/GetAll",
            type: 'GET',
            dataType: 'json',
            async: false,
            success: function (response) {
                var data = [];
                $.each(response, function (i, item) {
                    data.push({
                        id: item.Id,
                        text: item.Name,
                        parentId: item.ParentId,
                        sortOrder: item.SortOrder
                    });
                });
                var arr = tedu.unflattern1(data);
                $('#treeProductCategory').tree({
                    data: arr,
                    dnd: true,
                    onDblClick: function (node) {
                        fillData(node.id);
                    },
                    onContextMenu: function (e, node) {
                        e.preventDefault();
                        // select the node
                        $('#tt').tree('select', node.target);
                        $('#hidIdM').val(node.id);
                        // display context menu
                        $('#mm').menu('show', {
                            left: e.pageX,
                            top: e.pageY
                        });
                    },
                    onDrop: function (target, source, point) {
                        var targetNode = $(this).tree('getNode', target);
                        if (point === 'append') {
                            var children = [];
                            $.each(targetNode.children, function (i, item) {
                                children.push({
                                    key: item.id,
                                    value: i
                                });
                            });

                            $.ajax({
                                url: '/Admin/ProductCategory/UpdateParentId',
                                type: 'post',
                                dataType: 'json',
                                data: {
                                    sourceId: source.id,
                                    targetId: targetNode.id,
                                    items: children
                                },
                                success: function () {
                                    loadData();
                                }
                            });
                        }
                        else if (point === 'top' || point === 'bottom') {
                            $.ajax({
                                url: '/Admin/ProductCategory/ReOrder',
                                type: 'post',
                                dataType: 'json',
                                data: {
                                    sourceId: source.id,
                                    targetId: targetNode.id
                                },
                                success: function () {
                                    loadData();
                                }
                            });
                        }
                    }
                });
            }
        });

    }
}