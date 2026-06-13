# Parking Lot Management System

## Overview

The Parking Lot Management System is a .NET-based application that manages vehicle parking operations within a parking facility. The system supports parking and unparking vehicles, parking spot allocation, ticket generation, and fee calculation based on parking duration.

This project demonstrates Low-Level Design (LLD) principles, object-oriented design, entity relationships, and RESTful API development using ASP.NET Core and Entity Framework Core.

---

## Features

* Vehicle Registration and Tracking
* Parking Spot Management
* Vehicle Parking and Unparking
* Ticket Generation
* Parking Fee Calculation
* Spot Occupancy Tracking
* Support for Multiple Vehicle Types
* RESTful APIs
* Entity Framework Core Integration

---

## Technology Stack

* ASP.NET Core Web API
* C#
* Entity Framework Core
* SQL Server
* Swagger / OpenAPI
* Dependency Injection
* LINQ

---

## System Design

### Entities

#### Vehicle

Represents a vehicle entering the parking lot.

Properties:

* Id
* LicensePlate
* Type (Bike, Car, Truck)
* LastParked

#### ParkingSpot

Represents an available parking slot.

Properties:

* Id
* SpotNumber
* SpotType
* IsOccupied
* IsActive

#### Ticket

Represents a parking transaction.

Properties:

* Id
* EntryTime
* ExitTime
* ParkingFee
* VehicleId
* ParkingSpotId

---

## Parking Rules

| Vehicle Type | Allowed Spot Type |
| ------------ | ----------------- |
| Bike         | Bike              |
| Car          | Car               |
| Truck        | Truck             |

A vehicle can only be parked in a compatible parking spot.

---

## API Endpoints

### Vehicle APIs

#### Get All Vehicles

```http
GET /api/vehicles
```

Response:

```json
[
  {
    "id": "guid",
    "licensePlate": "MH12AB1234",
    "type": "Car"
  }
]
```

---

### Parking Spot APIs

#### Get All Parking Spots

```http
GET /api/parkingspots
```

#### Get Available Spots

```http
GET /api/parkingspots/available
```

#### Create Parking Spot

```http
POST /api/parkingspots
```

Request:

```json
{
  "spotNumber": "A101",
  "spotType": "Car"
}
```

---

### Parking Operations

#### Park Vehicle

```http
POST /api/parking/park
```

Request:

```json
{
  "licensePlate": "MH12AB1234",
  "type": "Car",
  "spotId": "spot-guid"
}
```

Success Response:

```json
{
  "message": "Vehicle parked successfully"
}
```

---

#### Unpark Vehicle

```http
POST /api/parking/unpark/{ticketId}
```

Success Response:

```json
{
  "message": "Vehicle unparked successfully",
  "parkingFee": 50
}
```

---

### Ticket APIs

#### Get All Tickets

```http
GET /api/tickets
```

#### Get Ticket By Id

```http
GET /api/tickets/{id}
```

---

## Fee Calculation

Current pricing strategy:

| Duration        | Fee |
| --------------- | --- |
| First Hour      | ₹20 |
| Additional Hour | ₹10 |

Example:

* 2 Hours 30 Minutes = ₹30
* 5 Hours = ₹60

Fee calculation can be extended using Strategy Design Pattern.

---

## Project Structure

```text
ParkingLotManagement
│
├── Controllers
│   ├── ParkingController
│   ├── ParkingSpotController
│   └── TicketController
│
├── Services
│   ├── ParkingService
│   ├── ParkingSpotService
│   └── FeeCalculationService
│
├── Models
│   ├── Vehicle
│   ├── ParkingSpot
│   └── Ticket
│
├── Data
│   └── ApplicationDbContext
│
├── Migrations
│
└── Program.cs
```

---

## Design Principles Applied

* Single Responsibility Principle (SRP)
* Dependency Injection
* Separation of Concerns
* Repository-Friendly Structure
* Extensible Fee Calculation Logic
* Domain-Driven Entity Modeling

---

## Future Enhancements

* Multi-Level Parking Support
* Reservation System
* Dynamic Pricing
* QR Code Ticketing
* Payment Gateway Integration
* Vehicle Entry/Exit Logs
* Admin Dashboard
* Role-Based Authentication & Authorization
* Real-Time Spot Availability Monitoring

---

## Running the Project

### Clone Repository

```bash
git clone <repository-url>
```

### Update Database

```bash
dotnet ef database update
```

### Run Application

```bash
dotnet run
```

### Open Swagger

```text
https://localhost:<port>/swagger
```

---

## Sample Workflow

1. Create Parking Spots
2. Register/Park Vehicle
3. Generate Ticket
4. Track Occupancy
5. Unpark Vehicle
6. Calculate Fee
7. Release Parking Spot

---

## Author

Parth

Built as a Low-Level Design (LLD) implementation to demonstrate object-oriented design, REST API development, and parking management workflows using ASP.NET Core.
