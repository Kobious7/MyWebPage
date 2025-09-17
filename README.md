# MyWebPage
This project for learning Angular, .Net.

-Introduce: This is my Angular project, I make this project with supporting of AI - ChatGPT. Purpose of this project: Learing Angular and .Net.

-Technology: Angular v20, .NET 8, 9, SQL Server.

-Setup:
    +Backend: 
        Installing: 
            .NET SDK v8: https://dotnet.microsoft.com/en-us/download
            Microsoft Visual Studio 2022: https://visualstudio.microsoft.com/fr/vs/
            SQL Server 2022 Express: https://www.microsoft.com/en-us/sql-server/sql-server-downloads
            SQL Server Management Studio 21 (SSMS 21): https://learn.microsoft.com/en-us/ssms/install/install (When you meet "Authentication Mode", choose "Mixed Mode")

        After installing all softwares:
            Open Visual Studio -> Open a project or solution -> Go to project folder -> backend/WithDB/WithDB.sln -> Click Open.
            Check NuGet Package: 
                Tools -> NuGet Package Manager -> Manage NuGet Package for Solution... -> Installed
                Check whether this project have files that listed below. If not, move to Browse find and instal with the lastest version.
                    Microsoft.AspNetCore.Authentication.JwtBearer
                    Microsoft.EntityFrameworkCore
                    Microsoft.EntityFrameworkCore.SqlServer
                    Microsoft.EntityFrameworkCore.Tools
                    Microsoft.VisualStudio.Web.CodeGeneration.Design
                    Swashbuckle.AspNetCore
        Edit ConnectionStrings:
            In appsetting.json edit with your server, expected db name, password (you can find all of this information in SSMS 21):
                "ConnectionStrings": {
                    "DefaultConnection": "Server=your-server;Database=db-name;User Id=sa;Password=your-password;TrustServerCertificate=True"
                }

            Example:
                "ConnectionStrings": {
                    "DefaultConnection": "Server=DESKTOP-2N55KKH\\SQLEXPRESS;Database=ProductDb;User Id=sa;Password=Kobious7;TrustServerCertificate=True"
                }
        Update Your Database with Migration:
            Tools -> NuGet Package Manager -> Package Manger Console:
                Add-Migration migration-name (You can set any name you want and you can find your migration at Migrations folder with corresponding name)
                Example: Add-Migration CreateMyDb
                Update-Database
                You can check update in SSMS 21.
        Run Project and Test API, you can aslo test with Postman.
    
    +Front-end:
        Visual Studio Code: https://code.visualstudio.com/
        Node.js: https://nodejs.org/fr/download

        Angular:
            After installing these sofwares: Open Command Prompt in Windows or Terminal in VS Code:
                With Terminal in VS Code by default, terminal is PowerShell, this can make error or warning if you install Angular without providing permission for PowerShell. The fastest way: Click to Down arrow icon and choose Command Prompt.
                    npm install -g @angular/cli@20
            After installing Angular check the current Angular version:
                ng version

        In VS Code: Choose File -> Open Folder -> Go to project folder -> frontend -> Choose myapp and Open
            Install Dependency: cd to myapp. In termial, run: npm install
            After: ng serve
            Open broswer: http://localhost:4200 or you can click to link in terminal.

Now, you are done. And can test the project.

See demo: https://youtu.be/VzjXG97gbNc?si=8K-7DFJ4dI7oc9K4
            
-Main Functions: product CRUD, login, signup.
