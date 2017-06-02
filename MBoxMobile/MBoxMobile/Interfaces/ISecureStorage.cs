namespace MBoxMobile.Interfaces
{
    public interface ISecureStorage
    {
        string Get();
        void Save(string server, string username, string password);
        void Delete();
    }
}
