$(document).ready(function () { 
    var table = $("#movies").DataTable({
        ajax: {
            url: "/api/Movies",
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
                render: function (data, type, movie) {
                    return "<a href='/movies/Details/" + movie.id + "'>" + movie.name + "</a>";
                }

            },
            // column 3
            {
                data: "genre.name"
            },
            // column 4
            {
                data: "id",
                render: function (data) {
                    return "<button class='btn btn-primary js-delete' data-movie-id=" + data + ">Delete</button>";
                }
            }
            
        ]
    });


    // select customers table and find all the elements with this class "js-delete"
    $("#movies").on("click", ".js-delete", function () {
        var button = $(this);
        // if our confirmation return true then we want to call our API using jQuery Ajax
        if (confirm("Are you sure you want to delete this customer ? ")) {
            $.ajax({
                url: "/api/movies/" + button.attr("data-movie-id"),
                method: "DELETE",
                success: function () {
                    /* 
                     * Bug : after deleting customer from table if you serach for that customer then it will appera in result but the customer is deleted from database. for that you have to refresh the page and then it will not shown. so this is the solution.

                     * priviously we are deleteing our customer from DOM see code -> button.parent("tr").remove();  
                     * in other words we are removing "tr" element from our table.
                     * But dataTable keeps list of customers internally.
                     * solution code will delete the corresponding customer from the internal list maintained by the dataTable
                     * All this method you see here like row(), remove(), draw() ar part of DataTable API.
                    */
                    table.row(button.parents("tr")).remove().draw();         
                }
            });
        }
    });


});