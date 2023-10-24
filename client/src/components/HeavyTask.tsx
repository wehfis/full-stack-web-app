import axios from 'axios';
import React, { useContext, useEffect, useState } from 'react';
import { Button, Form, Input } from 'antd';
import { authURL, taskURL } from '../endpoint';
import TokenStore from '../store/TokenStore';
import { Context } from '../index'
import { observer } from 'mobx-react-lite'
import TaskDTO from '../DTOs/taskDTO'

const { TextArea } = Input;

function generate_uuidv4() {
  return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g,
    function (c) {
      var uuid = Math.random() * 16 | 0, v = c == 'x' ? uuid : (uuid & 0x3 | 0x8);
      return uuid.toString(16);
    });
}

const HeavyTask = () => {
  const [taskName, setTaskName] = useState('');
  const [userId, setUserId] = useState('');
  const [taskDescription, setTaskDescription] = useState('');
  const [isClicked, setisClicked] = useState(false);
  const { tokenStore } = useContext(Context);

  const handle_post = async () => {
    if (taskName && taskDescription) {
      axios.get(`${authURL}/${tokenStore.email}`, {
        headers: {
          'Authorization': `Bearer ${tokenStore.token}`,
        }
      }).then(
        (response) => {
          setUserId(response.data.id);
        }
      )
      const newTask: TaskDTO = {
        id: generate_uuidv4(),
        name: taskName,
        description: taskDescription,
        ownerId: userId
      }
      const axiosConfig = {
        method: 'post',
        url: `${taskURL}`,
        data: newTask,
        withCredentials: true,
        headers: {
          'Authorization': `Bearer ${tokenStore.token}`,
        }
      };
      await axios(axiosConfig)
        .then(function (response) {
          console.log(response);
        })
        .catch(function (error) {
          console.log("sadld");
          console.log(error.data);
          console.log(error);
        });

    }
    else if (!(taskName && taskDescription)) {
      console.log("Task Name and Task Descriprion Should be setted");
    }
    else {
      console.log("back end unavailable for some reasons.")
    }
  }

  return (
    <>
      <div className='input-block'>
        <p className='general-text'>WELCOME!</p>
        <p>Input your task here:</p>
        <br />
        <Form
          name="wrap"
          labelCol={{ flex: '150px' }}
          labelAlign="left"
          labelWrap
          wrapperCol={{ flex: 1 }}
          colon={false}
          style={{ maxWidth: 600 }}
        >
          <Form.Item label="Task Name" name="name" rules={[{ required: true }]}>
            <Input name='Task Name' showCount maxLength={50} onChange={(e) => setTaskName(e.target.value)} />
          </Form.Item>

          <Form.Item label="Task Description" name="description" rules={[{ required: true }]}>
            <TextArea showCount maxLength={10000} onChange={(e) => setTaskDescription(e.target.value)} />
          </Form.Item>

          <Form.Item label=" ">
            {!isClicked &&
              <Button type="primary" htmlType="submit" onClick={handle_post}>
                Submit
              </Button>
            }
            {isClicked &&
              <Button type="primary" loading={isClicked}>
                Calculation
              </Button>
            }
          </Form.Item>
        </Form>

      </div>
    </>
  );
}

export default observer(HeavyTask);