import React, { useContext, useEffect, useState } from 'react';
import { Button, Form, Input, Modal, message } from 'antd';
import { Link } from 'react-router-dom';
import axios from 'axios';
import { authURL } from '../endpoint'
import { Navigate } from 'react-router-dom';
import User from '../DTOs/userDTO'
import { observer } from 'mobx-react-lite';
import { Context } from '../index';

const Login = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [errorMessage, setErrorMessage] = useState("");
    const [isModalErrorOpen, setIsModalErrorOpen] = useState(false);
    const { tokenStore } = useContext(Context);

    const onFinish = async () => {
        const newUser: User = {
            email: email,
            password: password
        }
        const axiosConfig = {
            method: 'post',
            url: `${authURL}/login`,
            data: newUser,
            withCredentials: true,
        };
        try {
            await axios(axiosConfig)
                .then(response => {
                    tokenStore.setToken(response.data, email);
                })
                .catch(err => {
                    if (err.response)
                        setErrorMessage(err.response.data);
                    else
                        setErrorMessage(err.message);

                    setIsModalErrorOpen(true);
                });
        } catch {
            message.error("error", 5);
        }
    };

    const isValidEmail = (email: string) => {
        const emailRegex = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i;
        return emailRegex.test(email);
    };

    const handleOk = () => {
        setIsModalErrorOpen(false);
    };

    if (tokenStore.isValid()) {
        return <Navigate replace to="/home" />
    }

    return (

        <div className='login-container'>
            <div className='login-btn'>
                <Link to="/">
                    <Button type="default" htmlType="submit">
                        Register
                    </Button>
                </Link>
            </div>
            <p className='general-text'>Log In</p>
            <Form
                name="basic"
                labelCol={{ span: 8 }}
                wrapperCol={{ span: 16 }}
                style={{ maxWidth: 800, minWidth: 600 }}
                initialValues={{ remember: true }}
                onFinish={onFinish}
                autoComplete="off"
            >
                <Form.Item<User>
                    label="Email"
                    name="email"
                    rules={[{ required: true, message: 'Please input your email address!' },
                    { validator: (_, value) => isValidEmail(value) ? Promise.resolve() : Promise.reject('Please enter a valid email address') },
                    ]}
                >
                    <Input showCount maxLength={50} onChange={(e) => setEmail(e.target.value)} />
                </Form.Item>

                <Form.Item<User>
                    label="Password"
                    name="password"
                    rules={[{ required: true, message: 'Please input your password!' }]}
                >
                    <Input.Password showCount maxLength={50} onChange={(e) => setPassword(e.target.value)} />
                </Form.Item>

                <Form.Item wrapperCol={{ offset: 14, span: 16 }}>
                    <Button type="primary" htmlType="submit">
                        Log In
                    </Button>
                </Form.Item>
            </Form>
            <Modal title="ERROR" open={isModalErrorOpen} closeIcon={null} footer={[
                <Button key="submit" type="primary" onClick={handleOk}>
                    OK
                </Button>]}>
                <p>{errorMessage}</p>
            </Modal>
        </div>
    )
};

export default observer(Login);