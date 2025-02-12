using MessagePack;

namespace Common
{
    public static class MessagePackHelper
    {
        public static readonly MessagePackSerializerOptions LZ4 = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);
    }
}
