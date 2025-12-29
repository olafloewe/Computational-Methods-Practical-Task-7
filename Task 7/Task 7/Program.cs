using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/*
 
Design a function named Rand which implements the MRG32k3a algorithm. 
The function should return a real value from the [0,1) interval. 
Then, use the Rand function to design and implement the following functions:

RandInt(a, b) which returns a random integer between a and b,
RandFloat(a, b) which returns a random real number between a and b,
RandElement(A) which returns a random element from the array of real
numbers A,
RandString(a) which returns a random string of lower- and upper-case letters, digits, and special symbols, of the length a.
 
 */

namespace Task_7 {
    internal class Program {

        // constant terms for m1 and m2
        private static readonly double m1 = Math.Pow(2, 32) - 209;
        private static readonly double m2 = Math.Pow(2, 32) - 22853.0;
        static Dictionary<long,double> memoizationA = new Dictionary<long, double>();
        static Dictionary<long, double> memoizationB = new Dictionary<long, double>();

        static void Main(string[] args) {
            Console.WriteLine(Rand());
        }

        static double MRG32k3a(long i) {
            // a and b based on i
            double ai = A(i);
            double bi = B(i);

            // xi calculation
            return (ai < bi)? ai - bi + m1 : ai - bi;
        }

        // calculation of Ai based on previous A values
        static double A(long i) {
            if (i <= 0) return 1; // base case
            // save results to reduce calculation load on larger i values
            if (memoizationA.ContainsKey(i)) return memoizationA[i];
            Console.WriteLine($"A{i}");
            memoizationA.Add(i, ((1403580.0 * A(i - 2)) - (810728.0 * A(i - 3))) % m1);
            return memoizationA[i];
        }

        // calculation of Bi based on previous B values
        static double B(long i) {
            if (i <= 0) return 1; // base case
            // save results to reduce calculation load on larger i values
            if (memoizationB.ContainsKey(i)) return memoizationB[i];
            Console.WriteLine($"B{i}");
            memoizationB.Add(i, ((527612.0 * B(i - 1)) - (1370589.0 * B(i - 3))) % m2);
            return memoizationB[i];
        }

        static double Rand() {
            // TODO recursive => itterative
            long time = long.Parse(DateTime.Now.ToFileTimeUtc().ToString().Substring(7));
            return MRG32k3a(time) / m1;
        }

        static int RandInt(int a, int b) {
            // TODO test for 0 - max or 1 - max+1
            // range * weight[0-1] + offset
            return (a < b)? (int)(Rand() * b-a) + a : (int)(Rand() * a - b) + b;
        }
    }
}