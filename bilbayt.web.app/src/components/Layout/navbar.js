import React from "react";
import { Link } from "react-router-dom";
import classes from './navbar.module.css'
import {useHistory} from 'react-router-dom'
import {useState} from 'react'
const Navbar = () => {
  const history = useHistory()
  const [path, setPath] = useState(history.location.pathname)

  const handleClick = () => {
      let nextPath = ''
      if(path === '/' || path === '/login') nextPath = '/register'
      else nextPath ='/'
      history.push(nextPath)
      setPath(nextPath)
  }
  return (
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
  );
};

export default Navbar;
