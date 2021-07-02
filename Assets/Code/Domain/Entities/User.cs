namespace Domain.Entities
{
    public class User
    {
        public readonly bool IsInitialized;

        public User(bool isInitialized)
        {
            IsInitialized = isInitialized;
        }
    }
}
