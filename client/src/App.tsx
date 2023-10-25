import axios from 'axios';
import React, { useContext, useEffect, useState } from 'react';
import { Button, Form, Input } from 'antd';
import Login from './pages/LoginPage';
import Register from './pages/RegisterPage';
import Home from './pages/HomePage';
import Admin from './pages/AdminPage';
import { BrowserRouter, Routes, Route, Navigate, useNavigate } from 'react-router-dom';
import { Context } from './index';

const { TextArea } = Input;

const App = () => {
  const [isHistoryButtonClicked, setIsHistoryButtonClicked] = useState(false);
  const { tokenStore } = useContext(Context);

  useEffect(() => {
    // axios.get('https://localhost:7173/api/HeavyTask')
    // .then((response) => {
    //   console.log(response.data);
    // });
  })

  const updatePage = (value: boolean) => {
    setIsHistoryButtonClicked(value);
  };

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" Component={Register}/>
        <Route path="/home" Component={Home}/>
        <Route path="/login" Component={Login}/>
        <Route path="/admin" Component={Admin}/>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
