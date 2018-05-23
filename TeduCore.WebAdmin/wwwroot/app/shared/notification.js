var notification = {
    showInfo: function (msg) {
        $("#notificationContent").data("kendoNotification").info(msg);
    },
    showError: function (msg) {
        $("#notificationContent").data("kendoNotification").error(msg);
    },
    showWarning: function (msg) {
        $("#notificationContent").data("kendoNotification").warning(msg);
    },
    showSuccess: function (msg) {
        $("#notificationContent").data("kendoNotification").success(msg);
    }
}