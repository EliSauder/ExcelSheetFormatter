using Formatter.UserInterface.Models;
using System;

namespace Formatter.UserInterface.Interfaces {

    [Obsolete("Not used in favor of a simpler system.", false)]
    public interface IBomFormat {
        BomExcelModel GetBomExcelModel();
        ProductNumberModel GetProductNumber();
        void SetProductNumber(string value);
    }
}
