using PhoneNumbers;
using Student.Infrastructure.Exceptions;
using System.Security.Cryptography;

namespace Student.Infrastructure.Helpers.Utilities
{
    public static class Util
    {
        public static string HashPassword(this string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
                throw new SmartException(nameof(password));
            using (var bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8, HashAlgorithmName.SHA1))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            var dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        public static string ValidatePhoneNumberAndFormat(string phoneNumber)
        {
            try
            {
               
                    var phoneNumberUtil = PhoneNumberUtil.GetInstance();
                    var parsedCode = phoneNumberUtil.Parse(phoneNumber, defaultRegion: "US");

                    if (phoneNumberUtil.IsValidNumber(parsedCode))
                    {
                        return phoneNumberUtil.Format(parsedCode, PhoneNumberFormat.NATIONAL);
                    }
                    else
                    {
                        return null;
                    }

                
               
            }
            catch (NumberParseException)
            {
                return null;
            }
        }


        public static string FormatAddress(string inputAddress)
        {
            string formattedAddress = ReplacePeriodsWithSpace(inputAddress).Trim();

            formattedAddress = RemoveConsecutiveSpaces(formattedAddress);

            formattedAddress = ConvertToAbbreviatedUpper(formattedAddress);


            return formattedAddress;
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
                return false;
            if (password == null)
                throw new SmartException(nameof(password));
            var src = Convert.FromBase64String(hashedPassword);
            if (src.Length != 0x31 || src[0] != 0)
                return false;
            var dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            var buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (var bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8, HashAlgorithmName.SHA1))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArrayCompare(buffer3, buffer4);
        }
        public static bool ByteArrayCompare(byte[] a1, byte[] a2)
        {
            if (a1.Length != a2.Length)
                return false;
            return !a1.Where((t, i) => t != a2[i]).Any();
        }

        #region Private
        private static string ReplacePeriodsWithSpace(string address)
        {
            return address.Replace('.', ' ');
        }

        private static string ConvertToAbbreviatedUpper(string address)
        {
            Dictionary<string, string> pluralMappings = new Dictionary<string, string>
            {
                { "apartments", "APTS" },
                { "avenues", "AVES" },
                { "roads", "RDS" },
                { "streets", "STS" },
                { "apartment", "APT" },
                { "avenue", "AVE" },
                { "road", "RD" },
                { "street", "ST" }
            };

            string[] words = address.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (pluralMappings.ContainsKey(words[i].ToLower()))
                {
                    words[i] = pluralMappings[words[i].ToLower()];
                }
                words[i] = words[i].ToUpper();
            }

            return string.Join(" ", words);
        }

        private static string RemoveConsecutiveSpaces(string address)
        {
            return System.Text.RegularExpressions.Regex.Replace(address, @"\s+", " ");
        }
        #endregion
    }
}
