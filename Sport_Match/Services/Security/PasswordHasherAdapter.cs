namespace Sport_Match.Services.Security
{
    public class PasswordHasherAdapter : IPasswordHasher
    {
        public (string Hash, string Salt) HashPassword(string password)
            => PasswordHasher.HashPassword(password);

        public bool VerifyPassword(string password, string storedHash, string storedSalt)
            => PasswordHasher.VerifyPassword(password, storedHash, storedSalt);
    }
}
