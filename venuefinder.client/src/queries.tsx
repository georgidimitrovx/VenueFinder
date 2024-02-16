import { gql } from '@apollo/client';

export const GET_VENUE_CATEGORIES_QUERY = gql`
  query GetVenueCategories {
    allVenueCategories {
      name
    }
  }
`;

export const GET_CATEGORY_VENUES_QUERY = gql`
    query GetCategoryVenues($category: String!, $limit: String!, $offset: String!) {
        venuesByCategory(category: $category, limit: $limit, offset: $offset) {
            id
            name
            category
        }
    }
`;

export const GET_VENUE_QUERY = gql`
    query GetVenue($id: String!) {
        venue(id: $id) {
            id
            name
            category
            geolocationDegrees
        }
    }
`;