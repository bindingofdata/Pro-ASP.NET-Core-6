namespace Platform.Services
{
    public interface ITimeStamper
    {
        string TimeStamp { get; }
    }

    public sealed class DefaultTimeStamper : ITimeStamper
    {
        public string TimeStamp => DateTime.Now.ToShortTimeString();
    }
}
