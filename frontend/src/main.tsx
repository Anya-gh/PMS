import React from 'react'
import ReactDOM from 'react-dom/client'
import './index.css'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import Home from './pages/Home'
import About
 from './pages/About'
ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
      <BrowserRouter basename={process.env.NODE_ENV === 'production' ? '/portfolio' : '/'}>
        <Routes>
          <Route path='' element={<Home />} />
          <Route path='/about' element={<About />} />
        </Routes>
      </BrowserRouter>
  </React.StrictMode>,
)
