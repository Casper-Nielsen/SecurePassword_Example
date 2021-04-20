namespace SecurePassword_Web_Example.Interfaces
{
    internal interface IHashing
    {
        byte[] ComputeMAC(byte[] message, byte[] salt);

        bool Validate(byte[] mac1, byte[] mac2);

        byte[] GenerateSalt(int lenght);
    }
}