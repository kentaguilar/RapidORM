using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidORM.Helpers
{
    public class NumberHelper
    {
        public static string[] GenerateNumberPasswordCombination(Random random)
        {
            int min = 10;
            int max = 99;

            int firstNumber = random.Next(min, max);
            int secondNumber = random.Next(min, max);

            return new string[]{
                firstNumber.ToString(),
                secondNumber.ToString()
            };
        }
    }
}
