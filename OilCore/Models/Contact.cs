namespace OilCore.Models;

public class Contact : Person
{
    public Contact()
        : base() // Default constructor for EF Core
    {
    }

    public Contact(string firstName, string lastName)
        : base(firstName, lastName)
    {
    }
    
    public List<Address> Addresses { get; set; } = [];
    public List<PhoneNumber> PhoneNumbers { get; set; } = [];
    public List<EmailAddress> EmailAddresses { get; set; } = []; 
    
}