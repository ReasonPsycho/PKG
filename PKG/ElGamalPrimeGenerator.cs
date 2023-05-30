using System;

namespace PKG
{
    public static class ElGamalPrimeGenerator
    {
        private static bool IsPrime(int number)
        {
            if (number < 2)
                return false;

            for (var i = 2; i <= Math.Sqrt(number); i++)
                if (number % i == 0)
                    return false;

            return true;
        }

        public static int Generate()
        {
            var random = new Random();

            var randomPrime = random.Next(1001, 10000);

            while (!IsPrime(randomPrime)) randomPrime = random.Next(1001, 10000);

            return randomPrime;
        }
    }
}