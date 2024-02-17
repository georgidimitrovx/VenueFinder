import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Link from '@mui/material/Link';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { ApolloError, useMutation } from '@apollo/client';
import { SIGN_UP_MUTATION } from '../mutations';

function Copyright(props: any) {
    return (
        <Typography variant="body2" color="text.secondary" align="center" {...props}>
            Venue Finder
        </Typography>
    );
}

export default function SignUpPage() {
    const [showPage, setShowPage] = useState(false);
    const [signUp, { error }] = useMutation(SIGN_UP_MUTATION);
    let navigate = useNavigate();

    // Check if user already logged in
    useEffect(() => {
        const token = localStorage.getItem('jwtToken');
        const jwtTokenExpiry = localStorage.getItem('jwtTokenExpiry');

        if (token != null && token != undefined && token != "" &&
            jwtTokenExpiry != null && jwtTokenExpiry != undefined && jwtTokenExpiry != "") {
            navigate('/');
            return;
        }

        setShowPage(true);
    }, []);

    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        const formData = new FormData(event.currentTarget);
        const username = formData.get('username');
        const password = formData.get('password');

        try {
            const { data } = await signUp({ variables: { username, password } });
            localStorage.setItem('username', data.signUp.username);
            localStorage.setItem('jwtToken', data.signUp.token);
            localStorage.setItem('jwtTokenExpiry', data.signUp.tokenExpiry);
            navigate('/');
        }
        catch (error) {
            if (error instanceof ApolloError) {
                console.error('GraphQL errors:', error.graphQLErrors);
                console.error('Network error:', error.networkError);
            } else {
                console.error('SignUp error:', error);
            }
        }
    };

    return showPage ? (
        <Container component="main" maxWidth="xs">
            <CssBaseline />
            <Box
                sx={{
                    marginTop: 8,
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                }}
            >
                <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
                    <LockOutlinedIcon />
                </Avatar>
                <Typography component="h1" variant="h5">
                    Sign up
                </Typography>
                {error != undefined ? (
                    <Typography component="h1" variant="subtitle1" sx={{ color: "red", marginTop: 2 }}>
                        {error.message}
                    </Typography>)
                    : null}
                <Box component="form" noValidate onSubmit={handleSubmit} sx={{ mt: 3 }}>
                    <Grid container spacing={2}>
                        <Grid item xs={12}>
                            <TextField
                                required
                                fullWidth
                                id="username"
                                label="Username"
                                name="username"
                                autoComplete="username"
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                required
                                fullWidth
                                name="password"
                                label="Password"
                                type="password"
                                id="password"
                                autoComplete="new-password"
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                required
                                fullWidth
                                name="password2"
                                label="Repeat Password"
                                type="password"
                                id="password2"
                                autoComplete="new-password"
                            />
                        </Grid>
                    </Grid>
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        sx={{ mt: 3, mb: 2 }}
                    >
                        Sign Up
                    </Button>
                    <Grid container justifyContent="center">
                        <Grid item>
                            <Link href="./signIn" variant="body2">
                                Already have an account? Sign in
                            </Link>
                        </Grid>
                    </Grid>
                </Box>
            </Box>
            <Copyright sx={{ mt: 5 }} />
        </Container>
    ) : <></>;
}