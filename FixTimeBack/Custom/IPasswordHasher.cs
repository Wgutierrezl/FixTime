namespace FixTimeBack.Custom
{
    public interface IPasswordHasher
    {
        string Encryptsha256(string text);
    }
}
