import React from "react";
import { Link } from "react-router-dom";
import classes from './navbar.module.css'
const Navbar = () => {
  return (
    <header className={classes.header}>
      <h1>Bilbayt</h1>
      <nav>
        <ul>
          <li>
            <Link className={classes.button} to="/register">
              <span className={classes.badge}>Register</span>
            </Link>
          </li>
        </ul>
      </nav>
    </header>
  );
};

export default Navbar;
