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
import { useMutation } from '@apollo/client';
import { SIGN_IN_MUTATION } from '../mutations';

function Copyright(props: any) {
    return (
        <Typography variant="body2" color="text.secondary" align="center" {...props}>
            Venue Finder
        </Typography>
    );
}

export default function SignInPage() {
    const [showPage, setShowPage] = useState(false);
    const [signIn, { error }] = useMutation(SIGN_IN_MUTATION);

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
            const { data } = await signIn({ variables: { username, password } });
            localStorage.setItem('username', data.signIn.username);
            localStorage.setItem('jwtToken', data.signIn.token);
            localStorage.setItem('jwtTokenExpiry', data.signIn.tokenExpiry);
            navigate('/');
        }
        catch (error) {
            console.error(error);
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
                <Avatar sx={{ m: 1, bgcolor: 'primary.main' }}>
                    <LockOutlinedIcon />
                </Avatar>
                <Typography component="h1" variant="h5">
                    Sign in
                </Typography>
                {error != undefined ? (
                    <Typography component="h1" variant="subtitle1" sx={{ color: "red", marginTop: 2 }}>
                        {error.message}
                    </Typography>)
                    : null}
                <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        id="username"
                        label="Username"
                        name="username"
                        autoComplete="username"
                        autoFocus
                    />
                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        name="password"
                        label="Password"
                        type="password"
                        id="password"
                        autoComplete="current-password"
                    />
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        sx={{ mt: 3, mb: 2 }}
                    >
                        Sign In
                    </Button>
                    <Grid container justifyContent="center">
                        <Grid item>
                            <Link href="./signUp" variant="body2">
                                {"Don't have an account? Sign Up"}
                            </Link>
                        </Grid>
                    </Grid>
                </Box>
            </Box>
            <Copyright sx={{ mt: 8, mb: 4 }} />
        </Container>
    ) : <></>;
}
