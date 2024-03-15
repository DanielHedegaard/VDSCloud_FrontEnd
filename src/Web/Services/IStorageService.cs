namespace Web.Services
{
    public interface IStorageService
    {
        string GetUsername();
        string GetToken();
        int GetUserId();
        void SetUsername(string username);
        void SetToken(string token);
        void SetUserId(int userId);
        void Clear();
    }
}
