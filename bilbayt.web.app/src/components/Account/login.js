import React,{useState, useEffect} from 'react'
import Card from '../UI/card'
import classes from './login.module.css'
import {useHistory} from 'react-router-dom'


const Login = () => {
    const [login, setLogin]  = useState({username:"",password: ""})
    const [loading, setloading] = useState(false)
    const [error, setError] = useState(null)
    const [valid, setValid] = useState(login.username && login.password)
    const history = useHistory()

    const loginCall = async () => {
       try {
           const response = await fetch("http://localhost:5000/api/account/login",{
               method: 'POST',
               headers: {"Content-Type": "application/json"},
               body: JSON.stringify(login)
           })
           if(response.status === 400) throw new Error('invalid username or password')
           if(response.status >= 200 && response.status >= 299) throw new Error("Something went wrong. kindly try again")
           
           const data = await response.json()
           setloading(false)
           localStorage.setItem('token', data.token)

           history.push('/profile')
       } catch (error) {
           setloading(false);
           setError(error.message)
           setTimeout(() => {
               setError(null)
           },2000)
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
              <div className="alert alert-danger" role="alert">
                {error}
              </div>
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
                  {loading ? (
                    <div class="spinner-border text-info" role="status">
                      <span class="sr-only"></span>
                    </div>
                  ) : (
                    "Login"
                  )}
                </button>
              )}
            </div>
          </form>
        </article>
      </Card>
    );
}



export default Login
