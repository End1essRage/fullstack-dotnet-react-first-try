
import './App.css';
import React from 'react';
import { LoginPage } from './modules/loginPage/loginPage';
import { HomePage } from './modules/homePage/homePage';

function App() {

  const [user, setUser] = React.useState('');

  return (
    <div className="App">
      <header className="App-header">
      </header>
      {user === '' ? <LoginPage /> : <HomePage />}
    </div>
  );
}

export default App;

