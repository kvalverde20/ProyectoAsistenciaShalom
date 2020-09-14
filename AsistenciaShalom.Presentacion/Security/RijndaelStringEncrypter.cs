using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AsistenciaShalom.Presentacion.Security
{
    public class RijndaelStringEncrypter : IEncryptString
    {
        private RijndaelManaged _encryptionProvider;
        private ICryptoTransform _encrypter;
        private ICryptoTransform _decrypter;
        private byte[] _key;
        private byte[] _iv;

        public RijndaelStringEncrypter(ISettingsProvider settings)
        {
            var saltBytes = UTF8Encoding.UTF8.GetBytes(settings.SaltGeneratorKey);
            var derivedbytes = new Rfc2898DeriveBytes(settings.EncryptionString, saltBytes);

            _encryptionProvider = new RijndaelManaged();
            _key = derivedbytes.GetBytes(_encryptionProvider.KeySize / 8);
            _iv = derivedbytes.GetBytes(_encryptionProvider.BlockSize / 8);
        }

        #region IEncryptString Members

        public string Encrypt(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("text");

            if (_encrypter == null)
            {
                _encrypter = _encryptionProvider.CreateEncryptor(_key, _iv);
            }

            var valueBytes = UTF8Encoding.UTF8.GetBytes(value);
            var encryptedBytes = _encrypter.TransformFinalBlock(valueBytes, 0, valueBytes.Length);
            var resp = Convert.ToBase64String(encryptedBytes);

            return resp.Replace('/', '-').Replace('+', '_');
        }

        public string Decrypt(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("cipherText");

            var value_ori = value.Replace('-', '/').Replace('_', '+');
            if (!IsBase64String(value_ori))
                throw new Exception("The cipherText input parameter is not base64 encoded");

            if (_decrypter == null)
            {
                _decrypter = _encryptionProvider.CreateDecryptor(_key, _iv);
            }

            string text;
            var valueBytes = Convert.FromBase64String(value_ori);
            var decryptedBytes = _decrypter.TransformFinalBlock(valueBytes, 0, valueBytes.Length);
            text = UTF8Encoding.UTF8.GetString(decryptedBytes);

            return text;
        }

        #endregion

        private bool IsBase64String(string base64String)
        {
            base64String = base64String.Trim();
            return (base64String.Length % 4 == 0) &&
                   Regex.IsMatch(base64String, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

        }

        #region IDisposable Members

        public void Dispose()
        {
            if (_encrypter != null)
            {
                _encrypter.Dispose();
                _encrypter = null;
            }

            if (_decrypter != null)
            {
                _decrypter.Dispose();
                _decrypter = null;
            }

            if (_encryptionProvider != null)
            {
                _encryptionProvider.Clear();
                _encryptionProvider.Dispose();
                _encryptionProvider = null;
            }
        }

        #endregion

    }
}
