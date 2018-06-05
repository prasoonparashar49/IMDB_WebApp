# IMDB_WebApp
This is made usign MVC and Entity Framework.
It is using Code First Approach to create the database, hence there is no need of any DB Script

Class IMDBInitializer.cs initializes Database with some seed values. 

In Web.Config, change connection string to the database that you want
  <connectionStrings>
    <add name="DefaultConnection" connectionString="Data Source=(localdb)\v11.0;AttachDbFilename=|DataDirectory|\aspnet-IMDB_WebApp-20180604073403.mdf;Initial Catalog=aspnet-IMDB_WebApp-20180604073403;Integrated Security=True" providerName="System.Data.SqlClient" />
  </connectionStrings>
  (localdb)\v11.0 above  is the database instance I am connected to. Just change it to the connection instance you want to connect to

The app Drop And Create database everytime you exectue it. So, make sure that you closes the connection to the database before executing the app. Otherwise, it may throw exception when you run app or try to create movie.

First create movie before browsing or editing it



#INSTRUCTIONS TO EXECUTE THE APP




#URL
1.url for creating movie
2.url for listing movies
3.url for editing movie
4.url for creating actor
5.url for creating producer

#Important Points
1.In Movie Creation page and Edit page, Ensure that you are selecting actors. I have not covered the case where user is not selecting any actor. It will lead to exception
2.