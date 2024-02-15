namespace VenueFinder.Domain
{
    public class AuthPayload
    {
        public string Username { get; set; }
        public string Token { get; set; }

        public AuthPayload(string username, string token)
        {
            Username = username;
            Token = token;
        }
    }
}
