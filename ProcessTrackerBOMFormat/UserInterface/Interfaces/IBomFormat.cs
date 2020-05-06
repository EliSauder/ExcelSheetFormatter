using Formatter.UserInterface.Models;
using System;

namespace Formatter.UserInterface.Interfaces
{

    [Obsolete("Not used in favor of a simpler system.", false)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IBomFormat'
    public interface IBomFormat
    {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IBomFormat'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IBomFormat.GetBomExcelModel()'
        BomExcelModel GetBomExcelModel();
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IBomFormat.GetBomExcelModel()'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IBomFormat.GetProductNumber()'
        ProductNumberModel GetProductNumber();
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IBomFormat.GetProductNumber()'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IBomFormat.SetProductNumber(string)'
        void SetProductNumber(string value);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IBomFormat.SetProductNumber(string)'
    }
}
