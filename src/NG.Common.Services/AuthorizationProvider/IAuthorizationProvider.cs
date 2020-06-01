namespace NG.Common.Services.AuthorizationProvider
{
    public interface IAuthorizationProvider
    {
        string GetToken(AuthorizedUser user);
    }
}
