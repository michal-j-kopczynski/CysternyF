using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baza_figur_1
{




    [CysternyF.Bryla("wal")]
    public class Walec : CysternyF.Cysterny1
    {
        public double base_level, height, radius, szukany_volume;
        public override void wczytaj_wymiary(string[] data)
        {
            //Console.WriteLine(data[4]);
            base_level = Convert.ToDouble(data[1]);
            height = Convert.ToDouble(data[2]);
            radius = Convert.ToDouble(data[3]);

        }

        public override double objetosc(double current_level_for_calculations)
        {
            //Console.WriteLine($"baseline: {base_level}, {height}, {current_level_for_calculations}");
            if (current_level_for_calculations <= base_level)
            {
                return 0;
            }

            double available_height = current_level_for_calculations - base_level;
            available_height = Math.Min(height, available_height);
            return available_height * Math.PI * radius * radius;
        }



        public void print()
        {
            Console.WriteLine("Jestem walcem");

        }
    }




    [CysternyF.Bryla("pr")]
    public class Prostopadloscian : CysternyF.Cysterny1
    {
        public double base_level, height, width, depth, szukany_volume;
        public override void wczytaj_wymiary(string[] data)
        {
            //Console.WriteLine(data[4]);
            base_level = Convert.ToDouble(data[1]);
            height = Convert.ToDouble(data[2]);
            width = Convert.ToDouble(data[3]);
            depth = Convert.ToDouble(data[4]);
        }



        public override double objetosc(double current_level_for_calculations)
        {
            //Console.WriteLine($"baseline: {base_level}, {height}, {current_level_for_calculations}");
            if (current_level_for_calculations <= base_level)
            {
                return 0;
            }

            double available_height = current_level_for_calculations - base_level;
            available_height = Math.Min(height, available_height);
            return available_height * depth * width;
        }


        public void print()
        {
            Console.WriteLine("Jestem prostopadłościanem");

        }
    }

    public class figuraBezMetaDanych : CysternyF.Cysterny1
    {
        public void print()
        {
            Console.WriteLine("Jestem pustą figurą");

        }
    }

}


[CysternyF.Bryla("pr_gui")]
public class Prostopadloscian_GUI : CysternyF.Cysterny1
{
    public void print_konsola()
    {
        Console.WriteLine("Jestem gui prostopadłościanem.");

    }
}


[CysternyF.Bryla("wal_gui")]
public class Walec_GUI : CysternyF.Cysterny1
{
    public void print_konsola()
    {
        Console.WriteLine("Jestem gui walcem.");

    }
}
