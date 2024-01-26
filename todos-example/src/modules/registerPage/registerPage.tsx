import { ChangeEvent, useState } from "react";
import styles from "./registerPage.module.css";
import { AppDispatch } from "../../redux/store";
import { useDispatch } from "react-redux";
import { register } from "../../redux/authSlice";
import { Link, useNavigate } from "react-router-dom";

export const RegisterPage = () => {

	const [email, setEmail] = useState('');
	const [userName, setUserName] = useState('');
	const [password, setPassword] = useState('');
	const [confirmPassword, setConfirmPassword] = useState('');

	const navigate = useNavigate();
	const dispatch = useDispatch<AppDispatch>();

	const updateEmail = (event: ChangeEvent<HTMLInputElement>) => {
		setEmail(event.target.value);
	}
	const updateUserName = (event: ChangeEvent<HTMLInputElement>) => {
		setUserName(event.target.value);
	}
	const updatePassword = (event: ChangeEvent<HTMLInputElement>) => {
		setPassword(event.target.value);
	}
	const updateConfirmPassword = (event: ChangeEvent<HTMLInputElement>) => {
		setConfirmPassword(event.target.value);
	}
	const onRegister = () => {
		if (password === confirmPassword) {
			const info = { email: email, userName: userName, password: password };
			dispatch(register(info)).then(c => {
				if (c.meta.requestStatus === 'fulfilled')
					navigate('/todos');
				if (c.meta.requestStatus === 'rejected')
					alert('неудачная попытка входа');
			});
		}
		else {
			alert('пароли не совпадают');
		}
	}

	return (
		<div className={styles.wrapper}>
			<div className={styles.form}>
				<h1>Register</h1>
				<div className={styles.inputBox}>
					<input type="text" placeholder="Email" onChange={updateEmail} value={email} required />
					{/*<div className={styles.icon}><FaRegUserCircle /></div>*/}
				</div>
				<div className={styles.inputBox}>
					<input type="text" placeholder="Username" onChange={updateUserName} value={userName} required />
					{/*<div className={styles.icon}><FaRegUserCircle /></div>*/}
				</div>
				<div className={styles.inputBox}>
					<input type="password" placeholder="Password" onChange={updatePassword} value={password} required />
					{/*<div className={styles.icon}><RiLockPasswordFill /></div>*/}
				</div>
				<div className={styles.inputBox}>
					<input type="password" placeholder="Confirm password" onChange={updateConfirmPassword} value={confirmPassword} required />
					{/*<div className={styles.icon}><RiLockPasswordFill /></div>*/}
				</div>

				<button onClick={onRegister}>Register</button>

				<div className={styles.loginLink}>
					<p>Already have an account?  <Link to='/login'>Login</Link></p>
				</div>
			</div>
		</div>
	);
}