import { Autocomplete, Box, Button, CircularProgress, Container, Grid, TextField, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { useNavigate } from 'react-router-dom';
import { ApolloError, useLazyQuery } from "@apollo/client";
import { GET_VENUE_CATEGORIES_QUERY } from "../queries";

export default function HomePage() {
    const navigate = useNavigate();

    // States
    const [categoryOptions, setCategoryOptions] = useState(["Option"]);
    const [selectedCategory, setSelectedCategory] = useState("");
    const [isLoading, setIsLoading] = useState(true);

    // GQL
    const [getVenueCategories] = useLazyQuery(GET_VENUE_CATEGORIES_QUERY);

    // Variables
    const username = localStorage.getItem('username');

    // Check if user already logged in
    useEffect(() => {
        const jwtToken = localStorage.getItem('jwtToken');
        const jwtTokenExpiry = localStorage.getItem('jwtTokenExpiry');

        if (jwtToken == undefined || jwtToken == null || jwtToken == "" ||
            jwtTokenExpiry == undefined || jwtTokenExpiry == null || jwtTokenExpiry == "") {
            navigate('/signIn');
            return;
        }

        const dateNow = new Date();
        if (parseInt(jwtTokenExpiry) < dateNow.getTime()) {
            onLogOut();
            return;
        }

        setCategories();
    }, []);

    const onLogOut = () => {
        localStorage.setItem('username', "");
        localStorage.setItem('jwtToken', "");
        localStorage.setItem('jwtTokenExpiry', "");
        navigate('/signIn');
    };

    const setCategories = async () => {
        try {
            const { data } = await getVenueCategories();

            if (data == undefined)
                return;

            const categories = data.allVenueCategories.map((v: { name: any; }) => v.name);
            setCategoryOptions(categories);
            setIsLoading(false);
        }
        catch (error) {
            if (error instanceof ApolloError) {
                console.error('GraphQL errors:', error.graphQLErrors);
                console.error('Network error:', error.networkError);
            } else {
                console.error('Error:', error);
            }
        }
    };

    const onSelectButtonClick = () => {
        if (selectedCategory == null || selectedCategory == "")
            return;

        navigate(`/category/${selectedCategory}`);
    };

    return isLoading ? (
        <Container component="main" maxWidth="xs">
            <Typography>
                Loading Venue Finder. Please wait.
            </Typography>
            <CircularProgress sx={{ marginTop: 3 }} />
        </Container>
    ) : (
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
                <Typography variant="h5" sx={{ marginX: 2, marginTop: 3 }}>
                    Categories
                </Typography>
                <Box sx={{ mt: 3 }}>
                    <Grid container spacing={2}>
                        <Grid item xs={12}>
                            <Autocomplete
                                disablePortal
                                id="combo-box-demo"
                                options={categoryOptions}
                                sx={{ width: 300 }}
                                renderInput={(params) => <TextField {...params} label="Category" />}
                                onChange={(_event: any, newValue: string | null) => {
                                    setSelectedCategory(newValue!);
                                }}
                            />
                        </Grid>
                    </Grid>
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        sx={{ mt: 3, mb: 2 }}
                        onClick={onSelectButtonClick}
                    >
                        Select category
                    </Button>
                </Box>
            </Box>
        </Container>
    );
}
