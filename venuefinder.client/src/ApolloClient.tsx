import { ApolloClient, InMemoryCache, createHttpLink } from '@apollo/client';
import { getEndpoint } from './Helpers';

const httpLink = createHttpLink({
    uri: getEndpoint() + 'graphql',
});

const apolloClient = new ApolloClient({
    link: httpLink,
    cache: new InMemoryCache(),
});

export default apolloClient;