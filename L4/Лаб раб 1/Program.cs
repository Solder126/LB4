using Containers;
using Things;
static void TestLab2()
{
    Container<Thing> showcase = 5;

    var sample = new Plotter(4000, "Dragon", "red", "MSI", 40000);
    var lab2Data = new List<Thing>
    {
        new Plotter(3000, "asd", "blu", "Intel", 3200),
        new Plotter(2000, "dsa", "ulb", "Xiomi", 6400),
        new Plotter(4000, "sad", "green", "CHSU", 12800)
    };
    foreach (var thing in lab2Data)
    {
        showcase.Push(thing);
    }
    showcase[4] = sample;
    sample.Id++;
    Console.WriteLine(sample);
    showcase.SortByName();
    showcase.Id++;
    Console.WriteLine(showcase);
}

static void TestLab3()
{
    var lab3Data = new List<IThing>
    {
        new Plotter(3000, "asd", "blu", "Intel", 3200),
        new Plotter(2000, "dsa", "ulb", "Xiomi", 6400),
        new Plotter(4000, "sad", "green", "CHSU", 12800)
    };
    var lab3Data2 = new List<HPlotter>
    {
        new (5555, "bsf", "red", "CHSU", 2071),
        new (6666, "sabtebed", "purple", "CHSU", 860),
    };

    Container<IThing> a11 = 7;
    IContainer<IThing> a1=a11;
    a1.Id = 1;
    Container<HPlotter> a22 = 3;
    IContainer<HPlotter> a2=a22;
    a2.Id = 10;

    foreach (var thing in lab3Data)
    {
        a1.Push(thing);
    }
    foreach (var thing in lab3Data2)
    {
        a1.Push(thing);
    }
    a1.SortByName();

    var sample1 = new HPlotter(7777, "HP-3000", "red", "HP", 2257);
    var sample2 = new Plotter(4000, "Epson SureColor SC-T5200", "red", "Epson", 40000);

    a2[0] = sample1;
    a1[5] = a2[0];
    a1[6] = sample2;

    a1.Id = 2222;
    sample1.Id++;
    sample2.Id++;

    Console.WriteLine(a1);
    a2[0] = (HPlotter)a1[5];
    Console.WriteLine("####################################################################33");
    Console.WriteLine(a2);
}
TestLab3();