using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace CysternyTerminal
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //lista assembly = pozbieraj_assemblies()
            string relativeDirectoryPath = "figury_extensions";
            List<Assembly> assemblies = CysternyF.Zadanie.zbieraj_assemblies(relativeDirectoryPath);
            //dla każdego obiektu typu assembly zbieraj klasy
            foreach (Assembly assembly in assemblies)
            {
                Console.WriteLine("Pobieram klasy figur z:");
                Console.WriteLine(assembly.FullName);
                CysternyF.Zadanie.pozbieraj_klasy(assembly);

            }

            System.IO.StreamReader tr = new System.IO.StreamReader("dane.TXT");
            string string_temp = tr.ReadLine();
            int how_many_tasks = Convert.ToInt32(string_temp);
            for (int i = 0; i < how_many_tasks; i++)
            {
                CysternyF.Zadanie zadanie1 = new CysternyF.Zadanie();
                zadanie1.wczytaj(tr);
                double temp_rezultat = zadanie1.rozwiaz();
                if (temp_rezultat == -1)
                {
                    Console.WriteLine("OVERFLOW");
                }
                else
                {
                    double roundedValue = Math.Round(temp_rezultat, 2);
                    Console.WriteLine(roundedValue.ToString("F2"));
                }

            }

            /*
            wczytaj ilosc zadan - 1 liczba/linijka
            while(3 razy działaj)
            {
            CysternyF.Zadanie zadanie1 = new CysternyF.Zadanie();
            zadanie1.wczytaj() //wczytaj tworzy kolekcje (np liste) przypisana do obiektu
            // zadanie1 - w liscie sa wszystkie obiekty typu (figura.jakas_figura z odpowiednio ustawionymi polami)
            zadanie1.rozwiąż()
            //pokaz_zadanie_w_konsoli(zadanie1);
            }
             
             */






        }

    }

}
