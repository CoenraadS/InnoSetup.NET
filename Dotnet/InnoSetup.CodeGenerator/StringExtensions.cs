namespace InnoSetup.CodeGenerator;

public static class StringExtensions
{
    public static string TrimStart(this string target, string trimString)
    {
        if (string.IsNullOrEmpty(trimString)) return target;

        string result = target;
        while (result.StartsWith(trimString))
        {
            result = result.Substring(trimString.Length);
        }

        return result;
    }
}
