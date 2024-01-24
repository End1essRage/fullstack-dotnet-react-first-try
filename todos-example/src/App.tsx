
import './App.css';
import React, { useEffect } from 'react';
import { LoginPage } from './modules/loginPage/loginPage';
import { TodosPage } from './modules/todosPage/todosPage';
import { BrowserRouter, Link, Navigate, Route, Routes } from 'react-router-dom';
import { UsersPage } from './modules/usersPage/usersPage';
import { RegisterPage } from './modules/registerPage/registerPage';
import { HomePage } from './modules/homePage/homePage';
import { useDispatch, useSelector } from 'react-redux';
import { AppDispatch, RootState } from './redux/store';
import { refreshToken, setUnAuthenticated } from './redux/authSlice';

function App() {

  const dispatch = useDispatch<AppDispatch>();
  //const [user, setUser] = React.useState('');
  const handleRefreshToken = () => dispatch(refreshToken());

  const handleLogout = () => dispatch(setUnAuthenticated());

  const isAuthed = useSelector((state: RootState) => state.auth.authenticated);

  return (
    <div className="App">
      <header className="App-header">
        <div className='DEV'>
          <div className='DEV-wrapper'>
            <div className='DEV-route-link'>
              <Link to='/todos'>TODOS</Link>
            </div>
            <div className='DEV-route-link'>
              <Link to='/login'>LOGIN</Link>
            </div>
            <div className='DEV-route-link'>
              <Link to='/register'>REGISTER</Link>
            </div>
            <div className='DEV-route-link'>
              <button onClick={handleRefreshToken}> REFRESH </button>
            </div>
            <div className='DEV-route-link'>
              <button onClick={handleLogout}> LOGOUT </button>
            </div>
          </div>
        </div>
      </header>
      <Routes>
        <Route path='/'
          Component={() => isAuthed ? <HomePage /> : <Navigate to="/login" replace={true} />} />
        <Route path='/login'
          Component={() => <LoginPage />} />
        <Route path='/register'
          Component={() => <RegisterPage />} />
        <Route path='/todos'
          Component={() => isAuthed ? <TodosPage /> : <Navigate to="/login" replace={true} />} />
        <Route path='/users'
          Component={() => isAuthed ? <UsersPage /> : <Navigate to="/login" replace={true} />} />
      </Routes>
    </div>
  );
}

export default App;

