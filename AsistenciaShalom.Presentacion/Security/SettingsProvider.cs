using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciaShalom.Presentacion.Security
{
    public class SettingsProvider : ISettingsProvider
    {
        private static readonly byte[] _encryptionKey;
        private static readonly string _encryptionString;
        private static readonly string _encryptionPrefix;
        private static readonly string _saltGeneratorKey;

        static SettingsProvider()
        {
            //read settings from configuration
            var useHashingString = "";// ConfiguracionApp._useHashingForEncryption;
            bool useHashing = string.Compare(useHashingString, "false", true) != 0;

            _encryptionPrefix = ""; //ConfiguracionApp._encryptionPrefix;
            if (string.IsNullOrWhiteSpace(_encryptionPrefix))
            {
                _encryptionPrefix = "encryptedHidden_";
            }

            _saltGeneratorKey = "DB762FDF-14DF-4B07-ACA7-871C740E4EBA"; // ConfiguracionApp._encryptionKey;
            _encryptionString = "D71F9BAD-E56B-42EF-B4BD-706DC4C0BE99"; // ConfiguracionApp._encryptionSalt;

            if (useHashing)
            {
                var hash = new SHA256Managed();
                _encryptionKey = hash.ComputeHash(UTF8Encoding.UTF8.GetBytes(_encryptionString));
                hash.Clear();
                hash.Dispose();
            }
            else
            {
                _encryptionKey = UTF8Encoding.UTF8.GetBytes(_encryptionString);
            }
        }

        #region ISettingsProvider Members

        public byte[] EncryptionKey
        {
            get { return _encryptionKey; }
        }

        public string EncryptionString
        {
            get { return _encryptionString; }
        }

        public string EncryptionPrefix
        {
            get { return _encryptionPrefix; }
        }

        public string SaltGeneratorKey
        {
            get { return _saltGeneratorKey; }
        }

        #endregion

    }
}
