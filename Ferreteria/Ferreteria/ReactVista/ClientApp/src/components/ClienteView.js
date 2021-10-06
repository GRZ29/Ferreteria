import React, { Component } from 'react';
import axios from 'axios';

import 'bootstrap/dist/css/bootstrap.min.css';
import 'jquery/dist/jquery.min.js';
//Datatable Modules

import "datatables.net/js/jquery.dataTables"

import "datatables.net-dt/js/dataTables.dataTables"
import "datatables.net-dt/css/jquery.dataTables.min.css"
import $ from 'jquery';

export class ClienteView extends Component {


    

    loadCliente() {
        console.log('loadCliente')
        $('#tblClientes').DataTable({
            "ajax": {

                "url": "/api/Clientes"
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


    componentDidMount() {
        console.log('componentDidMount')
        this.loadCliente();
    }
    render() {
        console.log('render')
        return (
            <div>
                <h1 class="text-center"><i class="fas fa-users"></i>Listado de Clientes</h1>
    
                <table class="table" id="tblClientes">

                    <thead class="thead-light">
                        <tr>
                            <th>Nombre</th>
                            <th>Apellidos</th>
                            <th>Teléfono</th>
                            <th>Correo electrónico</th>
                            <th>Acciones</th>
                        </tr>
                    </thead>

                </table>
            </div>
        );
    }
}
