namespace MediatR.Features.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MimeTypeAttribute : Attribute
    {
        const string AcceptableChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                                       "abcdefghijklmnopqrstuvwxyz" +
                                       "0123456789-_.~";

        public string Type { get; }
        public string Tree { get; }
        public string Subtype { get; }
        public string Vendor { get; set; }
        public string Suffix { get; }
        public int Version { get; }

        public MimeTypeAttribute(
            string subtype,
            string vendor,
            string type = "application",
            string tree = "vnd",
            string suffix = "json",
            int version = 1)
        {
            ValidateAsHeaderFriendly(subtype, nameof(subtype));
            ValidateAsHeaderFriendly(vendor, nameof(vendor));
            ValidateAsHeaderFriendly(type, nameof(type));
            ValidateAsHeaderFriendly(tree, nameof(tree));
            ValidateAsHeaderFriendly(suffix, nameof(suffix));

            Subtype = subtype;
            Vendor = vendor;
            Type = type;
            Tree = tree;
            Suffix = suffix;
            Version = version;
        }

        public override string ToString()
        {
            return $"{Type}/{Tree}.{Vendor}.{Subtype}.v{Version}+{Suffix}";
        }

        static void ValidateAsHeaderFriendly(string value, string parameterName)
        {
            if (value.Except(AcceptableChars).Any())
            {
                throw new ArgumentException(
                    "Value is not an acceptable HTTP header value.",
                    parameterName);
            }
        }
    }
}
