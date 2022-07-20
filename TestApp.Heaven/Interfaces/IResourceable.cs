using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using TestApp.Heaven.Enums;

namespace TestApp.Heaven.Interfaces
{
    public interface IResourceable
    {
        #region Public Properties
        [JsonPropertyName("id")]
        public abstract string Id { get; set; }

        [JsonPropertyName("location")]
        public abstract string Location { get; set; }

        [JsonPropertyName("name")]
        public abstract string Name { get; set; }

        [JsonPropertyName("resourcetype")]
        public abstract ResourceType Type { get; set; }

        [JsonPropertyName("tags")]
        public abstract string Tags { get; set; }

        [JsonPropertyName("parent")]
        public abstract IResourceGroupable Parent { get; set; }
        #endregion
    }
}
