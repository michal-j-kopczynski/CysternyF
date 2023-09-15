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
using System.Windows.Shapes;



namespace okienkowe_figury
{
    
    using System.Reflection;
    using System.Windows.Media.Media3D;
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        int ile_pr;
        int ile_st;
        public Window1()
        {
            InitializeComponent();
            
        }

        public void wyswietl_figury_gui(object[] cysterny, double temp_rezultat)
        {
            
            ile_pr = ile_st = 0;
            Canvas canvas = new Canvas();

            // Create a blue brush
            // Create a blue brush
            // Create a blue brush
            Brush blueBrush = new SolidColorBrush(Colors.Blue);

            // Calculate the height of the blue rectangle based on temp_rezultat
            double blueRectangleHeight = 10*temp_rezultat;
            if (blueRectangleHeight == -10)
                blueRectangleHeight = 1000;

            // Create a blue rectangle
                Rectangle blueRectangle = new Rectangle
            {
                Width = 800,               // Width of the canvas
                Height = blueRectangleHeight,
                Fill = blueBrush           // Fill with the blue brush
            };

            // Position the blue rectangle
            Canvas.SetLeft(blueRectangle, 0);
            Canvas.SetTop(blueRectangle, 690 - blueRectangleHeight);

            // Add the blue rectangle to the canvas
            canvas.Children.Add(blueRectangle);
            Line line = new Line
            {
                X1 = 0,
                Y1 = 690, 
                X2 = 800,
                Y2 = 690,
                Stroke = Brushes.Black,
                StrokeThickness = 10
            };

            canvas.Children.Add(line);



            for (int i = 0; i < cysterny.Length; i++)
            {

                //instance activate obiekt w zależności od cysterny[i].GetType(), rysuj nim na canvas
                if (cysterny[i].GetType().Equals(CysternyF.Zadanie.dict_figure_types["pr"]) )
                {
                    ile_pr++;
                    FieldInfo fieldInfo = CysternyF.Zadanie.dict_figure_types["pr"].GetField("height");
                    double fieldValue = (double)fieldInfo.GetValue(cysterny[i]);
                    FieldInfo fieldInfo2 = CysternyF.Zadanie.dict_figure_types["pr"].GetField("width");
                    double fieldValue2 = (double)fieldInfo2.GetValue(cysterny[i]);
                    Rectangle rect1 = new Rectangle()
                    {

                        Width = fieldValue2*8,
                        Height = fieldValue*8,
                        Fill = Brushes.DeepSkyBlue,
                        Stroke = Brushes.Black,
                        StrokeThickness = 2
                    };

                     fieldInfo = CysternyF.Zadanie.dict_figure_types["pr"].GetField("base_level");
                     fieldValue = (double)fieldInfo.GetValue(cysterny[i]);
                    
                    Canvas.SetLeft(rect1, 200-i*70);
                    Canvas.SetTop(rect1, 600- fieldValue*8);
                    canvas.Children.Add(rect1);
                }
                

                if (cysterny[i].GetType().Equals(CysternyF.Zadanie.dict_figure_types["wal"]))
                {
                    ile_pr++;
                    FieldInfo fieldInfo = CysternyF.Zadanie.dict_figure_types["wal"].GetField("height");
                    double fieldValue = (double)fieldInfo.GetValue(cysterny[i]);
                    FieldInfo fieldInfo2 = CysternyF.Zadanie.dict_figure_types["wal"].GetField("radius");
                    if (fieldInfo2 == null && CysternyF.Zadanie.dict_figure_types.ContainsKey("wal"))
                    {
                        // Field exists, you can proceed to access it
                        //fieldInfo2 = fieldInfo;
                        // ...
                    }
                    double fieldValue2 = (double)fieldInfo2.GetValue(cysterny[i]);
                    
                    Rectangle rect1 = new Rectangle()
                    {

                        Width = fieldValue2 * 8 *2,
                        Height = fieldValue * 8,
                        Fill = Brushes.DeepPink,
                        Stroke = Brushes.Black,
                        StrokeThickness = 2
                    };

                    fieldInfo = CysternyF.Zadanie.dict_figure_types["wal"].GetField("base_level");
                    fieldValue = (double)fieldInfo.GetValue(cysterny[i]);
                   
                    Canvas.SetLeft(rect1, 200 - i * 70);
                    Canvas.SetTop(rect1, 600 - fieldValue * 8);
                    canvas.Children.Add(rect1);


                }

            }
           
            
            myCanvas.Children.Add(canvas);

        }
    }
}
