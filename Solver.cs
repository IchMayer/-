using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_lab1
{
    public class Solver
    {
        /// <summary>
        /// Точность решения
        /// </summary>
        public static double EPS = 1e-3;
        /// <summary>
        /// Количество итераций
        /// </summary>
        public static int LastIters;
        /// <summary>
        /// Количество вычисления функции
        /// </summary>
        public static int FuncIters;
        public static List<StepInfo> LastSteps = new List<StepInfo>();


        public struct Point
        {
            public Point(double x0 = 0, double y0 = 0)
            {
                x = x0; y = y0;
            }
            public double x { set; get; }
            public double y { set; get; }

            public double Norm()
            {
                return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            }
            public PointT Transp()
            {
                return new PointT(x, y);
            }

            public static Point operator -(Point a)
            {
                return new Point(-a.x, -a.y);
            }
            public static Point operator -(Point a, Point b)
            {
                return new Point(a.x - b.x, a.y - b.y);
            }
            public static Point operator *(double b, Point a)
            {
                return new Point(b * a.x, b * a.y);
            }
            public static Point operator +(Point a, Point b)
            {
                return new Point(a.x + b.x, a.y + b.y);
            }
            public static double operator *(PointT a, Point b)
            {
                return a.x * b.x + a.y * b.y;
            }
            public static Matrix operator *(Point a, PointT b)
            {
                return new Matrix( a.x * b.x, a.y * b.x, a.x*b.y, a.y * b.y);
            }
        }
        #region lab4

        /// <summary>
        /// рандомный Алгоритм из лабораторной работы #4
        /// </summary>
        /// <param name="func"> функция где ищится минимум </param>
        /// <param name="m"> Количество попыток найти минимум </param>
        /// <returns> Найденный минимум </returns>
        static public Point RandomAlgaritm(Func<Point, double> func, int m)
        {
            FuncIters = 1;
            Random rand = new Random();
            Point min = new Point(rand.NextDouble() * 20 - 10, rand.NextDouble() * 20 - 10);
            double fmin = func(min);

            Point y;

            int n = 0;

            while (n < m)
            {
                y = new Point(rand.NextDouble() * 20 - 10, rand.NextDouble() * 20 - 10);
                FuncIters++;
                if (fmin < func(y))
                    n++;
                else
                {
                    min = y;
                    fmin = func(y);
                    //n = 0;
                }
                Console.Write($"\r {n} из {m}  -   {100.0 * n / m}%  F: {fmin}                                     ");
            }
            Console.WriteLine();
            Console.WriteLine($"N: {m}   Точность решения: {-9.17170610839834 - func(min)}  Func iter: {FuncIters}");
            return min;
        }

        /// <summary>
        /// Алгоритм 1 из лабораторной работы #4
        /// </summary>
        /// <param name="func"> функция где ищится минимум </param>
        /// <param name="m"> Количество попыток найти минимум </param>
        /// <param name="h"> Штрафные функции </param>
        /// <returns> Найденный минимум </returns>
        static public Point Algoritm1(Func<Point, double> func, int m, List<Func<Point, double>> h)
        {
            FuncIters = 0;
            Random rand = new Random();
            Point x = new Point(rand.NextDouble()* 20 - 10, rand.NextDouble() * 20 - 10);
            Point min = Solve_Penalty_function(func, h, x);
            //Point min = MSG_Fletchera_Riversa(func, x);
            Point y;
            double fmin = func(min);
            FuncIters++;
            int n = 0;

            while(n < m)
            {
                y = Solve_Penalty_function(func, h, new Point(rand.NextDouble() * 20 - 10, rand.NextDouble() * 20 - 10));
                //MSG_Fletchera_Riversa(func, new Point(rand.NextDouble() * 20 - 10, rand.NextDouble() * 20 - 10));
                FuncIters += 2;
                if ( fmin > func(y))
                {
                    min = y;
                    fmin = func(min);
                    FuncIters++;
                }
                else
                    n++;
                Console.Write($"\r {n} из {m}  -   {100.0 * n / m}%  F: {fmin}                                     ");
            }

            Console.WriteLine();
            Console.WriteLine($"N: {m}   Точность решения: {-9.17170610839834 - func(min)}  Func iter: {FuncIters}");
            return min;
        }


        /// <summary>
        /// Алгоритм 2 из лабораторной работы #4
        /// </summary>
        /// <param name="func"> функция где ищится минимум </param>
        /// <param name="m"> Количество попыток найти минимум </param>
        /// <param name="h"> Штрафные функции </param>
        /// <returns> Найденный минимум </returns>
        static public Point Algoritm2(Func<Point, double> func, int m, List<Func<Point, double>> h)
        {
            FuncIters = 0;
            Random rand = new Random();
            Point x = new Point(rand.NextDouble() * 20 - 10, rand.NextDouble() * 20 - 10);
            Point min = Solve_Penalty_function(func, h, x);
            //Point min = MSG_Fletchera_Riversa(func, x);
            Point y;
            double fmin = func(min);
            FuncIters++;

            int n = 0;

            while (n < m)
            {
                y = new Point(rand.NextDouble() * 20 - 10, rand.NextDouble() * 20 - 10);
                FuncIters++;
                if (fmin < func(y))
                    n++;
                else
                {
                    min = Solve_Penalty_function(func, h, y);
                    fmin = func(min);
                    FuncIters++;
                    n = 0;
                }
                Console.Write($"\r {n} из {m}  -   {100.0 * n / m}%  F: {fmin}                                       ");
            }

            Console.WriteLine();
            Console.WriteLine($"N: {m}   Точность решения: {-9.17170610839834 - func(min)}  Func iter: {FuncIters}");
            return min;
        }

        /// <summary>
        /// Алгоритм 3 Из лаюораторной работы #4
        /// </summary>
        /// <param name="func"> Функция где ищится минимум </param>
        /// <param name="m"> Количество попыток </param>
        /// <param name="h"> Штрафные функции </param>
        /// <param name="dx"> Длина шага </param>
        /// <returns> Найденную точку минимума </returns>
        static public Point Algoritm3(Func<Point, double> func, int m, List<Func<Point, double>> h, double dx)
        {
            Random rand = new Random();
            FuncIters = 0;
            Point x = new Point(rand.NextDouble() * 20 - 10, rand.NextDouble() * 20 - 10);
            Point min = Solve_Penalty_function(func, h, x);
            Point S = new Point(); // Квадрат x найправления в котором ищим минимум
            //Point min = MSG_Fletchera_Riversa(func, x);
            Point y;
            double fmin = func(min);   //Значение функции в найденном минимуме
            FuncIters++;
            int w;

            bool f;         //Показывает вышла ли точка за пределы области

            int n = 0;

            while (n < m)
            {
                f = true;
                S.x = rand.NextDouble() * 2 - 1;
                S.y = dx * Math.Sqrt(1 - S.x * S.x) *  Math.Sign(rand.NextDouble() - 0.5);
                S.x *= dx;
                w = 1;

                do
                {
                    // Console.WriteLine($"Func: {fmin},   {func(min + w * S)}");
                    FuncIters++;
                    if(fmin > func(min + w * S))
                    {
                        min = Solve_Penalty_function(func, h, min + w * S);
                        fmin = func(min);
                        FuncIters++;
                        n = 0;
                        break;
                    }

                    for (int i = 0; i < h.Count; i++)
                        if (h[i](min + w * S) > 0)
                        {
                            n++;
                            f = false;
                            break;
                        }
                    w++;
                }while (f);

                if (f)
                    n++;

                Console.Write($"\r {n} из {m}  -   {100.0 * n / m}%  F: {fmin}                                       ");
                //Console.WriteLine($"Func: {func(min)},   {min.x}, {min.y}");
            }

            Console.WriteLine();
            Console.WriteLine($"N: {m}   Точность решения: {-9.17170610839834 - func(min)}  Func iter: {FuncIters}");
            //Console.WriteLine($"Func: {func(min)}");
            return min;
        }

        #endregion
        #region lab3

        /// <summary>
        /// Метод штрафных функций
        /// </summary>
        /// <param name="func">Основная функция(минимум которой мы и должны найти)</param>
        /// <param name="h">Функции штрафа</param>
        /// <param name="p0">Начальная точка поиска</param>
        /// <returns>Минимальное значение в заданной области, ограниченной штрафными функциями</returns>
        static public Point Solve_Penalty_function(Func<Point, double> func, List<Func<Point, double>> h, Point p0 = new Point())
        {
            Point x = p0;

            //FuncIters = 0;
            //LastIters = 0;
            int ped = 22;

            //Коэфициент функции штрафа
            List<double> r = new List<double>();

            //Добавка для коэффициента функции штрафа  на каждом шаге
            List<double> value = new List<double>();

            //Коэфициент добавки
            double w = 2;

            for (int i = 0; i < h.Count; i++)
            {
                r.Add(15);
                value.Add(0);
            }

            Func<Point, double> Q = p =>
            {
                double Penatly = 0;
                for (int i = 0; i < h.Count; i++)
                    Penatly += r[i] * h[i](p);
                return func(p) + Penatly;
            };

            //Console.WriteLine("i".PadLeft(4) + "".PadLeft(ped) + "(x,y)".PadRight(ped) +  "f(x, y)".PadLeft(ped) + "r".PadLeft(ped));

            int end;

            var xlast = x;

            do
            {
                end = 0;
                for (int i = 0; i < h.Count; i++)
                    r[i] *= 2;// w * value[i];

                x = Devidona_Fletchera_Paualla_Metod(Q, x); /*MSG_Fletchera_Riversa(Q, x);*/  //Gause(Q, x);

                for (int i = 0; i < h.Count; i++)
                    value[i] = h[i](x);

                LastIters++;

                //Console.Write(LastIters.ToString().PadLeft(4) +
                //    x.x.ToString().PadLeft(ped) +
                //    x.y.ToString().PadLeft(ped) +
                //    Q(x).ToString().PadLeft(ped));

                //for (int i = 0; i < h.Count; i++)
                //    Console.Write(value[i].ToString().PadLeft(ped));
                //Console.Write("\n");

                for (int i = 0; i < h.Count; i++)
                    if (value[i] <= EPS)
                        end++;
            } while (end < h.Count && (xlast - x).Norm() > EPS);

            //Console.WriteLine("\n\nEPS: " + EPS);
            //Console.WriteLine("x0: \t(" + p0.x + " , " + p0.y + ")");
            //Console.WriteLine("Iter: " + LastIters);
            //Console.WriteLine("Func iter: " + FuncIters);
            //Console.WriteLine("Res: \t(" + x.x + " , " + x.y + ")");
            //Console.WriteLine("Func: " + func(x));

            return x;
        }
        /// <summary>
        /// Метод барьерных функций
        /// </summary>
        /// <param name="func">Основная функция</param>
        /// <param name="h">Функции барьеров</param>
        /// <param name="p0">Начальная точка поиска</param>
        /// <returns>Минимальное значение в заданной области, ограничкнной барьерными функциями</returns>
        static public Point Solve_Barrier_function(Func<Point, double> func, List<Func<Point, double>> h, Point p0 = new Point())
        {
            Point x = p0;

            FuncIters = 0;
            LastIters = 0;
            int ped = 22;

            //Коэфициент функции штрафа
            List<double> r = new List<double>();

            //Добавка для коэффициента функции штрафа  на каждом шаге
            List<double> value = new List<double>();

            //Коэфициент добавки
            double w = 100;

            for (int i = 0; i < h.Count; i++)
            {
                r.Add(100);
                value.Add(0);
            }

            Func<Point, double> Q = p =>
            {
                double Penatly = 0;
                for (int i = 0; i < h.Count; i++)
                    Penatly += r[i] * h[i](p);
                return func(p) + Penatly;
            };

            Console.WriteLine("i".PadLeft(4) + "".PadLeft(ped) + "(x,y)".PadRight(ped) + "f(x, y)".PadLeft(ped) + "r".PadLeft(ped));

            int end;

            var xlast = x;

            do
            {
                end = 0;
                for (int i = 0; i < h.Count; i++)
                    r[i] /= w; //* value[i];

                xlast = x;
                x = Gause(Q, x);


                for (int i = 0; i < h.Count; i++)
                    value[i] = h[i](x);

                LastIters++;

                Console.Write(LastIters.ToString().PadLeft(4) +
                    x.x.ToString().PadLeft(ped) +
                    x.y.ToString().PadLeft(ped) +
                    Q(x).ToString().PadLeft(ped));

                for (int i = 0; i < h.Count; i++)
                    Console.Write(value[i].ToString().PadLeft(ped));
                Console.Write("\n");

                for (int i = 0; i < h.Count; i++)
                    if (Math.Abs(value[i]) <= EPS)
                        end++;
            } while ( (xlast - x).Norm() > EPS /*end < h.Count*/);

            Console.WriteLine("\n\nEPS: " + EPS);
            Console.WriteLine("x0: \t(" + p0.x + " , " + p0.y + ")");
            Console.WriteLine("Iter: " + LastIters);
            Console.WriteLine("Func iter: " + FuncIters);
            Console.WriteLine("Res: \t(" + x.x + " , " + x.y + ")");
            Console.WriteLine("Func: " + func(x));

            return x;
        }

        /// <summary>
        /// Метод поиска минимума на 2d метдом Гаусса
        /// </summary>
        /// <param name="func">Функция</param>
        /// <param name="p0">Начальная точка</param>
        /// <returns>Минимальное значение в заданной области</returns>
        static public Point Gause(Func<Point, double> func, Point p0 = new Point())
        {
            Point x = p0;
            Point xlast;

            Point ex = new Point(1, 0);//  1 0
            Point ey = new Point(0, 1);// 0 1

            Point p;

            double lambda;

            Func<double, double> f;
            do
            {
                lambda = 1;
                xlast = x;

                f = landa => func(x + landa * ex);
                p = Find_Line(lambda, f);
                lambda = Solve_GoldSech(p.x, p.y, f);

                x += lambda * ex;

                lambda = 1;
                f = landa => func(x + landa * ey);
                p = Find_Line(lambda, f);
                lambda = Solve_GoldSech(p.x, p.y, f);

                x += lambda * ey;

                FuncIters+=2;

            } while ((xlast - x).Norm() > 1e-8) ;

            return x;
        }

        #endregion
        #region lab2
        //Тут Point может представляться не только как точка, но и как вектор

        /// <summary>
        /// Транспонированный вектор (Опирции умножения вектора и транспонированного вектора различны)
        /// </summary>
        public struct PointT
        {
            public PointT(double x0 = 0, double y0 = 0)
            {
                x = x0; y = y0;
            }
            public double x { set; get; }
            public double y { set; get; }

        }

        public struct Matrix
        {
            // xx xy
            // yx yy
            public Matrix(double x0 = 1, double x1 = 0, double y0 = 0, double y1 = 1)
            {
                xx = x0; xy = x1; yx = y0; yy = y1;
            }
            public double xx, xy, yx, yy;

            public Matrix Transp()
            {
                return new Matrix(xx, yx, xy, yy);
            }

            public static Point operator *(Matrix a, Point b)
            {
                return new Point(a.xx * b.x + a.xy * b.y, a.yx * b.x + a.yy * b.y);
            }
            public static Point operator *(Point a, Matrix b)
            {
                return new Point(b.xx * a.x + b.xy * a.y, b.yx * a.x + b.yy * a.y);
            }

            public static Matrix operator -(Matrix a, Matrix b)
            {
                return new Matrix(a.xx - b.xx, a.xy - b.xy, a.yx - b.yx, a.yy - b.yy);
            }
            public static Matrix operator +(Matrix a, Matrix b)
            {
                return new Matrix(a.xx + b.xx, a.xy + b.xy, a.yx + b.yx, a.yy + b.yy);
            }
            public static Matrix operator /(Matrix a, double b)
            {
                return new Matrix(a.xx / b, a.xy / b, a.yx / b, a.yy / b);
            }
        }

        /// <summary>
        /// Метод многомерного поиска минимума Дивидона-Флетчера-Пауэлла на 2d
        /// </summary>
        /// <param name="func"> Функция на которой ищется минимум </param>
        /// <param name="p0"> Начальное значение </param>
        /// <param name="Grad"> Градиент (Счас считатся численно)</param>
        /// <returns>Минимальное значение в заданной области</returns>
        static public Point Devidona_Fletchera_Paualla_Metod(Func<Point, double> func, Point p0 = new Point(), Func<Point, Point> Grad = null)
        {
            Point x = p0, z;
            Point dx, dg;
            Matrix nu = new Matrix(1, 0, 0, 1);

            Matrix H = new Matrix(0, 0, 0, 0);

            double lamda;
            Point S = -Gradient(func, x);// -Grad(x);
            Point gradt = -S;
            Point gradlast = gradt;

            Func<double, double> f;

            double ft = func(x);
            double flast = ft;
            int ped = 22;
            //Console.WriteLine("i".PadLeft(4) + "(x, y)".PadLeft(ped) + "f(x, y)".PadLeft(ped) + "(s1 , s2)".PadLeft(ped) + "lambda".PadLeft(ped)+ "|x| |y|".PadLeft(ped) + "df".PadLeft(ped) + "".PadLeft(ped) +  "H^-1".PadRight(ped));


            while (gradt.Norm() > EPS)
            {
                if(LastIters % 100 == 0)
                    nu = new Matrix(1, 0, 0, 1);
                lamda = 0;
                f = landa => func(x + landa * (nu * S));

                //Поиск отриезка содержащего минимум взять из лабы 1
                Point p = Find_Line(lamda, f);
                //Поиск lamda при котормом f(x + lamda * S) минимальна
                lamda = Solve_GoldSech(p.x, p.y, f);
                //lamda = Solve_Parabola(p.x, p.y, f);

                if (lamda == 0 || Double.IsNaN(nu.xx))
                    break;

                try
                {
                    dx = lamda * (nu * S);
                    x += dx;

                    gradlast = gradt;
                    gradt = Gradient(func, x);// Grad(x);

                    dg = gradt - gradlast;
                    FuncIters += 4; // 4 раза для опредиления градиентат в точке

                    z = nu * dg;

                    nu += (dx * dx.Transp()) / (dx.Transp() * dg) - (nu * dg * z.Transp()) / (z.Transp() * dg);
                    H += (dx * dx.Transp()) / (dx.Transp() * dg);
                    S = -1.0 * (nu * gradt);
                }
                catch (InvalidCastException e)
                {
                    break;
                }

                LastIters++;

                ft = func(x);
                //Console.WriteLine(LastIters.ToString().PadLeft(4) +
                //    x.x.ToString().PadLeft(ped) +
                //    ft.ToString().PadLeft(ped) +
                //    S.x.ToString().PadLeft(ped) +
                //    lamda.ToString().PadLeft(ped) +
                //    dx.y.ToString().PadLeft(ped) +
                //    (ft - flast).ToString().PadLeft(ped) +
                //    H.xx.ToString().PadLeft(ped) +
                //    H.xy.ToString().PadLeft(ped)
                //    );
                //Console.WriteLine(" ".PadLeft(4) +
                //    x.y.ToString().PadLeft(ped) +
                //    " ".PadLeft(ped) +
                //    S.y.ToString().PadLeft(ped) +
                //    " ".PadLeft(ped) +
                //    dx.y.ToString().PadLeft(ped) +
                //    " ".PadLeft(ped) +
                //    H.yx.ToString().PadLeft(ped) +
                //    H.yy.ToString().PadLeft(ped)
                //    );
                //Console.WriteLine();

                flast = ft;
            }

            //Console.WriteLine("\n\nEPS: " + EPS);
            //Console.WriteLine("x0: \t(" + p0.x + " , " + p0.y + ")");
            //Console.WriteLine("Iter: " + LastIters);
            //Console.WriteLine("Func iter: " + FuncIters);
            //Console.WriteLine("Res: \t(" + x.x + " , " + x.y + ")");
            //Console.WriteLine("Func: " + func(x));
            return x;
        }

        /// <summary>
        /// Метод многомерного поиска минимума Дивидона-Флетчера-Пауэлла на 2d
        /// </summary>
        /// <param name="func"> Функция на которой ищется минимум </param>
        /// <param name="p0"> Начальное значение </param>
        /// <param name="Grad"> Градиент (Счас считатся численно)</param>
        /// <returns>Минимальное значение в заданной области</returns>
        static public Point MSG_Fletchera_Riversa(Func<Point, double> func, Point p0 = new Point(), Func<Point, Point> Grad = null)
        {
            Point x = p0;
            Point S = -Gradient(func, x);
            LastIters = 0;
            FuncIters = 0;
            double w;

            double lamda;
            Func<double, double> f;
            Point gradt = -S;
            Point gradlast = gradt;

            int ped = 22;
           // Console.WriteLine("i".PadLeft(4) + "(x, y)".PadLeft(ped) + "f(x, y)".PadLeft(ped) + "(s1 , s2)".PadLeft(ped) + "lambda".PadLeft(ped) + "|x| |y|".PadLeft(ped) + "df".PadLeft(ped) + "grad(x, y)".PadLeft(ped));

            double ft = func(x);
            double flast = ft;

            while (S.Norm() > EPS)
            {
                lamda = 3;
                f = landa => func(x + landa * S);

                //Поиск отриезка содержащего минимум взять из лабы 1
                Point p = Find_Line(lamda, f);
                //Поиск lamda при котормом f(x + lamda * S) минимальна
                lamda = Solve_GoldSech(p.x, p.y, f);

                if (lamda == 0 || Double.IsNaN(S.x) || Double.IsNaN(S.y))
                    break;

                x += lamda * S;

                gradlast = gradt;
                if (gradlast.Norm() == 0)
                    break;

                gradt = Gradient(func, x);
                FuncIters += 4; // 4 раза для опредиления градиентат в точке
                if (gradlast.Norm() != 0)
                {
                    w = Math.Pow(gradt.Norm() / gradlast.Norm(), 2); ;
                    S = -gradt + w * S;
                }
                LastIters++;

                flast = ft;
                ft = func(x);
                //Console.WriteLine(LastIters.ToString().PadLeft(4) +
                //    x.x.ToString().PadLeft(ped) +
                //    ft.ToString().PadLeft(ped) +
                //    S.x.ToString().PadLeft(ped) +
                //    lamda.ToString().PadLeft(ped) +
                //    (lamda * S).x.ToString().PadLeft(ped) +
                //    (ft - flast).ToString().PadLeft(ped) +
                //    gradt.x.ToString().PadLeft(ped)
                //    );
                //Console.WriteLine(" ".PadLeft(4) +
                //    x.y.ToString().PadLeft(ped) +
                //    " ".PadLeft(ped) +
                //    S.y.ToString().PadLeft(ped) +
                //    " ".PadLeft(ped) +
                //    (lamda * S).y.ToString().PadLeft(ped) +
                //    " ".PadLeft(ped) +
                //    gradt.y.ToString().PadLeft(ped)
                //    );
                //Console.WriteLine();
            }

            Console.WriteLine("EPS: " + EPS);
            Console.WriteLine("x0: \t(" + p0.x + " , " + p0.y + ")");
            Console.WriteLine("Iter: " + LastIters);
            Console.WriteLine("Function iter: " + FuncIters);
            Console.WriteLine("Res: \t(" + x.x + " , " + x.y + ")");
            Console.WriteLine("Func: " + func(x));

            return x;
        }

        /// <summary>
        /// Метод порабул для одномерного поиска
        /// </summary>
        /// <param name="x1">1 точка</param>
        /// <param name="x3">2 точка</param>
        /// <param name="f">Функция</param>
        /// <returns>Минимальное значение на заданном промежутку</returns>
        public static double Solve_Parabola(double x1, double x3, Func<double, double> f)
        {
            //LastSteps.Clear();
            //LastIters = 0;

            var flag = true;
            var bsuba = x3 - x1;
            double x = (x3 + x1) / 2;
            var x2 = (x3 + x1) / 2;
            do
            {
                double f1 = f(x1), f2 = f(x2), f3 = f(x3);
                double a0 = f1,
                    a1 = (f2 - f1) / (x2 - x1),
                    a2 = 1 / (x3 - x2) * ((f3 - f1) / (x3 - x1) - (f2 - f1) / (x2 - x1));
                double nx = 0.5 * (x1 + x2 - a1 / a2);
                if (nx != nx) break;
                x = nx;
                //if (x < x1 || x > x3)

                double a = x1, b = x3;

                if (x < x2) x3 = x2;
                else x1 = x2;
               // LastSteps.Add(new StepInfo(LastIters++, a, b, bsuba, x1, x3, f1, f3));
                bsuba = b - a;
                if (x <= a || x >= b)
                {
                    x = Math.Clamp(x, a, b);
                    x1 = x3 = x;
                    break;
                }
                x2 = x;

            } while (x3 - x1 > 1e-10);
            return x;
        }


        static private Point Gradient(Func<Point, double> func, Point p)
        {
            Point g;

            double h = 1e-10 / 20.0;

            g = new Point((func(new Point(p.x + h, p.y)) - func(new Point(p.x - h, p.y))) / (2 * h), (func(new Point(p.x, p.y + h)) - func(new Point(p.x, p.y - h))) / (2 * h));

            return g;
        }
        static private Point Graditnt2(Func<Point, double> func, Point p)
        {
            Point g;

            double h = 1e-10 / 20.0;

            g = new Point((Gradient(func, new Point(p.x + h, p.y)).x - Gradient(func, new Point(p.x - h, p.y)).x) / (2 * h), (Gradient(func, new Point(p.x, p.y + h)).y - Gradient(func, new Point(p.x, p.y - h)).y) / (2 * h));

            return g;
        }
        #endregion
        #region lab1

        /// <summary>
        /// Метод Дихотомии, одномерного поиска
        /// </summary>
        /// <param name="a">1 точка</param>
        /// <param name="b">2 точка</param>
        /// <param name="f">Функция</param>
        /// <returns>Минимальное значение на заданном промежутку</returns>
        public static double Solv_Dihotom(double a, double b, Func<double, double> f)
        {
            LastIters = 0;
            //LastSteps.Clear();

            double x1=a, x2=b;
            double fx1 = 0, fx2 = 0;
            double delta = EPS / 2;
            double bsuba = b - a;
            while (bsuba>EPS)
            {
                x1 = (a + b - delta)/2;
                x2 = (a + b + delta)/2;
                fx1 = f(x1);
                fx2 = f(x2);

                if (fx1 < fx2)
                    b = x2;
                else
                    a = x1;
                //LastSteps.Add(new StepInfo(LastIters, a, b, bsuba, x1, x2, fx1, fx2));
                bsuba = b - a;
                LastIters++;
            }

            return (a+b) / 2;
        }

        /// <summary>
        /// Метод залотого сечения, одномерного поиска
        /// </summary>
        /// <param name="a">1 точка</param>
        /// <param name="b">2 точка</param>
        /// <param name="f">Функция</param>
        /// <returns>Минимальное значение на заданном промежутку</returns>
        public static double Solve_GoldSech(double a, double b, Func<double, double> f)
        {
            double c = (3.0 - Math.Sqrt(5)) / 2d;
            //LastIters = 0;
            double x1 = (a + c * (b - a));
            double x2 = (b - c * (b - a));
            double fx1 = f(x1);
            double fx2 = f(x2);
            FuncIters += 2;
            //LastSteps.Clear();
            double bsuba = b - a;
            //LastSteps.Add(new StepInfo(LastIters, a, b, bsuba, x1, x2, fx1, fx2));
            while (bsuba > 1e-10)
            {
                if (fx1 > fx2)
                {
                    a = x1;
                    x1 = x2;
                    x2 = (b - c * (b - a));
                    fx1 = fx2;
                    fx2 = f(x2);
                }
                else
                {
                    b = x2;
                    x2 = x1;
                    x1 = (a + c * (b - a));
                    fx2 = fx1;
                    fx1 = f(x1);
                }
                FuncIters++;
                //LastIters++;
                //LastSteps.Add(new StepInfo(LastIters, a, b, bsuba, x1, x2, fx1, fx2));
                bsuba = b - a;
            }
            return (a + b) / 2d;
        }

        /// <summary>
        /// Метод Фибоначи, одномерного поиска
        /// </summary>
        /// <param name="a">1 точка</param>
        /// <param name="b">2 точка</param>
        /// <param name="f">Функция</param>
        /// <returns>Минимальное значение на заданном промежутку</returns>
        public static double Solve_Fibanachi(double a, double b, Func<double, double> f)
        {
            double c = Math.Sqrt(5);
            //LastSteps.Clear();
            double c1 = (1 + c) / 2;
            double c2 = (1 - c) / 2;
            Func<double, double> F = t => (Math.Pow(c1, t) - Math.Pow(c2, t)) / c;
            LastIters = 0;
            double[] Fn = { 1, 1, 2 }; 
            int n = 1;

            double bsuba = (b - a) / EPS;
            while(bsuba > Fn[2])
            {
                Fn[0] = Fn[1];
                Fn[1] = Fn[2];
                Fn[2] = Fn[0] + Fn[1];
                n++;
            }

            bsuba = b - a;

            double x1 = a + Fn[0] / Fn[2] * (b - a);
            double x2 = a + Fn[1] / Fn[2] * (b - a);

            double fx1 = f(x1);
            double fx2 = f(x2);

            //LastSteps.Add(new StepInfo(LastIters, a, b, bsuba, x1, x2, fx1, fx2));

            for (; LastIters < n - 3; /*LastSteps.Add(new StepInfo(++LastIters, a, b, bsuba, x1, x2, fx1, fx2)),*/ bsuba = b - a)
            {
                if (fx1 <= fx2)
                {
                    b = x2;
                    x2 = x1;
                    fx2 = fx1;
                    x1 = a + (F(n - LastIters - 3) / F(n - LastIters - 1)) * (b - a);
                    fx1 = f(x1);
                }
                else
                {
                    a = x1;
                    x1 = x2;
                    fx1 = fx2;
                    x2 = a + (F(n - LastIters - 2) / F(n - LastIters - 1)) * (b - a);
                    fx2 = f(x2);
                }
            }
            return (a + b) / 2d;
        }

        /// <summary>
        /// Поиск отрезка содержащего минимум
        /// </summary>
        /// <param name="x0"> Начальное значение </param>
        /// <param name="f"> Функция на которой ищется минимум </param>
        /// <returns> Отрезок от x до y, который содержит минимум </returns>
        public static Point Find_Line(double x0, Func<double, double> f)
        {
            //Console.WriteLine("\nX0 = " + x0 + "\n");
            Point p;
            double maxH = 1e+5;
            int i = 0;
            double last = x0;
            double h = EPS / 2;
            double x1 = x0 + h;
            double fx0 = f(x0), fx1 = f(x0 + h);
            //Console.WriteLine(i.ToString().PadLeft(22) +
            //    x0.ToString().PadLeft(22) +
            //    fx0.ToString().PadLeft(22));
            if (fx0 < fx1)
                h = -h;
            do
            {
                last = x0;
                fx0 = fx1;
                x0 = x1;
                h *= 2d;
                x1 = x1 + h;
                fx1 = f(x1);
                i++;
    //            Console.WriteLine(i.ToString().PadLeft(22) +
    //x0.ToString().PadLeft(22) +
    //fx0.ToString().PadLeft(22));
            } while (fx0 > fx1 && h < maxH);

            if (f(last) > fx0)
                p = new Point(Math.Min(x1, last), Math.Max(x1, last));
            else
                p = new Point(Math.Min(x1, x0), Math.Max(x1, x0));

            return p;
        }

        #endregion
    }

    public struct StepInfo
    {
        public double a, b, prevBsubA, x1, x2, fx1, fx2;
        public int IterIndex;

        public  StepInfo(int index, double a, double b, double prevBsubA, double x1, double x2, double fx1, double fx2)
        {
            this.a = a;
            this.b = b;
            this.prevBsubA = prevBsubA;
            this.x1 = x1;
            this.x2 = x2;
            this.fx1 = fx1;
            this.fx2 = fx2;
            this.IterIndex = index;

        }

        static int pad = 22;
        public static string Title()
        {
            return "Iter\t:" +
                "A7:".PadLeft(pad) +
                "B:".PadLeft(pad) +
                "B-A".PadLeft(pad) +
                "(B-A)/prev(B-A)".PadLeft(pad) +
                "X1:".PadLeft(pad) +
                "X2:".PadLeft(pad) +
                "F1".PadLeft(pad) +
                "F2:".PadLeft(pad);
        }

        public override string ToString()
        {
            return IterIndex + "\t: " +
                a.ToString().PadLeft(pad) +
                b.ToString().PadLeft(pad) +
                (b - a).ToString().PadLeft(pad) +
                (prevBsubA/(b-a)).ToString().PadLeft(pad) +
                x1.ToString().PadLeft(pad) +
                x2.ToString().PadLeft(pad) +
                fx1.ToString().PadLeft(pad) +
                fx2.ToString().PadLeft(pad);
                ;
        }
    }
}
