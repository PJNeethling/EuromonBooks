namespace EuromonBooks.Domain.Abstractions.Models
{
    public class RequestLog
    {
        public string Timestamp { get; set; }

        public string CorrelationId { get; set; }

        public string Endpoint { get; set; }

        public string Request { get; set; }
    }

    public class ResponseLog
    {
        public string Timestamp { get; set; }

        public string CorrelationId { get; set; }

        public string Response { get; set; }

        public string Result { get; set; }
    }
}