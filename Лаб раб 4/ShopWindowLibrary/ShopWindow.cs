using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarcodeLibrary;
using PlotterLibrary;

namespace ShopWindowLibrary
{
    public class ShopWindow<T> : IShopWindow<T>
        where T : class, IProduct
    {
        public delegate void IDChangeDelegate(IShopWindow<T> shopwindow);
        private IDChangeDelegate IDChange;
        private T[] list;
        private static int lastID = 0; // Статическая переменная для хранения последнего ID
        private int ShopID;

        /// <summary>
        /// ID витрины
        /// </summary>
        public int ID
        {
            get => ShopID;
            set
            {
                if (ShopID == value) return;
                ShopID = value;
                IDChange?.Invoke(this);
            }
        }

        /// <summary>
        /// Конструктор создания витрины
        /// </summary>
        /// <param name="size">Вместимость витрины</param>
        private ShopWindow(int size)
        {
            list = new T[size];
            ShopID = lastID++; // Увеличиваем и присваиваем ID
        }
        private ShopWindow(int size, int id)
        {
            list = new T[size];
            lastID = id;
            ShopID = lastID++;
        }

        /// <summary>
        /// Неявное преобразование типа int в тип ShopWindow
        /// </summary>
        /// <param name="size">Число для преобразования</param>
        public static implicit operator ShopWindow<T>(int size)
            => new ShopWindow<T>(size);
        public static implicit operator ShopWindow<T>((int size, int id) L)
            => new ShopWindow<T>(L.size, L.id);
        /// <summary>
        /// Получение товара через индексацию []
        /// </summary>
        /// <param name="index">Номер товара</param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                if (index > list.Length) return null;
                T product = list[index];
                list[index] = null;
                if (product != null)
                {
                    IDChange -= product.UpdateBarcode;
                    product.ID_Changed -= IDChangedBarcode;
                }
                return product;
            }
            set
            {
                if (index > list.Length) return;
                if (list[index] != null) return;
                list[index] = value;
                value.UpdateBarcode(this);
                IDChange += value.UpdateBarcode;
                value.ID_Changed += IDChangedBarcode;
                //list[index] = value;
                //UpdateBarcode(index);
            }
        }
        private void IDChangedBarcode(object sender, Event e)
        {
            if (sender is T product)
            {
                product.UpdateBarcode(this);
            }
        }
        /// <summary>
        /// Обновление штрих-кода товара
        /// </summary>
        /// <param name="index">Номер товара для обновления</param>

        /// <summary>
        /// Замена товара
        /// </summary>
        /// <param name="product">Товар для замены</param>
        /// <param name="index">Номер заменяемого товара</param>
        /// <returns></returns>
        public T Switch(T product, int index)
        {
            T tmp = this[index];
            this[index] = product;
            return tmp;

        }
        /// <summary>
        /// Добавление товара в первую пустую ячейку
        /// </summary>
        /// <param name="product">Товар для добавления</param>
        public void Push(T product)
        {
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] == null)
                {
                    this[i] = product;
                    break;
                }
            }
        }
        /// <summary>
        /// Добавление товара по индексу
        /// </summary>
        /// <param name="product">Товар для добавления</param>
        /// <param name="index">Номер для добавления</param>
        public void Push(T product, int index) { this[index] = product; }
        /// <summary>
        /// Удаление первого товара
        /// </summary>
        public T Pop()
        {
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] != null)
                {
                    return this[i];
                }
            }
            return null;
        }
        /// <summary>
        /// Удаление товара по индексу
        /// </summary>
        /// <param name="index">Номер товара, необходимый удалить</param>
        /// <returns></returns>
        public T Pop(int index) { return this[index]; }
        /// <summary>
        /// Перестановка товаров
        /// </summary>
        /// <param name="index_1">Номер первого товара для перестановки</param>
        /// <param name="index_2">Номер второго товара для перестановки</param>
        public void Swap(int index_1, int index_2)
        {
            (this[index_1], this[index_2]) = (this[index_2], this[index_1]);
        }
        /// <summary>
        /// Поиск товаров по параметру
        /// </summary>
        /// <param name="predicate">Параметр для поиска</param>
        /// <returns></returns>
        public int Search(Predicate<T> predicate)
        {
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] != null && predicate(list[i]))
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// Поиск товаров по ID
        /// </summary>
        /// <param name="id">ID товара для поиска</param>
        /// <returns></returns>
        public int SearchByID(int id)
        {
            return Search(x => x.ID == id);
        }
        /// <summary>
        /// Поиск товаров по названию
        /// </summary>
        /// <param name="name">Название для поиска</param>
        /// <returns></returns>
        public int SearchByBrend(string name)
        {
            return Search(x => x.Brend == name);
        }
        public void Sort(Func<T, T, int> Comparison)
        {
            Array.Sort(list, (x, y) =>
            {
                if (x == null) return 1;
                if (y == null) return -1;
                return Comparison(x, y);
            }
            );
            IDChange.Invoke(this);
        }
        /// <summary>
        /// Сортировка товаров по ID
        /// </summary>
        public void SortByID()
        {
            Sort((x, y) => x.ID.CompareTo(y.ID));
        }
        /// <summary>
        /// Сортировка товаров по имени
        /// </summary>
        public void SortByBrend()
        {
            Sort((x, y) => x.Brend.CompareTo(y.Brend));
        }
        /// <summary>
        /// Преобразование витрины в строку
        /// </summary>
        public override string ToString()
        {
            StringBuilder str_builder = new StringBuilder();
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] == null) { str_builder.Append("\nnull\n\n"); }
                else { str_builder.Append(list[i].ToString() + "\n"); }
            }
            return str_builder.ToString();
        }
        /// <summary>
        /// Обновление штрих-кода всех товаров витрины
        /// </summary>
        //private void SetAllBarcodes()
        //{
        //    for (int i = 0; i < list.Length; i++)
        //    {
        //        if (list[i] != null) { UpdateBarcode(i); }
        //    }
        //}
    }
}
