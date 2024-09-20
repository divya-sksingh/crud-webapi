# Product API

A simple ASP.NET Core Web API project to perform CRUD operations on a Product model using Entity Framework Core and the Repository Pattern.

## Features
- Create, read, update, and delete products.
- Auto-generate 6-digit IDs for each product.
- Repository pattern for separation of concerns.
- Swagger UI for API testing.

## Follow these steps for Local setup

### Prerequisites
	1. Install visual studio 2022
	2. Install .Net 6 SDK
	3. Install Sql Server 2022 Developer Edition
	4. Install Sql Server Management Studio

### Setup

	1. Launch Sql Server Management Studio
	2. Click on 'Connect Server'
	3. In the dialog box, select Server Type : Database Engine
	4. Server Name: Loalhost
	5. Select Authentication as 'Windows Authentication'
	6. Click Connect
	7. In case you face issues connecting, in the same dialog box click on 'Options' at the bottom right corner
	8. Select 'Encryption' as Optional and try reconnecting

### Run app locally
	1. Clone the repository
	2. Modify your connection string in the key "DefaultConnection" of appsettings.json if needed
	3. In visual studio, go to Tools -> Nuget Package Manager -> Package Manager Console
	4. Run the below command
		Update-Database


Launch the application and debug away
