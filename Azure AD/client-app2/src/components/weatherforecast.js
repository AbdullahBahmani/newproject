import React, { useState, useEffect } from 'react'
import axios from 'axios'
import { loginRequest } from '../config/authConfig';
import * as msal from '@azure/msal-browser';
import { useMsal } from '@azure/msal-react';

const WeatherForecast = (props) => {
  const [weather, setWeather] = useState([])

  // const msalConfig = {
  //   auth :{
  //     clientId:'d34b312f-e473-479a-b3db-22d0107f9cbc',
  //     authority:'https://login.microsoftonline.com/cdece2d6-4fe7-4c9d-93b3-4e53c6261de2',
  //   }
  // };
  // const msalInstance = new msal.PublicClientApplication(msalConfig);
  
   const token=props.accessToken;
  useEffect(() => {
    (async () => {

      // const result = await msalInstance.acquireTokenSilent(loginRequest);
      //  const token = result.accessToken();


      // const tokenreqResponse = await axios.get('https://login.microsoftonline.com/cdece2d6-4fe7-4c9d-93b3-4e53c6261de2/oauth2/token', {
      //   headers: {
      //     'grant_type':'client_credentials',
      //     'client_id':'d34b312f-e473-479a-b3db-22d0107f9cbc',
      //     'client_secret':'bFs7Q~714xyZMXoWLuv3.Mq4dPcB8Z8O4-_~Y',
      //     'resource':'api://507cce57-62c8-4e89-9afc-3424a2b350ab'

      //   },
      // })
      // const token = await tokenreqResponse['access_token'];

      const response = await axios.get(
        'https://localhost:7015/WeatherForecast',
        {
          headers: {
            Authorization:
              `Bearer ${token}`,
          },
        },
      )
      if (response.status == 200) {
        const weatherForecasts = await response.data
        setWeather(weatherForecasts)
      } else {
        console.log('401 unathorized')
      }
    })()
  }, [])

  const hello = 'Hello'
  return (
    <div>
      {weather.map((item) => (
        <div key={item.date}>
          <div>Date: {item.date}</div>
          <div>TemperatureC: {item.temperatureC}</div>
          <div>TemperatureF: {item.temperatureF}</div>
          <div>Summary: {item.summary}</div>
        </div>
      ))}
    </div>
  )
}

export default WeatherForecast
