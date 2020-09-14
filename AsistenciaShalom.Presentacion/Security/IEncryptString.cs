using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsistenciaShalom.Presentacion.Security
{
    public interface IEncryptString : IDisposable
    {
        string Encrypt(string value);
        string Decrypt(string value);
    }
}
