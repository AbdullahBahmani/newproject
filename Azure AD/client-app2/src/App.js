import {
  AuthenticatedTemplate,
  UnauthenticatedTemplate,
  useMsal,
} from '@azure/msal-react'
import './App.css'
import WeatherForecast from './components/weatherforecast'

import React from 'react'
import { PageLayout } from './Layout/PageLayout'
import { useState } from 'react'
import { loginRequest } from './config/authConfig'
import Button from 'react-bootstrap/Button'

function App() {
  const { instance, accounts, inProgress } = useMsal()
  const [accessToken, setAccessToken] = useState(null)

  const name = accounts[0] && accounts[0].name

  function RequestAccessToken() {
    const request = {
      ...loginRequest,
      account: accounts[0],
    }

    // Silently acquires an access token which is then attached to a request for Microsoft Graph data
    instance
      .acquireTokenSilent(request)
      .then((response) => {
        setAccessToken(response.accessToken)
        localStorage.setItem('token', response.accessToken);
      })
      .catch((e) => {
        instance.acquireTokenPopup(request).then((response) => {
          setAccessToken(response.accessToken);
          
        })
      })
    }
    return (
      <>
        <PageLayout>
          <AuthenticatedTemplate>
            <WeatherForecast token={accessToken} />
            <div>
              <h5 className="card-title">Welcome {name}</h5>
              {accessToken ? (
                <div>
                  <p>Access Token Acquired!</p>
                  <p>{accessToken}</p>
                </div>
              ) : (
                <Button variant="secondary" onClick={RequestAccessToken}>
                  Request Access Token
                </Button>
              )}
            </div>
          </AuthenticatedTemplate>
          <UnauthenticatedTemplate>
            <p>You are not signed in! Please sign in.</p>
          </UnauthenticatedTemplate>
        </PageLayout>
      </>
    )
  }


export default App;
