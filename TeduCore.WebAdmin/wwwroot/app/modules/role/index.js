var RoleController = function () {
    var self = this;
    this.initialize = function () {
        kendo.bind($("#role-view"), viewModel);
    }

    var viewModel = kendo.observable({
        isVisible: true,
        onSave: function (e) {
            kendoConsole.log("event :: save(" + kendo.stringify(e.values, null, 4) + ")");
        },
        roles: new kendo.data.DataSource({
            schema: {
                model: {
                    id: "Id",
                    fields: {
                        Name: { type: "string" },
                        Description: { type: "string" }
                    }
                }
            },
            batch: true,
            transport: {
                read: {
                    url: SETTINGS.API_URL + "/api/role",
                    type:'GET',
                    dataType: "jsonp"
                },
                update: {
                    url: SETTINGS.API_URL + "/api/role",
                    dataType: "jsonp"
                },
                create: {
                    url: SETTINGS.API_URL + "/api/role",
                    dataType: "jsonp"
                },
                parameterMap: function (options, operation) {
                    if (operation !== "read" && options.models) {
                        return { models: kendo.stringify(options.models) };
                    }
                }
            }
        })
    });
}

