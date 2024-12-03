using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeLibrary;

/// <summary>
/// Вспомогательный класс для генерации штрихкодов
/// </summary>
public static class BarcodeHelper
{
    #region Help

    /// <summary>
    /// Генерация строки штрихкода по тексту.
    /// </summary>
    /// <param name="inputText">Входная строка для конвертации в штрихкод.</param>
    /// <returns>Возвращает строку с паттерном штрихкода.</returns>
    public static string GetCode(string inputText)
    {
        //Сборка штрих-кода из 0 и 1
        StringBuilder result = new StringBuilder();
        List<int> encodedSymbols = new List<int>();

        // Определяем, с какого режима начинать (текст или числа)
        bool isNumericMode = inputText.All(char.IsDigit);
        result.Append(Patterns[isNumericMode ? StartNumbers : StartText]);
        encodedSymbols.Add(isNumericMode ? StartNumbers : StartText);

        // Индекс текущего символа
        int index = 0;

        // Флаг, отвечающий за текущее состояние режима (числовой или текстовый)
        bool modeIsNumeric = isNumericMode;

        // Перебираем все символы входного текста
        while (index < inputText.Length)
        {
            if ((modeIsNumeric && !IsDigits(inputText, index, 2)) ||
                (!modeIsNumeric && IsDigits(inputText, index, 4)))
            {
                modeIsNumeric = !modeIsNumeric;
                AppendPattern(result, encodedSymbols, modeIsNumeric ? SwitchToNumbers : SwitchToText);
            }

            ProcessSymbol(ref index, encodedSymbols, modeIsNumeric, inputText, result);
        }

        // Добавляем контрольную сумму и символ завершения
        int checksum = CalculateChecksum(encodedSymbols);
        AppendPattern(result, encodedSymbols, checksum);
        AppendPattern(result, encodedSymbols, Stop);

        //Сборка готовой строки штрих-кода
        StringBuilder sb = new StringBuilder();
        foreach (var item in $"{Frame}{result}{Frame}".ToString().Split(2))
        {
            sb.Append(ToBarr(item));
        }
        var barcode = sb.ToString();
        var empty = "".PadRight(barcode.Length, '█');
        StringBuilder sb2 = new StringBuilder(empty);
        sb2.AppendLine();
        for (int i = 0; i < Height; i++)
        {
            sb2.AppendLine(barcode);
        }
        sb2.AppendLine(empty);
        return sb2.ToString();
    }

    // Обработка одного символа
    private static void ProcessSymbol(ref int index, IList<int> encodedSymbols, bool isNumeric, string input, StringBuilder result)
    {
        if (isNumeric)
        {
            // Берем пару чисел
            AppendPattern(result, encodedSymbols, Array.IndexOf(NumberSymbols, input.Substring(index, 2)));
            index += 2;
        }
        else
        {
            // Берем один символ текста
            AppendPattern(result, encodedSymbols, Array.IndexOf(TextSymbols, input.Substring(index, 1)));
            index++;
        }
    }

    // Добавление паттерна в штрихкод
    private static void AppendPattern(StringBuilder result, IList<int> encodedSymbols, int patternIndex)
    {
        result.Append(Patterns[patternIndex]);
        encodedSymbols.Add(patternIndex);
    }

    // Проверка, что символы в указанной позиции строки являются числами
    private static bool IsDigits(string input, int index, int length)
    {
        var chars = input.Skip(index).Take(length);
        return chars.Count() == length && chars.All(char.IsDigit);
    }

    // Вычисление контрольной суммы
    private static int CalculateChecksum(IList<int> encodedSymbols)
    {
        int sum = encodedSymbols[0];

        for (int i = 1; i < encodedSymbols.Count; i++)
        {
            sum += i * encodedSymbols[i];
        }

        return sum % 103;
    }
    private static char ToBarr(string item) => Bars[Convert.ToInt32(item, 2)];
    private static IEnumerable<string> Split(this string result, int part)
    {
        return Enumerable.Range(0, result.Length / part)
            .Select(i => result.Substring(i * part, part));
    }
    /// <summary>   
    /// Высота штрихкода (в строках)
    /// </summary>
    private const int Height = 4;

    /// <summary>
    /// Для получения рамки штрихкода по краям
    /// </summary>
    private const string Frame = "0000";

    /// <summary>
    /// Допустимые варианты штрихов
    /// </summary>
    public static readonly char[] Bars = { '█', '▌', '▐', ' ' };

    /// <summary>
    /// Начальный номер паттерна для текстовой строки
    /// </summary>
    private const int StartText = 104;

    /// <summary>
    /// Начальный номер паттерна для числовой строки
    /// </summary>
    private const int StartNumbers = 105;

    /// <summary>
    /// Переключить в числовой режим кодирования
    /// </summary>
    private const int SwitchToNumbers = 99;

    /// <summary>
    /// Переключить в текстовый режим кодирования
    /// </summary>
    private const int SwitchToText = 100;

    /// <summary>
    /// Номер паттерна завершения
    /// </summary>
    private const int Stop = 108;

    /// <summary>
    ///     Доступные паттерны
    /// </summary>
    private static readonly string[] Patterns = {
    "11011001100", "11001101100", "11001100110", "10010011000", "10010001100",
    "10001001100", "10011001000", "10011000100", "10001100100", "11001001000",
    "11001000100", "11000100100", "10110011100", "10011011100", "10011001110",
    "10111001100", "10011101100", "10011100110", "11001110010", "11001011100",
    "11001001110", "11011100100", "11001110100", "11101101110", "11101001100",
    "11100101100", "11100100110", "11101100100", "11100110100", "11100110010",
    "11011011000", "11011000110", "11000110110", "10100011000", "10001011000",
    "10001000110", "10110001000", "10001101000", "10001100010", "11010001000",
    "11000101000", "11000100010", "10110111000", "10110001110", "10001101110",
    "10111011000", "10111000110", "10001110110", "11101110110", "11010001110",
    "11000101110", "11011101000", "11011100010", "11011101110", "11101011000",
    "11101000110", "11100010110", "11101101000", "11101100010", "11100011010",
    "11101111010", "11001000010", "11110001010", "10100110000", "10100001100",
    "10010110000", "10010000110", "10000101100", "10000100110", "10110010000",
    "10110000100", "10011010000", "10011000010", "10000110100", "10000110010",
    "11000010010", "11001010000", "11110111010", "11000010100", "10001111010",
    "10100111100", "10010111100", "10010011110", "10111100100", "10011110100",
    "10011110010", "11110100100", "11110010100", "11110010010", "11011011110",
    "11011110110", "11110110110", "10101111000", "10100011110", "10001011110",
    "10111101000", "10111100010", "11110101000", "11110100010", "10111011110",
    // 100+
    "10111101110", "11101011110", "11110101110", "11010000100", "11010010000",
    "11010011100", "11000111010", "11010111000", "1100011101011"};
    /// <summary>
    ///     Разрешенные символы
    /// </summary>
    private static readonly string[] TextSymbols = {
    " ","!","\"","#","$","%","&","'","(",")",
    "*","+",",","-",".","/","0","1","2","3",
    "4","5","6","7","8","9",":",";","<","=",
    ">","?","@","A","B","C","D","E","F","G",
    "H","I","J","K","L","M","N","O","P","Q",
    "R","S","T","U","V","W","X","Y","Z","[",
    "\\","]","^","_","`","a","b","c","d","e",
    "f","g","h","i","j","k","l","m","n","o",
    "p","q","r","s","t","u","v","w","x","y",
    "z","{","|","|","~"
};
    /// <summary>
    ///     Разрешенные пары числовых строк
    /// </summary>
    private static readonly string[] NumberSymbols = {
    "00","01","02","03","04","05","06","07","08","09",
    "10","11","12","13","14","15","16","17","18","19",
    "20","21","22","23","24","25","26","27","28","29",
    "30","31","32","33","34","35","36","37","38","39",
    "40","41","42","43","44","45","46","47","48","49",
    "50","51","52","53","54","55","56","57","58","59",
    "60","61","62","63","64","65","66","67","68","69",
    "70","71","72","73","74","75","76","77","78","79",
    "80","81","82","83","84","85","86","87","88","89",
    "90","91","92","93","94","95","96","97","98","99"
};

    //  █▀▀▀▀▀█ ▀█▄█▄ █▀▀▀▀▀█
    //  █ ███ █ ▄▀ ▄  █ ███ █
    //  █ ▀▀▀ █ █▀▄▄▀ █ ▀▀▀ █
    //  ▀▀▀▀▀▀▀ █▄▀▄█ ▀▀▀▀▀▀▀
    //  █ ▄ ▀▄▀▀▀ ▄█▄▀▀▀▀█▄▄▀
    //  █▄█▀█ ▀▄▀█ ▀ ▄█▀█  ▀▄
    //  ▀  ▀  ▀▀█▀▀ ███▀▄ ▄▄█
    //  █▀▀▀▀▀█ ▀▄▄▄█▀ ▄▀  █▄
    //  █ ███ █ ▀  █▄ ▀▄▄█▄▄█
    //  █ ▀▀▀ █  █▄▀ ▄█ █▀   
    //  ▀▀▀▀▀▀▀ ▀ ▀ ▀▀▀▀ ▀  ▀

    #endregion
}