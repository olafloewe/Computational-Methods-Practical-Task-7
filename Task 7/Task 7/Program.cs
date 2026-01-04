using System;

namespace Task_7 {
    internal class Program {

        // constant terms for m1 and m2
        private static readonly long m1 = (long)Math.Pow(2, 32) - 209;
        private static readonly long m2 = (long)Math.Pow(2, 32) - 22853;

        // generator results
        private static long[] A = new long[] { 0, 0, 0 };
        private static long[] B = new long[] { 0, 0, 0 };

        static void Main(string[] args) {

            // new seed
            long time = long.Parse(DateTime.Now.ToFileTimeUtc().ToString());
            Console.WriteLine($"Seed: {time}");

            // write seed values into A and B
            A = new long[] { time, 0, 0 };
            B = new long[] { time, 0, 0 };

            // TESTING
            /*
            // INT test
            int[] results = new int[11];

            // FLOAT test
            List<float> results = new List<float>();

            // ELEMENT test
            float[] elements = new float[100];
            */

            // Console.WriteLine(Rand());
            for (int i = 0; i < 100; i++) {
                // TESTING
                /*
                // INT test
                results[RandInt(1, 0)]++; 
                
                //FLOAT test
                results.Add(RandFloat(0.0f, 1.0f)); 

                // ELEMENT test
                elements[i] = RandFloat(0.0f, 10.0f);

                // STRING test
                Console.WriteLine(RandString(100));
                */
            }

            // TESTING
            /*
            // INT test
            for (int i = 0; i < results.Length; i++) {
                Console.WriteLine($"{i}: {results[i]}");
            }

            // FLOAT test
            results.Sort();
            foreach (float f in results) {
                Console.WriteLine(f);
            }

            // ELEMENT test
            Console.WriteLine(RandElement(elements));
            */

        }

        // true modulo to handle negative results
        private static long mod(long x, long y) {
            long m = x % y;
            // result is negative, offset by modulo
            if (m < 0) return m + Math.Abs(y);

            // result is positive
            return m;
        }

        // combines generatos A and B to calculate xi
        static long MRG32k3a() {
            // generates next A and B values
            long ai = NextA();
            long bi = NextB();

            // xi calculation
            return (ai < bi) ? ai - bi + m1 : ai - bi;
        }

        // calculation of Ai based on previous A values
        static long NextA() {
            // save results
            A = new long[] { mod(((1403580 * A[1]) + (-810728 * A[2])), m1), A[0], A[1]};
            return A[0];
        }

        // calculation of Bi based on previous B values 
        static long NextB() {
            // save results
            B = new long[] { mod(((527612 * B[0]) - (1370589 * B[2])), m2), B[0], B[1] };
            return B[0];
        }

        // generate random double between 0 and 1
        static double Rand() {
            return (double) MRG32k3a() / m1;
        }

        // generate random integer between a and b
        static int RandInt(int a, int b) {
            return (a < b) ? (int)(Rand() * (b - a + 1)): (int)(Rand() * (a - b + 1));
        }

        // generate random float between a and b
        static float RandFloat(float a, float b) { 
            return (a < b) ? (float)(Rand() * (b - a)) : (float)(Rand() * (a - b));
        }

        // generate random element from array A
        static float RandElement(float[] A) { 
            if (A.Length > 0) {
                int index = RandInt(0, A.Length - 1);
                return A[index];
            }
            return 0.0f;
        }

        // generate a random string of length a consisting of all printable ASCII characters
        // (with the exception of SP(space) and DEL(delete))
        static string RandString(int a) {
            // extended allowed characters to all ASCII printable characters
            string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!\"#$%&\'()*+,-./:;<=>?@[\\]^_`{|}~";
            string result = "";

            // generate string of length a
            for (int i = 0; i < a; i++) result += allowedChars.ToCharArray()[RandInt(0, allowedChars.Length - 1)];

            return result;
        } 
    }
}