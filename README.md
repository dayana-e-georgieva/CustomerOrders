Overview

This repository contains a solution structured into three main layers/projects:
1. CustomerOrders.API

A Web API exposing the following endpoints:

    GET /customers — Returns all customers

    GET /customer/{id} — Returns a customer by ID

    GET /customer/{id}/orders — Returns all orders made by a specific customer

2. CustomerOrders.DataAccess

Handles data access using Entity Framework Core and connects to the Microsoft Northwind sample database.
3. CustomerOrders.Web

An ASP.NET MVC web application that displays:

    /Index — A list of all customers with the total number of orders placed

    /Details — Detailed customer view, showing:

        Order ID

        Grand Total

        Total quantity ordered

        Order Notes — displays a warning if a product is low in stock or discontinued

✅ Testing

Each project has a dedicated xUnit test project:

    CustomerOrders.API.Tests

    CustomerOrders.DataAccess.Tests

    CustomerOrders.Web.Tests
