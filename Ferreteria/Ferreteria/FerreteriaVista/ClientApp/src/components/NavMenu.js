import React, { useState } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import SesionActiva from './SesionActiva';
import CerrarSesion from './CerrarSesion';
import LoginUser from './LoginUser';


const NavMenu = (props) => {

	const [collapsed, setCollapsed] = useState(true);

	const toggleNavbar = () => {
		setCollapsed(!collapsed)
	}

	return (
		<header className="ThemeBlack">
			<Navbar className="navbar-expand-sm navbar-toggleable-sm ng-black border-bottom box-shadow mb-3" light>
				<Container>
					<NavbarBrand tag={Link} className="text-white" to="/">FerreteriaVista</NavbarBrand>
					<NavbarToggler onClick={toggleNavbar} className="mr-2" />
					<Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!collapsed} navbar>
						<ul className="navbar-nav flex-grow">
							<NavItem>
								<NavLink tag={Link} className="" to="/home" >Home</NavLink>
							</NavItem>
							{/*<NavItem>
									<NavLink tag={Link} className="text-dark" to="/counter">Counter</NavLink>
								</NavItem>
								<NavItem>
									<NavLink tag={Link} className="text-dark" to="/fetch-data">Fetch data</NavLink>
								</NavItem> */}
							<NavItem>
								<NavLink tag={Link} className="text-white" to="/public">Clientes</NavLink>
							</NavItem>
							<NavItem>
								<NavLink tag={Link} className="text-white" to="/protected">Clientes MUI</NavLink>
							</NavItem>
							<NavItem>
								<NavLink tag={Link} className="text-white" to="/protected">Login</NavLink>
							</NavItem>
							<NavItem>
								<CerrarSesion />
							</NavItem>
						</ul>
					</Collapse>
				</Container>
			</Navbar>
		</header>
	)

}

export default NavMenu;