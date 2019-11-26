using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CornAPI.Wallet.HLAPI.Models;
namespace CornAPI.Wallet.HLAPI
{
    /// <summary>
    /// Wallet client interface used to communicate with the wallet implementation
    /// </summary>
    public interface IWalletClient
    {
        /// <summary>
        /// Makes request to the wallet implementation
        /// </summary>
        /// <param name="method">which wallet method to call</param>
        /// <param name="parameters">parameters passed to the wallet call</param>
        /// <returns>Deserialized response from the wallet implementation</returns>
        Task<RawWalletResponse> MakeRequestAsync(ImplementedWalletMethods method, params object[] parameters);

    }
}
