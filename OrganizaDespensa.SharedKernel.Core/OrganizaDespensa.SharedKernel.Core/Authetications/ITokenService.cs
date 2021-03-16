namespace OrganizaDespensa.SharedKernel.Core.Authetications
{
    public interface ITokenService
    {
        public string RetrieveUserEmail(string token);
    }
}
