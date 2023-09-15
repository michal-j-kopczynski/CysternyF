using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace okienkowe_figury
{
    using System.Reflection;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Media3D;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            //lista assembly = pozbieraj_assemblies()

            //dla każdego obiektu typu assembly zbieraj klasy
            string relativeDirectoryPath = "figury_extensions";
            List<Assembly> assemblies = CysternyF.Zadanie.zbieraj_assemblies(relativeDirectoryPath);
            //dla każdego obiektu typu assembly zbieraj klasy
            foreach (Assembly assembly in assemblies)
            {
                CysternyF.Zadanie.pozbieraj_klasy(assembly);
            }

            //dla każdego assembly 

            System.IO.StreamReader tr = new System.IO.StreamReader("dane.TXT");
            string string_temp = tr.ReadLine();
            int how_many_tasks = Convert.ToInt32(string_temp);
            
            TextBlock textBlock = new TextBlock();
            textBlock.Text = $"Graficzna reprezentacja dla {how_many_tasks} zestawów danych."; 
            textBlock.FontSize = 16;
            textBlock.Foreground = Brushes.Black;

            
            Canvas.SetLeft(textBlock, 100); 
            Canvas.SetTop(textBlock, 200); 

            
            myCanvas.Children.Add(textBlock);
            for (int i = 0; i < how_many_tasks; i++)
            {
                CysternyF.Zadanie zadanie1 = new CysternyF.Zadanie();
                zadanie1.wczytaj(tr);
                double temp_rezultat = zadanie1.rozwiaz();

                Window1 instancja_zadania = new Window1();
                instancja_zadania.wyswietl_figury_gui(zadanie1.cysterny, temp_rezultat);
                instancja_zadania.Show();

            }
        }

        

    }
}
