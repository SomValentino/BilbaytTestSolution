import React,{useState, useEffect} from 'react'
import Card from '../UI/card'
import classes from './login.module.css'
import {useHistory} from 'react-router-dom'


const Login = (props) => {
    const [login, setLogin]  = useState({username:"",password: ""})
    const [loading, setloading] = useState(false)
    const [error, setError] = useState(null)
    const [valid, setValid] = useState(login.username && login.password)
    const history = useHistory()
    let makeapicall = false

    const loginCall = async () => {
       try {
           const response = await fetch("http://localhost:5000/api/account/login",{
               method: 'POST',
               body: JSON.stringify(login)
           })
           if(response.status >= 200 && response.status >= 299) throw new Error(response.statusText)
           
           const data = await response.json()
           setloading(false)
           localStorage.setItem('token', data.token)

           history.push('/profile')
       } catch (error) {
           setloading(false);
           setError(error.message)
       }
    }
    
    useEffect(() => {
        if(loading)
          loginCall()
    })
    
    const handleChange = (event) => {
        const name = event.target.name;
        const value = event.target.value;
        setLogin({...login,[name] : value })
        setValid(login.username && login.password)
    }

    const handleClick = (event) => {
        event.preventDefault()
        setloading(true)
    }
    return (
      <Card>
        <article className="form">
          <div className={classes.center}>
            <h4>Login</h4>
            <p>If you already have credential</p>
            {error && (
              <span className="alert alert-danger" role="alert">
                {error}
              </span>
            )}
          </div>
          <form>
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
                />
              </div>
            </div>
            <br />
            <div className="form-group row">
              <label htmlFor="username" className="control-label col-md-3">
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
                />
              </div>
            </div>
            <div>
              {!valid ? (
                <button type="submit" className={classes.button} disabled>
                  Login
                </button>
              ) : (
                <button
                  type="submit"
                  className={classes.button}
                  onClick={handleClick}
                >
                  {loading ? <div className={`spinner-border text-primary ${classes.spincolor}`} role="status">
                    <span className="sr-only"></span>
                  </div> : "Login"
                  }
                  
                </button>
              )}
            </div>
          </form>
        </article>
      </Card>
    );
}



export default Login
