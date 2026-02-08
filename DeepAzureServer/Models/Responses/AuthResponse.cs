namespace DeepAzureServer.Models.Responses
{
    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}
