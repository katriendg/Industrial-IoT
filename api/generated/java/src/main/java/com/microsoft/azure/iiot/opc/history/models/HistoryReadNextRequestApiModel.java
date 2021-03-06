/**
 * Copyright (c) Microsoft Corporation. All rights reserved.
 * Licensed under the MIT License. See License.txt in the project root for
 * license information.
 *
 * Code generated by Microsoft (R) AutoRest Code Generator 1.0.0.0
 * Changes may cause incorrect behavior and will be lost if the code is
 * regenerated.
 */

package com.microsoft.azure.iiot.opc.history.models;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * Request node history read continuation.
 */
public class HistoryReadNextRequestApiModel {
    /**
     * Continuation token to continue reading more
     * results.
     */
    @JsonProperty(value = "continuationToken", required = true)
    private String continuationToken;

    /**
     * Abort reading after this read.
     */
    @JsonProperty(value = "abort")
    private Boolean abort;

    /**
     * Optional request header.
     */
    @JsonProperty(value = "header")
    private RequestHeaderApiModel headerProperty;

    /**
     * Get continuation token to continue reading more
     results.
     *
     * @return the continuationToken value
     */
    public String continuationToken() {
        return this.continuationToken;
    }

    /**
     * Set continuation token to continue reading more
     results.
     *
     * @param continuationToken the continuationToken value to set
     * @return the HistoryReadNextRequestApiModel object itself.
     */
    public HistoryReadNextRequestApiModel withContinuationToken(String continuationToken) {
        this.continuationToken = continuationToken;
        return this;
    }

    /**
     * Get abort reading after this read.
     *
     * @return the abort value
     */
    public Boolean abort() {
        return this.abort;
    }

    /**
     * Set abort reading after this read.
     *
     * @param abort the abort value to set
     * @return the HistoryReadNextRequestApiModel object itself.
     */
    public HistoryReadNextRequestApiModel withAbort(Boolean abort) {
        this.abort = abort;
        return this;
    }

    /**
     * Get optional request header.
     *
     * @return the headerProperty value
     */
    public RequestHeaderApiModel headerProperty() {
        return this.headerProperty;
    }

    /**
     * Set optional request header.
     *
     * @param headerProperty the headerProperty value to set
     * @return the HistoryReadNextRequestApiModel object itself.
     */
    public HistoryReadNextRequestApiModel withHeaderProperty(RequestHeaderApiModel headerProperty) {
        this.headerProperty = headerProperty;
        return this;
    }

}
