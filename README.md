
MatutesAuctionHouse, where you can participate in buying or selling items at auctions.

The app emulates an auction house and uses .NET and AngularJS. Sessions are secured and authenticated using JWT Bearer tokens, ensuring only authorized users can access the system. Live price updates are managed using SignalR-WebSockets, allowing for real-time auction prices without needing to refresh the page. The database is SQL Server, and the ORM is Entity Framework. Images are currently stored as bytes in the database, but this could be improved by using blob storage.