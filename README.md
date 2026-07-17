# Hello World — ASP.NET Core (.NET 10)

A minimal ASP.NET Core web app targeting **.NET 10**, ready to deploy to
**Azure App Service (Web Apps)**.

## Endpoints

| Method | Path       | Response                                        |
| ------ | ---------- | ----------------------------------------------- |
| GET    | `/`        | A styled HTML "Hello, World!" landing page      |
| GET    | `/healthz` | `{ "status": "healthy" }`                       |

The landing page is a static file served from `wwwroot/index.html` (gradient
background, animated wave, and tech badges).

## Run locally

Requires the [.NET 10 SDK](https://dotnet.microsoft.com/download).

```bash
dotnet run
```

Then open the URL printed in the console (e.g. `http://localhost:5000`) or:

```bash
curl http://localhost:5000/
```

## Deploy to Azure Web Apps (manual)

The app uses the default ASP.NET Core hosting model, so it automatically binds to
the port Azure App Service provides — no extra configuration needed.

### Option A — `az webapp up` (simplest)

From the project directory:

```bash
az login

az webapp up \
  --name <your-unique-app-name> \
  --resource-group <your-resource-group> \
  --runtime "DOTNETCORE:10.0" \
  --sku B1 \
  --location <azure-region>
```

`az webapp up` builds, packages, and deploys the app, creating the resource group,
App Service plan, and Web App if they don't already exist.

> If `DOTNETCORE:10.0` isn't yet listed, check available stacks with
> `az webapp list-runtimes --os linux` and use the matching .NET 10 value.

### Option B — publish + zip deploy

```bash
dotnet publish -c Release -o ./publish
cd publish && zip -r ../app.zip . && cd ..

az webapp deploy \
  --name <your-unique-app-name> \
  --resource-group <your-resource-group> \
  --src-path app.zip \
  --type zip
```

After deployment, browse to `https://<your-unique-app-name>.azurewebsites.net/`.
