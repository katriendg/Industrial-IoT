﻿// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Api.Clients {
    using Microsoft.Azure.IIoT.OpcUa.Api.Models;
    using Microsoft.Azure.IIoT.Diagnostics;
    using Microsoft.Azure.IIoT.Http;
    using Newtonsoft.Json;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Implementation of v1 service adapter.
    /// </summary>
    public class OpcUaTwinApiClient : IOpcUaTwinApi {

        /// <summary>
        /// Create service client
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="config"></param>
        /// <param name="logger"></param>
        public OpcUaTwinApiClient(IHttpClient httpClient,
            IOpcUaTwinConfig config, ILogger logger) {
            _httpClient = httpClient;
            _logger = logger;
            _serviceUri = config.OpcUaTwinServiceUrl;

            if (string.IsNullOrEmpty(_serviceUri)) {
                _serviceUri = "http://localhost:9042/v1";
                _logger.Error(
                    "No opc twin service Uri specified.Using default " +
                    _serviceUri + ". If this is not your intention, or to " +
                    "remove this error, please configure the Url " +
                    "in the appsettings.json file or set the " +
                    "PCS_OPCTWIN_WEBSERVICE_URL environment variable.",
                    () => {});
            }
        }

        /// <summary>
        /// Returns service status
        /// </summary>
        /// <returns></returns>
        public async Task<StatusResponseApiModel> GetServiceStatusAsync() {
            var request = NewRequest($"{_serviceUri}/status");
            var response = await _httpClient.GetAsync(request).ConfigureAwait(false);
            response.Validate();
            return JsonConvertEx.DeserializeObject<StatusResponseApiModel>(response.Content);
        }


        /// <summary>
        /// Browse a tree node, returns node properties and all child nodes if not excluded.
        /// </summary>
        /// <param name="twinId">Server twin to talk to</param>
        /// <param name="content">browse node and filters</param>
        /// <returns></returns>
        public async Task<BrowseResponseApiModel> NodeBrowseAsync(string twinId,
            BrowseRequestApiModel content) {
            if (string.IsNullOrEmpty(twinId)) {
                throw new ArgumentNullException(nameof(twinId));
            }
            var request = NewRequest($"{_serviceUri}/browse/{twinId}");
            request.SetContent(content);
            var response = await _httpClient.PostAsync(request).ConfigureAwait(false);
            response.Validate();
            return JsonConvert.DeserializeObject<BrowseResponseApiModel>(response.Content);
        }

        /// <summary>
        /// Publish node values
        /// </summary>
        /// <param name="twinId">Server twin to talk to</param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<PublishResponseApiModel> NodePublishAsync(string twinId,
            PublishRequestApiModel content) {
            if (string.IsNullOrEmpty(twinId)) {
                throw new ArgumentNullException(nameof(twinId));
            }
            if (content == null) {
                throw new ArgumentNullException(nameof(content));
            }
            if (string.IsNullOrEmpty(content.NodeId)) {
                throw new ArgumentNullException(nameof(content.NodeId));
            }
            var request = NewRequest($"{_serviceUri}/publish/{twinId}");
            request.SetContent(content);
            var response = await _httpClient.PostAsync(request).ConfigureAwait(false);
            response.Validate();
            return JsonConvertEx.DeserializeObject<PublishResponseApiModel>(response.Content);
        }

        /// <summary>
        /// Get list of published nodes
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="twinId">Server twin to talk to</param>
        /// <returns></returns>
        public async Task<PublishedNodeListApiModel> ListPublishedNodesAsync(string continuation,
            string twinId) {
            if (string.IsNullOrEmpty(twinId)) {
                throw new ArgumentNullException(nameof(twinId));
            }
            var request = NewRequest($"{_serviceUri}/publish/{twinId}/state");
            if (continuation != null) {
                request.AddHeader(kContinuationTokenHeaderKey, continuation);
            }
            var response = await _httpClient.GetAsync(request).ConfigureAwait(false);
            response.Validate();
            return JsonConvertEx.DeserializeObject<PublishedNodeListApiModel>(response.Content);
        }

        /// <summary>
        /// Read a variable value
        /// </summary>
        /// <param name="twinId"></param>
        /// <param name="content">Read nodes</param>
        /// <returns></returns>
        public async Task<ValueReadResponseApiModel> NodeValueReadAsync(string twinId,
            ValueReadRequestApiModel content) {
            if (string.IsNullOrEmpty(twinId)) {
                throw new ArgumentNullException(nameof(twinId));
            }
            if (content == null) {
                throw new ArgumentNullException(nameof(content));
            }
            if (string.IsNullOrEmpty(content.NodeId)) {
                throw new ArgumentException(nameof(content.NodeId));
            }
            var request = NewRequest($"{_serviceUri}/read/{twinId}");
            request.SetContent(content);
            var response = await _httpClient.PostAsync(request).ConfigureAwait(false);
            response.Validate();
            return JsonConvertEx.DeserializeObject<ValueReadResponseApiModel>(response.Content);
        }

        /// <summary>
        /// Write variable value
        /// </summary>
        /// <param name="twinId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<ValueWriteResponseApiModel> NodeValueWriteAsync(string twinId,
            ValueWriteRequestApiModel content) {
            if (string.IsNullOrEmpty(twinId)) {
                throw new ArgumentNullException(nameof(twinId));
            }
            if (content == null) {
                throw new ArgumentNullException(nameof(content));
            }
            if (content.Node == null) {
                throw new ArgumentNullException(nameof(content.Node));
            }
            if (content.Value == null) {
                throw new ArgumentNullException(nameof(content.Value));
            }
            if (string.IsNullOrEmpty(content.Node.Id)) {
                throw new ArgumentException(nameof(content.Node.Id));
            }
            if (string.IsNullOrEmpty(content.Node.DataType)) {
                throw new ArgumentException(nameof(content.Node.DataType));
            }
            var request = NewRequest($"{_serviceUri}/write/{twinId}");
            request.SetContent(content);
            var response = await _httpClient.PostAsync(request).ConfigureAwait(false);
            response.Validate();
            return JsonConvert.DeserializeObject<ValueWriteResponseApiModel>(response.Content);
        }

        /// <summary>
        /// Get method meta data
        /// </summary>
        /// <param name="twinId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<MethodMetadataResponseApiModel> NodeMethodGetMetadataAsync(
            string twinId, MethodMetadataRequestApiModel content) {
            if (string.IsNullOrEmpty(twinId)) {
                throw new ArgumentNullException(nameof(twinId));
            }
            if (content == null) {
                throw new ArgumentNullException(nameof(content));
            }
            if (string.IsNullOrEmpty(content.MethodId)) {
                throw new ArgumentNullException(nameof(content.MethodId));
            }
            var request = NewRequest($"{_serviceUri}/call/{twinId}/$metadata");
            request.SetContent(content);
            var response = await _httpClient.PostAsync(request).ConfigureAwait(false);
            response.Validate();
            return JsonConvertEx.DeserializeObject<MethodMetadataResponseApiModel>(response.Content);
        }

        /// <summary>
        /// Call method
        /// </summary>
        /// <param name="twinId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<MethodCallResponseApiModel> NodeMethodCallAsync(
            string twinId, MethodCallRequestApiModel content) {
            if (string.IsNullOrEmpty(twinId)) {
                throw new ArgumentNullException(nameof(twinId));
            }
            if (content == null) {
                throw new ArgumentNullException(nameof(content));
            }
            if (string.IsNullOrEmpty(content.MethodId)) {
                throw new ArgumentNullException(nameof(content.MethodId));
            }
            var request = NewRequest($"{_serviceUri}/call/{twinId}");
            request.SetContent(content);
            var response = await _httpClient.PostAsync(request).ConfigureAwait(false);
            response.Validate();
            return JsonConvertEx.DeserializeObject<MethodCallResponseApiModel>(response.Content);
        }

        /// <summary>
        /// Helper to create new request
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private static HttpRequest NewRequest(string uri) {
            var request = new HttpRequest();
            request.SetUriFromString(uri);
            if (uri.ToLowerInvariant().StartsWith("https:", StringComparison.Ordinal)) {
                request.Options.AllowInsecureSSLServer = true;
            }
            return request;
        }

        private const string kContinuationTokenHeaderKey = "x-ms-continuation";
        private const string kPageSizeHeaderKey = "x-ms-max-item-count";
        private readonly IHttpClient _httpClient;
        private readonly ILogger _logger;
        private readonly string _serviceUri;
    }
}
