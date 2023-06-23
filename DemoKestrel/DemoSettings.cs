using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DemoKestrel
{
    /// <summary>
    /// Settings for the demo project.
    /// </summary>
    public class DemoSettings
    {
        /// <summary>
        /// Is the authorization disable?
        /// Default is false.
        /// </summary>
        [DefaultValue(false)]
        public bool DisableAuthorization { get; set; }

        public static DemoSettings Default() => Newtonsoft.Json.JsonConvert.DeserializeObject<DemoSettings>("{}");
    }
}
