import { Fragment } from "react";
import NavBar from "./navbar";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import Login from '../Account/login'
import Register from '../Account/register'

const Layout = () => {
  return (
    <Fragment>
      <Router>
        <NavBar />
        <Switch>
        <Route exact path='/'>
          <Login />
        </Route>
        <Route exact path='/people'>
          <Register />
        </Route>
        </Switch>
      </Router>
    </Fragment>
  );
};

export default Layout;
