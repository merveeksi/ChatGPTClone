namespace ChatGPTClone.Domain;

public static class StringExtensions
{
    // @ sembolünü içeriyorsa email olarak kabul eder.
    public static bool IsEmail(this string email)
    {
        return email.Contains("@");
    }
}