import React, { useState, useEffect, useContext } from "react";
import Card from "../UI/card";
import { PathContext } from "../Layout/navbar";
import { useHistory } from "react-router-dom";
import classes from "./profile.module.css";

const Profile = props => {
  const [profile, setProfile] = useState({ fullname: "", username: "" });
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const { moveToPath } = useContext(PathContext);
  const history = useHistory();
  const { REACT_APP_BaseUrl } = process.env;

  const getProfileCall = async () => {
    try {
      const response = await fetch(`${REACT_APP_BaseUrl}/api/profile`, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${localStorage.getItem("token")}`
        }
      });

      if (response.headers.get("Token-Expired"))
        throw new Error("Session expired. You will redirected to login page");
      if (response.status >= 400 && response <= 403)
        throw new Error(
          "You are not authorized to view this page. You will redirected to login page"
        );
      if (response.status >= 200 && response.status >= 299)
        throw new Error(
          "Invalid session credentials. You will redirected to login page"
        );

      const data = await response.json();
      setLoading(false);
      setProfile({
        ...profile,
        fullname: data.fullName,
        username: data.username
      });
    } catch (error) {
      setLoading(false);
      setError(error.message);

      setTimeout(() => {
        moveToPath(history.location.pathname);
      }, 3000);
    }
  };
  useEffect(() => {
    if (localStorage.getItem("token")) {
      getProfileCall();
    } else {
      moveToPath(history.location.pathname);
    }
  }, []);

  return (
    <Card>
      <div className={classes.center}>
        <h4>Profile</h4>
      </div>
      {loading ? (
        <div class="spinner-border text-info" role="status">
          <span class="sr-only"></span>
        </div>
      ) : error ? (
        <div className="alert alert-danger" role="alert">
          {error}
        </div>
      ) : (
        <table className="table table-bordered">
          <thead>
            <tr>
              <th scope="col">#</th>
              <th scope="col">Field</th>
              <th scope="col">Value</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <th scope="row">1</th>
              <td>Full Name</td>
              <td>{profile.fullname}</td>
            </tr>
            <tr>
              <th scope="row">2</th>
              <td>Username</td>
              <td>{profile.username}</td>
            </tr>
          </tbody>
        </table>
      )}
    </Card>
  );
};

export default Profile;
