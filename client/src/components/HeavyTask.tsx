import axios from 'axios';
import React, { useEffect } from 'react';
import { Button, Form, Input } from 'antd';

const { TextArea } = Input;

const HeavyTask = () => {



    const onChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        console.log('Change:', e.target.value);
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
    <Form.Item label="Task Name" name="username" rules={[{ required: true }]}>
    <Input name='Task Name' showCount maxLength={50} onChange={onChange} />
    </Form.Item>

    <Form.Item label="Task Description" name="password" rules={[{ required: true }]}>
      <TextArea showCount maxLength={10000} onChange={onChange} />
    </Form.Item>

    <Form.Item label=" ">
      <Button type="primary" htmlType="submit">
        Submit
      </Button>
    </Form.Item>
  </Form>
    
    </div>
    </>
  );
}

export default HeavyTask;