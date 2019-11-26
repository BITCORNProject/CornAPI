using System;

namespace CornAPI.Wallet.HLAPI
{
    /// <summary>
    /// Contains meta data about the high level wallet method implementation
    /// </summary>
    public class WalletMethodInfoAttribute : Attribute
    {
        /// <summary>
        /// Command line method that will be called, for example 'sendfrom'
        /// </summary>
        public ImplementedWalletMethods CliName { get; set; }
        /// <summary>
        /// Will this method require the wallet to be decrypted?
        /// </summary>
        public bool RequireDecrypted { get; set; }

        public WalletMethodInfoAttribute(ImplementedWalletMethods name,
            bool requireUnlock)
        {
            this.CliName = name;
            this.RequireDecrypted = requireUnlock;
            
        }
    }
}

