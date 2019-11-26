using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CornAPI.Wallet.LLAPI
{
    /// <summary>
    /// Handles communication with the wallet server
    /// </summary>
    public class LLAPIWalletClient : IDisposable
    {
        /// <summary>
        /// request content type, application/json only supported type at the moment
        /// </summary>
        public string ContentType { get; private set; }
        /// <summary>
        /// internal httpclient
        /// </summary>
        private HttpClient _httpClient = null;
        /// <summary>
        /// api endpoint
        /// </summary>
        private Uri _targetUri = null;
        /// <summary>
        /// Handles communication with the wallet server
        /// </summary>
        /// <param name="targetUri">api endpoint to use</param>
        /// <param name="contentType">request content type</param>
        /// <param name="accessToken">api call access token</param>
        /// <param name="messageHandler">optional message handler to easily extend the httpclient</param>
        public LLAPIWalletClient(Uri targetUri,
            string contentType,
            string accessToken,
            HttpMessageHandler messageHandler = null)
        {

            this._targetUri = targetUri;

            this.ContentType = contentType;
            if (messageHandler != null)
            {
                this._httpClient = new HttpClient(messageHandler, true);
            }
            else
            {
                this._httpClient = new HttpClient();
            }

            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            this._httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));

        }
        /// <summary>
        /// make direct call to the wallet server
        /// </summary>
        /// <param name="method">wallet method</param>
        /// <param name="parameters">parameters required by the wallet method</param>
        /// <returns>HttpResponseMessage returned by the wallet server</returns>
        protected async Task<HttpResponseMessage> MakeInternalRequestAsync(
            string method,
            object[] parameters)
        {

            if (!string.IsNullOrEmpty(method))
            {
                //this is the format the wallet expects the request body to be in
                var json = JsonConvert.SerializeObject(new
                {
                    method = method,
                    @params = parameters
                });

                var content = new StringContent(json, Encoding.UTF8);
                content.Headers.ContentType = new MediaTypeHeaderValue(ContentType);

                return await this._httpClient.PostAsync(_targetUri, content);
            }
            else
            {
                //dont make request to the server if method name is not defined.
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                response.Content = new StringContent("Method name expected.");

                return response;
            }

        }
        /// <summary>
        /// dispose internal http client
        /// </summary>
        public void Dispose()
        {
            if (this._httpClient != null)
            {
                this._httpClient.Dispose();
                this._httpClient = null;
            }
        }
    }
}
