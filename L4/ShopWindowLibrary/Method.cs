using PlotterLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWindowLibrary
{
    public static class Method
    {
        public static void UpdateBarcode<T>(this T product, IShopWindow<T> shopwindow)
            where T : class, IProduct
        {
            int index = shopwindow.SearchByID(product.ID);
            product.Barcode.Text = product.ID + " " + shopwindow.ID.ToString() + " " + index.ToString();
        }
    }
}
