import { ChangeEvent, useState } from "react";
import styles from "./loginPage.module.css";
import { FaRegUserCircle } from "react-icons/fa";
import { RiLockPasswordFill } from "react-icons/ri";
import { login } from "../../redux/authSlice";
import { useDispatch } from "react-redux";
import { AppDispatch } from "../../redux/store";

export const LoginPage = () => {
	const [userName, setUserName] = useState('');
	const [password, setPassword] = useState('');

	const dispatch = useDispatch<AppDispatch>();

	const updateUserName = (event: ChangeEvent<HTMLInputElement>) => {
		setUserName(event.target.value);
	}

	const updatePassword = (event: ChangeEvent<HTMLInputElement>) => {
		setPassword(event.target.value);
	}

	const onLogin = () => {
		const user = { userName: userName, password: password };
		dispatch(login(user));
	}

	return (
		<div className={styles.wrapper}>
			<div className={styles.form}>
				<h1>Login</h1>
				<div className={styles.inputBox}>
					<input type="text" placeholder="Username" onChange={updateUserName} value={userName} required />
					<div className={styles.icon}><FaRegUserCircle /></div>
				</div>
				<div className={styles.inputBox}>
					<input type="password" placeholder="Password" onChange={updatePassword} value={password} required />
					<div className={styles.icon}><RiLockPasswordFill /></div>
				</div>
				<div className={styles.forgotAndRemember}>
					<label><input type="checkbox" />Remember me</label>
					<a href="#">Forgot password?</a>
				</div>

				<button onClick={onLogin}>Login</button>

				<div className={styles.registerLink}>
					<p>Don't have an account? <a href="#">Register</a></p>
				</div>
			</div>
		</div>
	);
}
