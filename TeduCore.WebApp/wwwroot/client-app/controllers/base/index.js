var BaseController = function () {
    this.initialize = function () {
        registerEvents();
    }

    function registerEvents() {
        $('body').on('click', '.add-to-cart', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            $.ajax({
                url: '/Cart/AddToCart',
                type: 'post',
                data: {
                    productId: id,
                    quantity: 1
                },
                success: function (response) {
                    tedu.notify('Thêm giỏ hàng thành công', 'success');
                    loadHeaderCart();
                    loadMyCart();
                }
            });
        });

        $('body').on('click', '.remove-cart', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            $.ajax({
                url: '/Cart/RemoveFromCart',
                type: 'post',
                data: {
                    productId: id
                },
                success: function (response) {
                    tedu.notify('Xoá sản phẩm thành công', 'success');
                    loadHeaderCart();
                    loadMyCart();
                }
            });
        });
    }

    function loadHeaderCart() {
        $("#headerCart").load("/AjaxContent/HeaderCart");
    }

    function loadMyCart() {
        $("#sidebarCart").load("/AjaxContent/MyCart");
    }
}