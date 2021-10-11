namespace WotStatService.Models
{
    public class PrivateData
    {
        public string Status { get; set; }
        public string AccessToken { get; set; }
        public string Nickname { get; set; }
        public string AccountId { get; set; }
        public string ExpiresAt { get; set; }

        public PrivateData(PrivateData obj)
        {
            Status = obj.Status;
            AccessToken = obj.AccessToken;
            Nickname = obj.Nickname;
            AccountId = obj.AccountId;
            ExpiresAt = obj.ExpiresAt;
        }

        public PrivateData()
        {
        }
    }
}
