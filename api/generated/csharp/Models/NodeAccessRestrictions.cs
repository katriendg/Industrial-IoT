// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator 1.0.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.IIoT.Opc.Twin.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Runtime;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines values for NodeAccessRestrictions.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum NodeAccessRestrictions
    {
        [EnumMember(Value = "SigningRequired")]
        SigningRequired,
        [EnumMember(Value = "EncryptionRequired")]
        EncryptionRequired,
        [EnumMember(Value = "SessionRequired")]
        SessionRequired
    }
    internal static class NodeAccessRestrictionsEnumExtension
    {
        internal static string ToSerializedValue(this NodeAccessRestrictions? value)
        {
            return value == null ? null : ((NodeAccessRestrictions)value).ToSerializedValue();
        }

        internal static string ToSerializedValue(this NodeAccessRestrictions value)
        {
            switch( value )
            {
                case NodeAccessRestrictions.SigningRequired:
                    return "SigningRequired";
                case NodeAccessRestrictions.EncryptionRequired:
                    return "EncryptionRequired";
                case NodeAccessRestrictions.SessionRequired:
                    return "SessionRequired";
            }
            return null;
        }

        internal static NodeAccessRestrictions? ParseNodeAccessRestrictions(this string value)
        {
            switch( value )
            {
                case "SigningRequired":
                    return NodeAccessRestrictions.SigningRequired;
                case "EncryptionRequired":
                    return NodeAccessRestrictions.EncryptionRequired;
                case "SessionRequired":
                    return NodeAccessRestrictions.SessionRequired;
            }
            return null;
        }
    }
}
