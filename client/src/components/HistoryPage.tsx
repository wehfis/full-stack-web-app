import axios from 'axios';
import React, { useContext, useEffect, useState } from 'react';
import { Button, Form, Input, message } from 'antd';
import { Link, useNavigate } from 'react-router-dom';
import { Context } from '../index';
import TokenStore from '../store/TokenStore';
import { observer } from 'mobx-react-lite'
import { authURL, taskURL } from '../endpoint'
import TaskModel from '../models/TaskModel';

interface Props {
  isBtnClicked: (value: boolean) => void;
}

const History = (props: Props) => {
  const [isButtonClicked, setIsButtonClicked] = useState(false);
  const [userId, setUserId] = useState('');
  const [role, setRole] = useState('');
  const { tokenStore } = useContext(Context);
  const [historyData, setHistoryData] = useState<TaskModel[]>();
  const navigate = useNavigate();

  const preloadData = async () => {
    try {
      await axios.get(`${authURL}/${tokenStore.email}`, {
        headers: {
          'Authorization': `Bearer ${tokenStore.token}`,
        }
      }).then(
        (response) => {
          const tmpUserId = response.data.id;
          setUserId(tmpUserId);
        }
      )
    } catch {
      message.error("error", 5);
    }
  }

  useEffect(() => {
    preloadData();
    if (!tokenStore.isValid()) {
      tokenStore.clearToken();
      navigate('/login');
    }
    if (!role) {
      try {
        axios.get(`${authURL}/${tokenStore.email}`, {
          headers: {
            'Authorization': `Bearer ${tokenStore.token}`,
          }
        }).then(
          (response) => {
            const userRole = response.data.role;
            setRole(userRole);
          }
        )
      } catch {
        message.error("error", 5);
      }
    }
  }, [])

  const togglePage = async () => {
    const newState = !isButtonClicked;
    props.isBtnClicked(newState);
    setIsButtonClicked(newState);
  }

  const togglePageUploadData = async () => {
    const newState = !isButtonClicked;
    props.isBtnClicked(newState);
    setIsButtonClicked(newState);
    try {
      await axios.get(`${taskURL}/GetSpecUser/${userId}`, {
        headers: {
          'Authorization': `Bearer ${tokenStore.token}`,
        }
      }).then(
        (response) => {
          const responseData: TaskModel[] = response.data;
          setHistoryData(responseData);
        }
      )
    } catch {
      message.error("error", 5);
    }
  }

  const ExpireJWT = () => {
    tokenStore.clearToken();
  }

  const formattedDate = (dateString: string) => {
    if (!dateString) return "task isn't finished yet!"
    const date = new Date(dateString);
    return !isNaN(date.getTime()) ? date.toLocaleString() : dateString;
  };


  const downloadFile = (id: string) => {
    const historyDataOrDefault = historyData ?? [];
    let dataToDownload;
    let filename: string | undefined;

    if (id !== "all") {
      const historyRecord = historyData?.find((item) => item.id === id); // TaskModel type
      if (historyRecord) {
        const csvContent = `name, description, result, startedAt, finishedAt, percentageDone\n${historyRecord.name},${historyRecord.description},${historyRecord.result},${historyRecord.startedAt},${historyRecord.finishedAt},${historyRecord.percentageDone}`;
        dataToDownload = new Blob([csvContent], { type: 'text/plain' });
        filename = `record_${historyRecord.name}.csv`;
      }
    } else {
      const headerRow = 'name, description, result, startedAt, finishedAt, percentageDone';
      const dataRows = historyDataOrDefault
        .map((historyRecord: TaskModel) => `${historyRecord.name},${historyRecord.description},${historyRecord.result},${historyRecord.startedAt},${historyRecord.finishedAt},${historyRecord.percentageDone}`)
        .join('\n');
      const csvContent = `${headerRow}\n${dataRows}`;
      dataToDownload = new Blob([csvContent], { type: 'text/plain' });
      filename = `AllHistory.csv`;
      console.log("asfaulf");
      console.log(dataRows);
    }

    if (dataToDownload && filename) {
      const url = window.URL.createObjectURL(dataToDownload);
      const a = document.createElement('a');
      a.href = url;
      a.download = filename;
      a.click();
      window.URL.revokeObjectURL(url);
    }
  };


  return (
    <>
      {!isButtonClicked &&
        <div className='nav-bar'>
          <Button type="primary" htmlType="submit" onClick={togglePageUploadData}>
            View the history
          </Button>
          <Link to="/login">
            <Button type="primary" htmlType="submit" danger onClick={ExpireJWT}>
              Log Out
            </Button>
          </Link>
        </div>
      }
      {!isButtonClicked && role == "Admin" &&
        <Link to="/admin">
          <Button type="primary" htmlType="submit">
            Admin panel
          </Button>
        </Link>
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
            <ul className='history-records'>
              {historyData &&
                historyData.map((item: TaskModel) => (
                  <div className='history-record'>
                    <li key={item.id}>
                      <p>Name: <b>{item.name}</b></p>
                      <p>Description: <b>{item.description}</b></p>
                      <p>Started At: <b>{formattedDate(item.startedAt)}</b></p>
                      <p>Finished At: <b>{formattedDate(item.finishedAt)}</b></p>
                      <p>Percentage Done: <b>{item.percentageDone}%</b></p>
                      <p>Result: <b>{item.result}</b></p>
                    </li>
                    <Button type="primary" onClick={() => downloadFile(item.id)}>
                      Download History Record
                    </Button>
                  </div>
                ))}
            </ul>
            <Button type="primary" onClick={() => downloadFile("all")}>
              Download All Records
            </Button>
          </div>
        </div>
      }
    </>
  );
}

export default observer(History);
