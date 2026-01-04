using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using System.Xml.Linq;

/*
 
Design a function named Rand which implements the MRG32k3a algorithm. 
The function should return a real value from the [0,1) interval. 
Then, use the Rand function to design and implement the following functions:

    RandInt(a, b) which returns a random integer between a and b,

    RandFloat(a, b) which returns a random real number between a and b,

    RandElement(A) which returns a random element from the array of real numbers A,

    RandString(a) which returns a random string of lower- and upper-case letters, digits, and special symbols, of the length a.
 
 */

namespace Task_7 {
    internal class Program {

        // constant terms for m1 and m2
        private static readonly long m1 = (long)Math.Pow(2, 32) - 209;
        private static readonly long m2 = (long)Math.Pow(2, 32) - 22853;

        // generator results
        private static long[] A = new long[] { 0, 0, 0 };
        private static long[] B = new long[] { 0, 0, 0 };

        // storage for previous results
        static Dictionary<long, long> memoizationA = new Dictionary<long, long>(); 
        static Dictionary<long, long> memoizationB = new Dictionary<long, long>();

        // public Thread(System.Threading.ThreadStart start, int maxStackSize);

        static void Main(string[] args) {
            long time = long.Parse(DateTime.Now.ToFileTimeUtc().ToString());

            // new seed
            A = new long[] { time, 0, 0 };
            B = new long[] { time, 0, 0 };

            Console.WriteLine(Rand());
            Console.ReadKey();
            for (int i = 0; i < 100; i++) {
                Thread.Sleep(1);
                Console.WriteLine(Rand());
                // Console.WriteLine(RandInt(0, 1));
                //Console.WriteLine(RandInt(0, 10));
            }

        }

        
        static long MRG32k3a() {
            // generates next A and B values
            long ai = NextA();
            long bi = NextB();

            // xi calculation
            return (ai < bi)? ai - bi + m1 : ai - bi;
        }

        // calculation of Ai based on previous A values
        static long NextA() {
            // TODO try yield return
            
            A = new long[] { ((1403580 * A[1]) + (-810728 * A[2])) % m1, A[0], A[1]};

            return A[0];
            /*
            // base case
            if (i <= 0) return 1;
            
            // save results to reduce calculation load on larger i values
            if (memoizationA.ContainsKey(i)) return memoizationA[i];

            memoizationA.Add(i, ((1403580 * Ai(i - 2)) - (810728 * Ai(i - 3))) % m1);
            Console.WriteLine($"A{i} {memoizationA[i]}");
            return memoizationA[i];
            */
        }

        // calculation of Bi based on previous B values 
        static long NextB() {

            B = new long[] { ((527612 * B[0]) - (1370589 * B[2])) % m2, B[0], B[1] };

            return B[0];
            /*
            // base case
            if (i <= 0) return 1;

            // save results to reduce calculation load on larger i values
            if (memoizationB.ContainsKey(i)) return memoizationB[i];

            memoizationB.Add(i, ((527612 * Bi(i - 1)) - (1370589 * Bi(i - 3))) % m2);
            Console.WriteLine($"B{i} {memoizationB[i]}");
            return memoizationB[i];
            */
        }

        static double Rand() {
            // TODO recursive => itterative

            // Console.ReadKey();
            return (double) MRG32k3a() / m1;
        }


        // FUNCTIONS TO IMPLEMENT USING Rand()

        static int RandInt(int a, int b) {
            // TODO test for 0 - max or 1 - max+1
            // range * weight[0-1] + offset
            return (int)(Rand() * (a - b)) + b;
            return (a < b) ? (int)(Rand() * (b - a)) + a : (int)(Rand() * (a - b)) + b;
        }

        static float RandFloat(float a, float b) { 
            return 0.0f;
        }

        static float RandElement(float[] A) { 
            return 0.0f;
        }
        static string RandString(int a) { 
            return "";
        } 
    }
}