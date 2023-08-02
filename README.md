It`s my personal project for SoftUni ASP.NET Advance course August 2023
Car Repair Garage application is for users who want to shedule a Repair Services for their cars. Also, it provides an opportunity to Garages to register their Services and manage the appointments.

Usings
-----------------------------------------
 - ASP.NET 6
 - Entity Framework 6.0.18
 - Microsoft SQL
 - Identity.EntityFramework core 6.0.18
 - Newtonsoft JSON 13.0.3
 - NUnit 3.13.3

Implemented with
----------------------------------------
 - MVC Areas - Implemented Manager Area
 - Repository Pattern
 - Dependency Injection
 - Razor pages
 - Partial views
 - Components
 - jQuery

How to start it
---------------------------------------
The connection string for DB is located in appsettings.Development.json. ASPNETCORE-ENVIRONMENT is set to Development. 
Please replace the server string with yours. 
You should aplly the migrations by yourself to create the DB. There is no a setting for auto migration!

Once started the application will populate the DB with test data automatically using Seeder implementation. 
You can use the following test account:
 - User
   - Username: user@mail.com
   - Pass: 123456
 - Manager
   - Username: garageManager@mail.com
   - Pass 123456

Screenshots
------------------------------------
Homepage without user
![image](https://github.com/kstefanov4/CarRepairGarageSystem/assets/103167025/36623a9f-e0e2-4b52-ac7b-04b1870bb66f)

Garages - you will be able to Sorting, Filtering and Paging all of the registered garages
![image](https://github.com/kstefanov4/CarRepairGarageSystem/assets/103167025/150721aa-c857-4969-88e0-af1acb70b7f6)

Garage Details
![image](https://github.com/kstefanov4/CarRepairGarageSystem/assets/103167025/3b7bdc25-e739-43d2-8e14-55d79c70b278)

Create appointment
![image](https://github.com/kstefanov4/CarRepairGarageSystem/assets/103167025/1f2c98c8-4eb6-4be3-90f8-0e7e2765a14f)

The logged in User has Dropdown menu with My Cars, Add Car, My Appointments
![image](https://github.com/kstefanov4/CarRepairGarageSystem/assets/103167025/a6c2363d-63bb-479f-85c0-adfdb3bdf66c)

My Appointments
![image](https://github.com/kstefanov4/CarRepairGarageSystem/assets/103167025/0f4a4acd-74d8-4741-b264-640fb311dca4)

My Cars
![image](https://github.com/kstefanov4/CarRepairGarageSystem/assets/103167025/ad601683-fbd8-4a24-94b8-3a397d6cc6b9)


Manager User can manage garages
![image](https://github.com/kstefanov4/CarRepairGarageSystem/assets/103167025/1e688788-c254-4a6a-a374-35bd834a1da5)

Manager can add garage
![image](https://github.com/kstefanov4/CarRepairGarageSystem/assets/103167025/f57b2603-ae8c-4b0d-bc49-fb1355a64311)

The Managar has all appointments dashboard
![image](https://github.com/kstefanov4/CarRepairGarageSystem/assets/103167025/816753c0-acc1-452f-a773-d862d377b359)

Acknowledgments
------------------------------
 - part of the MVC Template of Nikolay Kostov
 - Stamo Petkov`s Lectures
 - Part of the code is copyed from Kristiyan Ivanov Workshops.
 - Some help from ChatGPT






-------------------------------------
