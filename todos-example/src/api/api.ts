import axios from "axios";

const instance = axios.create({
	baseURL: 'http://localhost:5048/'
});

export default instance;