namespace backend.Business.src.Abstractions
{
    public interface IPasswordService
    {
        void HashPassword(string originalPassword, out string hashedPassword, out byte[] salt);
        bool VerifyPassword(string originalPassword, string hashedPassword, byte[] salt);
    }
}