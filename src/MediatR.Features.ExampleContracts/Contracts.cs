using System;
using MediatR.Features.Attributes;

namespace MediatR.Features.ExampleContracts
{
    public interface IAnimal
    {
        public string Name { get; set; }
        public int Legs { get; set; }
    }

    [MimeType("cat", "acme")]
    public class Cat : IAnimal
    {
        public string Name { get; set; }
        public int Legs { get; set; }
    }

    [MimeType("dog", "acme")]
    public class Dog : IAnimal
    {
        public string Name { get; set; }
        public int Legs { get; set; }
    }

    [MimeType("bird", "acme")]
    public class Bird : IAnimal
    {
        public string Name { get; set; }
        public int Legs { get; set; }
    }

    [MimeType("person", "acme")]
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public PersonAddress[] Addresses { get; set; }
    }

    public enum AddressType
    {
        Unknown,
        Home,
        Work
    }

    public class PersonAddress
    {
        public AddressType Type { get; set; }
        public Address Address { get; set; }
    }

    public class Address
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Town { get; set; }
        public string Country { get; set; }
    }
}
