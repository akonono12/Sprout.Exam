import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';
import { EmployeesIndex } from './views/employees/Index';
import { EmployeeCreate } from './views/employees/Create';
import { EmployeeEdit } from './views/employees/Edit';
import { EmployeeCalculate } from './views/employees/Calculate';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={EmployeesIndex} />
        <Route path='/counter' component={Counter} />
        <AuthorizeRoute path='/fetch-data' component={FetchData} />
        <AuthorizeRoute path='/employees/index' component={EmployeesIndex} />
        <AuthorizeRoute path='/employees/create' component={EmployeeCreate} />
        <AuthorizeRoute path='/employees/:id/edit' component={EmployeeEdit} />
        <AuthorizeRoute path='/employees/:id/calculate' component={EmployeeCalculate} />
        <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
      </Layout>
    );
  }
}
