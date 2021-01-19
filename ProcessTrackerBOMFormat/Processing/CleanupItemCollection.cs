using Formatter.Configuration;
using System.Collections.ObjectModel;
using System.Linq;

namespace Formatter.Processing
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CleanupItemCollection'
    public class CleanupItemCollection : Collection<CleanupItem>
    {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CleanupItemCollection'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CleanupItemCollection.GetActions(ConfigurationCleanupActions.CleanupAction)'
        public CleanupItem[] GetActions(ConfigurationCleanupActions.CleanupAction action)
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CleanupItemCollection.GetActions(ConfigurationCleanupActions.CleanupAction)'

            Collection<CleanupItem> items = new Collection<CleanupItem>();

            foreach (CleanupItem item in this) if (item.Action == action) items.Add(item);

            return items.ToArray();
        }
    }
}
