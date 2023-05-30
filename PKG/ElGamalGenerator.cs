using System;
using System.Numerics;

namespace PKG
{
    public class ElGamalGenerator
    {
        //Method to find the smallest primitive root of p
        public static int FindGenerator(int p)
        {
            var primes = Factor(p - 1); //get prime factors of p-1
            for (var g = 2; g < p; g++)
            {
                var ok = true;
                for (var i = 0; i < primes.Length && ok; i++)
                {
                    var phi = (p - 1) / primes[i];
                    ok &= BigInteger.ModPow(g, phi, p) != 1; //check if g is a primitive root mod p
                }

                if (ok) return g;
            }

            throw new Exception("Failed to find generator!");
        }

        //Method to find the prime factors of a number
        public static int[] Factor(int n)
        {
            var factors = new int[32]; //max number of distinct prime factors
            var len = 0;
            for (var f = 2; f * f <= n; f++)
                while (n % f == 0)
                {
                    factors[len++] = f;
                    n /= f;
                }

            if (n > 1) factors[len++] = n;
            var result = new int[len];
            Array.Copy(factors, result, len);
            return result;
        }
    }
}