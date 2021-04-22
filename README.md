#### setup server
1. run the createscript localted docs/CreateScript.sql
2. verify that the sql connection string will work with your db localted in the SecurePassword_Web_Example/Controllers/userController.cs
3. build the SecurePassword_Web_Example
4. publish the SecurePassword_Web_Example to a folder 
5. copy the view folder to the publish folder
6. run SecurePassword_Web_Example.exe from the publish folder

now you will be able to remote connect to it

<br>

### setup test
use a brower and go to https://ip:5002/user/login
create a user with this values<br>
username : testattack<br>
passowrd : 098765

### hydra
open hydra and use the following command 

    hydra -l testattack -P /usr/share/wordlists/metasploit/unix_passwords.txt IP -S -s PORT http-post-form "/user/form/login:Username=^USER^&Password=^PASS^:wrong input" -vV -f

>IP = the ip of the website<br>
>PORT = the port for the website (in this project it is 5002 for https)
    
