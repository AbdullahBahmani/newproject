import logo from './logo.svg'
import {
  AuthenticatedTemplate,
  UnauthenticatedTemplate,
} from '@azure/msal-react'
import './App.css'
import WeatherForecast from './components/weatherforecast'
import { MsalAuthenticationTemplate, useMsal } from "@azure/msal-react";
import { InteractionType } from "@azure/msal-browser";



function App() {
  return (
    <>
      <p>Anyone can see this paragraph.</p>
      <MsalAuthenticationTemplate interactionType={InteractionType.Popup}>
        <p>At least one account is signed in!</p>
        <WeatherForecast />
      </MsalAuthenticationTemplate>
      <UnauthenticatedTemplate>
        <p>No users are signed in!</p>
      </UnauthenticatedTemplate>
    </>
  )
  
}

export default App
