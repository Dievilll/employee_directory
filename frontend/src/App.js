import React, { lazy, Suspense } from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Navigation from './components/Navigation';

const Auth = lazy(() => import('./components/Auth'));
const Register = lazy(() => import('./components/Register'));
const Employees = lazy(() => import('./components/EmployeeList'));
const Departments = lazy(() => import('./components/DepartmentList'));

const App = () => (
  <Router>
    <Navigation />
    <Suspense fallback={<div>Loading...</div>}>
      <Routes>
        <Route path="/auth" element={<Auth/>} />
        <Route path="/register" element={<Register/>} />
        <Route path="/employees" element={<Employees/>} />
        <Route path="/departments" element={<Departments/>} />
      </Routes>
    </Suspense>
  </Router>
);

export default App;