import axios from 'axios';
import React, { useEffect, useState } from 'react';
import { Button, Form, Input } from 'antd';

interface Props {
  isBtnClicked: (value: boolean) => void;
}

const History = (props: Props) => {
  const [isButtonClicked, setIsButtonClicked] = useState(false);
 
  useEffect(() => {
    
  }, [])

  const togglePage = () => {
    const newState = !isButtonClicked;
    setIsButtonClicked(newState);
    props.isBtnClicked(newState);
  }

  return (
    <>
      {!isButtonClicked &&
        <div className='history-block'>
          <Button type="default" htmlType="submit" onClick={togglePage}>
            View the history
          </Button>
        </div>
      }
      {isButtonClicked &&
        <div className='history-block'>
          <Button type="default" htmlType="submit" onClick={togglePage} danger>
            Close the history
          </Button>
          <div className='history-block-info'>
            <p className='history-text'>Your History:</p>
            <p>History1</p>
            <p>History12</p>
            <p>History13</p>
            <p>History14</p>

          </div>
        </div>
      }
    </>
  );
}

export default History;