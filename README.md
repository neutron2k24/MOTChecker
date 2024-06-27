MOT Checker Sample.
NET 8 Blazer (Interactive Server) Web App

Description:
A very small and unrefined WebApp. This small Web App contains a micro service, (IMOTApiService), that interacts with a third party API to query MOT history from a third party API and deserialises the JSON result into POCO classes.
The information is then presented utilising Blazor components.

Dependency Injection is utilised to inject a SCOPED instance of MOTApiService into the a Blazor Component, (MotChecker.razor), which contains a simple textbox which accepts a string. Upon submission, the registration is passed to the IMOTApiService to query the third party API.

The API key for the DVLA API is specified in the appsettings.json file. At runtime the settings are read and loaded, (in program.cs), into the MOTApiServiceSettings static class, and then referenced directly in the MOTApiService.
