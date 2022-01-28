import React from 'react'
import ReactDOM from 'react-dom'
import './index.css'
import App from './App'
import reportWebVitals from './reportWebVitals'

import { MsalProvider } from '@azure/msal-react'
import { Configuration, PublicClientApplication } from '@azure/msal-browser'
const configuration = {
  auth: {
    clientId: 'd34b312f-e473-479a-b3db-22d0107f9cbc',
    authority:
      'https://login.microsoftonline.com/cdece2d6-4fe7-4c9d-93b3-4e53c6261de2',
  },
}
const pca = new PublicClientApplication(configuration)

const AppProvider = () => (
  <MsalProvider instance={pca}>
    <App />
  </MsalProvider>
)

ReactDOM.render(<AppProvider />, document.getElementById('root'))

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals()
