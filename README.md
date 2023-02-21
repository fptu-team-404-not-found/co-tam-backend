# CÔ TẤM - SWD392 
[![Co Tam Project | © 2022 by fptu-team-404-not-found](https://github.com/fptu-team-404-not-found/co-tam-backend/actions/workflows/main_cotam.yml/badge.svg?branch=main)](https://github.com/fptu-team-404-not-found/co-tam-backend/actions/workflows/main_cotam.yml)

:wave: Welcome to our Software Architecture and Design Project (SWD392) :wave:

## Table of Contents
- [Description](#description)
- [Preview Screenshot](#preview-screenshot)
- [Technology](#technology)
- [Functional requirements](#functional-requirements)
- [Useful Resources](#useful-resources)
- [Contributors](#contributors)
- [References](#references)
- [License & Copyright](#license--copyright)

## Description
- Cô Tấm is a platform that provides hourly domestic help services through a technology application
- This project contains a website for admin and manager, an android mobile application for customer, an android mobile application for houseworker
- This project started from 05-09-2022 to 12-11-2022

## Preview Screenshot

**1. Website for admin and manager**

**2. Android mobile application for customer**
<div align="center">
  <img src="https://raw.githubusercontent.com/fptu-team-404-not-found/co_tam_houseworker_mobile/main/document/imgs/customer_mobile_login.png" alt="Customer Mobile Login" width="25%"></img> &nbsp;&nbsp; <img src="https://raw.githubusercontent.com/fptu-team-404-not-found/co_tam_houseworker_mobile/main/document/imgs/customer_mobile_home.png" alt="Customer Mobile Home" width="25%"></img> &nbsp;&nbsp; <img src="https://raw.githubusercontent.com/fptu-team-404-not-found/co_tam_houseworker_mobile/main/document/imgs/customer_mobile_order_history.png" alt="Customer Mobile Order History" width="25%"></img> &nbsp;&nbsp; <img src="https://raw.githubusercontent.com/fptu-team-404-not-found/co_tam_houseworker_mobile/main/document/imgs/customer_mobile_order_rating.png" alt="Customer Mobile Order Rating" width="25%"></img>
</div>

**3. Android mobile application for houseworker**
<div align="center">
  <img src="https://raw.githubusercontent.com/fptu-team-404-not-found/co_tam_houseworker_mobile/main/document/imgs/houseworker_mobile_login.png" alt="Houseworker Mobile Login" width="25%"></img> &nbsp;&nbsp; <img src="https://raw.githubusercontent.com/fptu-team-404-not-found/co_tam_houseworker_mobile/main/document/imgs/houseworker_mobile_home.png" alt="Houseworker Mobile Home" width="25%"></img> &nbsp;&nbsp; <img src="https://raw.githubusercontent.com/fptu-team-404-not-found/co_tam_houseworker_mobile/main/document/imgs/houseworker_mobile_order_receiving.png" alt="Houseworker Mobile Order Receiving" width="25%"></img> 
</div>
  
## Technology
**1. Frontend**
  - HTML, CSS, JavaScript
  - React

**2. Backend**
  - C# Language
  - .NET Core - Entity Framework
  
**3. Mobile**
  - Flutter

**3. Database**
  - Microsoft SQL Server - a relational model database server produced by Microsoft
  - Azure Cloud Service

**4. Other Technologies**
- RESTful API
- Google Oauth2 for Authentication Login with Google
- Firebase for push notification
- Azure App Service for Continuous Deployment
- JSON Web Tokens for authentication
- Material Design for design UI

**5. Tool**
  - Visual Studio 2022
  - Visual Studio Code 
  - Android Studio
  - Figma
  - Swagger API Documentation
  - Postman
  - Draw.io for ERD Diagram
  - Microsoft SQL Server Management Studio 18

## Functional requirements
:point_right: [Check out here for more details](https://github.com/fptu-team-404-not-found/co_tam_houseworker_mobile/tree/main/document/document)

**1. Customer:**
- [ ] View personal information
- [ ] Make an order for cleaning service
- [ ] Track current cleaning schedule progress.
- [ ] View the history of used services.
- [ ] Create a list of Houseworker favorites - blocked.
- [ ] ...

**2. Houseworker:**
- [ ] Management of personal information.
- [ ] Receive - cancel the schedule.
- [ ] View their work schedule.
- [ ] Track current cleaning schedule progress.
- [ ] View the history of completed/canceled schedules.
- [ ] ...

**3. Manager:**
- [ ] Manage the status of employees.
- [ ] Manage Customers.
- [ ] ...

**4. Admin:**
- [ ] Provide the right to add - remove events - promotions.
- [ ] Change the listed price of the service.
- [ ] Temporarily lock - unlock features - services.
- [ ] Update information about Application - terms - introduction.
- [ ] ...

## Useful Resources

#| #| Name | Description
-| -| ---- | -----------
1| -| Main Project Folder | Main source code
-| 1.1| [Front-end](https://github.com/fptu-team-404-not-found/co-tam-frontend) | Front-end source code
-| 1.2| [Customer Android Mobile App](https://github.com/fptu-team-404-not-found/co_tam_customer_mobile) | Android Mobile for Customer source code
-| 1.3| [Houseworker Android Mobile App](https://github.com/fptu-team-404-not-found/co_tam_houseworker_mobile) | Android Mobile for Houseworker source code
-| 1.4| [Back-end](https://github.com/fptu-team-404-not-found/co-tam-backend) | Back-end source code
2| -| Database | Database Information
-| 2.1| [Database Script](https://github.com/fptu-team-404-not-found/co_tam_houseworker_mobile/blob/main/document/database/CoTamDB.sql) | SQL Scipt
-| 2.2| [Database Entity Relationship Diagram](https://raw.githubusercontent.com/fptu-team-404-not-found/co_tam_houseworker_mobile/main/document/imgs/ERD-PhysicalERD.png) | Database ERD
3| -| UI Design | UI design on Figma
-| 3.1| [Style guide](https://www.figma.com/file/tjpHV6LA8K1vjBITENDw8c/C%C3%B4-T%E1%BA%A5m?node-id=63%3A1057) | Style guide
-| 3.2| [Customer App](https://www.figma.com/file/tjpHV6LA8K1vjBITENDw8c/C%C3%B4-T%E1%BA%A5m) | Customer Android Mobile Application
-| 3.3| [Houseworker App](https://www.figma.com/file/tjpHV6LA8K1vjBITENDw8c/C%C3%B4-T%E1%BA%A5m?node-id=159%3A3099) | Houseworker Android Mobile Application
-| 3.4| [Admin Website](https://www.figma.com/file/tjpHV6LA8K1vjBITENDw8c/C%C3%B4-T%E1%BA%A5m?node-id=125%3A1026) | Admin Website
-| 3.5| [Manager Website](https://www.figma.com/file/tjpHV6LA8K1vjBITENDw8c/C%C3%B4-T%E1%BA%A5m?node-id=158%3A1380) | Manager Website
4| -| [Swagger API Document](https://cotam.azurewebsites.net/swagger/index.html) | Swagger API Document

## Contributors
**1. Mentors:**
- Lecturer - Mentor: Lam Huu Khanh Phuong

**2. Members:**
- [Nguyen Dao Duc Quan](https://github.com/dq-qiji) - SE151008 - **Leader | Business Analyst | Android Mobile Developer | Back-end Developer**
- [Huynh Le Thuy Tien](https://github.com/tienhuynh-tn) - SE151104 - **Android Mobile Developer | Back-end Developer | Database Designer**
- [Nguyen Lam Thuy Phuong](https://github.com/nguyenlamthuyphuong25) - 	SE150999 - **UI Designer | Front-end Developer**
- [Tran Thanh Dat](https://github.com/DatTranLK) - SE151444 - **Database Designer | Back-end Developer**
- [Tran Ngoc Thang](https://github.com/thangtn2101) - SE151478 - **Database Designer | Android Mobile Developer**

## References
- [Flutter documentation](https://docs.flutter.dev/)

## License & Copyright
&copy; 2022 fptu-team-404-not-found Licensed under the [GPL-3.0 LICENSE](https://github.com/fptu-team-404-not-found/co-tam-backend/blob/main/LICENSE).
