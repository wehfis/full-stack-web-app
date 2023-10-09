import axios from 'axios';
import React, { useEffect } from 'react';
import { Button, Form, Input } from 'antd';
import HeavyTask from './components/HeavyTask';
import History from './components/HistoryPage';

const { TextArea } = Input;

const App = () => {

  useEffect(() => {
    axios.get('https://localhost:7173/api/HeavyTask')
    .then((response) => {
      console.log(response.data);
    });
  })


  return (
    <>
      <HeavyTask/>
      <History/>
    </>
  );
}

export default App;
