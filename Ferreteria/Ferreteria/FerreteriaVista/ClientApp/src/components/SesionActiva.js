import React from 'react';
import Cookies from 'universal-cookie';

function SesionActiva() {

	const cookies = new Cookies();
	
	return (
		<div>
			{
				cookies.get('id') > 0 
				? 
				cookies.get('usuario') 
				: 
				""
			}
		</div>
	);
}

export default SesionActiva;