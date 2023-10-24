import { makeObservable, observable, action } from 'mobx';
import { isExpired, decodeToken } from "react-jwt";

export default class TokenStore {
  token = '';
  email = '';

  constructor() {
    this.loadToken();

    makeObservable(this, {
      token: observable,
      email: observable,
      setToken: action,
      clearToken: action,
    });
  }
  isValid() {
    return !isExpired(this.token);
  }

  setToken(token: string, email:string) {
    this.token = token;
    this.email = email;
    localStorage.setItem('jwtToken', token);
    localStorage.setItem('email', email);
  }

  clearToken() {
    this.token = '';
    this.email = '';
    localStorage.removeItem('jwtToken');
    localStorage.removeItem('email');
  }

  loadToken() {
    const token = localStorage.getItem('jwtToken');
    const email = localStorage.getItem('email');
    if (token && email) {
        this.token = token;
        this.email = email;
    }else {
      this.token = '';
    }
  }
}
