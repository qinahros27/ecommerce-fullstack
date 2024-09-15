import React from 'react';
import { createRoot } from 'react-dom/client';
import { Provider } from 'react-redux';
import { PersistGate } from 'redux-persist/integration/react';
import reportWebVitals from './reportWebVitals';
import App from './App';
import store from './redux/store';
import { persistStore } from 'redux-persist';

const container = document.getElementById('root')!;
const root = createRoot(container);
let persistor= persistStore(store);

root.render(
  <React.StrictMode>
    <Provider store={store}>
        <PersistGate loading={<h1>Loading...</h1>} persistor={persistor}>
          <App />
        </PersistGate> 
    </Provider>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
