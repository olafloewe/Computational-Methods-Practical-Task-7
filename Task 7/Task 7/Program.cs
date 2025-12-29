using System;
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
        
        static void Main(string[] args) {
            Console.WriteLine(MRG32k3a(0) / m1);
            Console.WriteLine(MRG32k3a(1) / m1);
            Console.WriteLine(MRG32k3a(10) / m1);
            Console.WriteLine(MRG32k3a(20) / m1);
        }

        static double MRG32k3a(int i) {
            // a and b based on i
            double ai = A(i);
            double bi = B(i);

            // xi calculation
            return (ai < bi)? ai - bi + m1 : ai - bi;
        }

        // calculation of Ai based on previous A values
        static double A(int i) {
            if (i <= 0) return 1; // base case
            return ( (1403580.0 * A(i - 2)) - (810728.0 * A(i - 3)) ) % m1;
        }

        // calculation of Bi based on previous B values
        static double B(int i) {
            if (i <= 0) return 1; // base case
            return ((527612.0 * B(i - 1)) - (1370589.0 * B(i - 3))) % m2;
        }
    }
}
