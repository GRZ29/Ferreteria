import React, { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import Cookies from 'universal-cookie';
import { Container } from '@material-ui/core';
import { useHistory } from 'react-router-dom';
import fakeAuth from '../api/authentication';

function LoginUser(props) {

	const baseUrl = "https://localhost:44380/api/UserLogins";
	const cookies = new Cookies();

	const history = useHistory();
	const [form, setForm] = useState({
		username: '',
		password: ''
	});

	const handleChange = e => {
		const { name, value } = e.target;
		setForm({
			...form,
			[name]: value
		});
	}

	const iniciarSesion = async () => {
		await fakeAuth.authenticate(form.username, form.password);
		history.push('/home');
	}

	useEffect(() => {
		if (cookies.get('id')) {
			//history.goBack('/home');
			//alert("ya estas loqueado");
			//window.location.href = "https://localhost:44380/home"
			//history.push('/home');
		}
	}, []);

	return (
		<Container ms="4">
			<div className="containerPrincipal">
				<div className="containerLogin">
					<div className="form-group">
						<label>Usuario: </label>
						<br />
						<input
							type="text"
							className="form-control"
							name="username"
							onChange={handleChange}
						/>
						<br />
						<label>Contraseña: </label>
						<br />
						<input
							type="password"
							className="form-control"
							name="password"
							onChange={handleChange}
						/>
						<br />
						<button className="btn btn-primary" onClick={() => iniciarSesion()}>Iniciar Sesión</button>
					</div>
				</div>
			</div>
		</Container>
	);
}

export default LoginUser;