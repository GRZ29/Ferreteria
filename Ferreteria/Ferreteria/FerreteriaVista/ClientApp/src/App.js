import React from "react";
import {BrowserRouter as Router,Route,Switch,} from "react-router-dom";
import Cookies from 'universal-cookie';

import { Layout } from "./components/Layout";
import { Home } from "./components/Home";
import Clientes from "./components/Clientes";
import ClientesMUI from "./components/ClientesMUI";
import LoginUser from "./components/LoginUser";
import PrivateRoute from './components/PrivateRoute';
import fakeAuth from './api/authentication';

import "./custom.css";


export default function App() {	

	const cookies = new Cookies();

	return (
		<Router>
			<div>
				<Layout>
					<Switch>
						<Route exact path="/home" component={Home} />
					</Switch>
					<Route exact path="/public">
						<Clientes />
					</Route>
					<Route exact path="/login">
						{
							cookies.get('id')>0
							?
								fakeAuth.mantener()
							:
							<div>
								<LoginUser/>
							</div>
						}
					</Route>
					<PrivateRoute exact path="/protected">
						<ClientesMUI />
					</PrivateRoute>
				</Layout>
			</div>
		</Router>
	);
}
