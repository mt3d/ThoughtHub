# A medium.com clone, built using C# and ASP.NET Core.

**ThoughtHub** is a demo project inspired by Medium.com.

**Disclaimer:** This project is created **for educational and learning purposes only**.

## Organization
- Backend (ASP.NET Core API)
- Frontend (Standalone Blazor WebAssembly app)

## Building & Running

1. Clone this repository or download a ZIP archive of it.

2. The default URLs for the frontend and the backend are:

    * `backend` app (`PlatformUrls:BackendUrl`): `http://localhost:5120`
    * `frontend_blazorwasm` app (`PlatformUrls:BlazorWasmFrontendUrl`): `http://localhost:5220`

3. You can use the existing URLs or update them in the appsettings.json file of each project with new `BackendUrl` and `BlazorWasmFrontendUrl` endpoints:

    * `appsettings.json` file in the root of the `backend` app.
    * `wwwroot/appsettings.json` file in the `frontend_blazorwasm` app.

4. Run the `backend` and `frontend_blazorwasm` apps.

5. Navigate to the `frontend_blazorwasm` app at the `BlazorWasmFrontendUrl`.

6. Register a new user or use one of the preregistered test users.