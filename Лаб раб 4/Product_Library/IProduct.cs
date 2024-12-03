using BarcodeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlotterLibrary
{
    public interface IProduct
    {
        int ID { get; set; }
        IBarcode Barcode { get; }
        string Brend { get; }

        event EventHandler<Event> ID_Changed;
    }
}
