using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Phoenix.Heaven.Enums
{
    public enum CloudType
    {
        [Description("Azure")]
        Azure = 1,
        [Description("AWS")]
        AWS = 2,
        [Description("Google Cloud")]
        Google = 3
    }
}
