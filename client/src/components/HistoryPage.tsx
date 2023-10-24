import axios from 'axios';
import React, { useContext, useEffect, useState } from 'react';
import { Button, Form, Input } from 'antd';
import { Link, useNavigate } from 'react-router-dom';
import { Context } from '../index';
import TokenStore from '../store/TokenStore';
import {observer} from 'mobx-react-lite'

interface Props {
  isBtnClicked: (value: boolean) => void;
}

const History = (props: Props) => {
  const [isButtonClicked, setIsButtonClicked] = useState(false);
  const { tokenStore } = useContext(Context);
  const navigate = useNavigate();

  useEffect(() => {
    if(!tokenStore.isValid()){
      navigate('/login');
    }
  }, [])

  const togglePage = () => {
    const newState = !isButtonClicked;
    setIsButtonClicked(newState);
    props.isBtnClicked(newState);
  }

  const ExpireJWT = () => {
    tokenStore.clearToken();
  }

  return (
    <>
      {!isButtonClicked &&
        <div className='nav-bar'>
          <Button type="primary" htmlType="submit" onClick={togglePage}>
            View the history
          </Button>
          <Link to="/login">
            <Button type="primary" htmlType="submit" danger onClick={ExpireJWT}>
              Log Out
            </Button>
          </Link>
        </div>
      }
      {isButtonClicked &&
        <div className='history-block'>
          <div className='nav-bar2'>
            <Button type="primary" htmlType="submit" onClick={togglePage} danger>
              Close the history
            </Button>
          </div>
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

export default observer(History);