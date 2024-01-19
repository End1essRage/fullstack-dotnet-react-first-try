const inMemoryJWTService = () => {
	let inMemoryJWT: string | undefined = undefined;

	const getToken = () => inMemoryJWT;

	const setToken = (token: string, tokenExpiration?: Date) => {
		inMemoryJWT = token;
	}

	return { getToken, setToken };
}

export default inMemoryJWTService();