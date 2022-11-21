using System.Collections.Generic;
using System.Linq;

namespace ControlSum
{
    /// <summary>
    /// An algorithm for calculating the check digit of a plastic card number in accordance with
    /// ISO / IEC 7812. It is not a cryptographic tool primarily designed to detect errors that cause unintentional
    /// data corruption over the phone). Only taking into account the fact that there is a possibility of errors in the
    /// block of numbers, but it does not make it possible to find and detect inaccuracies.
    /// https://en.wikipedia.org/wiki/Luhn_algorithm
    /// </summary>
    public class Luhn
    {
        /// <summary>
        /// Generates a check number based on an array
        /// </summary>
        /// <param name="array">Card number split into an array without Luhn number</param>
        /// <returns>Return number from range from 0 to 9</returns>
        public static int Create(int[] array)
        {
            IEnumerable<int> nums = Enumerable.Range(1, array.Length);
            int sum = array.Length % 2 != 0
                ? nums
                      .Where(w => w % 2 == 0)
                      .Sum(s => array[s - 1]) +
                  nums
                      .Where(w => w % 2 != 0)
                      .Sum(index =>
                      {
                          int num = array[index - 1] * 2;
                          if (num > 9) num -= 9;
                          return num;
                      })
                : nums
                      .Where(w => w % 2 != 0)
                      .Sum(s => array[s - 1]) +
                  nums
                      .Where(w => w % 2 == 0)
                      .Sum(index =>
                      {
                          int num = array[index - 1] * 2;
                          if (num > 9) num -= 9;
                          return num;
                      });
            
            return 10 - sum % 10;  
        }
        
        /// <summary>
        /// Check digit checks the last value in the array
        /// </summary>
        /// <param name="array">Card number split into an array with Luhn number</param>
        /// <returns>if the numbers match then true is returned otherwise false</returns>
        public static bool Check(IEnumerable<int> array)=>
            array.TakeLast(1).FirstOrDefault() == Create(array.SkipLast(1).ToArray());
    }
}