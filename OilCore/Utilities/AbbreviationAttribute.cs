namespace OilCore.Utilities;

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class AbbreviationAttribute : Attribute
{
    public string Code { get; }

    public AbbreviationAttribute(string code)
    {
        Code = code;
    }
}