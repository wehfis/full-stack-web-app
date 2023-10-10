import axios from 'axios';
import React, { useEffect, useState } from 'react';
import { Button, Form, Input } from 'antd';

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
  const [taskDescription, setTaskDescription] = useState('');
  const [isClicked, setisClicked] = useState(false);

  const handle_post = () => {
    if (taskName && taskDescription) {
      setisClicked(true);
      axios.post('https://localhost:7173/api/HeavyTask', {
        "id": generate_uuidv4(),
        "name": taskName,
        "description": taskDescription
      })
        .then(function (response) {
          console.log(response);
        })
        .catch(function (error) {
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
        <p>Input your task here:</p>
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
              <Button  type="primary" loading={isClicked}>
                Calculation
              </Button>
            }
          </Form.Item>
        </Form>

      </div>
    </>
  );
}

export default HeavyTask;