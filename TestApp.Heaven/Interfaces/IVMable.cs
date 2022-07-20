using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using TestApp.Heaven.Enums;

namespace TestApp.Heaven.Interfaces
{
    public interface IVMable : IResourceable
    {
        #region Public Properties
        [JsonPropertyName("vmid")]
        public abstract string VmId { get; set; }

        [JsonPropertyName("status")]
        public abstract VmState Status { get; set; }
        #endregion

        #region Public Methods
        bool Start();

        bool Stop();

        bool Restart();
        #endregion
    }
}
