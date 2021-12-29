using System;
using System.Globalization;

namespace StringVerification
{
    public static class IsbnVerifier
    {
        /// <summary>
        /// Verifies if the string representation of number is a valid ISBN-10 identification number of book.
        /// </summary>
        /// <param name="number">The string representation of book's number.</param>
        /// <returns>true if number is a valid ISBN-10 identification number of book, false otherwise.</returns>
        /// <exception cref="ArgumentException">Thrown if number is null or empty or whitespace.</exception>
        public static bool IsValid(string number)
        {
            if (string.IsNullOrEmpty(number) || string.IsNullOrWhiteSpace(number))
            {
                throw new ArgumentException("Thrown if number is null or empty or whitespace", nameof(number));
            }

            string numberwithouthyphen = number.Replace("-", string.Empty, StringComparison.CurrentCulture);
            for (int i = 0; i < number.Length; i++)
            {
                if (number[i] == '-')
                {
                    string[] splitedstring = number.Split('-');
                    if (splitedstring[0].Length != 1 || splitedstring[1].Length != 3)
                    {
                        return false;
                    }
                }
            }

            if (numberwithouthyphen.Length != 10)
            {
                return false;
            }

            int numbersum = 0;
            bool result;
            for (int i = 0; i < numberwithouthyphen.Length; i++)
            {
                if (numberwithouthyphen[i] == 'X')
                {
                   numbersum += 10 * (10 - i);
                   break;
                }

                try
                {
                    numbersum += Convert.ToInt32(new string(numberwithouthyphen[i], 1)) * (10 - i);
                }
                catch (FormatException)
                {
                    return false;
                }
            }

            result = numbersum % 11 == 0;
            return result;
        }
    }
}
