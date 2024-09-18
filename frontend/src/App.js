import React, { lazy, Suspense } from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Navigation from './components/Navigation';
import ProtectedRoute from './components/ProtectedRoute';
import { AuthProvider } from './components/AuthContext';

const Auth = lazy(() => import('./components/Auth'));
const Employees = lazy(() => import('./components/EmployeeList'));
const Departments = lazy(() => import('./components/DepartmentList'));
const Unauthorized = lazy(() => import('./components/Unauthorized'));


const App = () => (
  <AuthProvider>
    <Router>
      <Navigation />
      <Suspense fallback={<div>Loading...</div>}>
        <Routes>
          <Route path="/auth" element={<Auth />} />
          <Route path="/employees" element={<ProtectedRoute><Employees /></ProtectedRoute>} />
          <Route path="/departments" element={<ProtectedRoute><Departments /></ProtectedRoute>} />
          <Route path="/unauthorized" element={<Unauthorized />} />
        </Routes>
      </Suspense>
    </Router>
  </AuthProvider>
);

export default App;