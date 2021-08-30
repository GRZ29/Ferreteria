import React from "react";
import {
	Route,
	Redirect,
} from "react-router-dom";
import fakeAuth from "../api/authentication";

function PrivateRoute({ children, ...rest }) {
	return (
		<Route {...rest} render={({ location }) => {
			return fakeAuth.isAuthenticated === true
				? children
				: <div>
                    <Redirect to={{
					pathname: "/login",
					state: { from: location },
				}}
				/>
                </div>
		}}
		/>
	);
}

export default PrivateRoute;