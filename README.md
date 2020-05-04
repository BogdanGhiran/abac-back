## Development server

Open the sln filein visual studio

Right click it and set up multiple startup projects, starting XPAND.Captains.API and XPAND.Planets.API

Run the project(F5)

By default, the ports should be as follows:

XPAND.Captains.API should run on http://localhost:56520

XPAND.Planets.API should run on http://localhost:56521


These are the urls used by the angular app so it should work without any further modifications provided both the client and backend are started


Notes: 
1.The Captains microservice calls the Planets microservice to replicate some meta data to be used for display. 
If you decide to change the ports that the APIs run on, you should change the value in appsettings.Development.json, as well as the angular appsettings file

2.The database runs on an Azure server that has been temporarily made public. Details about it have been provided via email

3.You won't be able to see anything unless logged in. 
The register functionality works as intended, but a basic account that  you can use to test is
user:mcKay 
pass:stargate
