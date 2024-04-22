import React from 'react'
import ReactDOM from 'react-dom/client'
import Home from './pages/Home'
import './index.css'
import { BrowserRouter } from 'react-router-dom'

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <BrowserRouter basename={process.env.NODE_ENV === 'production' ? '/portfolio' : '/'}>
      <Home />
    </BrowserRouter>
  </React.StrictMode>,
)
