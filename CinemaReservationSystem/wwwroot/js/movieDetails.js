var dataTable;

function GetScreenings(id) {
    $.ajax({
        type: "GET",
        url: '/Admin/Screening/GetScreeningsForMovie',
        data: {
            movieId: id
        },
        contentType: "application/json",
        success: function (data) {
            console.log(data);
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            }
            else {
                toastr.error(data.message);
            }
        }
    });
}


function loadDataTable(id) {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Screening/GetScreeningsForMovie",
            "data": {
                "movieId": id
            }
        },
     

        "columns": [
            { "data": "screeningStart", "width": "20%" },
            { "data": "date", "width": "20%" },
            { "data": "auditorium.name", "width": "20%" },
            { "data": "ticketPrice", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Customer/Booking/Index/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                     <i class="fas fa-ticket-alt"></i> Book </a>
                                `;
                }, "width": "20%"
            }
        ]
    });
}

/*function Delete(url) {
    swal({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}*/