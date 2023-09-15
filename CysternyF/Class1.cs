using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;


namespace CysternyF
{
    [AttributeUsage(AttributeTargets.Class)]
    public class BrylaAttribute : Attribute
    {
        public string Name { get; }

        public BrylaAttribute(string name)
        {
            Name = name;
        }
    }
    public class Cysterny1
    {
        virtual public double objetosc(double current_level_for_calculations)
        {
            throw new NotImplementedException();
        }
        virtual public void wczytaj_wymiary(string[] data)
        {
            throw new NotImplementedException();
        }


    }
    public class Zadanie
    {
        public static Dictionary<string, Type> dict_figure_types = new Dictionary<string, Type>();
        public object[] cysterny;
        private double szukany_volume;





        public static List<Assembly> zbieraj_assemblies(string relativeDirectoryPath)
        {
            List<Assembly> assemblies = new List<Assembly>();


            // Get the current directory where the executing assembly is located
            string currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // Combine the current directory with the relative directory path
            string absoluteDirectoryPath = Path.Combine(currentDirectory, relativeDirectoryPath);

            // Get all files with .dll extension in the specified directory
            string[] files = Directory.GetFiles(absoluteDirectoryPath, "*.dll");

            // Load each assembly file and add it to the list
            foreach (string file in files)
            {
                Assembly assembly = Assembly.LoadFrom(file);
                assemblies.Add(assembly);
            }


            return assemblies;
        }
        public static void pozbieraj_klasy(Assembly assembly1) // załaduj jedną assembly
        {

            //dict_figure_types.Add("pr", typeof(int));


            // string assemblyPath = "C:/Users/Michał/Desktop/Zdalne zajęcia/4semestr/probiekt2/maj/baza_figur_1/bin/Debug/baza_figur_1.dll";
            // Assembly assembly1 = Assembly.LoadFrom(assemblyPath);

            //Type figureType = assembly1.GetType("baza_figur_1.Walec");

            foreach (Type type in assembly1.GetTypes())
            {
                if (type.Name.EndsWith("_gui"))
                    continue;
                //Console.WriteLine(type.FullName);
                object[] attributes = type.GetCustomAttributes(true);

                // Iterate through the attributes
                foreach (object attribute in attributes)
                {
                    // Print the attribute information
                    //Console.WriteLine($"Attribute applied to {type.Name}: {attribute.GetType().Name}");

                    if (attribute is CysternyF.BrylaAttribute intancjaBryly)
                    {
                        // Access the value of the attribute
                        string attributeValue = intancjaBryly.Name;
                        //Console.WriteLine($"Value of attribute: {attributeValue}\n");
                        Zadanie.dict_figure_types.Add(attributeValue, type);
                    }
                }
            }


            //object figureInstance = Activator.CreateInstance(figureType);

            // Invoke the Draw() method on the Figure object
            //MethodInfo printMethod = figureType.GetMethod("print");
            //printMethod.Invoke(figureInstance, null);



        }



        public static Dictionary<Type, Type> dict_figure_types_gui = new Dictionary<Type, Type>();

        public static void pozbieraj_klasy_gui(Assembly assembly)
        {


            // Get all types in the assembly
            Type[] types = assembly.GetTypes();

            // Iterate over each type
            foreach (Type type in types)
            {

                if (Attribute.IsDefined(type, typeof(BrylaAttribute)))
                {

                    BrylaAttribute attribute = (BrylaAttribute)Attribute.GetCustomAttribute(type, typeof(BrylaAttribute));
                    string metadata = attribute.Name;


                    string guiClassName = metadata + "_gui";


                    Type guiType = Array.Find(types, t => t.Name.Equals(guiClassName));

                    if (guiType != null)
                    {

                        dict_figure_types_gui.Add(type, guiType);
                    }
                }
            }
        }




        public void wczytaj(System.IO.StreamReader tr)
        {
            //załaduj odpowiednią figurę
            string string_temp = tr.ReadLine();
            int how_many_cisterns = Convert.ToInt32(string_temp);
            this.cysterny = new object[how_many_cisterns];
            for (int i = 0; i < how_many_cisterns; i++)
            {

                string_temp = tr.ReadLine();
                string[] strlist = string_temp.Split(' ');
                this.cysterny[i] = Activator.CreateInstance(Zadanie.dict_figure_types[strlist[0]], new object[] { });
                //MethodInfo printMethod = Zadanie.dict_figure_types[strlist[0]].GetMethod("print");
                //printMethod.Invoke(this.cysterny[i], null);

                MethodInfo wczytaj_dane = Zadanie.dict_figure_types[strlist[0]].GetMethod("wczytaj_wymiary");
                wczytaj_dane.Invoke(this.cysterny[i], new object[] { strlist });


                //Console.WriteLine(strlist[1]);
                //Console.WriteLine(strlist[2]);
                //Console.WriteLine(strlist[3]);
                //Console.WriteLine(strlist[4]);

                MethodInfo volumeMethod = cysterny[i].GetType().GetMethod("objetosc");
                volumeMethod.Invoke(this.cysterny[i], new object[] { 3 });

            }
            string_temp = tr.ReadLine();

            this.szukany_volume = Convert.ToDouble(string_temp);

        }

        private static double objetosc_helper(object obj, double currentLevel)
        {
            // Call the volume method on the provided object
            MethodInfo volumeMethod = obj.GetType().GetMethod("objetosc");
            double volume = (double)volumeMethod.Invoke(obj, new object[] { currentLevel });

            //Console.WriteLine("Volume: " + volume);
            return volume;
        }
        private double suma_objetosci(double current_level)
        {
            double temp_suma = 0;
            int length = cysterny.Length;
            for (int i = 0; i < length; i++)
            {
                temp_suma += Zadanie.objetosc_helper(cysterny[i], current_level);
                //Console.WriteLine(temp_suma);
            }
            return temp_suma;
        }


        public double find_level_for_given_volume(double searching_for_volume)
        {
            double lower_bound = 0.0;
            double upper_bound = 2 * 10e9;
            double tolerance = 0.00000001;

            while (upper_bound - lower_bound >= tolerance)
            {
                double current_level = (lower_bound + upper_bound) / 2;
                double current_volume = suma_objetosci(current_level);

                if (current_volume == searching_for_volume)
                {

                    while (suma_objetosci(current_level - 0.001) == searching_for_volume)
                    {
                        current_level -= 0.001;
                    }
                    return current_level;
                }
                else if (current_volume < searching_for_volume)
                {
                    lower_bound = current_level;
                }
                else
                {
                    upper_bound = current_level;
                }
            }

            double temp_suma = (lower_bound + upper_bound) / 2.0;
            while (suma_objetosci(temp_suma - 0.001) == searching_for_volume)
            {
                temp_suma -= 0.001;
            }
            return temp_suma;
        }

        public double rozwiaz()
        {
            //Console.WriteLine($"\n\n{Zadanie.dict_figure_types["st"]}, {Zadanie.dict_figure_types["pr"]}");
            //Console.WriteLine($"test objetosc: {Zadanie.objetosc_helper(cysterny[0], 0)}");
            double temp_result = this.suma_objetosci(4);
            //Console.WriteLine($"rezultat: {temp_result}");
            temp_result = find_level_for_given_volume(this.szukany_volume);
            if (Math.Abs(temp_result - 2 * 10e9) < 1)
            {
                temp_result = -1; //overflow
            }
            return temp_result;
        }

    }

}




