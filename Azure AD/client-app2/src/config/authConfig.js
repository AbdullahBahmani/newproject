export const msalConfig = {
    auth: {
      clientId: "507cce57-62c8-4e89-9afc-3424a2b350ab",
      authority: "https://login.microsoftonline.com/cdece2d6-4fe7-4c9d-93b3-4e53c6261de2", // This is a URL (e.g. https://login.microsoftonline.com/{your tenant ID})
      redirectUri: "http://localhost:3000",
    },
    cache: {
      cacheLocation: "sessionStorage", // This configures where your cache will be stored
      storeAuthStateInCookie: false, // Set this to "true" if you are having issues on IE11 or Edge
    }
  };
  
  // Add scopes here for ID token to be used at Microsoft identity platform endpoints.
  export const loginRequest = {
    scopes: ["api://507cce57-62c8-4e89-9afc-3424a2b350ab/admin-fullcontrol"],
  //  roles:[ "user:read","admin:fullcontrol"]
   
  };
  
  // Add the endpoints here for Microsoft Graph API services you'd like to use.
  export const graphConfig = {
      graphMeEndpoint: "https://localhost:7015/WeatherForecast"
  };


  export const apiConfig = {
    resourceUri: "https://localhost:7015/WeatherForecast",
    resourceScopes: ["api://507cce57-62c8-4e89-9afc-3424a2b350ab/admin-fullcontrol"]
}