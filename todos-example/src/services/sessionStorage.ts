export const loadValue = (key: string) => {
	try {
		const serializedState = sessionStorage.getItem(key);

		if (serializedState === null) {
			return undefined;
		}

		return JSON.parse(serializedState);
	} catch (error) {
		return undefined;
	}
};

export const setValue = (key: string, value: string) => {
	try {
		const serializedState = JSON.stringify(value);
		sessionStorage.setItem(key, serializedState);
	} catch (error) {
		// Ignore write errors.
	}
};