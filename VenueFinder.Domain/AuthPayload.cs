namespace VenueFinder.Domain
{
    public class AuthPayload
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public string TokenExpiry { get; set; }

        public AuthPayload(string username, string token, string tokenExpiry)
        {
            Username = username;
            Token = token;
            TokenExpiry = tokenExpiry;
        }
    }
}
