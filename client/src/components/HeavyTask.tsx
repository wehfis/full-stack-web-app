import axios from 'axios';
import React, { useContext, useEffect, useState } from 'react';
import { Button, Form, Input } from 'antd';
import { authURL, taskURL } from '../endpoint';
import TokenStore from '../store/TokenStore';
import { Context } from '../index'
import { observer } from 'mobx-react-lite'
import TaskDTO from '../DTOs/taskDTO'
import { message } from 'antd';
import TaskModel from '../models/TaskModel';

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
  const [taskId, setTaskId] = useState('');
  const [taskDescription, setTaskDescription] = useState('');
  const [isClicked, setIsClicked] = useState(false);
  const [percentageDone, setPercentageDone] = useState(0);
  const { tokenStore } = useContext(Context);
  const [form] = Form.useForm();

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

  const getTaskStatus = async () => {
    try {
      if (isClicked) {
        const axiosConfig = {
          method: 'get',
          url: `${taskURL}/${taskId}`,
          headers: {
            'Authorization': `Bearer ${tokenStore.token}`,
          }
        };
        await axios(axiosConfig)
          .then(function (response) {
            const percentages = response.data["percentageDone"];
            setPercentageDone(percentages);
            if (percentages === 100) {
              tokenStore.clearTask();
              setIsClicked(false);
              setPercentageDone(0);
              message.success('Task successfully finished', 5);
            }
          })
          .catch(error => {
            message.error("error", 5)
            setIsClicked(false);
            setPercentageDone(0);
          });
      }
    } catch {
      message.error("error", 5)
    }
  }

  useEffect(() => {
    let interval: any;
    preloadData();
    if (tokenStore.task_id) { 
      interval = setInterval(getTaskStatus, 1000); 
      setTaskId(tokenStore.task_id);
      setIsClicked(true);
    }

    return () => {
      clearInterval(interval);
    };
  }, [isClicked])
  const handle_post = async () => {
    if (taskName && taskDescription) {
      const newTask: TaskDTO = {
        id: generate_uuidv4(),
        name: taskName,
        description: taskDescription,
        ownerId: userId
      }
      setTaskId(newTask.id);
      tokenStore.setTask(newTask.id);
      const axiosConfig = {
        method: 'post',
        url: `${taskURL}`,
        data: newTask,
        headers: {
          'Authorization': `Bearer ${tokenStore.token}`,
        }
      };
      try {
        await axios(axiosConfig)
          .then(function (response) {
            setIsClicked(true);
            setTaskName('');
            setTaskDescription('');
            form.resetFields();
            message.success('Task successfully created', 5);
          })
          .catch(function (error) {
            message.error("error", 5)
            setIsClicked(false);
            setPercentageDone(0);
          });
      } catch {
        message.error("error", 5);
      }
    }
    else if (!(taskName && taskDescription)) {
      console.log("Task Name and Task Descriprion Should be setted");
    }
    else {
      console.log("back end unavailable for some reasons.")
    }
  }

  const CancelTask = () => {
    tokenStore.clearTask();
    setIsClicked(false);
    setPercentageDone(0);
    message.success('Task successfully cancelled', 5);
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
          form={form}
          onFinish={handle_post}
        >
          <Form.Item label="Task Name" name="name" rules={[{ required: true }]}>
            <Input name='Task Name' showCount maxLength={50} value={taskName} onChange={(e) => setTaskName(e.target.value)} />
          </Form.Item>

          <Form.Item label="Task Description" name="description" rules={[{ required: true }]}>
            <TextArea showCount maxLength={10000} value={taskDescription} onChange={(e) => setTaskDescription(e.target.value)} />
          </Form.Item>

          <Form.Item label=" ">
            {!isClicked &&
              <Button type="primary" htmlType="submit">
                Submit
              </Button>
            }
            {isClicked &&
              <>
              <Button type="primary" loading={isClicked}>
                {/* {percentageDone == 100 setIsClicked(false)} */}
                Calculation In Proccess the {percentageDone}% is done...
              </Button>
              <Button type="primary" danger onClick={CancelTask}>
                Cancel Task
              </Button>
              </>
            }
          </Form.Item>
        </Form>

      </div>
    </>
  );
}

export default observer(HeavyTask);