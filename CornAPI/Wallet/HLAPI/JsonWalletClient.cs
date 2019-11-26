using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using CornAPI.Wallet.HLAPI.Models;
using CornAPI.Wallet.LLAPI;
using CornAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CornAPI.Wallet.HLAPI
{
    /// <summary>
    /// Wallet client that Deserializes responses as json
    /// </summary>
    public class JsonWalletClient : LLAPIWalletClient, IWalletClient
    {

        /// <summary>
        /// Handles communication with the wallet server
        /// </summary>
        /// <param name="walletServer">current wallet server</param>
        /// <param name="contentType">request content type</param>
        /// <param name="accessToken">api call access token</param>
        /// <param name="messageHandler">optional message handler to easily extend the httpclient</param>
        public JsonWalletClient(string endpoint,
            string accessToken,
            HttpMessageHandler messageHandler = null) 
            : base(new Uri(endpoint),"application/json",accessToken,messageHandler)
        {
    
        }
        /// <summary>
        /// implement IWalletClient interface
        /// </summary>
        /// <param name="method">wallet method</param>
        /// <param name="parameters">parameters required by the wallet method</param>
        /// <returns>Deserialized wallet response</returns>
        public async Task<RawWalletResponse> MakeRequestAsync(ImplementedWalletMethods method, object[] parameters)
        {
            var response = await MakeInternalRequestAsync(method.ToString(), parameters);

            RawWalletResponse data = new RawWalletResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {

                string json = await response.Content.ReadAsStringAsync();
                data.FromJson(json);

            }
            else
            {
                var error = CreateHttpErrorResponse(response);
                data.Error = error;
            }

            data.StatusCode = response.StatusCode;
            return data;

        }
        /// <summary>
        /// Creates wallet error object with HTTP_ERROR flag indicating that the error was in the request
        /// </summary>
        /// <returns></returns>
        private WalletError CreateHttpErrorResponse(HttpResponseMessage message)
        {
            var error = new WalletError();
            error.Code =  WalletErrorCodes.HTTP_ERROR;
            error.Message = message.StatusCode.ToString();
            return error;
        }

    }
}
