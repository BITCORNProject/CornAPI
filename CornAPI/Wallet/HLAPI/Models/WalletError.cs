using CornAPI.Wallet.LLAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CornAPI.Wallet.HLAPI.Models
{
    /// <summary>
    /// Deserialized error object returned by the wallet
    /// </summary>
    public class WalletError
    {

        public WalletErrorCodes Code { get; set; }

        public string Message { get; set; }

    }
}
