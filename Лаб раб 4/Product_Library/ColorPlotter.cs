using BarcodeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlotterLibrary
{
    public sealed class ColorPlotter : Plotter, IProduct
    {
        /// <summary>
        /// Цвет Плоттера
        /// 
        /// </summary>
        private string PlotterColor { get; }
        /// <summary>
        /// Конструктор цветной лампы
        /// </summary>
        /// <param name="brend">Бренд</param>
        /// <param name="id">ID</param>
        /// <param name="type">Тип плоттера</param>
        /// <param name="power">Мощность плоттера</param>
        /// <param name="color">Цвет плоттера</param>
        public ColorPlotter(string brend, int id, string type, double power, int brightness, string color)
            : base(brend, id, type, power, brightness, new BarcodeRecord(id.ToString()))
        {
            PlotterColor = color;
        }
        IBarcode IProduct.Barcode => new BarcodeRecord(base.ID.ToString());
        /// <summary>
        /// Получение информации о товаре
        /// </summary>
        protected override string Info
            => base.Info + "Plotter Color: " + PlotterColor + "\n";
    }
}

