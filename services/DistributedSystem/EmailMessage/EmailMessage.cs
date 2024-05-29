namespace CommonServices
{
    public class EmailMessage
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> Attachments { get; set; } = new List<string>();
        public string CallbackQueue { get; set; } // Queue name for the callback/notification
    }
}
