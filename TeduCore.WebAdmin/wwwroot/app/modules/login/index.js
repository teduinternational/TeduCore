var LoginController = function () {
    var self = this;

    var viewModel = kendo.observable({
        userName: "",
        password: "",
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
            data: {
                userName: viewModel.userName,
                password: viewModel.password
            }, 
            type: 'POST',
            contentType: 'json',
            success: function (response) {
                var token = response.token;
                sessionStorage.setItem(SETTINGS.TOKEN_STORAGE, token);
            }
        });
    }
}