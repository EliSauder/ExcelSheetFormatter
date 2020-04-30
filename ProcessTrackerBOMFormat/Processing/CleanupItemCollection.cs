using Formatter.Configuration;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Formatter.Processing {
    public class CleanupItemCollection : Collection<CleanupItem> {

        public CleanupItem[] GetActions(ConfigurationCleanupActions.CleanupAction action) {

            Collection<CleanupItem> items = new Collection<CleanupItem>();

            foreach(CleanupItem item in this) if (item.Action == action) items.Add(item);

            return items.ToArray();
        }
    }
}
