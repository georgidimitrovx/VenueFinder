import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import './index.css'
import { ThemeProvider, createTheme } from '@mui/material';
import { ApolloProvider } from '@apollo/client';
import apolloClient from './ApolloClient.tsx';

const darkTheme = createTheme({
    palette: {
        mode: 'dark', // Switches the theme to dark mode
    },
});

ReactDOM.createRoot(document.getElementById('root')!).render(
    <React.StrictMode>
        <ApolloProvider client={apolloClient}>
            <ThemeProvider theme={darkTheme}>
                <App />
            </ThemeProvider>
        </ApolloProvider>
    </React.StrictMode>,
)
