# Secure File Uploader REST API using ASP.NET Core

This project implements a secure file uploader REST API using ASP.NET Core. The service allows users to upload files securely, with features ensuring data privacy and user authentication.

## Table of Contents
- [About](#about)
- [Features](#features)
- [Getting Started](#getting-started)
  - [Installation](#installation)
  - [Usage](#usage)
  - [Using Postman](#using-postman)
- [Database](#database)
- [Testing](#testing)
- [Contributing](#contributing)
- [License](#license)

## About
This project implements a Secure File Uploader REST API using ASP.NET Core. It includes features such as user authentication using JWT, file upload validation, and secure file retrieval. The service ensures data privacy by only allowing authenticated users to upload and access their files.

## Features
- User authentication using JWT
- File upload with server-side validation (file type, size)
- Secure file retrieval for authenticated users
- SQLite database storage with migrations

## Getting Started

### Installation
1. Clone the repository:
    ```bash
    git clone https://github.com/hrist0/FileUploader.git
    ```
2. Navigate to the project directory:
    ```bash
    cd FileUploader
    ```
3. Install dependencies:
    ```bash
    dotnet restore
    ```
4. Run migrations to create the SQLite database:
    ```bash
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```

### Usage
To run the API, use the following command:
```bash
dotnet run
```
The API will be accessible at http://localhost:7281 by default (or any other configured port).

### Using Postman
```markdown

### Authentication:

## Login Endpoint:

- **URL:** https://localhost:7281/api/login
- **Method:** POST
- **Body:**
  ```json
  {
    "email": "your_username",
    "password": "your_password"
  }
  ```
  Replace your_username and your_password with your credentials.

  Successful login returns a JWT token in the response body.

#### Include JWT Token in Subsequent Requests:

Set the "Authorization" header to Bearer <your_jwt_token>, replacing <your_jwt_token> with the token you received from the login.

### File Upload:

#### Upload Endpoint:

- **URL:** https://localhost:7281/api/fileupload
- **Method:** POST
- **Headers:**
  - Authorization: Bearer <your_jwt_token>
- **Body:**
  Select the file you want to upload.

#### Retrieve User Files:

- **URL:** https://localhost:7281/api/fileupload
- **Method:** GET
- **Headers:**
  - Authorization: Bearer <your_jwt_token>

#### Retrieve Single File:

- **URL:** https://localhost:7281/api/fileupload/{{file_id}}
  Replace {{file_id}} with the ID of the file you want to retrieve.
- **Method:** GET
- **Headers:**
  - Authorization: Bearer <your_jwt_token>

### Response Format:

API responses will be in JSON format. Successful responses include relevant information about the uploaded file or requested data. Rejected requests will provide clear error messages indicating the reason for rejection.


## Database
This project uses SQLite as the database. Migration commands are used to create and update the database schema. Refer to the Entity Framework Core documentation for more information on migrations.

## Testing
Unit tests are provided in the FileUploader.Tests project to ensure the reliability and correctness of the code.

## Contributing
Contributions are welcome! Please follow the Contribution Guidelines for details on how to contribute to this project.

## License
This project is licensed under the MIT License.
