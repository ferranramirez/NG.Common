using System;

namespace NG.Common.Services.AuthorizationProvider
{
    public class AuthorizedUser
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool EmailConfirmed { get; set; }

        public AuthorizedUser() { }

        public AuthorizedUser(Guid userId, string email, string role, bool emailConfirmed)
        {
            UserId = userId;
            Email = email;
            Role = role;
            EmailConfirmed = emailConfirmed;
        }
        public override bool Equals(object obj)
        {
            return obj is AuthorizedUser authorizedUser
                && UserId.Equals(authorizedUser.UserId)
                && Email.Equals(authorizedUser.Email)
                && Role.Equals(authorizedUser.Role)
                && EmailConfirmed.Equals(authorizedUser.EmailConfirmed);
        }

        public override int GetHashCode()
        {
            return new { UserId, Email, Role, EmailConfirmed }.GetHashCode();
        }
    }
}
