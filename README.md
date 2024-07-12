
# BuyInBulk

## Introduction

BuyInBulk is an E-Commerce platform designed for buying and selling products in bulk. The application includes an admin dashboard and is built using the ASP.NET MVC architecture. This project showcases a robust e-commerce solution with functionalities for managing products, orders, and users.


## Features

- User registration and authentication
- Product listing and management
- Order processing and management
- Admin dashboard for overseeing platform activities
- Responsive design for seamless user experience



## Requirements

- .NET Core SDK
- SQL Server (or any other compatible database)
- Visual Studio (or any other compatible IDE)

## Installation

1. Clone the repository:

```bash
    git clone https://github.com/DShubhamBhardwaj/BuyInBulk.git
   cd BuyInBulk

```

2. Open the solution file BuyInBulk.sln in Visual Studio.


3. Set up the database:

- Update the connection string in appsettings.json.
- Run the following commands in the Package Manager Console:


```bash
Update-Database
```

4. Build and run the project:

- Press F5 in Visual Studio to build and run the application.4


## Project Structure

- BuyInBulk/: Contains the main ASP.NET MVC application.
- BuyInBulk.DataAccess/: Data access layer for interacting with - the database.
- BuyInBulk.Models/: Data models representing the entities in - the application.
- BuyInBulk.Utility/: Utility classes and helpers.
- BuyInBulk.sln: Solution file for the project.
## Usage

1. Run the application:

2. Start the application from Visual Studio.
- Access the admin dashboard:

3. Navigate to /admin to access the admin dashboard.
- Manage products, orders, and users through the admin dashboard.

4. Use the platform to browse and purchase products in bulk.

## Screenshots

- Landing Page \
![Landing Page](https://github.com/DShubhamBhardwaj/BuyInBulk/blob/master/ProjectScreenshots/LandingPage.png)

- Product Details Page \
![Product Details Page](https://github.com/DShubhamBhardwaj/BuyInBulk/blob/master/ProjectScreenshots/ProductInfoPage.png)

- Cart Page \
![Cart Page](https://github.com/DShubhamBhardwaj/BuyInBulk/blob/master/ProjectScreenshots/cartPage.png)

- Customer Information Page \
![Customer Information Page](https://github.com/DShubhamBhardwaj/BuyInBulk/blob/master/ProjectScreenshots/OrderPlacement.png)

- Stripe Integration \
![Stripe Integration](https://github.com/DShubhamBhardwaj/BuyInBulk/blob/master/ProjectScreenshots/StripeIntegration.png)

- Admin Screen \
![Admin Screen](https://github.com/DShubhamBhardwaj/BuyInBulk/blob/master/ProjectScreenshots/AdminScreenContent.png)

- Add Product Admin Screen
![Add Product Admin Screen](https://github.com/DShubhamBhardwaj/BuyInBulk/blob/master/ProjectScreenshots/ProductUpsert.png)

- User Authentication\
![USer Authentication](https://github.com/DShubhamBhardwaj/BuyInBulk/blob/master/ProjectScreenshots/UserAuthentication.png)

## Acknowledgements

 - [React](https://reactjs.org/)
 - [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
