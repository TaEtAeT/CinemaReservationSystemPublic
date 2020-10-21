var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Customer/UserBookings/GetAll"
        },
        "columns": [
            { "data": "reservation.screening.movie.title", "width": "10%" },
            { "data": "reservation.screening.auditorium.name", "width": "10%" },
            { "data": "reservation.screening.screeningStart", "width": "10%" },
            { "data": "reservation.screening.date", "width": "10%" },
            { "data": "reservation.amountToPay", "width": "10%" },
            { "data": "seats", "width": "10%" },
            { "data": "reservation.isPaid", "width": "10%" },
            { "data": "reservation.isCanceled", "width": "10%" },
            {
                "data": "reservation.id",
                "render": function (data) {
                    return `
                            <div class="text-center">
     <a onclick=BuyTickets("/Customer/UserBookings/Buy/${data}") class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="far fa-credit-card"></i>
                                </a>
                            </div>
                           `;
                }, "width": "10%"
            },
            {
                "data": "reservation.id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a onclick=Delete("/Customer/UserBookings/Cancel/${data}") class="btn btn-primary text-white" style="cursor:pointer">
                                    <i class="fas fa-window-close"></i>
                                </a>
                           
                            </div>
                           `;
                }, "width": "10%"
            }
        ]
    });
}

function BuyTickets(url) {
   // alert('Tickets bought');
    $.ajax({
        type: "GET",
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


function Delete(url) {
    swal({
        title: "Are you sure you want to cancel the reservation?",
        text: "You cannot undo this action.",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "GET",
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
}