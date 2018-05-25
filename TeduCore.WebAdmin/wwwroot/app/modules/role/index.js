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
           pageSize: 10,
            transport: {
                read: {
                    cache: false,
                    url: SETTINGS.API_URL + "/api/role/getAllPaging",
                    dataType: "json",
                    type: "GET",
                    beforeSend: function(req) {
                        req.setRequestHeader('Authorization', 'Bearer ' + sessionStorage.getItem(SETTINGS.TOKEN_STORAGE));
                    }
                },
                update: {
                    url: SETTINGS.API_URL + "/api/role",
                    dataType: "json", //"jsonp" is required for cross-domain requests; use "json" for same-domain requests
                    type: 'PUT',
                    beforeSend: function (req) {
                        req.setRequestHeader('Authorization', 'Bearer ' + sessionStorage.getItem(SETTINGS.TOKEN_STORAGE));
                    }
                },
                parameterMap: function (options, type) {
                    return JSON.stringify(options);
                }
            },
            contentType: 'application/json',
            schema: {
                model: {
                    id: "Id",
                    fields: {
                        Id: { type: "string" },
                        Name: { type: "string" },
                        Description: { type: "string" }
                    }
                },
                data: function (response) {
                    return response.Data;
                }, // records are returned in the "data" field of the response,
                total: function (response) {
                    return response.Total;
                } // total number of records is in the "total" field of the response
            },
            serverPaging: true,
            serverFiltering: true,
            serverSorting: true,
            pageSizes: [10, 20, 50, 100, 200],
            batch: true,
        })
    });
}

