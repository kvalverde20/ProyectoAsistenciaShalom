using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsistenciaShalom.Presentacion.Security;
using Microsoft.AspNetCore.Mvc;

namespace AsistenciaShalom.Presentacion.Controllers
{

    public class BaseController : Controller
    {

        #region Encriptación Rijndael

        public IEncryptString _encryptManager;

        public IEncryptString GetDecrypter()
        {
            if (_encryptManager == null)
            {
                _encryptManager = new RijndaelStringEncrypter(new SettingsProvider());
            }
            return _encryptManager;
        }

        #endregion

    }



}