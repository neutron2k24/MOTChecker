MOT Checker Sample.
NET 8 Blazer (Interactive Server) Web App

Description:
A very small and unrefined WebApp. 
This small Web App demonstrates creation of a API wrapper service, (IMOTApiService), that interacts with a third party API to query MOT history, and deserialises the JSON result into POCO classes.
The information is then presented utilising Blazor components.

Dependency Injection is utilised to inject a scoped instance of the MOTApiService into a Blazor Component, (MotChecker.razor), which contains a simple textbox which accepts a string. Upon submission, the registration is passed Asynchronously to the MOTApiService to query the third party API. Results are then passed to a second Blazor component, (VehicleDetails.razor), which checks for a valid result and recent MOT, and displays the relevvant basic information.

The API key for the DVLA API is specified in the appsettings.json file. At runtime the settings are read and loaded, (in program.cs), into the MOTApiServiceSettings static class, and then referenced directly in the MOTApiService.
