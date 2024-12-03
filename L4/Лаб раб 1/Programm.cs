using BarcodeLibrary;
using PlotterLibrary;
using ShopWindowLibrary;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

namespace LR;

class Main1
{
    public static void Main(string[] args)
    {
        bool prog = true;
        int S = 0;
        var Shop1 = new List<ShopWindow<IProduct>>();
        int Shop1id = 1;
        var Shop2 = new List<ShopWindow<Plotter>>();
        int Shop2id = 1;
        var Shop3 = new List<ShopWindow<ColorPlotter>>();
        int Shop3id = 1;
        var Storage1 = new List<Plotter>();
        int Storage1id = 1;
        var Storage2 = new List<ColorPlotter>();
        int Storage2id = 1;
        while (prog)
        {
            switch (S)
            {
                case 0:
                    Console.WriteLine("Choose what you want to do:\n1-Create a showcase\n2-Create an object\n3-Work with a showcase\n4-Display all objects\n5-End all work\nChoice: ");
                    S = Int32.Parse(Console.ReadLine());
                    break;
                case 1:
                    Console.WriteLine("Select the type of products for the showcase:\n1-For any products\n2-Only for Plotter\n3-Only for colored Plotter\n0-Go back\nChoice: ");
                    int Sh = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Specify the size of the showcase: ");
                    int size = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Choose:\n1-Showcase with a standard ID\n2-Showcase with a custom ID\nChoice: ");
                    int choice = Int32.Parse(Console.ReadLine());
                    int tmpid1 = 0;
                    if (choice == 1)
                    {
                        switch (Sh)
                        {
                            case 0:
                                S = 0;
                                break;
                            case 1:
                                ShopWindow<IProduct> x = size;
                                Shop1.Add(x);
                                tmpid1 = Shop1id++;
                                break;
                            case 2:
                                ShopWindow<Plotter> y = size;
                                Shop2.Add(y);
                                tmpid1 = Shop2id++;
                                break;
                            case 3:
                                ShopWindow<ColorPlotter> z = size;
                                Shop3.Add(z);
                                tmpid1 = Shop3id++;
                                break;
                            default:
                                break;
                        }
                    }
                    else if (choice == 2)
                    {
                        Console.WriteLine("Specify the ID: ");
                        int idd = Int32.Parse(Console.ReadLine());
                        switch (Sh)
                        {
                            case 0:
                                S = 0;
                                break;
                            case 1:
                                ShopWindow<IProduct> x = (size, idd);
                                Shop1.Add(x);
                                tmpid1 = idd;
                                Shop1id++;
                                break;
                            case 2:
                                ShopWindow<Plotter> y = (size, idd);
                                Shop2.Add(y);
                                tmpid1 = idd;
                                Shop2id++;
                                break;
                            case 3:
                                ShopWindow<ColorPlotter> z = (size, idd);
                                Shop3.Add(z);
                                tmpid1 = idd;
                                Shop3id++;
                                break;
                            default:
                                break;
                        }
                    }
                    if (Sh > 0 && Sh < 4) Console.WriteLine("The showcase was successfully created. Its number: " + (tmpid1) + "\n");
                    S = 0;
                    break;
                case 2:
                    Console.WriteLine("Choose which object to create:\n1-Regular Plotter\n2-Colored Plotter\n0-Go back\nChoice: ");
                    int St = Int32.Parse(Console.ReadLine());
                    int tmpid2 = 0;
                    switch (St)
                    {
                        case 0:
                            S = 0;
                            break;
                        case 1:
                            Console.WriteLine("Specify the Plotter brand: ");
                            string brand1 = Console.ReadLine();
                            Console.WriteLine("Specify the Plotter ID: ");
                            int id1 = Int32.Parse(Console.ReadLine());
                            tmpid2 = id1;
                            Console.WriteLine("Specify the Plotter type: ");
                            string type1 = Console.ReadLine();
                            Console.WriteLine("Specify the Plotter power: ");
                            double power1 = double.Parse(Console.ReadLine());
                            Console.WriteLine("Specify the Plotter brightness: ");
                            int brightness1 = Int32.Parse(Console.ReadLine());
                            Storage1.Add(new Plotter(brand1, id1, type1, power1, brightness1));
                            break;
                        case 2:
                            Console.WriteLine("Specify the Plotter brand: ");
                            string brand2 = Console.ReadLine();
                            Console.WriteLine("Specify the Plotter ID: ");
                            int id2 = Int32.Parse(Console.ReadLine());
                            tmpid2 = id2;
                            Console.WriteLine("Specify the Plotter type: ");
                            string type2 = Console.ReadLine();
                            Console.WriteLine("Specify the Plotter power: ");
                            double power2 = double.Parse(Console.ReadLine());
                            Console.WriteLine("Specify the Plotter brightness: ");
                            int brightness2 = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Specify the Plotter color: ");
                            string color2 = Console.ReadLine();
                            Storage2.Add(new ColorPlotter(brand2, id2, type2, power2, brightness2, color2));
                            break;
                        default:
                            break;
                    }
                    if (St > 0 && St < 3) Console.WriteLine("Plotter successfully created. Its ID: " + (tmpid2) + "\n");
                    S = 0;
                    break;
                case 3:
                    Console.WriteLine("Select the type of showcase:\n1-For any products\n2-Only for regular Plotter\n3-Only for colored Plotter\n0-Go back\nChoice: ");
                    int Sp = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Specify the ID of the showcase: ");
                    int idv = Int32.Parse(Console.ReadLine());
                    int ch = 1;
                    while (ch == 1)
                    {
                        switch (Sp)
                        {
                            case 0:
                                S = 0;
                                break;
                            case 1:
                                Console.WriteLine("Choose an action:\n1-Add an object\n2-Remove an object\n3-Show contents\n4-Sort\n5-Search\n0-Go back\nChoice: ");
                                int Ss1 = Int32.Parse(Console.ReadLine());
                                switch (Ss1)
                                {
                                    case 0:
                                        S = 3;
                                        break;
                                    case 1:
                                        Console.WriteLine("Specify the type of object (1-Plotter, 2-ColorPlotter): ");
                                        int typ = Int32.Parse(Console.ReadLine());
                                        Console.WriteLine("Specify the object ID: ");
                                        int ido11 = Int32.Parse(Console.ReadLine());
                                        if (typ == 1)
                                        {
                                            foreach (var shopwind in Shop1)
                                            {
                                                if (shopwind != null && (shopwind.ID == idv))
                                                {
                                                    foreach (var lmp in Storage1)
                                                    {
                                                        if (lmp != null && (lmp.ID == ido11))
                                                        {
                                                            shopwind.Push(lmp);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else if (typ == 2)
                                        {
                                            foreach (var shopwind in Shop1)
                                            {
                                                if (shopwind != null && (shopwind.ID == idv))
                                                {
                                                    foreach (var lmp in Storage2)
                                                    {
                                                        if (lmp != null && (lmp.ID == ido11))
                                                        {
                                                            shopwind.Push(lmp);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                    case 2:
                                        Console.WriteLine("Specify the object ID: ");
                                        int ido2 = Int32.Parse(Console.ReadLine());
                                        foreach (var shopwind in Shop1)
                                        {
                                            if (shopwind != null && (shopwind.ID == idv))
                                            {
                                                shopwind.Pop(shopwind.SearchByID(ido2));
                                            }
                                        }
                                        break;
                                    case 3:
                                        foreach (var shopwind in Shop1)
                                        {
                                            if (shopwind != null && (shopwind.ID == idv))
                                            {
                                                Console.WriteLine(shopwind);
                                            }
                                        }
                                        break;
                                    case 4:
                                        Console.WriteLine("Specify the type of sorting (1-by ID, 2-by brand): ");
                                        int typ2 = Int32.Parse(Console.ReadLine());
                                        foreach (var shopwind in Shop1)
                                        {
                                            if (shopwind != null && (shopwind.ID == idv))
                                            {
                                                if (typ2 == 1) shopwind.SortByID();
                                                else if (typ2 == 2) shopwind.SortByBrend();
                                            }
                                        }
                                        break;
                                    case 5:
                                        Console.WriteLine("Specify the type of search (1-by ID, 2-by brand): ");
                                        int typ3 = Int32.Parse(Console.ReadLine());
                                        if (typ3 == 1)
                                        {
                                            Console.WriteLine("Specify the ID to search for: ");
                                            int search = Int32.Parse(Console.ReadLine());
                                            foreach (var shopwind in Shop1)
                                            {
                                                if (shopwind != null && (shopwind.ID == idv))
                                                {
                                                    shopwind.SearchByID(search);
                                                }
                                            }
                                        }
                                        else if (typ3 == 2)
                                        {
                                            Console.WriteLine("Specify the brand to search for: ");
                                            string search = Console.ReadLine();
                                            foreach (var shopwind in Shop1)
                                            {
                                                if (shopwind != null && (shopwind.ID == idv))
                                                {
                                                    shopwind.SearchByBrend(search);
                                                }
                                            }
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                break;

                            case 2:
                                Console.WriteLine("Choose an action:\n1-Add an object\n2-Remove an object\n3-Show contents\n4-Sort\n5-Search\n0-Go back\nChoice: ");
                                int Ss2 = Int32.Parse(Console.ReadLine());
                                switch (Ss2)
                                {
                                    case 0:
                                        S = 3;
                                        break;
                                    case 1:
                                        Console.WriteLine("Specify the object ID: ");
                                        int ido11 = Int32.Parse(Console.ReadLine());
                                        foreach (var shopwind in Shop2)
                                        {
                                            if (shopwind != null && (shopwind.ID == idv))
                                            {
                                                foreach (var lmp in Storage1)
                                                {
                                                    if (lmp != null && (lmp.ID == ido11))
                                                    {
                                                        shopwind.Push(lmp);
                                                    }
                                                }
                                            }
                                        }
                                        break;

                                    case 2:
                                        Console.WriteLine("Specify the object ID: ");
                                        int ido2 = Int32.Parse(Console.ReadLine());
                                        foreach (var shopwind in Shop2)
                                        {
                                            if (shopwind != null && (shopwind.ID == idv))
                                            {
                                                shopwind.Pop(shopwind.SearchByID(ido2));
                                            }
                                        }
                                        break;
                                    case 3:
                                        foreach (var shopwind in Shop2)
                                        {
                                            if (shopwind != null && (shopwind.ID == idv))
                                            {
                                                Console.WriteLine(shopwind);
                                            }
                                        }
                                        break;
                                    case 4:
                                        Console.WriteLine("Specify the sorting type (1-by ID, 2-by brand): ");
                                        int typ2 = Int32.Parse(Console.ReadLine());
                                        foreach (var shopwind in Shop2)
                                        {
                                            if (shopwind != null && (shopwind.ID == idv))
                                            {
                                                if (typ2 == 1) shopwind.SortByID();
                                                else if (typ2 == 2) shopwind.SortByBrend();
                                            }
                                        }
                                        break;
                                    case 5:
                                        Console.WriteLine("Specify the search type (1-by ID, 2-by brand): ");
                                        int typ3 = Int32.Parse(Console.ReadLine());
                                        if (typ3 == 1)
                                        {
                                            Console.WriteLine("Specify the ID to search for: ");
                                            int search = Int32.Parse(Console.ReadLine());
                                            foreach (var shopwind in Shop2)
                                            {
                                                if (shopwind != null && (shopwind.ID == idv))
                                                {
                                                    shopwind.SearchByID(search);
                                                }
                                            }
                                        }
                                        else if (typ3 == 2)
                                        {
                                            Console.WriteLine("Specify the brand to search for: ");
                                            string search = Console.ReadLine();
                                            foreach (var shopwind in Shop2)
                                            {
                                                if (shopwind != null && (shopwind.ID == idv))
                                                {
                                                    shopwind.SearchByBrend(search);
                                                }
                                            }
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case 3:
                                Console.WriteLine("Choose an action:\n1-Add an object\n2-Remove an object\n3-Show contents\n4-Sort\n5-Search\n0-Go back\nChoice: ");
                                int Ss3 = Int32.Parse(Console.ReadLine());
                                switch (Ss3)
                                {
                                    case 0:
                                        S = 3;
                                        break;
                                    case 1:
                                        Console.WriteLine("Specify the object ID: ");
                                        int ido11 = Int32.Parse(Console.ReadLine());
                                        foreach (var shopwind in Shop3)
                                        {
                                            if (shopwind != null && (shopwind.ID == idv))
                                            {
                                                foreach (var lmp in Storage2)
                                                {
                                                    if (lmp != null && (lmp.ID == ido11))
                                                    {
                                                        shopwind.Push(lmp);
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                    case 2:
                                        Console.WriteLine("Specify the object ID: ");
                                        int ido2 = Int32.Parse(Console.ReadLine());
                                        foreach (var shopwind in Shop3)
                                        {
                                            if (shopwind != null && (shopwind.ID == idv))
                                            {
                                                shopwind.Pop(shopwind.SearchByID(ido2));
                                            }
                                        }
                                        break;
                                    case 3:
                                        foreach (var shopwind in Shop3)
                                        {
                                            if (shopwind != null && (shopwind.ID == idv))
                                            {
                                                Console.WriteLine(shopwind);
                                            }
                                        }
                                        break;
                                    case 4:
                                        Console.WriteLine("Specify the sorting type (1-by ID, 2-by brand): ");
                                        int typ2 = Int32.Parse(Console.ReadLine());
                                        foreach (var shopwind in Shop3)
                                        {
                                            if (shopwind != null && (shopwind.ID == idv))
                                            {
                                                if (typ2 == 1) shopwind.SortByID();
                                                else if (typ2 == 2) shopwind.SortByBrend();
                                            }
                                        }
                                        break;
                                    case 5:
                                        Console.WriteLine("Specify the search type (1-by ID, 2-by brand): ");
                                        int typ3 = Int32.Parse(Console.ReadLine());
                                        if (typ3 == 1)
                                        {
                                            Console.WriteLine("Specify the ID to search for: ");
                                            int search = Int32.Parse(Console.ReadLine());
                                            foreach (var shopwind in Shop3)
                                            {
                                                if (shopwind != null && (shopwind.ID == idv))
                                                {
                                                    shopwind.SearchByID(search);
                                                }
                                            }
                                        }
                                        else if (typ3 == 2)
                                        {
                                            Console.WriteLine("Specify the brand to search for: ");
                                            string search = Console.ReadLine();
                                            foreach (var shopwind in Shop3)
                                            {
                                                if (shopwind != null && (shopwind.ID == idv))
                                                {
                                                    shopwind.SearchByBrend(search);
                                                }
                                            }
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            default:
                                break;
                        }
                        Console.WriteLine("Continue working with the showcase? (1 - yes, 0 - no): ");
                        ch = Int32.Parse(Console.ReadLine());
                        if (ch != 1) S = 0;
                    }
                    break;
                case 4:
                    Console.WriteLine("Displaying all objects in storage:");
                    Console.WriteLine("\nRegular Plotter:");
                    foreach (var Plotter in Storage1)
                    {
                        Console.WriteLine(Plotter);
                    }
                    Console.WriteLine("\nColor Plotter:");
                    foreach (var colorPlotter in Storage2)
                    {
                        Console.WriteLine(colorPlotter);
                    }
                    S = 0;
                    break;
                case 5:
                    Console.WriteLine("Exiting the program");
                    prog = false;
                    break;
                default:
                    break;
            }
        }

    }
}


