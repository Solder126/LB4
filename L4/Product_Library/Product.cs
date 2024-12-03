using BarcodeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlotterLibrary
{
    public abstract class Product : IProduct
    {
        /// <summary>
        /// ID товара
        /// </summary>
        private int idproduct;
        /// <summary>
        /// Штрих-код товара
        /// </summary>
        private IBarcode barcode_product;
        /// <summary>
        /// Конструктор товара
        /// </summary>
        /// <param name="brend">Бренд товара</param>
        /// <param name="id">ID товара</param>
        protected Product(string brend, int id, IBarcode barcode)
        {
            barcode_product = barcode;
            Brend = brend;
            idproduct = id;
        }
        /// <summary>
        /// ID товара
        /// </summary>
        public int ID
        {
            get { return idproduct; }
            set
            {
                if (idproduct == value) return;
                int last_id = idproduct;
                idproduct = value;
                barcode_product.Text = idproduct.ToString();
                ID_Changed?.Invoke(this, new Event(last_id, idproduct));
            }
        }
        /// <summary>
        /// Штрихкод товара
        /// </summary>
        public IBarcode Barcode => barcode_product;

        /// <summary>
        /// Бренд товара
        /// </summary>
        public string Brend { get; set; }
        /// <summary>
        /// Информация о товаре
        /// </summary>
        protected abstract string Info { get; }
        /// <summary>
        /// Функция преобразования товара в строку
        /// </summary>
        public override string ToString()
        {
            return GetType().Name.ToString() + ": " + Brend + "\n" + Info + barcode_product;
        }
        public event EventHandler<Event> ID_Changed;
    }
}
