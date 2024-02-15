import { Autocomplete, Box, Button, Container, Grid, TextField, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { useNavigate } from 'react-router-dom';
import { getEndpoint } from "../Helpers";

export default function HomePage() {
    const [showPage, setShowPage] = useState(false);
    const [categoryOptions, setCategoryOptions] = useState(["Option"]);
    let navigate = useNavigate();
    const jwtToken = localStorage.getItem('jwtToken');
    const username = localStorage.getItem('username');

    // Check if user already logged in
    useEffect(() => {
        if (jwtToken == undefined || jwtToken == null || jwtToken == "") {
            navigate('/signIn');
        } else {
            setCategories();
            setShowPage(true);
        }
    }, []);

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
    };

    const onLogOut = () => {
        localStorage.setItem('username', "");
        localStorage.setItem('jwtToken', "");
        navigate('/signIn');
    };

    const setCategories = () => {
        fetch(getEndpoint() + 'api/VenueCategories', {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${jwtToken}`
            },
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then((data) => {
                const categories = data.map((v: { name: any; }) => v.name);
                setCategoryOptions(categories);
            })
            .catch(error => console.error('Error fetching data: ', error));
    };

    return showPage ? (
        <Container component="main" maxWidth="xs">
            <Box
                sx={{
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                }}
            >
                <Box sx={{
                    display: 'flex',
                    alignItems: 'center',
                }}>
                    <Typography>
                        Logged in as {username}
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </Typography>
                    <Button onClick={onLogOut}>
                        Log out
                    </Button>
                </Box>
                <Box component="form" noValidate onSubmit={handleSubmit} sx={{ mt: 3 }}>
                    <Grid container spacing={2}>
                        <Grid item xs={12}>
                            <Autocomplete
                                disablePortal
                                id="combo-box-demo"
                                options={categoryOptions}
                                sx={{ width: 300 }}
                                renderInput={(params) => <TextField {...params} label="Category" />}
                            />
                        </Grid>
                    </Grid>
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        sx={{ mt: 3, mb: 2 }}
                    >
                        Select category
                    </Button>
                </Box>
            </Box>
        </Container>
    ) : <></>;
}
