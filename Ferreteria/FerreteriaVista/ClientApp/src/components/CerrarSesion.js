import React, {useEffect} from 'react';
import Cookies from 'universal-cookie';
import { useHistory } from 'react-router-dom';
import fakeAuth from '../api/authentication';

function CerrarSesion() {

    const cookies = new Cookies();
    const history = useHistory();
    
    const cerrarSesion=()=>{
        fakeAuth.signout();
        history.push('/home');
    }

    // const rolusuario = (nombre) => {
	// 	if(nombre =="grosales"){
    //         return <div>{cookies.get('usuario')}
    //                 <button className="btn btn-danger" onClick={()=>cerrarSesion()}>holas</button>
    //                 </div>
    //     }
	// }


    useEffect(()=>{
        
    },[]);

    return (
        <div>
            {
                cookies.get('id')>0
                ?
                <div>{/*es una forma de determinar si solo se quiere que una persona vea algo*/}
                    {/* {rolusuario(cookies.get('usuario'))} */}
                    {cookies.get('usuario')}
                    <button className="btn btn-danger" onClick={()=>cerrarSesion()}>Cerrar Sesión</button>
                </div>
                :
                <div></div>
            }
        </div>
    );
}

export default CerrarSesion;