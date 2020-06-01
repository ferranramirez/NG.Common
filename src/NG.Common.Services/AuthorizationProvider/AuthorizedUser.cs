using System;

namespace NG.Common.Services.AuthorizationProvider
{
    public class AuthorizedUser
    {
        public Guid UserId { get; }
        public string Email { get; }
        public string Role { get; }

        public AuthorizedUser(Guid userId, string email, string role)
        {
            UserId = userId;
            Email = email;
            Role = role;
        }
        public override bool Equals(object obj)
        {
            return obj is AuthorizedUser authorizedUser
                && UserId.Equals(authorizedUser.UserId)
                && Email.Equals(authorizedUser.Email)
                && Role.Equals(authorizedUser.Role);
        }

        public override int GetHashCode()
        {
            return new { UserId, Email, Role }.GetHashCode();
        }
    }
}
