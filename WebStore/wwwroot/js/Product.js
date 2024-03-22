$(document).ready(function () {
    $("#tblData").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        //"searching": true,
        //order: [[1, "asc"]],
        "ajax": {
            "url": "/Product/getall",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }
        ],
        "columns": [
            { "data": "productId", "name": "productId", "autoWidth": true },
            { "data": "productName", "name": "productName", "autoWidth": true },
            { "data": "author", "name": "Author", "autoWidth": true },
            { "data": "isbn", "name": "ISBN", "autoWidth": true },
            { "data": "price", "name": "Price", "autoWidth": true },
            { "data": "addedDate", "name": "Added Date", "autoWidth": true },
            {
                //"render": function (data, row) { return "<a href='/product/DeletePost?productId='" + row.productId + "' class='btn btn-danger'>Delete</a>"; }
                data: "productId",
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/Product/Edit?productId=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i>Edit</a>
                    <a href="/Product/DeleteById?productId=${data}" class="btn btn-danger mx-2" onclick=return DeleteData('" + data.ProductID + "');><i class="bi bi-trash-fill"></i>Delete</a>
                    </div > `
                },
                "width":"25%"
            },
        ]
    });
});

function DeleteData(ProductID) {
    if (confirm("Are you sure you want to delete ...?")) {
        Delete(CustomerID);
    } else {
        return false;
    }
} 
/*
$(document).ready(function () {
   loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/Product/getall' },
        "columns": [
            { data: 'productName', "width": "15%" },
            { data: 'author', "width": "15%" },
            { data: 'isbn', "width": "15%" },
            { data: 'price', "width": "15%" },
        ]
    });
}
 */