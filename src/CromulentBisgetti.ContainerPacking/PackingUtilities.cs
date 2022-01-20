using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace CromulentBisgetti.ContainerPacking
{
    public static class PackingUtilities
    {
        private static Func<decimal, decimal, decimal, string> _fCreateTripletString;
        private static Func<decimal, decimal, string> _fCreateCartesianString;
        
        static PackingUtilities()
        {
            _fCreateTripletString = new Func<decimal, decimal, decimal, string>(
                (v1, v2, v3) => $"({v1},{v2},{v3})");
            _fCreateCartesianString = new Func<decimal, decimal, string>(
                (v1, v2) => $"({v1},{v2})");
        }

        public static string CreateTriplet(decimal v1, decimal v2, decimal v3)
        {
            return _fCreateTripletString(v1, v2, v3);
        }
        public static string CreateTriplet(int v1, int v2, int v3)
        {
            return _fCreateTripletString(v1, v2, v3);
        }
        public static string CreateCartesian(decimal v1, decimal v2)
        {
            return _fCreateCartesianString(v1, v2);
        }
        public static string CreateCartesian(int v1, int v2)
        {
            return _fCreateCartesianString(v1, v2);
        }

        public static string HashString(string text, string salt = "")
        {
            if (String.IsNullOrEmpty(text))
            {
                return String.Empty;
            }

            // Uses SHA256 to create the hash
            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                // Convert the string to a byte array first, to be processed
                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text + salt);
                byte[] hashBytes = sha.ComputeHash(textBytes);

                // Convert back to a string, removing the '-' that BitConverter adds
                string hash = BitConverter
                    .ToString(hashBytes)
                    .Replace("-", String.Empty);

                return hash;
            }
        }
    
        public static bool AreEquivalent<T>(this IEnumerable<T> source, IEnumerable<T> target)
        {
            return (!source.Except(target).Any()
                && !target.Except(source).Any());
        }
        public static int GetEnumerableHashCode<T>(this IEnumerable<T> source)
        {
            return source.Distinct().Aggregate(
                0, (x, y) => x.GetHashCode() ^ y.GetHashCode());
        }
        public static int CreateHashCode(int[] componentHashes, int hashBase = 0)
        {
            
            int hash = ( hashBase == 0 ? 23 : hashBase);
            foreach (var item in componentHashes)
            {
                try
                {
                    hash = hash * 31 + item;
                }
                catch
                {

                }
            }
            return hash;
        }
        public static string TimeAction(Action action)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            action.Invoke();
            stopWatch.Stop();
            return stopWatch.Elapsed.ToString();
        }
        
    }
}
