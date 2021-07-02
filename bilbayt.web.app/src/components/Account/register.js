import React, {useState, useEffect} from 'react'
import Card from '../UI/card'
import classes from './login.module.css'
import {useHistory} from 'react-router-dom'

const Register = () => {

    const [login, setLogin] = useState({ fullName:"",username: "", password: "", confirmPassword:"", email: "" });
    const [loading, setloading] = useState(false);
    const [error, setError] = useState(null);
    const [valid, setValid] = useState(false);
    const [touch, setTouch] = useState(false)
    const [message, setMessage] = useState(null)
    
    const history = useHistory()
    const regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,100}$/

    const validateForm = () => {
        if(touch){
            let iserror = false;
            
            if (login.fullName.length < 5 || login.fullName.length > 500)
              iserror = true;

            if (login.username.length < 5 || login.username.length > 50)
              iserror = true;

            if (!regex.test(login.password)) iserror = true;

            if (login.password !== login.confirmPassword) iserror = true;

            if (iserror) {
              setValid(false);
              return;
            }

            setValid(true);
        }
    }

    const registerCall = async () => {
      try {
        const jsonData =  JSON.stringify(login)
        console.log(jsonData)
        console.log(history)
        const response = await fetch(
          "http://localhost:5000/api/account/register",
          {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: jsonData
          }
        );
        if(response.status === 400){
            const error = await response.json()
            const message = []
            if (error.errors && typeof(error.errors) === "object") {
              for (const key in error.errors) {
                message.push(`${key}:${error.errors[key]}`);
              }
            } else message.push(error.errors);

            throw new Error(message.join('\n'))
            
        }
        if (response.status >= 200 && response.status >= 299){
            throw new Error("An error occured with registraion. Kindly try again");
        }
          
        setloading(false);
        setMessage('Registration successful. You will soon be redirected to the login page')
        setTimeout(() => {
            setMessage(null)
            history.push("/")
        },3000)

      } catch (error) {
        setloading(false);
        setError(error.message);
        setTimeout(() => {
          setError(null);
        }, 3000);
      }
    };

    useEffect(() => {
        validateForm()
    },[login.fullName,login.username,login.password,login.email,login.confirmPassword])

    useEffect(() => {
        if(loading)
            registerCall()
    })

    const handleChange = event => {
      const name = event.target.name;
      const value = event.target.value;
      setTouch(true)
      setLogin(prevState => {
         return {...prevState,[name]:value}
      });
    };

    const handleClick = event => {
      event.preventDefault();
      setloading(true);
    };

    return (
      <Card>
        <article>
          {message && (
            <div className="alert alert-success" role="alert">
              {message}
            </div>
          )}
          {error && (
            <div className="alert alert-danger" role="alert">
              {error}
            </div>
          )}
          <form>
            <div className="form-group row">
              <label htmlFor="fullName" className="control-label col-md-3">
                Full Name
              </label>
              <div className="col-md-9">
                <input
                  type="text"
                  id="fullName"
                  name="fullName"
                  value={login.fullName}
                  onChange={handleChange}
                  className="form-control"
                  required
                  maxLength="500"
                  minLength="5"
                />
                {touch &&
                  (login.fullName.length < 5 || login.fullName.length > 500) && (
                    <span className={classes.spincolor}>
                      fullname is required and must be between 5 to 500
                      characters in length
                    </span>
                  )}
              </div>
            </div><br/>
            <div className="form-group row">
              <label htmlFor="username" className="control-label col-md-3">
                Username
              </label>
              <div className="col-md-9">
                <input
                  type="text"
                  id="username"
                  name="username"
                  value={login.username}
                  onChange={handleChange}
                  className="form-control"
                  required
                  maxLength="50"
                  minLength="5"
                />
                {touch &&
                  (login.username.length < 5 || login.username.length > 50) && (
                    <span className={classes.spincolor}>
                      username is required and must be between 5 to 50
                      characters in length
                    </span>
                  )}
              </div>
            </div>
            <br />
            <div className="form-group row">
              <label htmlFor="email" className="control-label col-md-3">
                Email
              </label>
              <div className="col-md-9">
                <input
                  type="email"
                  id="email"
                  name="email"
                  value={login.email}
                  onChange={handleChange}
                  className="form-control"
                  required
                />
              </div>
            </div>
            <br />
            <div className="form-group row">
              <label htmlFor="password" className="control-label col-md-3">
                Password
              </label>
              <div className="col-md-9">
                <input
                  type="password"
                  id="password"
                  name="password"
                  value={login.password}
                  onChange={handleChange}
                  className="form-control"
                  required
                />
                {touch && !regex.test(login.password) && (
                  <span className={classes.spincolor}>
                    Invalid Password. Password of the user must contain aleast 8
                    characters , 1 CAP, 1 special, 1 Number
                  </span>
                )}
              </div>
            </div>
            <br />
            <div className="form-group row">
              <label
                htmlFor="confirmPassword"
                className="control-label col-md-3"
              >
                Confirm Password
              </label>
              <div className="col-md-9">
                <input
                  type="password"
                  id="confirmPassword"
                  name="confirmPassword"
                  value={login.confirmPassword}
                  onChange={handleChange}
                  className="form-control"
                  required
                />
                {touch && login.confirmPassword !== login.password && (
                  <span className={classes.spincolor}>
                    Confirm password must match Password
                  </span>
                )}
              </div>
            </div>
            <br />
            <div>
              {!valid ? (
                <button type="submit" className={classes.button} disabled>
                  Register
                </button>
              ) : (
                <button
                  type="submit"
                  className={classes.button}
                  onClick={handleClick}
                >
                  {loading ? (
                    <div className="spinner-border text-info" role="status">
                      <span className="sr-only"></span>
                    </div>
                  ) : (
                    "Register"
                  )}
                </button>
              )}
            </div>
          </form>
        </article>
      </Card>
    );
}


export default Register
