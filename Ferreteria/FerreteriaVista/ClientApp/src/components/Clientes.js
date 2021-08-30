import React, { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap-grid.min.css';
import axios from 'axios';
import { Modal, ModalBody, ModalFooter, ModalHeader } from 'reactstrap';
import "react-loader-spinner/dist/loader/css/react-spinner-loader.css";
import ScaleLoader from "react-spinners/ScaleLoader";
import { Container } from '@material-ui/core';
import style from './Style';
import { css } from "@emotion/react";


const Clientes = () => {
	const baseURL = "https://localhost:44380/api/Clientes";
	const [data, setData] = useState([]);
	const [modalInsertar, setModalInsertar] = useState(false);
	const [modalEditar, setModalEditar] = useState(false);
	const [modalEliminar, setModalEliminar] = useState(false);
	const [loading, setLoading] = useState(true);
	const [clienteSelect, setClienteSelect] = useState({
		idCliente: '',
		nombreCliente: '',
		apellidosCliente: '',
		telefonoCliente: '',
		correoCliente: ''
	})

	const peticionGet = async () => {
		try {
			const response = await axios.get(baseURL)
			setData(response.data.data);
			setLoading(false)
		} catch (error) {
			console.log(error);
		}
	}

	const peticionPost = async () => {
		try {
			delete clienteSelect.idCliente;
			const response = await axios.post(baseURL, clienteSelect)
			console.log(response.data);
			setData(data.concat(response.data));
			abrirCerrarModalInsertar();
		} catch (error) {
			console.log(error);
		}
	}

	const peticionPut = () => {
		//string interpolations en js
		let url = `${baseURL}/${clienteSelect.idCliente}`
		axios.put(url, clienteSelect)
			.then(response => {
				var respuesta = response.data;
				console.log(response);
				var dataAux = data;
				dataAux.map(clientes => {
					if (clientes.idCliente === clienteSelect.idCliente) {
						clientes.nombreCliente = respuesta.nombreCliente;
						clientes.apellidosCliente = respuesta.apellidosCliente;
						clientes.telefonoCliente = respuesta.telefonoCliente;
						clientes.correoCliente = respuesta.correoCliente;
					}
				});
				abrirCerrarModalEditar();
			}).catch(error => {
				console.log(error);
			})
	}

	const peticionDelete = () => {
		axios.delete(baseURL + "/" + clienteSelect.idCliente)
			.then(response => {
				setData(data.filter(cliente => cliente.idCliente !== response.data));
				abrirCerrarModalEliminar();
			}).catch(error => {
				console.log(error);
			})
	}
	// const peticionPost = () =>{
	//     try {
	//         delete clienteSelect.idCliente;
	//         axios.post(baseURL,clienteSelect)
	//         .then(response=>{
	//             setData(data.concat(response.data));
	//             abrirCerrarModalInsertar();

	//         }).catch(error=>{
	//             console.log(error);
	//     })
	//     }catch (error) {
	//         console.log(error);
	//     }

	// }

	// const peticionGetPromise = () =>{
	//     axios.get(baseURL)
	//     .then(response=>{
	//         setData(response.data.data);
	//     }).catch(error=>{
	//         console.log(error);
	//     })
	// }


	const handleChange = e => {
		const { name, value } = e.target;
		setClienteSelect({
			...clienteSelect,
			[name]: value
		});
		console.log(clienteSelect);
	}

	const abrirCerrarModalInsertar = () => {
		setModalInsertar(!modalInsertar);
	}

	const abrirCerrarModalEliminar = () => {
		setModalEliminar(!modalEliminar);
	}

	const abrirCerrarModalEditar = () => {
		setModalEditar(!modalEditar);
	}

	const seleccionarCliente = (cliente, caso) => {
		setClienteSelect(cliente);
		(caso == "Editar") ?
			abrirCerrarModalEditar() : abrirCerrarModalEliminar();
	}

	useEffect(() => {
		peticionGet();
		//setLoading(true)
		// setTimeout(()=>{
		//     setLoading(false)
		// },1000)
	}, [])

	const override = css`
        display: block;
        margin: 10% 44%;
    `;

	return (
		<Container component="main" maxWidth="md" justify="center">

			<div style={{ textAlign: "justify" }}>

				<button type="button" className="btn btn-success btn-lg pull-left" style={style.centrar} onClick={() => abrirCerrarModalInsertar()}>Insertar Cliente</button>
				<br />
				<br />
				{
					loading ?
						<ScaleLoader color={"#F37A24"} loading={loading} size={30} css={override} />
						:
						<table className="table table-bordered">
							<thead>
								<tr>
									<th hidden>ID</th>
									<th>Nombre</th>
									<th>Apellidos</th>
									<th>Telefono</th>
									<th>Correo</th>
									<th>Acciones</th>
								</tr>
							</thead>
							<tbody>
								{
									data.map(cliente => (
										<tr key={cliente.idCliente} >
											<td hidden>{cliente.idCliente}</td>
											<td>{cliente.nombreCliente}</td>
											<td>{cliente.apellidosCliente}</td>
											<td>{cliente.telefonoCliente}</td>
											<td>{cliente.correoCliente}</td>
											<td style={{ textAlign: "center" }}>
												<button className="btn btn-primary" onClick={() => seleccionarCliente(cliente, "Editar")} >Editar</button>{" "}
												<button className="btn btn-danger" onClick={() => seleccionarCliente(cliente, "Eliminar")}>Eliminar</button>
											</td>
										</tr>
									))
								}
							</tbody>
						</table>

				}

				{/* <ClientesMUI/> */}

				<Modal isOpen={modalInsertar}>
					<ModalHeader>Insertar Clientes</ModalHeader>
					<ModalBody>
						<div className="form-group">
							<label>Nombre: </label>
							<br />
							<input type="text" className="form-control" name="nombreCliente" onChange={handleChange} />
							<br />
							<label>Apellidos: </label>
							<br />
							<input type="text" className="form-control" name="apellidosCliente" onChange={handleChange} />
							<br />
							<label>Telefono: </label>
							<br />
							<input type="text" className="form-control" name="telefonoCliente" onChange={handleChange} />
							<br />
							<label>Correo: </label>
							<br />
							<input type="text" className="form-control" name="correoCliente" onChange={handleChange} />
						</div>
					</ModalBody>
					<ModalFooter>
						<button className="btn btn-primary" onClick={() => peticionPost()} >Insertar</button>{"  "}
						<button className="btn btn-primary" onClick={() => abrirCerrarModalInsertar()}>Cancelar</button>
					</ModalFooter>
				</Modal>

				<Modal isOpen={modalEditar}>
					<ModalHeader>Editar Clientes</ModalHeader>
					<ModalBody>
						<div className="form-group">
							<label>ID: </label>
							<br />
							<input type="text" className="form-control" value={clienteSelect && clienteSelect.idCliente} onChange={handleChange} readOnly />
							<br />
							<label>Nombre: </label>
							<br />
							<input type="text" className="form-control" name="nombreCliente" value={clienteSelect && clienteSelect.nombreCliente} onChange={handleChange} />
							<br />
							<label>Apellidos: </label>
							<br />
							<input type="text" className="form-control" name="apellidosCliente" value={clienteSelect && clienteSelect.apellidosCliente} onChange={handleChange} />
							<br />
							<label>Telefono: </label>
							<br />
							<input type="text" className="form-control" name="telefonoCliente" value={clienteSelect && clienteSelect.telefonoCliente} onChange={handleChange} />
							<br />
							<label>Correo: </label>
							<br />
							<input type="text" className="form-control" name="correoCliente" value={clienteSelect && clienteSelect.correoCliente} onChange={handleChange} />
						</div>
					</ModalBody>
					<ModalFooter>
						<button className="btn btn-primary" onClick={() => peticionPut()} >Editar</button>{"  "}
						<button className="btn btn-primary" onClick={() => abrirCerrarModalEditar()}>Cancelar</button>
					</ModalFooter>
				</Modal>

				<Modal isOpen={modalEliminar} >
					<ModalBody>
						Estas seguro que deseas eliminar el Cliente {clienteSelect && clienteSelect.nombreCliente}?
					</ModalBody>
					<ModalFooter>
						<button className="btn btn-danger" onClick={() => peticionDelete()}>Si</button>
						<button className="btn btn-secondary" onClick={() => abrirCerrarModalEliminar()} >No</button>
					</ModalFooter>
				</Modal>

			</div>
		</Container>
	);
};


export default Clientes;