using FluentAssertions;

namespace MediatR.Features.Attributes.UnitTests
{
    public class MimeTypeAttributeTests
    {
        [Test]
        public void can_create_attribute_with_defaults()
        {
            var subject = new MimeTypeAttribute("widget", "acme");
            subject.ToString().Should().Be("application/vnd.acme.widget.v1+json");
        }

        [Test]
        public void can_create_attribute_with_overridden_version()
        {
            var subject = new MimeTypeAttribute("widget", "acme", version: 2);
            subject.ToString().Should().Be("application/vnd.acme.widget.v2+json");
        }

        [Test]
        public void cannot_create_attribute_with_invalid_chars()
        {
            Action a = () => new MimeTypeAttribute("widget*", "acme");
            a.Should().Throw<ArgumentException>();
        }

        [Test]
        public void cannot_create_attribute_with_invalid_chars_2()
        {
            Action a = () => new MimeTypeAttribute("widget", "acme&");
            a.Should().Throw<ArgumentException>();
        }
    }
}