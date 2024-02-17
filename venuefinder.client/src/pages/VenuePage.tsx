import { Box, Button, CircularProgress, Container, Grid, Paper, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from 'react-router-dom';
import { ApolloError, useLazyQuery } from "@apollo/client";
import { GET_VENUE_QUERY } from "../queries";
import venueIcon from '../assets/venueIcon.png';

export default function VenuePage() {
    const navigate = useNavigate();
    let { venueId } = useParams();
    const [isLoading, setIsLoading] = useState(false);

    // States
    const [showPage, setShowPage] = useState(false);
    const [venue, setVenue] = useState({ id: 1, name: "", geolocationDegrees: "" });

    // GQL
    const [getVenueQuery] = useLazyQuery(GET_VENUE_QUERY);

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

        setShowPage(true);
        loadVenue();
    }, []);

    const onLogOut = () => {
        localStorage.setItem('username', "");
        localStorage.setItem('jwtToken', "");
        navigate('/signIn');
    };

    const loadVenue = async () => {
        try {
            setIsLoading(true);

            const { data } = await getVenueQuery({
                variables: {
                    id: venueId,
                }
            });

            setIsLoading(false);

            if (data == undefined)
                return;

            setVenue(data.venue);
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

    const goBackToVenues = () => {
        navigate(-1);
    }

    return showPage ? (
        <Container component="main">
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
                    Venue details
                </Typography>
                {isLoading ? (<CircularProgress sx={{ marginTop: 3 }} />) : (
                    <Grid container spacing={2} sx={{
                        marginTop: 3, width: {
                            //sx: "90%",
                            //md: "70%",
                            //lg: "60%"
                        }
                    }}>
                        <Grid item xs={12}>
                            <Paper sx={{
                                display: 'flex',
                                padding: 2,
                                alignItems: 'center',
                                justifyContent: 'center'
                            }}>
                                <Box>
                                    <img src={venueIcon} width="64" />
                                </Box>
                                <Box sx={{
                                    marginLeft: 4,
                                }}>
                                    <Typography variant="h6" sx={{
                                        whiteSpace: 'nowrap',
                                        overflow: 'hidden',
                                        textOverflow: 'ellipsis',
                                        width: "100%"
                                    }}>
                                        {venue.name}
                                    </Typography>
                                    <Typography variant="body1" sx={{
                                        whiteSpace: 'nowrap',
                                        overflow: 'hidden',
                                        textOverflow: 'ellipsis',
                                        width: "100%"
                                    }}>
                                        [{venue.geolocationDegrees}]
                                    </Typography>
                                </Box>
                            </Paper>
                        </Grid>
                    </Grid>
                )}
                <Box sx={{
                    display: 'flex',
                    alignItems: 'center',
                    marginTop: 5,
                }}>
                    <Button onClick={goBackToVenues} variant="contained">
                        Back to category venues
                    </Button>
                </Box>
            </Box>
        </Container>
    ) : <></>;
}
