import React, { createContext } from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import TokenStore from './store/TokenStore';

interface State {
  tokenStore: TokenStore
}
const tokenStore = new TokenStore();

export const Context = createContext<State>({
  tokenStore,
})

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
  // <React.StrictMode>
  <Context.Provider value = {{tokenStore}}>
    <App />
  </Context.Provider>
  // </React.StrictMode>
);
