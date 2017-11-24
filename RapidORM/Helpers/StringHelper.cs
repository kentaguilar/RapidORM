using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;

namespace RapidORM.Helpers
{
    public class StringHelper
    {
        /// <summary>
        /// Prepare and convert string to CSV cell compatible
        /// </summary>
        /// <param name="rawString"></param>
        public static string ParseStringToCSVCell(string rawString)
        {
            bool mustQuote = (rawString.Contains(",") || rawString.Contains("\"") || rawString.Contains("\r") || rawString.Contains("\n"));
            if (mustQuote)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("\"");
                foreach (char nextChar in rawString)
                {
                    sb.Append(nextChar);
                    if (nextChar == '"')
                        sb.Append("\"");
                }
                sb.Append("\"");
                return sb.ToString();
            }

            return rawString;
        }

        /// <summary>
        /// Validates if user defined number is a US phone number
        /// </summary>
        /// <param name="number"></param>
        public static bool IsUSPhoneNumber(string number)
        {
            string regExPattern = @"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$";
            return MatchStringFromRegex(number, regExPattern);
        }

        /// <summary>
        /// Checks if user defined string matches the given Regex
        /// </summary>
        /// <param name="rawString"></param>
        /// <param name="regexString"></param>
        public static bool MatchStringFromRegex(string rawString, string regexString)
        {
            rawString = rawString.Trim();
            System.Text.RegularExpressions.Regex pattern = new System.Text.RegularExpressions.Regex(regexString);
            return pattern.IsMatch(rawString);
        }

        /// <summary>
        /// Generates password from random combination
        /// </summary>
        /// <param name="random"></param>
        public static string[] GenerateCharacterPasswordCombination(Random random)
        {
            string targetCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            int firstIndex = random.Next(0, targetCharacters.Length - 1);
            int secondIndex = random.Next(0, targetCharacters.Length - 1);
            int thirdIndex = random.Next(0, targetCharacters.Length - 1);
            int fourthIndex = random.Next(0, targetCharacters.Length - 1);

            return new string[]{
                string.Format("{0}{1}", targetCharacters[firstIndex], targetCharacters[secondIndex]),
                string.Format("{0}{1}", targetCharacters[thirdIndex], targetCharacters[fourthIndex])
            };
        }

        /// <summary>
        /// Generates random alphanumeric password
        /// </summary>
        /// <param name=""></param>
        public static string GetAlphanumericPassword()
        {
            var characterSet = GenerateCharacterPasswordCombination(new Random());
            var numberSet = NumberHelper.GenerateNumberPasswordCombination(new Random());

            return string.Format("{0}{1}{2}{3}", characterSet[0], numberSet[0], characterSet[1], numberSet[1]);
        }

        /// <summary>
        /// Formats raw string to proper string
        /// </summary>
        /// <param name="rawString"></param>
        public static String SetToProperCase(String rawString)
        {
            String properString = rawString.Substring(0, 1).ToUpper();
            rawString = rawString.Substring(1).ToLower();

            String strPrev = "";

            for (int index = 0; index < rawString.Length; index++)
            {
                if (index > 1)
                {
                    strPrev = rawString.Substring(index - 1, 1);
                }

                if (strPrev.Equals(" ") ||
                    strPrev.Equals("\t") ||
                    strPrev.Equals("\n") ||
                    strPrev.Equals("."))
                {
                    properString += rawString.Substring(index, 1).ToUpper();
                }
                else
                {
                    properString += rawString.Substring(index, 1);
                }
            }

            return properString;
        }

        /// <summary>
        /// Finds given string and replace with a user defined string
        /// </summary>
        /// <param name="rawString"></param>
        /// <param name="stringToFind"></param>
        /// <param name="stringToReplace"></param>
        public static String ReplaceString(String rawString, String stringToFind, String stringToReplace)
        {
            int iPos = rawString.IndexOf(stringToFind);
            String strReturn = "";

            while (iPos != -1)
            {
                strReturn += rawString.Substring(0, iPos) + stringToReplace;
                rawString = rawString.Substring(iPos + stringToFind.Length);
                iPos = rawString.IndexOf(stringToFind);
            }

            if (rawString.Length > 0)
                strReturn += rawString;
            return strReturn;
        }

        /// <summary>
        /// Parse given string and add a space between words
        /// </summary>
        /// <param name="rawString"></param>
        public static String SingleWhitespaceOnlyBetweenWords(String rawString)
        {
            int position = rawString.IndexOf(" ");
            if (position == -1)
            {
                return rawString;
            }
            else
            {
                return SingleWhitespaceOnlyBetweenWords(rawString.Substring(0, position) +
                rawString.Substring(position + 1));
            }
        }

        /// <summary>
        /// Count occurrence of a string given a string
        /// </summary>
        /// <param name="rawString"></param>
        /// <param name="stringToCount"></param>
        public static int CountStringOccurrence(String rawString, String stringToCount)
        {
            int iCount = 0;
            int iPos = rawString.IndexOf(stringToCount);

            while (iPos != -1)
            {
                iCount++;
                rawString = rawString.Substring(iPos + 1);
                iPos = rawString.IndexOf(stringToCount);
            }
            return iCount;
        }

        /// <summary>
        /// Make the first letter uppercase
        /// </summary>
        /// <param name="rawString"></param>
        public static string UpperCaseFirst(string rawString)
        {
            rawString = rawString.ToLower();
            if (string.IsNullOrEmpty(rawString))
            {
                return string.Empty;
            }

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(rawString.ToLower());
        }

        /// <summary>
        /// Truncate long string and show only number of characters from a string
        /// </summary>
        /// <param name="rawString"></param>
        /// <param name="maxLength"></param>
        public static string TruncateLongString(string rawString, int maxLength)
        {
            string result = string.Empty;

            if (rawString != null)
            {
                result = rawString.Length > maxLength ? rawString.Substring(0, maxLength) : rawString;
            }

            return result;
        }

        /// <summary>
        /// Reverse a given string
        /// </summary>
        /// <param name="rawString"></param>
        public static String ReverseString(String rawString)
        {
            if (rawString.Length == 1)
            {
                return rawString;
            }
            else
            {
                return ReverseString(rawString.Substring(1)) + rawString.Substring(0, 1);
            }
        }

        /// <summary>
        /// Retrieves characters from left side of the string
        /// </summary>
        /// <param name="rawString"></param>
        /// <param name="length"></param>
        public static String GetCharactersFromLeftOfString(String rawString, int length)
        {
            if (length > 0)
            {
                return rawString.Substring(0, length);
            }
            else
            {
                return rawString;
            }
        }

        /// <summary>
        /// Retrieves characters from right side of the string
        /// </summary>
        /// <param name="rawString"></param>
        /// <param name="length"></param>
        public static String GetCharactersFromRightOfString(String rawString, int length)
        {
            if (length > 0)
            {
                return rawString.Substring(rawString.Length - length, length);
            }
            else
            {
                return rawString;
            }
        }
    }
}
