[![Build Status](https://dev.azure.com/valentineazom0088/BilbaytTest/_apis/build/status/SomValentino.BilbaytTestSolution?branchName=master)](https://dev.azure.com/valentineazom0088/BilbaytTest/_build/latest?definitionId=3&branchName=master)
# BilbaytTestSolution
The solution has five project listed and described below:

1. Bilbayt.Domain: The contains the domain models of the application
2. Bilbayt.Data: The project that conatains the data layer code for the application. This project is responsible for data connection to the database, reading and writing data to the database.
3. Bilbayt.Web.API: The project is exposes the restful api endpoints for handling registration, login and profile of the user.
4. Bilbayt.Web.API.Tests: This project conatins unit test for testing the Login API.
5. bilbayt.web.app: The react.js web project for the front end application.

# Running Solution with Docker
The solution including the web api, database and front end applications has been setup to run with docker as shown in the docker-compose.yaml file in the root directory. If you docker installed in your system, Run the command below in your cmd:

```
docker-compose up --build
```
The command with create docker containers for the api, react.js app and mongo database. The containers can be accessed in the following urls:

a. API:  http://localhost:5000/swagger
b. Web App: http://localhost:3005
c. database: mongodb://mongodb34:27017

# Running Solution without Docker
Setup your mongo db instance and copy connectionstring to key "connectionString" in the appsettings.development.json file in Bilbayt.Web.API folder of the solution.

Change directory to  Bilbayt.Web.API folder and run the command below:

```
dotnet run
```

That will start up the web api application that can now be accessed at http://localhost:5000/swagger to see the end points

Then change directory to bilbayt.web.app and run the command below:

```
npm install
```
The above command will install the necessary dependencies for the react.js application.
```
npm start
```
The above command will start the development server for the react.js application.

# Sending Email After Registration

The email functionality has been implemented in a asynchronous manner. It is not sent immediately when a successful registration is completed. Rather it is queued in the database and a background job written as a .net IHostedService that runs every 2 mins to pick up pending emails which is then sent out to the SendGrid server. The benefits of this approach is:

1. Emails won't lost if network issues occur while sending emails.
2. The api doesn't have delay the registration response while sending the emails and request won't fail because errors occured while sending emails.
3. Emails that are not sent due errors can be later resent by changing their status in the db

# Deployment
An azure build pipeline has been created as shown in the azure-pipeline.yml file that builds each projects and run unit tests. The status of the buid is displayed in the badge above.
