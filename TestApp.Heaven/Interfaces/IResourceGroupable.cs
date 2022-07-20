using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TestApp.Heaven.Interfaces
{
    public interface IResourceGroupable
    {
        #region Public Properties
        [JsonPropertyName("id")]
        public abstract string Id { get; set; }

        [JsonPropertyName("location")]
        public abstract string Location { get; set; }

        [JsonPropertyName("name")]
        public abstract string Name { get; set; }

        [JsonPropertyName("type")]
        public abstract string Type { get; set; }

        [JsonPropertyName("tags")]
        public abstract string Tags { get; set; }

        [JsonPropertyName("resources")]
        public abstract List<IResourceable> Resources { get; }

        [JsonPropertyName("parent")]
        public abstract ISubscriptionable Parent { get; set; }
        #endregion
    }
}
