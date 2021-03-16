namespace OrganizaDespensa.SharedKernel.Core.Authetications
{
    public class TokenService : ITokenService
    {
        public string RetrieveUserEmail(string token)
        {
            var dadosDoToken = token.Split(',');
            var dadosDoEmail = dadosDoToken[1].Substring(10, dadosDoToken[1].Length - 10);

            var email = dadosDoEmail.Replace("]", "").Replace("}", "").Replace("'", "").Replace("\"", string.Empty);
            return email;
        }
    }
}
