using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarcodeLibrary;

namespace PlotterLibrary
{

    public class Plotter : Product
    {
        /// <summary>
        /// Тип плоттера
        /// </summary>
        private string PlotterType { get; }
        /// <summary>
        /// Мощность плоттера
        /// </summary>
        private double PlotterPower { get; }
        /// <summary>
        /// скорость плоттера
        /// </summary>
        private int PlotterSpeed { get; }
        /// <summary>
        /// Конструктор лампы
        /// </summary>
        /// <param name="brend">Бренд</param>
        /// <param name="id">ID</param>
        /// <param name="type">Тип плоттера</param>
        /// <param name="power">Мощность плоттера</param>
        /// <param name="speed">скорость плоттера</param>
        public Plotter(string brend, int id, string type, double power, int speed)
            : this(brend, id, type, power, speed, new Barcode(id.ToString())) { }
        protected Plotter(string brend, int id, string type, double power, int speed, IBarcode barcode)
            : base(brend, id, barcode)
        {
            PlotterType = type;
            PlotterPower = power;
            PlotterSpeed = speed;
        }

        /// <summary>
        /// Получение информации о товаре
        /// </summary>
        protected override string Info
            => "Plotter Type: " + PlotterType + "\n" +
            "Plotter Power: " + PlotterPower.ToString() + "\n" +
            "Plotter Speed: " + PlotterSpeed.ToString() + "\n";
    }
}
