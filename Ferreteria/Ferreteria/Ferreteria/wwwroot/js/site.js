var dataTable;
var dataTable2;
var dataTable3;
var dataTable4;
var dataTableMateriales;
var dataTableTrabaja;
var dataTableCalculos;
var dataTableClientes;

$(document).ready(function () {
    loadMateriales();
    loadDataTableTrabajo();
    loadCalculos();
    loadCliente();
});


function loadCliente() {
    dataTableClientes = $('#tblClientes').DataTable({
        "ajax": {

            "url": "/Admin/Cliente/Listar"
        },
        "columns": [
            { "data": "nombreCliente", "width": "15%" },
            { "data": "apellidosCliente", "width": "15%" },
            { "data": "telefonoCliente", "width": "15%" },
            { "data": "correoCliente", "width": "15%" },
            {
                "data": "idCliente",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a onclick="ShowPopup('/Admin/Cliente/Upsert/?id=${data}','Actualizar Cliente')" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="fas fa-edit"></i> 
                            </a>
                            <a onclick=Borrar("/Admin/Cliente/Borrar/?id=${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                <i class="fas fa-trash-alt"></i> 
                            </a>
                        </div>
                        `;
                }, "width": "40%"
            }
        ]
    }
    );
}

function loadCalculos() {
    dataTableCalculos = $('#tblCalculos').DataTable({
        "ajax": {

            "url": "/Admin/Precio/Listar"
        },
        "columns": [
            { "data": "trabajo.nombreTrabajo", "width": "15%" },
            { "data": "costo", "width": "15%" },
            {
                "data": "idPrecio",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a onclick="ShowPopup('/Admin/Precio/Upsert2/?id=${data}','Actualizar Precio')" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="fas fa-edit"></i> 
                            </a>
                            <a onclick=Borrar("/Admin/Precio/Borrar/?id=${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                <i class="fas fa-trash-alt"></i> 
                            </a>
                        </div>
                        `;
                }, "width": "40%"
            }
        ]
    }
    );
}


function loadMateriales() {
    dataTableMateriales = $('#tblMaterial').DataTable({
        "ajax": {
            "url": "/Admin/Material/Listar"
        },
        "columns": [
            { "data": "nombreMaterial", "width": "15%" },
            { "data": "costoMaterial", "width": "15%" },
            {
                "data": "idMaterial",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a onclick="ShowPopup('/Admin/Material/Upsert/?id=${data}','Actualizar Material')" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="fas fa-edit"></i> 
                            </a>
                            <a onclick=Borrar("/Admin/Material/Borrar/?id=${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                <i class="fas fa-trash-alt"></i> 
                            </a>
                        </div>
                        `;
                }, "width": "40%"
            }
        ]
    }
    );
}

function loadDataTableTrabajo() {
    dataTableTrabaja = $('#tblTrabajos').DataTable({
        "ajax": {
            "url": "/Admin/Trabajo/Listar"
        },
        "columns": [
            { "data": "cliente.nombreCliente", "width": "15%" },
            { "data": "nombreTrabajo", "width": "15%" },
            {
                "data": "idTrabajo",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a onclick="ShowPopup('/Admin/Trabajo/Upsert/?id=${data}','Actualizar Trabajo')" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="fas fa-edit"></i> 
                            </a>
                            <a onclick=Borrar("/Admin/Trabajo/Borrar/?id=${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                <i class="fas fa-trash-alt"></i> 
                            </a>
                        </div>
                        `;
                }, "width": "40%"
            }
        ]
    }
    );
}



function Borrar(url) {
    swal({
        title: "¿Está seguro que quiere borrar los datos?",
        text: "¡Una vez borrados no pueden ser recuperados!",
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
                        dataTableMateriales.ajax.reload();
                        dataTableTrabaja.ajax.reload();
                        dataTableCalculos.ajax.reload();
                        dataTableClientes.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}

$(function () {
    $("#loaderbody").addClass('hide');

    $(document).bind('ajaxStart', function () {
        $("#loaderbody").removeClass('hide');
    }).bind('ajaxStop', function () {
        $("#loaderbody").addClass('hide');
    });
});


ShowPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
        }
    })
}

PostForm = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTableMateriales.ajax.reload();
                    dataTableTrabaja.ajax.reload();
                    dataTableCalculos.ajax.reload();
                    dataTableClientes.ajax.reload();
                }
                else
                    toastr.error(data.message);

                $('#form-modal .modal-body').html('');
                $('#form-modal .modal-title').html('');
                $('#form-modal').modal('hide');
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}


