using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Authorization.Domain
{
    public class AppUser
    {
        public int Id { get; init; }
        public string Login { get; private set; } = default!;
        public void UpdateLogin(string login)
        {
            if (string.IsNullOrEmpty(login))
            {
                throw new ArgumentException("Login is empty", nameof(login));
            }
            else if (login.Length > 50 || login.Length < 2)
            {
                throw new ArgumentException("Login length must be from 2 to 50 symbols", nameof(login));
            }
            else
            {
                Login = login;
            }
        }
        public string PasswordHash { get; private set; } = default!;
        public void UpdatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Login is empty", nameof(password));
            }
            else
            {
                PasswordHash = password;
            }
        }
        public string Email { get; private set; } = default!;
        public void UpdateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email is empty", nameof(email));
            }
            else if (!(new EmailAddressAttribute().IsValid(email)))
            {
                throw new ArgumentException("Email has incorrect format", nameof(email));
            }
            else
            {
                Email = email;
            }
        }
        public string Phone { get; private set; } = default!;
        public void UpdatePhone(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                throw new ArgumentException("Phone number is empty", nameof(phone));
            }
            else if (Regex.Match(phone, @"^(\+[0-9]{11})$").Success)
            {
                throw new ArgumentException("Phone number has incorrect format", nameof(phone));
            }
            else
            {
                Phone = phone;
            }
        }
        public virtual IEnumerable<AppUserAppRole> AppUserAppRoles { get; set; }

        public AppUser() { }
    }
}
