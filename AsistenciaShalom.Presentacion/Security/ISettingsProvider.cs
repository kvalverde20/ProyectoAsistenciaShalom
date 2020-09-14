using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsistenciaShalom.Presentacion.Security
{
    public interface ISettingsProvider
    {
        byte[] EncryptionKey { get; }
        string EncryptionString { get; }
        string EncryptionPrefix { get; }
        string SaltGeneratorKey { get; }
    }
}
