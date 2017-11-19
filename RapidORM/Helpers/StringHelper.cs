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
        public static string ParseStringToCSVCell(string str)
        {
            bool mustQuote = (str.Contains(",") || str.Contains("\"") || str.Contains("\r") || str.Contains("\n"));
            if (mustQuote)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("\"");
                foreach (char nextChar in str)
                {
                    sb.Append(nextChar);
                    if (nextChar == '"')
                        sb.Append("\"");
                }
                sb.Append("\"");
                return sb.ToString();
            }

            return str;
        }

        public static bool IsUSPhoneNumber(string number)
        {
            string regExPattern = @"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$";
            return MatchStringFromRegex(number, regExPattern);
        }

        public static bool MatchStringFromRegex(string str, string regexstr)
        {
            str = str.Trim();
            System.Text.RegularExpressions.Regex pattern = new System.Text.RegularExpressions.Regex(regexstr);
            return pattern.IsMatch(str);
        }

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

        public static string GetAlphanumericPassword()
        {
            var characterSet = GenerateCharacterPasswordCombination(new Random());
            var numberSet = NumberHelper.GenerateNumberPasswordCombination(new Random());

            return string.Format("{0}{1}{2}{3}", characterSet[0], numberSet[0], characterSet[1], numberSet[1]);
        }

        public static String SetToProperCase(String givenString)
        {
            String strProper = givenString.Substring(0, 1).ToUpper();
            givenString = givenString.Substring(1).ToLower();
            String strPrev = "";

            for (int iIndex = 0; iIndex < givenString.Length; iIndex++)
            {
                if (iIndex > 1)
                {
                    strPrev = givenString.Substring(iIndex - 1, 1);
                }

                if (strPrev.Equals(" ") ||
                strPrev.Equals("\t") ||
                strPrev.Equals("\n") ||
                strPrev.Equals("."))
                {
                    strProper += givenString.Substring(iIndex, 1).ToUpper();
                }
                else
                {
                    strProper += givenString.Substring(iIndex, 1);
                }
            }
            return strProper;
        }

        public static String ReplaceString(String strText, String strFind, String strReplace)
        {
            int iPos = strText.IndexOf(strFind);
            String strReturn = "";

            while (iPos != -1)
            {
                strReturn += strText.Substring(0, iPos) + strReplace;
                strText = strText.Substring(iPos + strFind.Length);
                iPos = strText.IndexOf(strFind);
            }

            if (strText.Length > 0)
                strReturn += strText;
            return strReturn;
        }

        public static String SingleWhitespaceOnlyBetweenWords(String strParam)
        {
            int iPosition = strParam.IndexOf(" ");
            if (iPosition == -1)
            {
                return strParam;
            }
            else
            {
                return SingleWhitespaceOnlyBetweenWords(strParam.Substring(0, iPosition) +
                strParam.Substring(iPosition + 1));
            }
        }

        public static int CountStringOccurrence(String strSource, String strToCount)
        {
            int iCount = 0;
            int iPos = strSource.IndexOf(strToCount);

            while (iPos != -1)
            {
                iCount++;
                strSource = strSource.Substring(iPos + 1);
                iPos = strSource.IndexOf(strToCount);
            }
            return iCount;
        }

        public static string UpperCaseFirst(string rawText)
        {
            rawText = rawText.ToLower();
            if (string.IsNullOrEmpty(rawText))
            {
                return string.Empty;
            }

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(rawText.ToLower());
        }

        public static string TruncateLongString(string rawString, int maxLength)
        {
            string result = string.Empty;

            if (rawString != null)
            {
                result = rawString.Length > maxLength ? rawString.Substring(0, maxLength) : rawString;
            }

            return result;
        }

        public static String ReverseString(String strParam)
        {
            if (strParam.Length == 1)
            {
                return strParam;
            }
            else
            {
                return ReverseString(strParam.Substring(1)) + strParam.Substring(0, 1);
            }
        }

        public static String GetCharactersFromLeftOfString(String strParam, int iLen)
        {
            if (iLen > 0)
                return strParam.Substring(0, iLen);
            else
                return strParam;
        }

        public static String GetCharactersFromRightOfString(String strParam, int iLen)
        {
            if (iLen > 0)
                return strParam.Substring(strParam.Length - iLen, iLen);
            else
                return strParam;
        }
    }
}
