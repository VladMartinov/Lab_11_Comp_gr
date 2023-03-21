using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace ConvertFileToPicture
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            curvgen();
            MessageBox.Show("6-ти угольники сгенерированы в файл Scratch");
        }

        struct Simple
        { public double xx; public double yy; public int ii; };
        Simple s;
        FileInfo my_file = new FileInfo("SCRATCH");
        BinaryWriter fw;
        Random rnd = new Random();
        /* Создание файла Scratch и открытие его на запись */
        void pfopen()
        {
            fw = new BinaryWriter(my_file.Open(FileMode.Create,
            FileAccess.Write));
        }
        /* Запись в файл точки с флагом перемещения */
        void pmove(double x, double y)
        {
            s.xx = x; s.yy = y; s.ii = 0;
            fw.Write(s.xx); fw.Write(s.yy); fw.Write(s.ii);
        }
        /* Запись в файл точки с флагом рисования */
        void pdraw(double x, double y)
        {
            s.xx = x; s.yy = y; s.ii = 1;
            fw.Write(s.xx); fw.Write(s.yy); fw.Write(s.ii);
        }
        /* Закрытие файла */
        void pfclose()
        {
            fw.Close();
        }
        /* Функция, возвращающая новые координаты точки*/
        PointF randomSize(PointF point, int iter)
        {
            PointF point_new = point;
            if(iter == 0)
            {
                point_new.X = point_new.X - ((float)rnd.NextDouble() + 1.0f);
                point_new.Y = point_new.Y + ((float)rnd.NextDouble() + 1.0f);
            }
            else if(iter == 1)
            {
                point_new.X = point_new.X + ((float)rnd.NextDouble() + 1.0f);
                point_new.Y = point_new.Y + ((float)rnd.NextDouble() + 1.0f);
            } else if(iter == 2)
            {
                point_new.X = point_new.X + ((float)rnd.NextDouble() + 1.0f);
                point_new.Y = point_new.Y;
            } else if(iter == 3)
            {
                point_new.X = point_new.X + ((float)rnd.NextDouble() + 1.0f);
                point_new.Y = point_new.Y - ((float)rnd.NextDouble() + 1.0f);
            } else if(iter == 4)
            {
                point_new.X = point_new.X - ((float)rnd.NextDouble() + 1.0f);
                point_new.Y = point_new.Y - ((float)rnd.NextDouble() + 1.0f);
            } else
            {
                point_new.X = point_new.X - ((float)rnd.NextDouble() + 1.0f);
                point_new.Y = point_new.Y;
            }
            
            return point_new;
        }
        /* Главная функция генерации точек для вложенных 6-ти угольников*/
        void curvgen()
        {
            PointF[] arrayPoints = new PointF[]
            {
                new PointF((float) 3.0, (float) 5.5),
                new PointF((float) 6.0, (float) 5.5),
                new PointF((float) 7.5, (float) 3.5),
                new PointF((float) 6.0, (float) 2.5),
                new PointF((float) 3.0, (float) 2.5),
                new PointF((float) 1.5, (float) 3.5),
                new PointF((float) 3.0, (float) 5.5)
            };
             
            pfopen();
            pmove(arrayPoints[0].X, arrayPoints[0].Y);

            for (int i = 1; i <= 20; i++)
            {
                for(int j = 0; j < 7; j++)
                {
                    pdraw(arrayPoints[j].X, arrayPoints[j].Y);
                    arrayPoints[j] = randomSize(arrayPoints[j], j);

                    if (j == 6)
                    {
                        arrayPoints[j].X = arrayPoints[0].X;
                        arrayPoints[j].Y = arrayPoints[0].Y;
                    }
                }

                pmove(arrayPoints[0].X, arrayPoints[0].Y);
            }
            pfclose();
        }
    }
}