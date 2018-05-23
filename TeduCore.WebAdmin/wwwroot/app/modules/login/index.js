var LoginController = function () {
    var self = this;

    var viewModel = kendo.observable({
        userName: "",
        password: "",
        isRememberMe: false,
        events: {
            login: authenticate
        }
    });

    this.initialize = function () {
        kendo.bind($("#login-view"), viewModel);
    }

    function authenticate() {
        $.ajax({
            url: SETTINGS.API_URL + "/api/account/login",
            data: JSON.stringify({
                UserName: viewModel.userName,
                Password: viewModel.password,
                RememberMe: viewModel.isRememberMe
            }),
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            success: function (response) {
                var token = response.token;
                sessionStorage.setItem(SETTINGS.TOKEN_STORAGE, token);
                window.location.href = "/Home/Index";
            },
            error: function (err) {
                notification.showError(err.responseJSON);
            }
        });
    }
}