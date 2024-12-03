using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeLibrary
{
    public class Barcode : IBarcode
    {
        private string text = "";

        public Barcode(string text)
        {
            BarcodeType = BarcodeType.Full;
            Text = text;
        }
        public Barcode()
        {
            BarcodeType = BarcodeType.Full;
            Text = text;
        }
        public string Text
        {
            get { return text; }
            set
            {
                if (text == value) return;
                text = value;
                BarcodeText = BarcodeHelper.GetCode(value);
            }
        }

        public string BarcodeText { get; private set; }

        public static BarcodeType BarcodeType { get; set; }

        public override string ToString()
        {
            return BarcodeType switch
            {
                BarcodeType.Barcode => BarcodeText,
                BarcodeType.Text => Text,
                _ => Gettext()
            };
        }
        private string Gettext()
        {
            int width = 0;
            while (BarcodeText[width] != '\n') width++;
            string centeredText = Text.PadLeft((width + Text.Length) / 2);
            return BarcodeText + centeredText;
        }
    }
}
