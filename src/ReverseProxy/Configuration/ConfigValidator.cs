using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarp.ReverseProxy.Configuration
{
    internal sealed class ConfigValidator : IConfigValidator
    {
        private static readonly HashSet<string> _validMethods = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "HEAD", "OPTIONS", "GET", "PUT", "POST", "PATCH", "DELETE", "TRACE",
        };

        // Note this performs all validation steps without short circuiting in order to report all possible errors.
        public ValueTask<IList<Exception>> ValidateClusterAsync(ClusterConfig cluster)
        {
            _ = cluster ?? throw new ArgumentNullException(nameof(cluster));
            var errors = new List<Exception>();

            if (string.IsNullOrEmpty(cluster.ClusterId))
            {
                errors.Add(new ArgumentException("Missing Cluster Id."));
            }

            return new ValueTask<IList<Exception>>(errors);
        }
    }
}
