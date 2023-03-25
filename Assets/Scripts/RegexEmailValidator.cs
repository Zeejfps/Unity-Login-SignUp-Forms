using System;
using System.Globalization;
using System.Text.RegularExpressions;

public sealed class RegexEmailValidator : IEmailValidator
{
    public EmailValidationStatus Validate(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return EmailValidationStatus.Empty;
            
        try
        {
            // Normalize the domain
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                RegexOptions.None, TimeSpan.FromMilliseconds(200));

            // Examines the domain part of the email and normalizes it.
            string DomainMapper(Match match)
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new IdnMapping();

                // Pull out and process domain name (throws ArgumentException on invalid)
                string domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException)
        {
            return EmailValidationStatus.Invalid;
        }
        catch (ArgumentException)
        {
            return EmailValidationStatus.Invalid;
        }

        try
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250))
                ? EmailValidationStatus.Valid
                : EmailValidationStatus.Invalid;
        }
        catch (RegexMatchTimeoutException)
        {
            return EmailValidationStatus.Invalid;
        }
    }
}