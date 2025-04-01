using OilCore.Models.Base;
using OilCore.Services;

namespace OilCore.Models;

/// <summary>
/// Represents a person entity with associated contact information.
/// Inherits from <see cref="PersonModel"/> to support polymorphic behavior in the DbContext.
/// </summary>
public class Person : PersonModel, IDeletable
{
    protected Person() {}

    public Person(string firstName, string lastName) : base(firstName, lastName) {}

    public void AddAddress(Address address) => Addresses.Add(address ?? throw new ArgumentNullException(nameof(address)));
    public void AddEmail(EmailAddress email) => EmailAddresses.Add(email ?? throw new ArgumentNullException(nameof(email)));
    public void AddPhoneNumber(PhoneNumber phone) => PhoneNumbers.Add(phone ?? throw new ArgumentNullException(nameof(phone)));

    public void RemoveAddress(Address address)
    {
        if (!Addresses.Remove(address ?? throw new ArgumentNullException(nameof(address))))
            throw new ArgumentException("Address not found", nameof(address));
    }

    public void RemoveEmail(EmailAddress email)
    {
        if (!EmailAddresses.Remove(email ?? throw new ArgumentNullException(nameof(email))))
            throw new ArgumentException("Email not found", nameof(email));
    }

    public void RemovePhoneNumber(PhoneNumber phone)
    {
        if (!PhoneNumbers.Remove(phone ?? throw new ArgumentNullException(nameof(phone))))
            throw new ArgumentException("Phone not found", nameof(phone));
    }
}