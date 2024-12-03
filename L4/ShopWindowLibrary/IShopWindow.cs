using PlotterLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWindowLibrary
{
    public interface IShopWindow<T>
    //where T : class, IProduct
    {
        int ID { get; set; }
        T this[int index] { get; set; }
        T Switch(T product, int index);
        void Push(T product);
        void Push(T product, int index);
        T Pop();
        T Pop(int index);
        void Swap(int index_1, int index_2);
        int SearchByID(int id);
        int SearchByBrend(string name);
        void SortByID();
        void SortByBrend();
    }
}
