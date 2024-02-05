# Fullstack Project - Ecommerce App

![TypeScript](https://img.shields.io/badge/TypeScript-v.4-green)
![SASS](https://img.shields.io/badge/SASS-v.4-hotpink)
![React](https://img.shields.io/badge/React-v.18-blue)
![Redux toolkit](https://img.shields.io/badge/Redux-v.1.9-brown)
![.NET Core](https://img.shields.io/badge/.NET%20Core-v.7-purple)
![EF Core](https://img.shields.io/badge/EF%20Core-v.7-cyan)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-v.14-drakblue)

This project involves creating a Fullstack project with React and Redux on the frontend and ASP.NET Core 7 on the backend. The goal is to provide a seamless experience for users, along with robust management system for administrators.

- Frontend: SASS, TypeScript, React, Redux Toolkit
- Backend: ASP .NET Core, Entity Framework Core, PostgreSQL

## Table of Contents

1. [Features](#features)
   - [Mandatory features](#mandatory-features)
   - [Extra features](#extra-features)
2. [Requirements](#requirements)
3. [Project Structure](#project-structure)
4. [API Planning](#api-planning)
5. [Getting Started](#getting-started)
6. [Testing](#testing)

## Features

### Mandatory features

#### User Functionalities

1. User Management: Users should be able to register for an account and log in. Users cannot register themselves as admin.
2. Browse Products: Users should be able to view all available products and single product, search and sort products.
3. Add to Cart: Users should be able to add products to a shopping cart, and manage cart.
4. Checkout: Users should be able to place order.

#### Admin Functionalities

1. User Management: Admins should be able to view and delete users.
2. Product Management: Admins should be able to view, edit, delete and add new products.
3. Order Management: Admins should be able to view all orders

### Extra features

#### User Functionalities

1. User Management: Users should be able to view and edit only certain properties in their accounts. They also can unregister their own accounts.
2. Authentication and account registration with Google Oauth.
3. Order Management: Users should be able to view their order history, track the status of their orders, and potentially cancel orders within a certain timeframe.

#### Admin Functionalities

1. User Management: Admins should be able to edit users' role and create new users.
2. Order Management: Admins should be able to update order status, view order details, handle returns/refunds, and cancel orders.

And any other extra features that you want to implement ...

## Requirements

1. Apply CLEAN architecture in your backend. In README file, explain the architecture of your project as well.
2. Implement Error Handling Middleware: This will ensure any exceptions thrown in your application are handled appropriately and helpful error messages are returned.
3. Document with Swagger: Make sure to annotate your API endpoints and generate a Swagger UI for easier testing and documentation.
4. Project should have proper file structure, naming convention, and comply with Rest API.
5. `README` file should sufficiently describe the project, as well as the deployment.

## Project Structure

### Backend structure according to CLEAN architecture: 
1. Web Api layer:
   - Authorization Requirement
   - Configuration
   - Database
   - Middleware
   - Repository Implementation
  
3. Controllers:
   - Controllers

3. Use Cases - Business layer: 
   - Abstractions
   - Dto entities
   - Implementations - Logic apply
   - Shared

4. Entities - Domain layer: 
   - Abstractions 
   - Entities 
   - Shared

+++ Testing

### Frontend structure :
1. authenticate
2. hooks
3. images
4. pages
5. redux
6. styles
7. tests
8. types
   
App.tsx

index.css

index.tsx

## API Planning
Queries for all: 
```
Filter by search:
[GET] /api/v1/products?Search=BookName (title of the product)
[GET] /api/v1/users?Search=Anna (first name or last name of user)
[GET] /api/v1/categorys?Search=Book (name of the category)
Filter by order: (order by what property)
[GET] /api/v1/products?Order=Title
[GET] /api/v1/users?Order=FirstName
Filter by order and order descending:
[GET] /api/v1/products?Order=Title&OrderByDescending=true
[GET] /api/v1/users?Order=FirstName&OrderByDescending=true
Filter by offset and limit:
[GET] /api/v1/products?Offset=1&Limit=50
[GET] /api/v1/users?Offset=30&Limit=80
```

Products:
```
[GET] /api/v1/products: Get a list of products
[GET] /api/v1/products/:id : Get a product by id 
[POST]  /api/v1/products: Create a new product [authorize admin]
[PUT] /api/v1/products/:id : Modify a product [authorize admin]
[DELETE] /api/v1/products/:id: Delete a product [authorize admin] 
```

Queries for only product (extra): 
```
Filter by min price and max price
[GET] /api/v1/products?MinPrice=500&MaxPrice=1200
Filter by min price, max price, category
[GET] /api/v1/products?MinPrice=200&MaxPrice=500&CategroryId=2
```

Category:
```
[GET] /api/v1/categories: Get a list of categories
[GET] /api/v1/ categories/:id : Get a category by id
[POST]  /api/v1/ categories: Create a new category [authorize admin]
[PUT] /api/v1/ categories/:id : Modify a category [authorize admin]
[DELETE] /api/v1/ categories/:id: Delete a category by id  [authorize admin]
```

Order:
```
[GET] /api/v1/orders: Get a list of orders [authorize-admin]
[GET] /api/v1/ orders/:id : Get a order by id [authorize]
[POST]  /api/v1/orders: Create a new order  [authorize]
[PUT] /api/v1/ orders/:id : Modify a order [authorize-owner]
[DELETE] /api/v1/orders/:id: Delete a order [authorize]
```

Payment: 
```
[GET] /api/v1/payment: Get a list of payment [authorize-admin]
[GET] /api/v1/payment/:id : Get a payment by id [authorize-admin]
[POST]  /api/v1/payment: Create a new payment [authorize] 
[PUT] /api/v1/payment/:id : Modify a payment [authorize]
[DELETE] /api/v1/payment/:id: Delete a payment [authorize]
```

Rate:
```
[GET] /api/v1/rates: Get a list of rates
[GET] /api/v1/rates/:id : Get a rate by id
[POST]  /api/v1/rates: Create a new rate
[PUT] /api/v1/ rates/:id : Modify a rate
[DELETE] /api/v1/rates/:id: Delete a rate
```

User_card:
```
[GET] /api/v1/user-card/:id : Get a card by id [authorize-own]
[POST]  /api/v1/user-card: Create a new card [authorize-own]
[PUT] /api/v1/user-card/:id : Modify a card [authorize-own]
[DELETE] /api/v1/user-car/:id: Delete a card [authorize-own]
```

User:
```
[GET] /api/v1/users: Get a list of users [authorize-admin]
[GET] /api/v1/users/:id : Get a user by id [authorize]
[POST]  /api/v1/users: Create a new user 
[PUT] /api/v1/users/:id : Modify a user [authorize]
[DELETE] /api/v1/users/:id: Delete a user [authorize]
```

Authentication:
```
[POST] /api/v1/signup: Sign up as a user , using email and password (required) [authorize]
[POST] /api/v1/signin: Sign in using email and password. The system will return JWT [authorize]
[PUT] /api/v1/changePassword: Change user’s password [authorize]
```

Review:
```
[GET] /api/v1/reviews: Get a list of reviews
[GET] /api/v1/reviews/:id : Get a review by id
[POST]  /api/v1/reviews: Create a new review [authorize]
[PUT] /api/v1/reviews/:id : Modify a review [authorize]
[DELETE] /api/v1/reviews/:id: Delete a review [authorize]
```

Shipment:
```
[GET] /api/v1/shipment: Get a list of shipment
[GET] /api/v1/shipment/:id : Get a shipment by id
[POST]  /api/v1/shipment: Create a new shipment [authorize-admin]
[PUT] /api/v1/shipment/:id : Modify a shipment [authorize-admin]
[DELETE] /api/v1/shipment/:id: Delete a shipment [authorize-admin]
```

## Getting Started
Clone the respository from github: git clone

<!-- This is the deploy link for the backend from Azure: [BackEnd Link](https://ecommerce-backend-fs15.azurewebsites.net) -->

<!-- Using this link plus the proper endpoint to retrieve the data. -->

<!-- This is the netlify deploy link for the frontend: [FrontEnd Link](https://anhnguyenecommerce.netlify.app/) -->

The front-end now just have some basic function: 
1. Show all products , and filter product by category and price
2. Show product detail
3. Add product to cart
4. Log in and Sign Up
5. Manage products ( For user role Admin) : Add, edit, delete

<!-- Note: If you navigate to the front-end link and see nothing (One tip: you can run one of the backend links first, such as [this](https://ecommerce-backend-fs15.azurewebsites.net/api/v1/products), and if it works, then run the frontend link again), that is because the server has an internal error. I encountered this problem, and after restarting the server, it worked normally. I think the reason for that is maybe because of sending many requests in a short time, so the server crashes. However, I will figure out how to solve this bug and add more functions for the frontend. By now, these are what I have. -->

## Testing

Unit testing, and optionally integration testing, must be included for both frontend and backend code. Aim for high test coverage and ensure all major functionalities are covered.