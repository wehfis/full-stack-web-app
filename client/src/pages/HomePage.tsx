import axios from 'axios';
import React, { useEffect, useState } from 'react';
import { Button, Form, Input } from 'antd';
import HeavyTask from '../components/HeavyTask';
import History from '../components/HistoryPage';


const Home = () => {
  const [isHistoryButtonClicked, setIsHistoryButtonClicked] = useState(false);

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
    <div className='container'>
      <History isBtnClicked = {updatePage}/>
      {!isHistoryButtonClicked &&
       <HeavyTask/>
      }
    </div>
  );
}

export default Home;
