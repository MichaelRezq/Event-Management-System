# EMS
Event Management System (EMS) Web Application
This is a readme file for the EMS (Event Management System) project. The web application is designed to manage events, including times, locations, and topics/sectors, where presenters meet with investors to present their companies. The system ensures that meetings are scheduled only if both the presenter and investor are available at the chosen time slot, share the same topics, and if a suitable room is available. All meetings have a duration of one hour.

Installation
To install and run the EMS web application, follow these steps:

Clone the repository from GitHub: [https://github.com/MichaelRezq/Event-Management-System/edit/NewStyling]
Install [Asp.Net Core MVC 6]
Set up the database by 
Configure the application settings, including the database connection string, in the appsettings.json file.
Build and run the application.
Usage
The EMS web application consists of several web pages and an SSIS package. Below is a description of each page and its functionality:

Hotel Information Page: This page allows users to add hotel information, including the hotel name and conference rooms with their corresponding time slots. Users can define multiple time slots for each conference room.

Investor Information Page: On this page, users can add investor information, including name, mobile number, and the sectors they are interested in. Users can specify the preferred meeting times for each sector.

Presenter Information Page: Users can enter presenter information, including name, mobile number, and the sectors they are interested in. Similar to investors, presenters can specify their preferred meeting times for each sector.

Reservation Page: This page enables users to reserve a presenter for an investor. The system matches the investor's interests and availability with a suitable presenter. Once a match is found, available rooms for the selected time and hotel are displayed. Users can confirm the reservation, and the system will mark the room, investor, and presenter as occupied to prevent double booking during the reserved time.

SSIS Package: The SSIS package is responsible for populating the hotel table in the database. It reads hotel information from CSV files and adds it to the table. Each CSV file contains a single line of hotel information.

Technologies Used
The EMS web application is built using the following technologies:

.NET Core MVC The application framework.
MS SQL Server: The relational database management system.

Contact Information
If you have any questions or need further assistance, please contact Michael at michaelrezq9@gmail.com.

Thank you for using the EMS web application!





