using OilCore.Models.Base;

namespace OilCore.Models;

public class UserCredential : UserCredentialModel
{
    public UserCredential() {}
    public UserCredential(User user, string password) : base(user, password) {}
}