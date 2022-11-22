# EuromonBooks
 Eurominotor Books Api

Tech used: 
- .net Core 6 
- EntityFramework
- EntityFramework 
- FluentValidation 
- AutoFixture 
- XUnit 

1. Create the network, by running 
      `docker network create core-shared`
2. Run the db-createEmptyDB.sh file
     Sometime on windows, the db doesn't want to create itself. You then have to run the SqlCmdScript.sql script yourself.
3. Run the db-populateData.sh file
4. Start up the Api Project (http://localhost:9998/swagger/index.html)
5. Create a new User or Register new User
6. Assign roles to the newly created user (Note that it's not necessary to assign roles to a registered user.)
7. Login using the newly created user credentials
8. Use the system using the token received by the login endpoint
      Purchase a book
      Assign books to users

*Note, this will change in the future, but for now books will have to be manually added into the db (localhost, 1433), the credentials can be found in the docker compose files. (login: sa, password: Passw0rd)

There is a front end built for this api, you are welcome to use the client:
https://github.com/PJNeethling/EuromonBooks-Client