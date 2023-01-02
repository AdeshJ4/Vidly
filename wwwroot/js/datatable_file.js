$(document).ready(function () { 
    $("#customers").DataTable({
        ajax: {
            url: "/api/customers",
            dataSrc: ""
        },
        columns: [
                
            // column 1
            {
                data: "id"
            },
            //column 2
            {
                data: "name",
                render: function (data, type, customer) {
                    return "<a href='/customers/Details/" + customer.id + "'>" + customer.name + "</a>";
                }

            },
            // column 3
            {
                data: "id",
                render: function (data) {
                    return "<button class='btn btn-primary js-delete' data-customer-id=" + data + ">Delete</button>";
                }
            }
            
        ]
    });
});