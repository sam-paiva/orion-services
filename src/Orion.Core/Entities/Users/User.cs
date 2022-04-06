using Orion.Core.Base;
using Orion.Core.Entities.Properties;

namespace Orion.Core.Entities.Users
{
    public class User : Entity
    {
        public User(string firstName, string lastName, string email, string passwordHash)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            IsActive = true;
            EmailIsVerified = false;
            TokenHash = Guid.NewGuid();
            Properties = new List<Immobile>();
        }

        public string? PhoneContact { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public bool IsActive { get; private set; }
        public bool EmailIsVerified { get; private set; }
        public Guid TokenHash { get; private set; }
        public string? ProfileImageUrl { get; private set; }
        public virtual ICollection<Immobile> Properties { get; private set; }
        public string FullName { get => $"{FirstName} {LastName}"; }

        public void AddImmobile(Immobile immobile) => Properties.Add(immobile);
        public void VerifyEmail() => EmailIsVerified = true;
    }
}
