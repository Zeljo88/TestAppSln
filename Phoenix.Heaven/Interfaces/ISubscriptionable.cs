using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Phoenix.Heaven.Interfaces
{
    public interface ISubscriptionable
    {
        #region Public Properties
        [JsonPropertyName("id")]
        public abstract string Id { get; set; }

        [JsonPropertyName("displayname")]
        public abstract string DisplayName { get; set; }

        [JsonPropertyName("subscriptionid")]
        public abstract string SubscriptionId { get; set; }

        [JsonPropertyName("tenantid")]
        public abstract string TenantId { get; set; }

        [JsonPropertyName("tags")]
        public abstract string Tags { get; set; }

        [JsonPropertyName("resourcegroup")]
        public abstract List<IResourceGroupable> Resources { get; }

        public abstract ITenantable Parent { get; set; }
        #endregion

    }
}
