var BillController = function () {
    var cachedObj = {
        products: [],
        productItems: [],
        paymentMethods: [],
        billStatuses: []
    }
    this.initialize = function () {
        $.when(loadBillStatus(),
            loadPaymentMethod(),
            loadProducts())
            .done(function () {
                loadData();
            });

        registerEvents();
    }
    var delay = (function () {
        var timer = 0;
        return function (callback, ms) {
            clearTimeout(timer);
            timer = setTimeout(callback, ms);
        };
    })();
    function calculateTotalAmount() {
        var totalAmount = 0;
        $('#tbl-bill-details tr').each(function (i, item) {
            var total = parseFloat($(item).find('.hidTotal').first().val());
            totalAmount += total;
        });
        $('#lblTotalAmount').text(tedu.formatNumber(totalAmount, 0));
    }

    function registerEvents() {
        $('#txtFromDate, #txtToDate').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        });
        //Init validation
        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'vi',
            rules: {
                txtCustomerName: { required: true },
                txtCustomerAddress: { required: true },
                txtCustomerMobile: { required: true },
                txtCustomerMessage: { required: true },
                ddlBillStatus: { required: true }
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

        $("#btn-create").on('click', function () {
            resetFormMaintainance();
            $('#modal-detail').modal('show');
        });
        $("#ddl-show-page").on('change', function () {
            tedu.configs.pageSize = $(this).val();
            tedu.configs.pageIndex = 1;
            loadData(true);
        });

        $('body').on('change', '.ddlProductId', function () {
            var element = $(this);
            delay(function () {
                var productId = parseInt($(element).val());
                var lblTotal = $(element).closest('tr').find('.lblTotal').first();
                var hidTotal = $(element).closest('tr').find('.hidTotal').first();
                var quantity = $(element).closest('tr').find('.txtQuantity').first().val();

                if (isNaN(quantity)) {
                    lblTotal.text('0');
                    return false;
                }
                var products = $.grep(cachedObj.products, function (n) {
                    return n.Id === productId;
                });
                if (products.length > 0) {
                    var price = products[0].Price;
                    var total = price * quantity;
                    lblTotal.text(tedu.formatNumber(total, 0));
                    hidTotal.val(total);

                    calculateTotalAmount();
                } else {
                    $('#lblTotalAmount').text('0');
                    hidTotal.val('0');
                    lblTotal.text('0');
                    tedu.notify('Mã sản phẩm không tồn tại', 'error');
                }
                return false;
            }, 1000);
        });
        $('body').on('keydown', '.txtQuantity', function () {
            var element = $(this);
            delay(function () {
                var quantity = parseInt($(element).val());
                var lblTotal = $(element).closest('tr').find('.lblTotal').first();
                var hidTotal = $(element).closest('tr').find('.hidTotal').first();
                if (isNaN(quantity)) {
                    lblTotal.text('0');
                    return false;
                }
                var productId = parseInt($(element).closest('tr').find('.ddlProductId').first().val());
                var products = $.grep(cachedObj.products, function (n) {
                    return n.Id === productId;
                });
                if (products.length > 0) {
                    var price = products[0].Price;
                    var total = price * quantity;
                    lblTotal.text(tedu.formatNumber(total, 0));
                    hidTotal.val(total);
                    calculateTotalAmount();
                } else {
                    $('#lblTotalAmount').text('0');
                    hidTotal.val('0');
                    lblTotal.text('0');
                    tedu.notify('Mã sản phẩm không tồn tại', 'error');
                }
                return false;
            }, 1000);

        });
        $('body').on('click', '.btn-view', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            $.ajax({
                type: "GET",
                url: "/Admin/Bill/GetById",
                data: { id: that },
                beforeSend: function () {
                    tedu.startLoading();
                },
                success: function (response) {
                    var data = response;
                    $('#hidId').val(data.Id);
                    $('#txtCustomerName').val(data.CustomerName);

                    $('#txtCustomerAddress').val(data.CustomerAddress);
                    $('#txtCustomerMobile').val(data.CustomerMobile);
                    $('#txtCustomerMessage').val(data.CustomerMessage);
                    $('#ddlPaymentMethod').val(data.PaymentMethod);
                    $('#ddlCustomerId').val(data.CustomerId);
                    $('#ddlBillStatus').val(data.BillStatus);

                    $('#txtCustomerFacebook').val(data.CustomerFacebook);
                    $('#txtShippingFee').val(data.ShippingFee);

                    var billDetails = data.BillDetails;
                    if (data.BillDetails !== null && data.BillDetails.length > 0) {
                        var render = '';
                        var templateDetails = $('#template-table-bill-details').html();
                        var totalBill = 0;
                        $.each(billDetails, function (i, item) {
                            var total = item.Quantity * item.Product.Price;
                            var products = getProductOptions(item.ProductId, item.Price);
                            render += Mustache.render(templateDetails,
                                {
                                    Id: item.Id,
                                    Products: products,
                                    Price: item.Price,
                                    Quantity: item.Quantity,
                                    Total: tedu.formatNumber(total, 0),
                                    TotalNumber: total
                                });
                            totalBill += total;
                        });
                        $('#lblTotalAmount').text(tedu.formatNumber(totalBill, 0));
                        $('#tbl-bill-details').html(render);
                    }
                    $('#modal-detail').modal('show');
                    tedu.stopLoading();

                },
                error: function () {
                    tedu.notify('Có lỗi xảy ra', 'error');
                    tedu.stopLoading();
                }
            });
        });

        $('#btnSave').on('click', function (e) {
            if ($('#frmMaintainance').valid()) {
                e.preventDefault();
                var id = $('#hidId').val();
                var customerName = $('#txtCustomerName').val();
                var customerAddress = $('#txtCustomerAddress').val();
                var customerId = $('#ddlCustomerId').val();
                var customerMobile = $('#txtCustomerMobile').val();
                var customerMessage = $('#txtCustomerMessage').val();
                var customerFacebook = $('#txtCustomerFacebook').val();
                var shippingFee = $('#txtShippingFee').val();
                var paymentMethod = $('#ddlPaymentMethod').val();
                var billStatus = $('#ddlBillStatus').val();
                //bill detail

                var billDetails = [];
                $.each($('#tbl-bill-details tr'), function (i, item) {
                    billDetails.push({
                        Id: $(item).data('id'),
                        ProductId: $(item).find('select.ddlProductId').first().val(),
                        Quantity: $(item).find('input.txtQuantity').first().val(),
                        BillId: id
                    });

                });
                console.log(billDetails);
                $.ajax({
                    type: "POST",
                    url: "/Admin/Bill/SaveEntity",
                    data: {
                        Id: id,
                        BillStatus: billStatus,
                        CustomerAddress: customerAddress,
                        CustomerId: customerId,
                        CustomerMessage: customerMessage,
                        CustomerMobile: customerMobile,
                        CustomerName: customerName,
                        CustomerFacebook: customerFacebook,
                        ShippingFee: shippingFee,
                        PaymentMethod: paymentMethod,
                        Status: 1,
                        BillDetails: billDetails
                    },
                    dataType: "json",
                    beforeSend: function () {
                        tedu.startLoading();
                    },
                    success: function () {
                        tedu.notify('Cập nhật thành công', 'success');
                        $('#modal-detail').modal('hide');
                        resetFormMaintainance();

                        tedu.stopLoading();
                        loadData(true);
                    },
                    error: function () {
                        tedu.notify('Cập nhật có lỗi xảy ra', 'error');
                        tedu.stopLoading();
                    }
                });
                return false;
            }
            return false;
        });
        $('#btnConfirm').on('click', function (e) {
            e.preventDefault();
            var id = $('#hidId').val();
            $.ajax({
                type: "POST",
                url: "/Admin/Bill/ConfirmBill",
                data: {
                    billId: id
                },
                beforeSend: function () {
                    tedu.startLoading();
                },
                success: function (response) {
                    tedu.notify(response, 'success');
                    $('#modal-detail').modal('hide');
                    resetFormMaintainance();
                    tedu.stopLoading();
                    loadData(true);
                },
                error: function (err) {
                    tedu.notify(err.responseText, 'error');
                    tedu.stopLoading();
                }
            });
            return false;
        });

        $('#btnCancel').on('click', function (e) {
            e.preventDefault();
            var id = $('#hidId').val();
            $.ajax({
                type: "POST",
                url: "/Admin/Bill/CancelBill",
                data: {
                    billId: id
                },
                beforeSend: function () {
                    tedu.startLoading();
                },
                success: function (response) {
                    tedu.notify(response, 'success');
                    $('#modal-detail').modal('hide');
                    resetFormMaintainance();
                    tedu.stopLoading();
                    loadData(true);
                },
                error: function (err) {
                    tedu.notify(err.responseText, 'error');
                    tedu.stopLoading();
                }
            });
            return false;
        });

        $('#btnPending').on('click', function (e) {
            e.preventDefault();
            var id = $('#hidId').val();
            $.ajax({
                type: "POST",
                url: "/Admin/Bill/PendingBill",
                data: {
                    billId: id
                },
                beforeSend: function () {
                    tedu.startLoading();
                },
                success: function (response) {
                    tedu.notify(response, 'success');
                    $('#modal-detail').modal('hide');
                    resetFormMaintainance();
                    tedu.stopLoading();
                    loadData(true);
                },
                error: function (err) {
                    tedu.notify(err.responseText, 'error');
                    tedu.stopLoading();
                }
            });
            return false;
        });

        $('#btnAddDetail').on('click', function () {
            var template = $('#template-table-bill-details').html();
            var render = Mustache.render(template,
                {
                    Id: 0,
                    Products: getProductOptions(0, 0),
                    Quantity: 0,
                    Total: 0,
                    TotalNumber: 0
                });
            $('#tbl-bill-details').append(render);
        });

        $('body').on('click', '.btn-delete-detail', function () {
            $(this).parent().parent().remove();
        });

        $("#btnExport").on('click', function () {
            var that = $('#hidId').val();
            $.ajax({
                type: "POST",
                url: "/Admin/Bill/ExportExcel",
                data: { billId: that },
                beforeSend: function () {
                    tedu.startLoading();
                },
                success: function (response) {
                    window.location.href = response;

                    tedu.stopLoading();

                }
            });
        });
    };

    function loadBillStatus() {
        return $.ajax({
            type: "GET",
            url: "/admin/bill/GetBillStatus",
            dataType: "json",
            success: function (response) {
                cachedObj.billStatuses = response;
                var render = "";
                $.each(response, function (i, item) {
                    render += "<option value='" + item.Value + "'>" + item.Name + "</option>";
                });
                $('#ddlBillStatus').html(render);
            }
        });
    }
    function loadPaymentMethod() {
        return $.ajax({
            type: "GET",
            url: "/admin/bill/GetPaymentMethod",
            dataType: "json",
            success: function (response) {
                cachedObj.paymentMethods = response;
                var render = "";
                $.each(response, function (i, item) {
                    render += "<option value='" + item.Value + "'>" + item.Name + "</option>";
                });
                $('#ddlPaymentMethod').html(render);
            }
        });
    }

    function loadProducts() {
        return $.ajax({
            type: "GET",
            url: "/Admin/Product/GetAll",
            dataType: "json",
            beforeSend: function () {
                tedu.startLoading();
            },
            success: function (response) {
                cachedObj.products = response;
                tedu.stopLoading();
                $('#btn-create').removeAttr('disabled');
            },
            error: function () {
                tedu.notify('Có lỗi xảy ra', 'error');
            }
        });
    }
    function getProductOptions(selectedId, price) {
        var products = "<select data-price='" + price + "' class='form-control ddlProductId'>";
        $.each(cachedObj.products, function (i, product) {
            if (selectedId === product.Id)
                products += '<option value="' + product.Id + '" selected="select">' + product.Name + "-" + product.Code + '</option>';
            else
                products += '<option value="' + product.Id + '">' + product.Name + "-" + product.Code + '</option>';
        });
        products += "</select>";
        return products;
    }
    function resetFormMaintainance() {
        $('#hidId').val(0);
        $('#txtCustomerName').val('');

        $('#txtCustomerAddress').val('');
        $('#txtCustomerMobile').val('');
        $('#txtCustomerMessage').val('');
        $('#ddlPaymentMethod').val('');
        $('#ddlCustomerId').val('');
        $('#ddlBillStatus').val('');
        $('#tbl-bill-details').html('');

        $('#txtCustomerFacebook').val('');
        $('#txtShippingFee').val('0');
        $('#lblTotalAmount').val('0');
    }

    function loadData(isPageChanged) {
        $.ajax({
            type: "GET",
            url: "/admin/bill/GetAllPaging",
            data: {
                startDate: $('#txtFromDate').val(),
                endDate: $('#txtToDate').val(),
                keyword: $('#txtSearchKeyword').val(),
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
                            CustomerName: item.CustomerName,
                            Id: item.Id,
                            PaymentMethod: getPaymentMethodName(item.PaymentMethod),
                            DateCreated: tedu.dateTimeFormatJson(item.DateCreated),
                            BillStatus: getBillStatusName(item.BillStatus)
                        });
                    });
                    $("#lbl-total-records").text(response.RowCount);
                    if (render !== undefined) {
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
    function getPaymentMethodName(paymentMethod) {
        var method = $.grep(cachedObj.paymentMethods, function (element) {
            return element.Value === paymentMethod;
        });
        if (method.length > 0)
            return method[0].Name;
        else return '';
    }
    function getBillStatusName(status) {
        if (status === 1)
            return '<span class="badge bg-red">Đơn mới</span>';
        else if (status === 2)
            return '<span class="badge badge-primary">Đang xử lý</span>';
        else if (status === 3)
            return '<span class="badge badge-secondary">Đã huỷ</span>';
        else if (status === 5)
            return '<span class="badge badge-info">Đang giao</span>';
        else if (status === 6)
            return '<span class="badge badge-success">Đã hoàn tất</span>';
        else if (status === 7)
            return '<span class="badge badge-warning">Đã hoãn</span>';
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