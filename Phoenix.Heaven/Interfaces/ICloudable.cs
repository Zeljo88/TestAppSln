using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Phoenix.Heaven.Enums;

namespace Phoenix.Heaven.Interfaces
{
    public interface ICloudable
    {
        #region Public Properties
        [JsonPropertyName("id")]
        public abstract string Id { get; set; }

        [JsonPropertyName("name")]
        public abstract string Name { get; set; }

        [JsonPropertyName("type")]
        public abstract CloudType Type { get; set; }

        [JsonPropertyName("resourcegroup")]
        public abstract List<ITenantable> Tenants { get; }

        public abstract IHeavenable Parent { get; set; }
        #endregion
    }
}
