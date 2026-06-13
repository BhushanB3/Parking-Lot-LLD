# Parking Lot Management System

## Overview

This project showcases the practical application of Low-Level Design (LLD) concepts through a Parking Lot Management System. It helps in understanding Object-Oriented Programming, SOLID Principles, API development with ASP.NET Core, Entity Framework Core relationships, database design, and scalable service-oriented architecture used in real-world applications.

The system manages parking spots, vehicle parking operations, ticket generation, occupancy tracking, and vehicle exit processing. The design focuses on clean object-oriented principles, separation of concerns, and extensibility.

---

## Features

* Add parking spots dynamically
* Remove parking spots
* Find available parking spots based on vehicle type
* Park vehicles in compatible spots
* Generate parking tickets
* Unpark vehicles
* Parking fee calculation
* Payment type support
* Spot occupancy management

---

## Technology Stack

* ASP.NET Core Web API
* C#
* Entity Framework Core
* SQL Server
* Swagger/OpenAPI

---

## Core Entities

### Vehicle

Represents a vehicle entering the parking lot.

| Property     | Description                 |
| ------------ | --------------------------- |
| Id           | Unique identifier           |
| LicensePlate | Vehicle registration number |
| Type         | Bike, Car, Truck            |
| LastParked   | Last parking timestamp      |

### ParkingSpot

Represents a parking slot.

| Property   | Description            |
| ---------- | ---------------------- |
| Id         | Unique identifier      |
| SpotNumber | Spot number            |
| SpotType   | Bike, Car, Truck       |
| IsOccupied | Occupancy status       |
| IsActive   | Active/Inactive status |

### Ticket

Represents a parking session.

| Property    | Description           |
| ----------- | --------------------- |
| Id          | Unique identifier     |
| EntryTime   | Vehicle entry time    |
| ExitTime    | Vehicle exit time     |
| ParkingFee  | Calculated fee        |
| Vehicle     | Associated vehicle    |
| ParkingSpot | Assigned parking spot |

---

## API Endpoints

### 1. Add Parking Spot

Creates a parking spot at a specified level.

```http
POST /api/parking/add-spot
```

Request:

```json
{
  "levelNumber": 1,
  "spotType": "Car"
}
```

Response:

```json
{
  "message": "Spot added successfully"
}
```

---

### 2. Remove Parking Spot

Removes an existing parking spot.

```http
DELETE /api/parking/remove-spot/{levelNumber}/{spotId}
```

Response:

```json
{
  "message": "Spot removed successfully"
}
```

---

### 3. Get Available Spots

Returns all available spots that can accommodate a given vehicle type.

```http
GET /api/parking/available-spots/{vehicleType}
```

Example:

```http
GET /api/parking/available-spots/Car
```

Response:

```json
[
  {
    "id": "guid",
    "spotType": "Car",
    "isOccupied": false
  }
]
```

---

### 4. Park Vehicle

Assigns a vehicle to a parking spot and generates a ticket.

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

Response:

```json
{
  "message": "Vehicle parked successfully"
}
```

---

### 5. Unpark Vehicle

Processes vehicle exit, calculates parking fee, updates ticket information, and releases the parking spot.

```http
POST /api/parking/unpark/{ticketId}
```

Request:

```json
{
  "paymentType": "UPI"
}
```

Response:

```json
{
  "message": "Vehicle unparked successfully"
}
```

---

## Parking Workflow

1. Add parking spots to a level.
2. Retrieve available spots based on vehicle type.
3. Select a spot.
4. Park vehicle.
5. Generate parking ticket.
6. Vehicle exits.
7. Fee is calculated.
8. Payment is processed.
9. Spot becomes available again.

---

## Design Principles

* SOLID Principles
* Separation of Concerns
* Service-Oriented Architecture
* Entity Framework Core Relationships
* Extensible Parking Fee Logic
* Clean Domain Modeling

---

## Future Enhancements

* Multi-level parking analytics
* Reservation system
* Dynamic pricing strategy
* QR-based ticket generation
* Authentication & Authorization
* Admin dashboard
* Real-time parking availability

---

## Run Locally

```bash
git clone <repository-url>
cd ParkingLotManagement
```

Apply migrations:

```bash
dotnet ef database update
```

Run the application:

```bash
dotnet run
```

Open Swagger:

```text
https://localhost:<port>/swagger
```

---

## Author

Bhushan Baviskar

ASP.NET Core Low-Level Design implementation demonstrating Parking Lot Management concepts, Entity Framework Core relationships, ticket management, and parking workflow automation.
