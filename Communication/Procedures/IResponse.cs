using Communication.Data;

namespace Communication.Procedures
{
    public interface IResponse
    {
        public byte[] RequestGUID { get; set; }

        public ResponseState ResponseState { get; set; }
    }
}
