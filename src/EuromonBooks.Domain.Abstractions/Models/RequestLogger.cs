using Newtonsoft.Json;

namespace EuromonBooks.Domain.Abstractions.Models
{
    public class RequestLogger
    {
        private DateTime Timestamp;
        public DateTime StartTimestamp { get; set; } = DateTime.UtcNow;
        public DateTime EndTimestamp { get; set; }
        public string CorrelationId { get; set; }
        public string Endpoint { get; set; }

        public RequestLog LogStart()
        {
            Timestamp = StartTimestamp;

            var baseInfo = BaseLogInfo();
            baseInfo.Endpoint = Endpoint;

            return baseInfo;
        }

        public RequestLog LogStart(string requestData)
        {
            var start = LogStart();
            start.Request = requestData;

            return start;
        }

        public RequestLog LogStart<T>(T requestObject)
        {
            var start = LogStart();
            start.Request = JsonConvert.SerializeObject(requestObject);

            return start;
        }

        public ResponseLog LogSuccess()
        {
            return LogResponse("Success");
        }

        public ResponseLog LogSuccess(string responseData)
        {
            var success = LogSuccess();
            success.Response = responseData;

            return success;
        }

        public ResponseLog LogSuccess<T>(T responseObject)
        {
            var success = LogSuccess();
            success.Response = JsonConvert.SerializeObject(responseObject);

            return success;
        }

        public ResponseLog LogFailure()
        {
            return LogResponse("Failure");
        }

        public ResponseLog LogFailure(string responseData)
        {
            var failure = LogFailure();
            failure.Response = responseData;

            return failure;
        }

        public ResponseLog LogFailure<T>(T responseObject)
        {
            var failure = LogFailure();
            failure.Response = JsonConvert.SerializeObject(responseObject);

            return failure;
        }

        private RequestLog BaseLogInfo()
        {
            return new RequestLog
            {
                Timestamp = $"{Timestamp:yyyy-MM-ddTHH:mm:ss.ffff}",
                CorrelationId = CorrelationId
            };
        }

        private ResponseLog LogResponse(string successOrFailure)
        {
            if (EndTimestamp == DateTime.MinValue)
            {
                EndTimestamp = DateTime.UtcNow;
            }

            Timestamp = EndTimestamp;
            var baseInfo = BaseLogInfo();

            return new ResponseLog
            {
                Timestamp = baseInfo.Timestamp,
                CorrelationId = baseInfo.CorrelationId,
                Result = successOrFailure
            };
        }
    }
}