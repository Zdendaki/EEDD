using MessagePack;
using MessagePack.Resolvers;

namespace Common
{
    public static class MessagePackHelper
    {
        private static readonly IFormatterResolver resolver = CompositeResolver.Create(
            NativeGuidResolver.Instance,
            StandardResolver.Instance
        );

        public static readonly MessagePackSerializerOptions LZ4 = MessagePackSerializerOptions.Standard
            .WithCompression(MessagePackCompression.Lz4BlockArray)
            .WithResolver(resolver);
    }
}
