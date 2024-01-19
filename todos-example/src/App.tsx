
import './App.css';
import React from 'react';
import { LoginPage } from './modules/loginPage/loginPage';
import { TodosPage } from './modules/todosPage/todosPage';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { UsersPage } from './modules/usersPage/usersPage';
import { RegisterPage } from './modules/registerPage/registerPage';
import { HomePage } from './modules/homePage/homePage';
import { useSelector } from 'react-redux';
import { RootState } from './redux/store';

function App() {

  //const [user, setUser] = React.useState('');

  var user = useSelector((state: RootState) => state.auth.user);

  return (
    <BrowserRouter>
      <div className="App">
        <header className="App-header">
        </header>
        <Routes>
          <Route path='/'
            Component={() => user === undefined ? <LoginPage /> : <HomePage />} />
          <Route path='/login'
            Component={() => <LoginPage />} />
          <Route path='/register'
            Component={() => <RegisterPage />} />
          <Route path='/todos'
            Component={() => user === undefined ? <TodosPage /> : <TodosPage />} />
          <Route path='/users'
            Component={() => user === undefined ? <LoginPage /> : <UsersPage />} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;

