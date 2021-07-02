import { Fragment } from "react";
import NavBar from "./navbar";
import { BrowserRouter as Router } from "react-router-dom";


const Layout = () => {
  return (
    <Fragment>
      <Router>
        <NavBar />
      </Router>
    </Fragment>
  );
};

export default Layout;
