# Blazor WASM Example
Generic Template for .NET Core 3.1 WebApi with Entity Framework Core and Identity

## Make sure to update the following:
1. In *TokenService.cs* within the **BusinessLogic** project, make sure to change `_key`.
2. Choose your appropriate provider for Email and SMS in the *CommunicationService.cs* within the **BusinessLogic** project.
3. Update `appsettings.Development.json` within **Server** project with the appropriate SQL Server connection string.
