using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace CompGraphLab1
{
    /// <summary>
    /// Класс для отрисовки замка
    /// </summary>
    class Castle
    {
        /// <summary>
        /// Содержит все отрисовываемые объекты
        /// </summary>
        DrawingObject[] drawingObjects;

        /// <summary>
        /// Создает отрисовываемые объекты
        /// </summary>
        public Castle()
        {
            DrawingObject wall = new DrawingObject()
            {
                matrix = new int[,]{
                { -15, 10},
                { -13, 10},
                { -13, 8},
                { -9, 8},
                { -9, 10},
                { -7, 10},
                { -7, 0},
                { 7, 0},
                { 7, 10},
                { 9, 10},
                { 9, 8},
                { 13, 8},
                { 13, 10},
                { 15, 10},
                { 15, -14},
                { 5, -14},
                { 5, -4},
                { -5, -4},
                { -5, -14},
                { -15, -14}
            }
            };

            DrawingObject flag = new DrawingObject()
            {
                matrix = new int[,]{
                { 11, 8 },
                { 11, 14},
                { 13, 13},
                { 11, 12}
            }
            };

            drawingObjects = new DrawingObject[]
            {
                wall,
                flag,
                new Window(-13, 6),
                new Window(11, 6),
                new Window(-13, -4),
                new Window(11, -4)
            };
        }


        /// <summary>
        /// Рисует замок
        /// </summary>
        /// <param name="graphics">Рисующий объект</param>
        /// <param name="zoom">Масштаб</param>
        /// <param name="rotation">Угол поворота</param>
        /// <param name="offsetX">Смещение X</param>
        /// <param name="offsetY">Смещение Y</param>
        public void Draw(Graphics graphics, int zoom, int rotation, int offsetX, int offsetY)
        {
            /// Угол поворота в радианах
            double angle = rotation * PI / 180;
            /// Матрица для поворота
            double[,] T1 =
            {
                { Cos(angle),    Sin(angle)},
                { -Sin(angle),   Cos(angle)}
            };
            /// Матрица для масшабирования
            double[,] T2 =
            {
                { (double)zoom / 10,    0},
                { 0,                    (double)zoom/ 10}
            };
            /// Матрица для сдвига
            double[,] T3 =
            {
                { 1,    (double)offsetY / 10},
                { (double)offsetX / 10,    1}
            };
            /// Итоговая матрица
            double[,] T = multiply(multiply(T1, T2), T3);   
            foreach (var i in drawingObjects)
            {
                i.draw(graphics, T);
            }
        }

        class Window : DrawingObject
        {
            public Window(int x, int y)
            {
                matrix = new int[,]
                    {
                        { x, y},
                        { x + 2, y},
                        { x + 2, y - 4},
                        { x, y - 4}
                    };
            }
        }
        

        private class DrawingObject
        {
            /// <summary>
            /// Матрица точек
            /// </summary>
            public int[,] matrix { get; set; }

            /// <summary>
            /// Отрисовка объекта
            /// </summary>
            /// <param name="graphics">Рисующий объект</param>
            /// <param name="T">Матрица обработки</param>
            public void draw(Graphics graphics, double[,] T)
            {
                int dx = (int)(graphics.ClipBounds.Width / 50); // Соотношение усл.ед к пикселям
                int dy = (int)(graphics.ClipBounds.Height / 50);
                dx = dy = Math.Min(dx, dy);
                int countRows = matrix.GetLength(0);        //Количество строк
                double[,] matrixDrow = multiply(matrix, T); // Матрица отображения
                for (int i = 0; i < countRows; i++)         // Преобразование от усл. ед. к пикселям
                {
                    matrixDrow[i, 0] = (graphics.ClipBounds.Width / 2 + dx * matrixDrow[i, 0]);
                    matrixDrow[i, 1] = (graphics.ClipBounds.Height / 2 - dy * matrixDrow[i, 1]);
                }
                for (int i = 1; i < countRows; i++) // Рисование
                {
                    graphics.DrawLine(Pens.Black,
                        (float)matrixDrow[i - 1, 0],
                        (float)matrixDrow[i - 1, 1],
                        (float)matrixDrow[i, 0],
                        (float)matrixDrow[i, 1]
                        );
                }
                graphics.DrawLine(Pens.Black,
                        (float)matrixDrow[countRows - 1, 0],
                        (float)matrixDrow[countRows - 1, 1],
                        (float)matrixDrow[0, 0],
                        (float)matrixDrow[0, 1]
                        );
            }
        }

        /// <summary>
        /// Перемножение матриц
        /// </summary>
        public static double[,] multiply(int[,] m1, double[,] m2) 
        {
            double[,] result = new double[m1.GetLength(0), m2.GetLength(1)];

            for (int i = 0; i < m1.GetLength(0); i++)
            {
                for (int j = 0; j < m2.GetLength(1); j++)
                {
                    double sum = 0;

                    for (int k = 0; k < m1.GetLength(1); k++)
                    {
                        sum += m1[i, k] * m2[k, j];
                    }

                    result[i, j] = sum;
                }
            }
            return result;
        }
        /// <summary>
        /// Перемножение матриц
        /// </summary>
        public static double[,] multiply(double[,] m1, double[,] m2)
        {
            double[,] result = new double[m1.GetLength(0), m2.GetLength(1)];

            for (int i = 0; i < m1.GetLength(0); i++)
            {
                for (int j = 0; j < m2.GetLength(1); j++)
                {
                    double sum = 0;

                    for (int k = 0; k < m1.GetLength(1); k++)
                    {
                        sum += m1[i, k] * m2[k, j];
                    }

                    result[i, j] = sum;
                }
            }
            return result;
        }

    }


}
