using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Phoenix.Heaven.Interfaces
{
    public interface ITenantable
    {
        #region Public Properties
        [JsonPropertyName("id")]
        public abstract string Id { get; set; }

        [JsonPropertyName("name")]
        public abstract string Name { get; set; }

        [JsonPropertyName("resourcegroup")]
        public abstract List<ISubscriptionable> Subscriptions { get; }

        [JsonPropertyName("parent")]
        public abstract ICloudable Parent { get; set; }
        #endregion
    }
}
