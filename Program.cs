using System;
using System.Collections.Generic;

namespace csharp_lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 1 lab
            //Solver.EPS = 1e-1;
            //Console.WriteLine("EPS:     " + Solver.EPS);

            //var res = Solver.Solve_Fibanachi(0, 100, x => Math.Exp(x));
            //Console.WriteLine("Result:  " + res);
            //Console.WriteLine("Iters:   " + Solver.LastIters);
            //Console.WriteLine(StepInfo.Title());
            //foreach (var item in Solver.LastSteps)
            //    Console.WriteLine(item);

            //Solver.Find_Line(0, x => (x - 50) * (x - 10));

            //Solver.Find_Line(100, x => (x - 50) * (x - 10));
            //Solver.Find_Line(30 - 1e-10, x => (x - 50) * (x - 10));
            //Solver.Find_Line(30 + 1e-10, x => (x - 50) * (x - 10));
            //Solver.Find_Line(30, x => (x - 50) * (x - 10));

            //Solver.EPS = 1e-7;
            //Console.WriteLine("EPS:     " + Solver.EPS);

            //res = Solver.Solve_Fibanachi(0, 100, x => Math.Exp(x));
            //Console.WriteLine("Result:  " + res);
            //Console.WriteLine("Iters:   " + Solver.LastIters);
            //Console.WriteLine(StepInfo.Title());
            //foreach (var item in Solver.LastSteps)
            //    Console.WriteLine(item);
            #endregion
            #region 2 lab

            //Solver.EPS = 1e-5;
            //minimum 1 1
            //100(y - x)^2 + (1 - x)^2
            //Solver.Point p = Solver.MSG_Fletchera_Riversa(p2 => 100 * Math.Pow(p2.y - p2.x, 2) + Math.Pow(1 - p2.x, 2), p1 => new Solver.Point(202 *  p1.x  - 200 * p1.y - 2 , 200 * (p1.y - p1.x)), new Solver.Point(3, 1));
            //Solver.Point p = Solver.Devidona_Fletchera_Paualla_Metod(p2 => 100 * Math.Pow(p2.y - p2.x, 2) + Math.Pow(1 - p2.x, 2), p1 => new Solver.Point(202 *  p1.x  - 200 * p1.y - 2 , 200 * (p1.y - p1.x)), new Solver.Point(3, 1));

            //minimum 1 1
            //100(y - x^2)^2 + (1 - x)^2
            //Solver.Point p = Solver.MSG_Fletchera_Riversa(p2 => 100 * Math.Pow(p2.y - Math.Pow(p2.x, 2), 2) + Math.Pow(1 - p2.x, 2), p1 => new Solver.Point(2 * (200 * Math.Pow(p1.x,3)  - 200 * p1.x * p1.y + p1.x - 1), 200 * (p1.y - Math.Pow(p1.x,2))), new Solver.Point(-5, 7));
            //Solver.Point p = Solver.Devidona_Fletchera_Paualla_Metod(p2 => 100 * Math.Pow(p2.y - Math.Pow(p2.x, 2), 2) + Math.Pow(1 - p2.x, 2), p1 => new Solver.Point(2 * (200 * Math.Pow(p1.x,3)  - 200 * p1.x * p1.y + p1.x - 1), 200 * (p1.y - Math.Pow(p1.x,2))), new Solver.Point(3, 1));

            //minimum 2.79914 195318
            // -1 * (2 / (1 + ((x - 2)/3)^2 + (y - 2)^2 ) + 1/ (1 + (x - 3)^2 + ((y - 1) / 3)^2 ))
            //Solver.Point p = Solver.MSG_Fletchera_Riversa(p2 => - 1 *( 2 / (1 + Math.Pow((p2.x - 2)/3 ,2) + Math.Pow(p2.y - 2, 2)) + 1/ (1 + Math.Pow(p2.x - 3, 2) + Math.Pow((p2.y - 1) / 3, 2) )), p1 => new Solver.Point( (2 * (p1.x - 3) / Math.Pow(Math.Pow(p1.x - 3, 2) + 1.0/9 * Math.Pow(p1.y - 1, 2) + 1, 2)) + (4 * (p1.x - 2) / (9* Math.Pow(1.0 / 9 * Math.Pow(p1.x - 2, 2) + Math.Pow(p1.y - 2, 2) +1, 2))), (4 * (p1.y - 2) / Math.Pow(1.0 / 9 * Math.Pow(p1.x - 2, 2) + Math.Pow(p1.y - 2, 2) + 1, 2)) + (2 *(p1.y - 1) / Math.Pow( 9 * (Math.Pow( p1.x - 3, 2) + 1.0 / 9 * Math.Pow(p1.y - 1 ,2) + 1), 2))), new Solver.Point(3, 3) );
            //Solver.Point p = Solver.Devidona_Fletchera_Paualla_Metod(p2 => - 1 *( 2 / (1 + Math.Pow((p2.x - 2)/3 ,2) + Math.Pow(p2.y - 2, 2)) + 1/ (1 + Math.Pow(p2.x - 3, 2) + Math.Pow((p2.y - 1) / 3, 2) )), p1 => new Solver.Point( (2 * (p1.x - 3) / Math.Pow(Math.Pow(p1.x - 3, 2) + 1.0/9 * Math.Pow(p1.y - 1, 2) + 1, 2)) + (4 * (p1.x - 2) / (9* Math.Pow(1.0 / 9 * Math.Pow(p1.x - 2, 2) + Math.Pow(p1.y - 2, 2) +1, 2))), (4 * (p1.y - 2) / Math.Pow(1.0 / 9 * Math.Pow(p1.x - 2, 2) + Math.Pow(p1.y - 2, 2) + 1, 2)) + (2 *(p1.y - 1) / Math.Pow( 9 * (Math.Pow( p1.x - 3, 2) + 1.0 / 9 * Math.Pow(p1.y - 1 ,2) + 1), 2))), new Solver.Point(-5, 7) );

            //Console.WriteLine( p.x + "   " + p.y );

            #endregion
            #region 3 lab
            //Solver.EPS = 1e-5;
            //Solver.Point p = Solver.Gause(p2 => 100 * Math.Pow(p2.y - p2.x, 2) + Math.Pow(1 - p2.x, 2), new Solver.Point(3, 1));
            //Console.WriteLine(p.x + "   " + p.y);

            Solver.EPS = 1e-5;

            List<Func<Solver.Point, double>> func = new List<Func<Solver.Point, double>>();

            //Штрафные функции
            //a)
            //func.Add(p1 => Math.Max(-p1.y + p1.x + 2, 0));
            //func.Add(p1 => Math.Pow(1.0 / 2.0 * (-p1.y + p1.x + 2 + Math.Abs(-p1.y + p1.x + 2)), 2));
            //func.Add(p1 => Math.Pow(1.0 / 2.0 * (-p1.y + p1.x + 2 + Math.Abs(-p1.y + p1.x + 2)), 100));

            //б)
            //func.Add(p1 => Math.Abs(p1.x + p1.y));
            //func.Add(p1 => Math.Pow(p1.x + p1.y, 2));
            //func.Add(p1 => Math.Pow(p1.x + p1.y, 100));

            //Барьерные функции
            //func.Add(p1 =>
            //{
            //    if (p1.y - p1.x - 2 > 0) return 1.0 / (p1.y - p1.x - 2);
            //    else return 1e+100 + 1e+98 * (-p1.y + p1.x + 2);
            //});
            func.Add(p1 =>
            {
                if (p1.y - p1.x - 2 > 0) return -Math.Log(p1.y - p1.x - 2);
                else return 1e+100 + 1e+98 * (-p1.y + p1.x + 2);
            });


            //Solver.Point p = Solver.Solve_Penalty_function(p2 => 7 * Math.Pow(p2.x - p2.y, 2) + Math.Pow(p2.y - 6, 2), func, new Solver.Point(-3, 3));
            //Solver.Point p = Solver.Solve_Penalty_function(p2 => 7 * Math.Pow(p2.x - p2.y, 2) + Math.Pow(p2.y - 6, 2), func, new Solver.Point(7, 3));
            //Solver.Point p = Solver.Solve_Penalty_function(p2 => 7 * Math.Pow(p2.x - p2.y, 2) + Math.Pow(p2.y - 6, 2), func, new Solver.Point(1000, -3000));

            Solver.Point p = Solver.Solve_Barrier_function(p2 => 7 * Math.Pow(p2.x - p2.y, 2) + Math.Pow(p2.y - 6, 2), func, new Solver.Point(-3, 3));
            //Solver.Point p = Solver.Solve_Barrier_function(p2 => 7 * Math.Pow(p2.x - p2.y, 2) + Math.Pow(p2.y - 6, 2), func, new Solver.Point(7, 3));
            //Solver.Point p = Solver.Solve_Barrier_function(p2 => 7 * Math.Pow(p2.x - p2.y, 2) + Math.Pow(p2.y - 6, 2), func, new Solver.Point(1000, -3000));

            Console.WriteLine(p.x + "   " + p.y);

            #endregion

            Console.ReadKey();
        }
    }
}
