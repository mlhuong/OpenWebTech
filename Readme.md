You can use swagger to test out the apis:
http://localhost:62012/swagger

The data are stored in an in-memory sql database, so data are lost if we close the application.

To try the application for actions that need authorization:
- use the "register" route in the Account controller to create an account (a user of the application)
- copy the output of the register api  (it is a jwt token)
- click on the "authorize" button in the top right corner of swagger page, and paste in the content of the token. Click "Authorize", then "close". 
This will set the token automatically in the headers of each call. 




