_MatutesAuctionHouse_, where you can participate in buying or selling items at auctions.
---------------------------------------------

The app emulates an auction house and uses .NET in backend and AngularJS in front.  
Sessions are secured and authenticated using JWT Bearer tokens, ensuring only authorized users can access the system.  
Live price updates are managed using SignalR-WebSockets, allowing for real-time auction prices without needing to refresh the page.  
The database is SQL Server, and the ORM is Entity Framework. Images are currently stored as bytes in the database, but this could be improved by using blob storage.

---------------------------------------------
Database Table Diagram:

![image](https://github.com/user-attachments/assets/5448ea72-f540-47c8-a4fa-53beee1d7ca3).

Clarifications:  
-Item has the owner ID in 'user_id'.  
-Auction corresponds to an single item.  
-ItemPrice contains the last and highest price, as well as the 'user_id' of the user who made the bid.
  
---------------------------------------------
How to install:  
1st. Clone the repo in a folder:   
&emsp;&emsp;git clone https://github.com/An7u4n/MatutesAuctionHouse.git  
&emsp;&emsp;cd ./MatutesAuctionHouse  
2nd. Install Dependencies:  
&emsp;&emsp;npm install  
&emsp;&emsp;dotnet restore  
3rd. Execute the .Net App:  
&emsp;&emsp;dotnet run  
4th. Execute the Angular App in other cmd:  
&emsp;&emsp;cd ./ClientApp  
&emsp;&emsp;ng serve  
5th. Now you can use the app on http://localhost:4200  
