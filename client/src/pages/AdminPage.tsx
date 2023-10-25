import axios from 'axios';
import React, { useEffect, useState, useContext } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Button, Form, Input, Modal, message } from 'antd';
import HeavyTask from '../components/HeavyTask';
import History from '../components/HistoryPage';
import { authURL, taskURL } from '../endpoint';
import TokenStore from '../store/TokenStore';
import { Context } from '../index';
import { observer } from 'mobx-react-lite'
import UserModel from '../models/UserModel';


const Admin = () => {
  const { tokenStore } = useContext(Context);
  const [usersData, setUsersData] = useState<UserModel[]>();
  const [deleteUserId, setDeleteUserId] = useState('');
  const [isConfirmationModalVisible, setConfirmationModalVisible] = useState(false);

  const loadUserData = async () => {
    try {
      await axios.get(`${authURL}`, {
        headers: {
          'Authorization': `Bearer ${tokenStore.token}`,
        }
      }).then(
        (response) => {
          const usersDatatmp = response.data;
          setUsersData(usersDatatmp);
        }
      )
    } catch {
      message.error("error", 5);
    }
  }

  useEffect(() => {
    loadUserData();
  })

  const delete_user = async (id: string) => {
    try {
      const axiosConfig = {
        method: 'delete',
        url: `${authURL}/${id}`,
        headers: {
          'Authorization': `Bearer ${tokenStore.token}`,
        }
      };
      await axios(axiosConfig)
        .then(function (response) {
          //modal success
          console.log(response.data)
        })
        .catch(function (error) {
          console.log(error.data);
          console.log(error);
        });
      setConfirmationModalVisible(false);
    } catch {
      message.error("error", 5);
    }
  }

  const showConfirmationModal = (id: string) => {
    setDeleteUserId(id);
    setConfirmationModalVisible(true);
  };

  const closeModal = () => {
    setConfirmationModalVisible(false);
  };

  return (
    <div className='container'>
      <div className='nav-bar'>
        <Link to="/home">
          <Button type="primary" htmlType="submit" danger>
            Back to Tasks
          </Button>
        </Link>
      </div>
      <div className='history-block-info'>
        <p className='history-text'>Users:</p>
        <ul className='history-records'>
          {usersData &&
            usersData.map((user: UserModel) => (
              <div className='history-record'>
                <li key={user.id}>
                  <p>Email: <b>{user.email}</b></p>
                  <p>Role: <b>{user.role}</b></p>
                  <Button
                    type="primary"
                    htmlType="submit"
                    onClick={() => showConfirmationModal(user.id)}
                    danger
                  >
                    Delete
                  </Button>
                </li>
              </div>
            ))}
        </ul>
      </div>

      <Modal
        title="Confirm Deletion"
        visible={isConfirmationModalVisible}
        onOk={() => delete_user(deleteUserId)}
        onCancel={closeModal}
      >
        Are you sure you want to delete this user?
      </Modal>
    </div>
  );
}

export default observer(Admin);