using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace task3
{
    class Computer
    {

        public byte[] secretKey { get; private set; }
        public byte[] hmac { get; private set; }

        public int step { get; private set; }

        public void MakeTurn(string[] possibleSteps)
        { 
            secretKey = GenerateSecretKey();
            step = RandomNumberGenerator.GetInt32(possibleSteps.Length-1);
            hmac = ComputeHMAC(possibleSteps[step]);
        }

        private byte[] GenerateSecretKey()
        {
            byte[] key = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(key);
            return key;
        }

        private byte[] ComputeHMAC(string stepInString)
        {
            HMACSHA256 hmac = new HMACSHA256(secretKey);
            byte[] hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(stepInString));
            return hashValue;
        }
    }
}
