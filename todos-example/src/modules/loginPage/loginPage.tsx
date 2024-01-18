import styles from "./loginPage.module.css";
import { FaRegUserCircle } from "react-icons/fa";
import { RiLockPasswordFill } from "react-icons/ri";

export const LoginPage = () => {
	return (
		<div className={styles.wrapper}>
			<div className={styles.form}>
				<form action=''>
					<h1>Login</h1>
					<div className={styles.inputBox}>
						<input type="text" placeholder="Username" required />
						<div className={styles.icon}><FaRegUserCircle /></div>
					</div>
					<div className={styles.inputBox}>
						<input type="password" placeholder="Password" required />
						<div className={styles.icon}><RiLockPasswordFill /></div>
					</div>
					<div className={styles.forgotAndRemember}>
						<label><input type="checkbox" />Remember me</label>
						<a href="#">Forgot password?</a>
					</div>

					<button type="submit">Login</button>

					<div className={styles.registerLink}>
						<p>Don't have an account? <a href="#">Register</a></p>
					</div>
				</form>
			</div>
		</div>
	);
}