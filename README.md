Secure File Uploader REST API using ASP.NET Core
This project implements a secure file uploader REST API using ASP.NET Core. The service allows users to upload files securely, with features ensuring data privacy and user authentication.

Table of Contents
About
Features
Getting Started
Installation
Usage
Using Postman
Database
Contributing
License
About
This project implements a Secure File Uploader REST API using ASP.NET Core. It includes features such as user authentication using JWT, file upload validation, and secure file retrieval. The service ensures data privacy by only allowing authenticated users to upload and access their files.

Features
User authentication using JWT
File upload with server-side validation (file type, size)
Secure file retrieval for authenticated users
SQLite database storage with migrations
Getting Started
Installation
Clone the repository:

bash
Copy code
https://github.com/hrist0/FileUploader.git
Navigate to the project directory:

bash
Copy code
cd FileUploader
Install dependencies:

bash
Copy code
dotnet restore
Run migrations to create the SQLite database:

bash
Copy code
dotnet ef migrations add InitialCreate
dotnet ef database update

Usage
To run the API, use the following command:

bash
Copy code
dotnet run
The API will be accessible at http://localhost:5000 by default.

Using Postman
You can use Postman to interact with the API endpoints. Import the provided Postman collection to get started.

Postman Collection

Database
This project uses SQLite as the database. Migration commands are used to create and update the database schema. Refer to the Entity Framework Core documentation for more information on migrations.

Contributing
Contributions are welcome! Please follow the Contribution Guidelines for details on how to contribute to this project.

License
This project is licensed under the MIT License.
