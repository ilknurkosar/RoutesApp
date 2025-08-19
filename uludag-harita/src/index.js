import React from 'react';
import { createRoot } from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';

const container = document.getElementById('root');
const root = createRoot(container); // React 18 için root oluşturduk
root.render(<App />);

// Web vitals ölçümü (isteğe bağlı)
reportWebVitals();
