import React from "react";
import { Route, Switch,Link } from "react-router-dom";
import classes from './navbar.module.css'
import {useHistory} from 'react-router-dom'
import {useState} from 'react'
import Login from "../Account/login";
import Register from "../Account/register";

const PathContext = React.createContext()
const Navbar = () => {
  const history = useHistory()
  const [path, setPath] = useState(history.location.pathname)
  
  const handleClick = () => {
      moveToPath(path)
  }
  const moveToPath = (currentPath) =>{
    let nextPath = "";
    if (currentPath === "/" || currentPath === "/login") nextPath = "/register";
    else if (currentPath === path || currentPath === "/register") nextPath = "/";
    else nextPath = currentPath;
    history.push(nextPath);
    setPath(nextPath);
  }
  return (
    <PathContext.Provider value={{moveToPath}}>
      <header className={classes.header}>
        <h1>Bilbayt</h1>
        <nav>
          <ul>
            <li>
              {path === "/" || path === "/login" ? (
                <Link className={classes.button} onClick={handleClick}>
                  <span className={classes.badge}>Register</span>
                </Link>
              ) : path === "/register" ? (
                <Link className={classes.button} onClick={handleClick}>
                  <span className={classes.badge}>Login</span>
                </Link>
              ) : (
                <a className={classes.button} onClick={handleClick}>
                  <span className={classes.badge}>Logout</span>
                </a>
              )}
            </li>
          </ul>
        </nav>
      </header>
      <Switch>
        <Route exact path="/">
          <Login />
        </Route>
        <Route exact path="/register">
          <Register />
        </Route>
      </Switch>
    </PathContext.Provider>
  );
};

export default Navbar;
export {PathContext}
