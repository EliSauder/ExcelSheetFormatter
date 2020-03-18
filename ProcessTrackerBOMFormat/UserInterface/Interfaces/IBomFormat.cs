using ProcessTrackerBOMFormat.UserInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessTrackerBOMFormat.UserInterface.Interfaces {

    [Obsolete("Not used in favor of a simpler system.", false)]
    public interface IBomFormat {
        BomExcelModel GetBomExcelModel();
        ProductNumberModel GetProductNumber();
        void SetProductNumber(string value);
    }
}
