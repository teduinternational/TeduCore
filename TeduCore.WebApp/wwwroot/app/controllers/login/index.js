var LoginController = function () {

    var login = function (user, pass) {
        $.ajax({
            type: 'POST',
            data: {
                UserName: user,
                Password: pass
            },
            dataType: 'json',
            url: '/admin/login/authen',
            beforeSend: function () {
                tedu.startLoading();
            },
            success: function (res) {
                if (res.Success) {
                    window.location.href = '/Admin/Home/Index';
                }
                else{
                    tedu.notify('Đăng nhập không đúng','error');
                }
            }
        });
    }
    var registerEvents = function () {
        $('#btnLogin').off('click').on('click', function () {
            var user = $('#txtUserName').val();
            var password = $('#txtPassword').val();
            login(user, password);
        });
    }

    this.initialize = function () {
        registerEvents();
    }

   
}