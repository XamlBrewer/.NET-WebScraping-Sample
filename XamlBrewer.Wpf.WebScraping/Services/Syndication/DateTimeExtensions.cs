namespace XamlBrewer.Services.Syndication
{
    using System;

    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns a DateTime from an RFC 822 date and time string with a named time zone.
        /// </summary>
        public static DateTimeOffset FromRfc822String(this DateTimeOffset dt, string rfc822)
        {
            //  Validate parameter
            if (String.IsNullOrEmpty(rfc822))
            {
                throw new ArgumentNullException("rfc822");
            }

            int pos = rfc822.LastIndexOf(" ");
            if (pos < 1)
            {
                throw new FormatException("Unknown date format.");
            }

            // Find timezone
            var baseDateTime = rfc822.Substring(0, pos);
            var timeZoneName = rfc822.Substring(pos + 1);

            // Set base date.
            var result = DateTime.Parse(baseDateTime);

            // Military time zone
            switch (timeZoneName)
            {
                case "A":
                    return result.AddHours(1);
                case "B":
                    return result.AddHours(2);
                case "C":
                    return result.AddHours(3);
                case "D":
                    return result.AddHours(4);
                case "E":
                    return result.AddHours(5);
                case "F":
                    return result.AddHours(6);
                case "G":
                    return result.AddHours(7);
                case "H":
                    return result.AddHours(8);
                case "I":
                    return result.AddHours(9);
                case "K":
                    return result.AddHours(10);
                case "L":
                    return result.AddHours(11);
                case "M":
                    return result.AddHours(12);
                case "N":
                    return result.AddHours(-1);
                case "O":
                    return result.AddHours(-2);
                case "P":
                    return result.AddHours(-3);
                case "Q":
                    return result.AddHours(-4);
                case "R":
                    return result.AddHours(-5);
                case "S":
                    return result.AddHours(-6);
                case "T":
                    return result.AddHours(-7);
                case "U":
                    return result.AddHours(-8);
                case "V":
                    return result.AddHours(-9);
                case "W":
                    return result.AddHours(-10);
                case "X":
                    return result.AddHours(-11);
                case "Y":
                    return result.AddHours(-12);
                case "Z":
                // UTC Zone
                case "UT":
                case "GMT":
                    return result;
                case "EDT":
                    return result.AddHours(4);
                case "EST":
                    return result.AddHours(5);
                case "CDT":
                    return result.AddHours(5);
                case "CST":
                    return result.AddHours(6);
                case "MDT":
                    return result.AddHours(6);
                case "MST":
                    return result.AddHours(7);
                case "PDT":
                    return result.AddHours(7);
                case "PST":
                    return result.AddHours(8);
                default:
                    // RFC822 says: if timezone unknown then offset = 0
                    return result;
            }
        }
    }
}
