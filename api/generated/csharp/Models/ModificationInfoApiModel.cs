// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator 1.0.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.IIoT.Opc.History.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Modification information
    /// </summary>
    public partial class ModificationInfoApiModel
    {
        /// <summary>
        /// Initializes a new instance of the ModificationInfoApiModel class.
        /// </summary>
        public ModificationInfoApiModel()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ModificationInfoApiModel class.
        /// </summary>
        /// <param name="modificationTime">Modification time</param>
        /// <param name="updateType">Operation. Possible values include:
        /// 'Insert', 'Replace', 'Update', 'Delete'</param>
        /// <param name="userName">User who made the change</param>
        public ModificationInfoApiModel(System.DateTime? modificationTime = default(System.DateTime?), HistoryUpdateOperation? updateType = default(HistoryUpdateOperation?), string userName = default(string))
        {
            ModificationTime = modificationTime;
            UpdateType = updateType;
            UserName = userName;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets modification time
        /// </summary>
        [JsonProperty(PropertyName = "modificationTime")]
        public System.DateTime? ModificationTime { get; set; }

        /// <summary>
        /// Gets or sets operation. Possible values include: 'Insert',
        /// 'Replace', 'Update', 'Delete'
        /// </summary>
        [JsonProperty(PropertyName = "updateType")]
        public HistoryUpdateOperation? UpdateType { get; set; }

        /// <summary>
        /// Gets or sets user who made the change
        /// </summary>
        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }

    }
}
