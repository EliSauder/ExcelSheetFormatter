using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formatter.Utility {
    public static class PathExtention {

        public static string GetRelativePath(string relativeTo, string path) {
            if (string.IsNullOrEmpty(relativeTo)) return path;
            if (string.IsNullOrEmpty(path)) return "";

            relativeTo = Path.GetFullPath(relativeTo);
            path = Path.GetFullPath(path);

            relativeTo.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

            relativeTo = !relativeTo.EndsWith(Path.DirectorySeparatorChar.ToString()) ? relativeTo + Path.DirectorySeparatorChar : relativeTo;
            path = !path.EndsWith(Path.DirectorySeparatorChar.ToString()) ? path + Path.DirectorySeparatorChar : path;

            Uri reltiveToUri = new Uri(relativeTo);
            Uri pathUri = new Uri(path);

            if (reltiveToUri.Scheme != pathUri.Scheme) { return path; } // path can't be made relative.

            Uri relativeUri = reltiveToUri.MakeRelativeUri(pathUri);
            string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            if (pathUri.Scheme.Equals("file", StringComparison.InvariantCultureIgnoreCase)) {
                relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            }

            return relativePath;
        }

    }
}
