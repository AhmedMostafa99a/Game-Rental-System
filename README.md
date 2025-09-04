# 🎮 Games Rental System

A **Windows Forms** application built with **C#** and **SQL Server Database** to manage a **Game Rental System**.  
This project provides a user-friendly GUI for managing users, games, and rental operations while ensuring proper database integration.

---

## 📌 Project Overview

Game rental systems provide an easy and cost-effective way to try out new games without having to purchase every one.  
Each game is developed by a **vendor**, while an **admin** manages rentals and system operations.  

This project implements the following main features:
- 👤 **User Management**
  - Sign up (Admin / Client)
  - Update user details
- 🎮 **Game Management** (Admin only)
  - Add new games
  - Update existing games
- 🔍 **Game Browsing**
  - Search by year, vendor, category, etc.
  - View all available games
- 📦 **Rental Operations**
  - Rent a game
  - Return a game

---

## 🛠 Tech Stack

- **Language:** C#  
- **Framework:** Windows Forms (.NET Framework)  
- **Database:** SQL Server  
- **Architecture:** ERD → Physical Model (DDL scripts)  

---

## 📂 Project Requirements

1. **ERD Design**  
   - Create an **Entity Relationship Diagram (ERD)** for the system.  

2. **Database Schema**  
   - Convert ERD into a **physical model** (DDL scripts).  

3. **SQL Queries / Reports**  
   - Support the following **inquiries**:
     - 🏆 What was the most interesting game with the maximum number of renters (clients)?
     - 📅 What were the games that had **no renters** last month?
     - 👤 Who was the client with the **maximum rentals** last month?
     - 🕹 Which vendor had the **highest renting activity** last month?
     - 🚫 Which vendors had **no rentals** last month?
     - 📉 Which vendors didn’t add any new game last year?

---

## 📸 GUI Features (Windows Forms)

- **Login & Sign-Up Screens**  
- **Admin Dashboard** (add/update games, view reports)  
- **Client Dashboard** (browse, rent, return games)  
- **Search Filters** (by year, vendor, category)  
- **Rental History** tracking
- **generate report** 
