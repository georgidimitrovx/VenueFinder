import { Autocomplete, Box, Button, CircularProgress, Container, Grid, Paper, TextField, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from 'react-router-dom';
import { ApolloError, useLazyQuery } from "@apollo/client";
import { GET_CATEGORY_VENUES_QUERY } from "../queries";
import venueIcon from '../assets/venueIcon.png';

export default function CategoryPage() {
    const navigate = useNavigate();
    let { categoryName, categoryPageParam } = useParams();
    const [categoryPage, setCategoryPage] = useState(categoryPageParam);
    const [isLoading, setIsLoading] = useState(false);

    if (categoryPage == undefined)
        setCategoryPage("1");

    // States
    const [showPage, setShowPage] = useState(false);
    const [venues, setVenues] = useState([{ id: 1, name: "" }]);

    // GQL
    const [getCategoryVenues] = useLazyQuery(GET_CATEGORY_VENUES_QUERY);

    // Variables
    const jwtToken = localStorage.getItem('jwtToken');
    const username = localStorage.getItem('username');

    // Check if user already logged in
    useEffect(() => {
        if (jwtToken == undefined || jwtToken == null || jwtToken == "") {
            navigate('/signIn');
        } else {
            setShowPage(true);
            listCategoryVenues();
        }
    }, [categoryPage]);

    const onLogOut = () => {
        localStorage.setItem('username', "");
        localStorage.setItem('jwtToken', "");
        navigate('/signIn');
    };

    const listCategoryVenues = async () => {
        try {
            setIsLoading(true);

            const { data } = await getCategoryVenues({
                variables: {
                    category: categoryName,
                    limit: "3",
                    offset: String(3 * parseInt(categoryPage == undefined ? "1" : categoryPage))
                }
            });

            setIsLoading(false);

            if (data == undefined)
                return;

            //const testVenues = [
            //    { "id": 1, "name": "Cupcakes", "description": "Description of Product 1" },
            //    { "id": 2, "name": "Cocktails", "description": "Description of Product 2" },
            //    { "id": 3, "name": "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW", "description": "Description of Product 3" }
            //];

            setVenues(data.venuesByCategory);
        }
        catch (error) {
            setIsLoading(false);

            if (error instanceof ApolloError) {
                console.error('GraphQL errors:', error.graphQLErrors);
                console.error('Network error:', error.networkError);
            } else {
                console.error('Error:', error);
            }
        }
    };

    const onPreviousPage = () => {
        if (categoryPage == undefined)
            return;

        const newCategoryPage = parseInt(categoryPage) - 1;
        navigate(`/category/${categoryName}/${newCategoryPage}`);
        setCategoryPage(newCategoryPage.toString());
    }

    const onNextPage = () => {
        if (categoryPage == undefined)
            return;

        const newCategoryPage = parseInt(categoryPage) + 1;
        navigate(`/category/${categoryName}/${newCategoryPage}`);
        setCategoryPage(newCategoryPage.toString());
    }

    const goBackToCategories = () => {
        navigate(`/`);
    }

    const viewVenue = (id: number) => {
        navigate(`/venue/${id}`);
    }

    return showPage ? (
        <Container component="main" sx={{
            //maxWidth: {
            //    xs: "xs",
            //    sm: "xs",
            //    md: "sm",
            //    lg: "md",
            //    xl: "lg",
            //}
        }}>
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
                <Typography variant="h4" sx={{ marginX: 2, marginTop: 3}}>
                    Venues in category '{categoryName}'
                </Typography>
                {isLoading ? (<CircularProgress sx={{ marginTop: 3 }} />) : (
                    <Grid container spacing={2} sx={{
                        marginTop: 3, width: {
                            sx: "90%",
                            md: "70%",
                            //lg: "60%"
                        }
                    }}>
                        {venues.map((venue, index) => (
                            <Grid item key={index} xs={12}>
                                <Paper sx={{
                                    display: 'flex', padding: 2
                                }}>
                                    <Box sx={{
                                        display: 'flex',
                                        width: "100%",
                                        alignContent: 'left',
                                        justifyItems: 'left',
                                    }}>
                                        <img src={venueIcon} width="10%" />
                                        <Box sx={{
                                            display: 'flex',
                                            alignItems: 'center',
                                            marginLeft: 4,
                                            width: "75%"
                                        }}>
                                            <Typography variant="h6" sx={{
                                                whiteSpace: 'nowrap',
                                                overflow: 'hidden',
                                                textOverflow: 'ellipsis',
                                            }}>
                                                {venue.id}. {venue.name}
                                            </Typography>
                                        </Box>
                                        <Button onClick={() => viewVenue(venue.id)}>
                                            View
                                        </Button>
                                    </Box>
                                </Paper>
                            </Grid>
                        ))}
                    </Grid>
                )}
                <Box sx={{
                    display: 'flex',
                    alignItems: 'center',
                    marginTop: 3,

                }}>
                    {parseInt(categoryPage == undefined ? "1" : categoryPage) != 1 ? (
                        <Button onClick={onPreviousPage} variant="outlined">
                            {"<"}
                        </Button>
                    ) : null}
                    <Typography variant="body1" sx={{ marginX: 2 }}>
                        Page {categoryPage}
                    </Typography>
                    <Button onClick={onNextPage} variant="outlined">
                        {">"}
                    </Button>
                </Box>
                <Box sx={{
                    display: 'flex',
                    alignItems: 'center',
                    marginTop: 2,
                }}>
                    <Button onClick={goBackToCategories} variant="contained">
                        Back to categories
                    </Button>
                </Box>
            </Box>
        </Container>
    ) : <></>;
}
