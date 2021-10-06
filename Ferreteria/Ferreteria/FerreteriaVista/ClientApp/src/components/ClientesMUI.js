import React, { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap-grid.min.css';
import axios from 'axios';
import "react-loader-spinner/dist/loader/css/react-spinner-loader.css";
import ScaleLoader from "react-spinners/ScaleLoader";
import { TextField, Button, Modal } from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles'
import { css } from "@emotion/react";
import  "@material-ui/icons";
import MaterialTable from "material-table";


const baseURL = "https://localhost:44380/api/Clientes";

const columnas = [
	{ title: 'Nombre', field: 'nombreCliente' },
	{ title: 'Apelldios', field: 'apellidosCliente' },
	{ title: 'Telefono', field: 'telefonoCliente' },
	{ title: 'Correo', field: 'correoCliente' }
]

const useStyles = makeStyles((theme) => ({
	modal: {
		position: 'absolute',
		width: 600,
		backgroundColor: theme.palette.background.paper,
		border: '3px solid #000',
		borderRadius: '5px',
		boxShadow: theme.shadows[5],
		padding: theme.spacing(2, 4, 3),
		top: '50%',
		left: '50%',
		transform: 'translate(-50%, -50%)',
		borderColor: '#0c629d'
	},
	iconos: {
		cursor: 'pointer'
	},
	inputMaterial: {
		width: '100%'
	},
	botton: {
		color: '#0c629d'
	},
	colorPeligro: {
		color: 'ff0000'
	}
}));

const ClientesMUI = () => {
	const styles = useStyles();
	const [data, setData] = useState([]);
	const [loading, setLoading] = useState(true);
	const [modalInsertar, setModalInsertar] = useState(false);
	const [modalEditar, setModalEditar] = useState(false);
	const [modalEliminar, setModalEliminar] = useState(false);
	const [clienteSelect, setClienteSelect] = useState({
		idCliente: '',
		nombreCliente: '',
		apellidosCliente: '',
		telefonoCliente: '',
		correoCliente: ''
	})

	const abrirCerrarModalInsertar = () => {
		setModalInsertar(!modalInsertar);
	}

	const abrirCerrarModalEditar = () => {
		setModalEditar(!modalEditar);
	}

	const abrirCerrarModalEliminar = () => {
		setModalEliminar(!modalEliminar);
	}

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
		let url = `${baseURL}/${clienteSelect.idCliente}`
		axios.put(url, clienteSelect)
			.then(response => {
				var respuesta = response.data;
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

	const seleccionarCliente = (cliente, caso) => {
		setClienteSelect(cliente);
		(caso === "Editar") ? abrirCerrarModalEditar()
			:
			abrirCerrarModalEliminar()
	}

	const handleChange = e => {
		const { name, value } = e.target;
		setClienteSelect({
			...clienteSelect,
			[name]: value
		});
		console.log(clienteSelect);
	}

	useEffect(() => {
		peticionGet();
	}, [])

	const override = css`
        display: block;
        margin: 10% 44%;
    `;

	const bodyInsertar = (
		<div className={styles.modal}>
			<h3>Agregar nuevo cliente</h3>
			<TextField className={styles.inputMaterial} label="Nombre" name="nombreCliente" onChange={handleChange} />
			<br />
			<TextField className={styles.inputMaterial} label="Apellidos" name="apellidosCliente" onChange={handleChange} />
			<br />
			<TextField className={styles.inputMaterial} label="Telefono" name="telefonoCliente" onChange={handleChange} />
			<br />
			<TextField className={styles.inputMaterial} label="Correo" name="correoCliente" onChange={handleChange} />
			<br /><br />
			<div aling="right">
				<Button color="primary" onClick={() => peticionPost()} >Insertar</Button>
				<Button onClick={() => abrirCerrarModalInsertar()}>Cancelar</Button>
			</div>
		</div>
	)

	const bodyEditar = (
		<div className={styles.modal}>
			<h3>Editar cliente</h3>
			<TextField className={styles.inputMaterial} label="Nombre" name="nombreCliente" value={clienteSelect && clienteSelect.nombreCliente} onChange={handleChange} />
			<br />
			<TextField className={styles.inputMaterial} label="Apellidos" name="apellidosCliente" value={clienteSelect && clienteSelect.apellidosCliente} onChange={handleChange} />
			<br />
			<TextField className={styles.inputMaterial} label="Telefono" name="telefonoCliente" value={clienteSelect && clienteSelect.telefonoCliente} onChange={handleChange} />
			<br />
			<TextField className={styles.inputMaterial} label="Correo" name="correoCliente" value={clienteSelect && clienteSelect.correoCliente} onChange={handleChange} />
			<br /><br />
			<div aling="right">
				<Button color="primary" onClick={() => peticionPut()} >Editar</Button>
				<Button onClick={() => abrirCerrarModalEditar()}>Cancelar</Button>
			</div>
		</div>
	)

	const bodyEliminar = (
		<div className={styles.modal}>
			<p>Estás seguro que deseas eliminar al cliente <b>{clienteSelect && clienteSelect.nombreCliente}</b>? </p>
			<div align="right">
				<Button color="secondary" onClick={() => peticionDelete()}>Sí</Button>
				<Button onClick={() => abrirCerrarModalEliminar()}>No</Button>

			</div>

		</div>
	)

	return (
		<div>
			<br />
			<Button onClick={() => abrirCerrarModalInsertar()} className={styles.botton}>Insertar Cliente</Button>
			<br /><br />
			{
				loading ?
					<ScaleLoader color={"green"} loading={loading} size={30} css={override} />
					:
					<MaterialTable
						columns={columnas}
						data={data}
						title="Clientes"
						actions={[
							{
								icon: 'edit',
								tooltip: 'Editar Cliente',
								color: '#F37A24',
								onClick: (event, rowData) => seleccionarCliente(rowData, "Editar")
							},
							{
								icon: 'delete',
								tooltip: 'Eliminar Cliente',
								onClick: (event, rowData) => seleccionarCliente(rowData, "Eliminar")
							}
						]}
						options={{
							actionsColumnIndex: -1
						}}
						localization={{
							header: {
								actions: 'Acciones'
							}
						}}
					/>
			}
			<Modal
				open={modalInsertar}
				onClose={abrirCerrarModalInsertar}>
				{bodyInsertar}
			</Modal>

			<Modal
				open={modalEditar}
				onClose={abrirCerrarModalEditar}>
				{bodyEditar}
			</Modal>

			<Modal
				open={modalEliminar}
				onClose={abrirCerrarModalEliminar}>
				{bodyEliminar}
			</Modal>
		</div>
	);
};


export default ClientesMUI;