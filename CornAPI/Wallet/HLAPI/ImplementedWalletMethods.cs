using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CornAPI.Wallet.HLAPI
{
    /// <summary>
    /// Lists all implemented wallet methods on the high level api as constants for Attribute access
    /// </summary>
    public enum ImplementedWalletMethods
    {
        /// <summary>
        /// Creates new address in the wallet
        /// </summary>
        getnewaddress,
        /// <summary>
        /// Gets information about transaction 
        /// </summary>
        gettransaction,
        /// <summary>
        /// Gets informaiton about the wallet
        /// </summary>
        getwalletinfo,
        /// <summary>
        /// Lists all accounts in the wallet
        /// </summary>
        listaccounts,
        /// <summary>
        /// Lists transactions in the wallet
        /// </summary>
        listtransactions,
        /// <summary>
        /// Move funds from account a to account b, inside the wallet
        /// </summary>
        move,
        /// <summary>
        /// Send funds from account to address
        /// </summary>
        sendfrom,
        /// <summary>
        /// Batched sendfrom, 1 call to send to multiple addresses
        /// </summary>
        sendmany,
        /// <summary>
        /// Sends funds from the wallet (any account) to address
        /// </summary>
        sendtoaddress,
        /// <summary>
        /// Gets addresses by account
        /// </summary>
        getaddressesbyaccount,
        getbalance
    }
}

