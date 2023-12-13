namespace EventPlus.Core.Extensions;

public static class StringExtensions
{
	public static string FirstCharToUpper(this string input) =>
		string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1).ToString().ToLower());
}