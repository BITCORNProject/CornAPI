using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CornAPI.Wallet.HLAPI.Models;
using System;

namespace CornAPI.Wallet.HLAPI
{
    /// <summary>
    /// Implements high level methods for wallet server
    /// </summary>
    public static class WalletAPI
    {
        /// <summary>
        /// Gets all implemented high level wallet implementation, 
        /// all methods contain WalletMethodInfoAttribute that can be used to check some metadata about the method.
        /// </summary>
        public static IEnumerable<MethodInfo> GetMethods()
        {
            var methods = typeof(WalletAPI).
                GetMethods(
                BindingFlags.Static
                |BindingFlags.Public)
                .Where((method)=>method.IsDefined(typeof(WalletMethodInfoAttribute)));
            foreach (var method in methods)
            {
                yield return method;
            }
        }
        
        /// <summary>
        /// Returns a new CORN address for receiving payments. 
        /// If [account] is specified payments received with the address will be credited to [account].
        /// </summary>
        /// <param name="walletAccount">Wallet account name for the new address</param>
        /// <returns>Wallet response wrapper; Success: wallet address, Error: WalletError object</returns>
        [WalletMethodInfo(ImplementedWalletMethods.getnewaddress, true)]
        public static async Task<ParsedWalletResponse<string>> GetNewAddressAsync(this IWalletClient client,
            string walletAccount)
        {
            var response = await client.MakeRequestAsync(ImplementedWalletMethods.getnewaddress, walletAccount);
            if (response.Error == null)
            {
                return ParsedWalletResponse<string>.CreateContent(response.Result);
            }
            else
            {
                return ParsedWalletResponse<string>.CreateError(response.Error);
            }
        }

        /// <summary>
        /// Get detailed information about in-wallet transaction <txid>.
        /// </summary>
        /// <param name="txid">Transaction id</param>
        /// <returns>Wallet response wrapper; Success: Transaction info object, Error: WalletError object</returns>
        [WalletMethodInfo(ImplementedWalletMethods.gettransaction, false)]
        public static async Task<ParsedWalletResponse<TransactionInfo>> GetTransactionAsync(this IWalletClient client,
            string txid,
            bool includeWatchonly = false)
        {
            //make internal request to the wallet implementation
            var response = await client.MakeRequestAsync(ImplementedWalletMethods.gettransaction, txid, includeWatchonly);

            if (response.Error == null)
            {
                TransactionInfo transactionInfo = default(TransactionInfo);

                try
                {
                    transactionInfo = JsonConvert.DeserializeObject<TransactionInfo>(response.Result);
                }
                catch(Exception ex)
                {
                    throw ex;
                }

                return ParsedWalletResponse<TransactionInfo>.CreateContent(transactionInfo);
            }
            else
            {
                return ParsedWalletResponse<TransactionInfo>.CreateError(response.Error);
            }
        }

        /// <summary>
        /// Returns an object containing various wallet state info.
        /// </summary>
        /// <returns>Wallet response wrapper; Success: WalletInfo object, Error: WalletError object</returns>
        [WalletMethodInfo(ImplementedWalletMethods.getwalletinfo, false)]
        public static async Task<ParsedWalletResponse<WalletInfo?>> GetWalletInfoAsync(this IWalletClient client)
        {
            //make internal request to the wallet implementation
            var response = await client.MakeRequestAsync(ImplementedWalletMethods.getwalletinfo);

            if (response.Error == null)
            {
                //try deserialize walletinfo
                WalletInfo walletInfo = default(WalletInfo);

                try
                {
                    walletInfo = JsonConvert.DeserializeObject<WalletInfo>(response.Result);
                }
                catch(Exception ex)
                {
                    throw ex;
                }

                return ParsedWalletResponse<WalletInfo?>.CreateContent(walletInfo);
            }
            else
            {
                return ParsedWalletResponse<WalletInfo?>.CreateError(response.Error);
            }
        }

        /// <summary>
        /// Returns Object that has account names as keys, account balances as values.
        /// </summary>
        /// <param name="miniumConfirmations"></param>
        /// <returns>Wallet response wrapper; Success: KeyValuePair array of the account balances, Error: WalletError object</returns>
        [WalletMethodInfo(ImplementedWalletMethods.listaccounts, false)]
        public static async Task<ParsedWalletResponse<KeyValuePair<string,decimal>[]>> ListAccountsAsync(this IWalletClient client,
            int miniumConfirmations = 1,
            bool includeWatchonly = false)
        {          
            //make internal request to the wallet implementation
            var response = await client.MakeRequestAsync(ImplementedWalletMethods.listaccounts);
            if (response.Error == null)
            {
                //convert the response to KeyValuePair
                List<KeyValuePair<string, decimal>> list = new List<KeyValuePair<string, decimal>>();

                try
                {
                    foreach (var item in JObject.Parse(response.Result))
                    {
                        list.Add(new KeyValuePair<string, decimal>(item.Key, (decimal)item.Value));
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }

                return ParsedWalletResponse<KeyValuePair<string, decimal>[]>.CreateContent(list.ToArray());
            }
            else
            {
                return ParsedWalletResponse<KeyValuePair<string, decimal>[]>.CreateError(response.Error);
            }
        }

        /// <summary>
        /// Returns up to [count] most recent transactions skipping the first [from] transactions for account [account].
        /// If [account] not provided it'll return recent transactions from all accounts.
        /// </summary>
        /// <param name="walletAccount">List from this wallet account</param>
        /// <param name="count">How many transactions should be listed</param>
        /// <param name="from">Start index for the listing</param>
        /// <returns>Wallet response wrapper; Success: Json array of the transactions, Error: WalletError object</returns>
        [WalletMethodInfo(ImplementedWalletMethods.listtransactions, false)]
        public static async Task<ParsedWalletResponse<JArray>> ListTransactionsAsync(this IWalletClient client,
            string walletAccount = null,
            int count = 10,
            int from = 0,
            bool includeWatchonly = false)
        {
            RawWalletResponse response = null;

            //this has to be checked for null because if the walletAccount is null in the request object, this will be invalid request
            if (walletAccount != null)
            {
                //make internal request to the wallet implementation
                response = await client.MakeRequestAsync(ImplementedWalletMethods.listtransactions,
                    walletAccount,
                    count,
                    from,
                    includeWatchonly);

            }
            else
            {
                //not defining wallet account in the request is fine, but it cannot be defined as null
                //make internal request to the wallet implementation
                response = await client.MakeRequestAsync(ImplementedWalletMethods.listtransactions);

            }

            if (response.Error == null)
            {
                try
                {
                    var jsonArray = JArray.Parse(response.Result);
                    return ParsedWalletResponse<JArray>.CreateContent(jsonArray);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return ParsedWalletResponse<JArray>.CreateError(response.Error);
            }
        }

        /// <summary>
        /// Move from one account in your wallet to another
        /// </summary>
        /// <param name="fromWalletAccount">Source wallet account name</param>
        /// <param name="toWalletAccount">Target wallet account name</param>
        /// <param name="amount">Transfer amount</param>
        /// <param name="miniumConfirmations">Minium confirmations required for sending</param>
        /// <param name="comment">Optional comment tied to the movement</param>
        /// <returns>Wallet response wrapper; Success: Boolean, returns true if move was succesfull, Error: WalletError object</returns>
        [WalletMethodInfo(ImplementedWalletMethods.move, false)]
        public static async Task<ParsedWalletResponse<bool>> MoveAsync(this IWalletClient client,
            string fromWalletAccount,
            string toWalletAccount,
            decimal amount,
            int miniumConfirmations = 1,
            string comment = "")
        {
            //make internal request to the wallet implementation
            var response = await client.MakeRequestAsync(ImplementedWalletMethods.move,
                fromWalletAccount,
                toWalletAccount,
                amount,
                miniumConfirmations,
                comment);

            if (response.Error == null)
            {
                return ParsedWalletResponse<bool>.CreateContent(bool.Parse(response.Result));
            }
            else
            {
                return ParsedWalletResponse<bool>.CreateError(response.Error);
            }
        }

        /// <summary>
        /// amount is a real and is rounded to 8 decimal places.
        /// Will send the given amount to the given address, ensuring the account has a valid balance using [minconf] confirmations.
        /// Returns the transaction ID if successful
        /// </summary>
        /// <param name="fromWalletAccount">Source wallet account</param>
        /// <param name="toAddress">Target wallet address</param>
        /// <param name="amount">Send amount</param>
        /// <param name="miniumConfirmations">Minium confirmations required for sending</param>
        /// <param name="comment">Optional comment tied to the transaction</param>
        /// <param name="commentTo">Optional comment tied to the transaction</param>
        /// <returns>Wallet response wrapper; Success: transaction id, Error: WalletError object</returns>
        [WalletMethodInfo(ImplementedWalletMethods.sendfrom, true)]
        public static async Task<ParsedWalletResponse<string>> SendFromAsync(this IWalletClient client,
            string fromWalletAccount,
            string toAddress,
            decimal amount,
            int miniumConfirmations = 1,
            string comment = null,
            string commentTo = null)
        {
            //make internal request to the wallet implementation
            var response = await client.MakeRequestAsync(ImplementedWalletMethods.sendfrom,
                fromWalletAccount,
                toAddress,
                amount,
                miniumConfirmations,
                comment,
                commentTo);

            if (response.Error == null)
            {
                return ParsedWalletResponse<string>.CreateContent(response.Result);
            }
            else
            {
                return ParsedWalletResponse<string>.CreateError(response.Error);
            }

        }

        /// <summary>
        /// Send multiple times. Amounts are double-precision floating point numbers
        /// </summary>
        /// <param name="fromWalletAccount">Source wallet account</param>
        /// <param name="toCollection">Collection of the target addresses and payment amounts</param>
        /// <param name="miniumConfirmations">Minium confirmations required for sending</param>
        /// <param name="comment">Optional comment tied to the transaction</param>
        /// <returns>Wallet response wrapper; Success: transaction id, Error: WalletError object</returns>
        [WalletMethodInfo(ImplementedWalletMethods.sendmany, true)]
        public static async Task<ParsedWalletResponse<string>> SendManyAsync(this IWalletClient client,
            string fromWalletAccount,
            Dictionary<string, decimal> toCollection,
            int miniumConfirmations = 1,
            string comment = null)
        {
            //make internal request to the wallet implementation
            var response = await client.MakeRequestAsync(ImplementedWalletMethods.sendmany,
                fromWalletAccount,
                toCollection,
                miniumConfirmations,
                comment);

            if (response.Error == null)
            {
                return ParsedWalletResponse<string>.CreateContent(response.Result);
            }
            else
            {
                return ParsedWalletResponse<string>.CreateError(response.Error);
            }
        }

        /// <summary>
        /// Send an amount to a given address. 
        /// amount is a real and is rounded to 8 decimal places. Returns the transaction ID <txid> if successful.
        /// </summary>
        /// <param name="toAddress">Target address</param>
        /// <param name="amount">Send amount</param>
        /// <param name="comment">Optional comment tied to the transaction</param>
        /// <param name="commentTo">Optional comment tied to the transaction</param>
        /// <returns>Wallet response wrapper; Success: transaction id, Error: WalletError object</returns>
        [WalletMethodInfo(ImplementedWalletMethods.sendtoaddress, true)]
        public static async Task<ParsedWalletResponse<string>> SendToAddressAsync(this IWalletClient client,
            string toAddress,
            decimal amount,
            string comment = null,
            string commentTo = null)
        {
            //make internal request to the wallet implementation
            var response = await client.MakeRequestAsync(ImplementedWalletMethods.sendtoaddress,
                toAddress,
                amount,
                comment,
                commentTo);

            if (response.Error == null)
            {
                return ParsedWalletResponse<string>.CreateContent(response.Result);
            }
            else
            {
                return ParsedWalletResponse<string>.CreateError(response.Error);
            }
        }

        /// <summary>
        /// Returns the list of addresses for the given account.
        /// </summary>
        /// <param name="walletAccount"></param>
        /// <returns></returns>
        [WalletMethodInfo(ImplementedWalletMethods.getaddressesbyaccount, false)]
        public static async Task<ParsedWalletResponse<string[]>> GetAddressesByAccountAsync(this IWalletClient client,
            string walletAccount)
        {
            var response = await client.MakeRequestAsync(ImplementedWalletMethods.getaddressesbyaccount, walletAccount);
            if (response.Error == null)
            {
                string[] content = null;
                try
                {
                    content = JArray.Parse(response.Result).ToObject<string[]>();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return ParsedWalletResponse<string[]>.CreateContent(content);
            }
            else
            {
                return ParsedWalletResponse<string[]>.CreateError(response.Error);
            }

        }

        /// <summary>
        /// PLEASE NOTE! wallet account balances dont work as you would expect! => https://github.com/PIVX-Project/PIVX/wiki/Accounts-Explained
        /// </summary>
        [WalletMethodInfo(ImplementedWalletMethods.getbalance, false)]
        public static async Task<ParsedWalletResponse<decimal?>> GetBalanceAsync(this IWalletClient client,
            string walletAccount = null,
            int miniumConfirmations = 1,
            bool includeWatchonly = false)
        {
            RawWalletResponse response = null;
            if (walletAccount != null)
            {
                response = await client.MakeRequestAsync(ImplementedWalletMethods.getbalance, walletAccount, miniumConfirmations, includeWatchonly);

            }
            else
            {
                response = await client.MakeRequestAsync(ImplementedWalletMethods.getbalance);

            }

            if (response.Error == null)
            {
                decimal? amount = null;
                //parsing the string fails because , or .
                var culture = System.Globalization.CultureInfo.InvariantCulture;
                var style = System.Globalization.NumberStyles.Any;

                if (decimal.TryParse(response.Result, style, culture,out decimal d))
                {
                    amount = d;
                }
                return ParsedWalletResponse<decimal?>.CreateContent(d);
            }
            else
            {
                return ParsedWalletResponse<decimal?>.CreateError(response.Error);
            }
        }
    }

}

