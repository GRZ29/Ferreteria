import Cookies from "universal-cookie";
import axios from "axios";
import md5 from "md5";

const cookies = new Cookies();
const baseUrl = "https://localhost:44380/api";

const fakeAuth = {
  isAuthenticated: false,
  async authenticate(username, password) {
	  try {
		let response = await axios.get(`${baseUrl}/UserLogins/${username}/${md5(password)}`);
		let data = await response.data;
		
			if (data.length > 0) {
				var respuesta = data[0]; //mirar otra vez para la funcion
				cookies.set("id", respuesta.id, { path: "/" });
				cookies.set("usuario", respuesta.usuario, { path: "/" });
				cookies.set("contra", respuesta.contra, { path: "/" });
				this.isAuthenticated = true;
			} else {
				
				alert("El usuario o la contraseña no son correctos");
			}
		} catch (error) {
			console.log("Error =>", error);
		}
	},
	signout() {
		cookies.remove("id", { path: "/" });
		cookies.remove("usuario", { path: "/" });
		cookies.remove("contra", { path: "/" });
		this.isAuthenticated = false;
	},
	mantener(){
		this.isAuthenticated = true;
	}
};

export default fakeAuth;
