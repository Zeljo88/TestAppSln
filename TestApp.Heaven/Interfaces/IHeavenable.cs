using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TestApp.Heaven.Interfaces
{
    public interface IHeavenable
    {
        #region Public Properties
        [JsonPropertyName("id")]
        public abstract string Id { get; set; }

        [JsonPropertyName("name")]
        public abstract string Name { get; set; }

        [JsonPropertyName("resourcegroup")]
        public abstract List<ICloudable> Clouds { get; }
        #endregion
    }
}
