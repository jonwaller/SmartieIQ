using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace SmartieIQ
{
    class Program
    {
        private struct Operators
        {
            public List<float> Operatos;

        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Waiting for input: (e.g. 2, 4, 6, 8)");

                List<string> inItems = readItemsFromConsole();

                //writeItems("Input:", inItems);

                List<string> outItems = expandItems(inItems, inItems.Count + 1);
                writeItems("Output:", outItems);

                exitConsole();
            }
        }

        private static void writeItems(String caption, List<string> items)
        {
            Console.WriteLine(caption);
            foreach (string item in items)
            {
                Console.WriteLine("  '" + item + "'");
            }
            Console.WriteLine();
        }

        private static List<string> expandItems(List<string> inItems, int requestedLength)
        {
            List<float> points = new List<float>();
            for (int x = 0; x < inItems.Count; x++)
            {
                points.Add(
                    float.Parse(inItems[x])
                );
            }

            List<float> fitPoints = fitLagrange(points);
            
            List<string> outItems = new List<string>();

            for (int i = 0; i < requestedLength; i++) {
                outItems.Add(fitPoints[i].ToString());
            }

            return outItems;
        }

        private static List<float> fitLagrange(List<float> floats) {

            List<PointF> points = new List<PointF>();

            for (int i = 0; i < floats.Count; i++)
            {
                points.Add(new PointF(i,floats[i]));
            }

            return fitLagrange(points);
        }

        private static List<float> fitLagrange(List<PointF> pointList)
        {
            List<Operators> OpList = new List<Operators>();
            List<float> Xs = new List<float>();
            List<float> Ys = new List<float>();

            if (pointList.Count > 0)
            {
                //compute lagrange operator for each X coordinate
                for (int x = 0; x < 2000; x++)
                {
                    //list of float to hold the Lagrange operators
                    List<float> L = new List<float>();
                    //Init the list with 1's
                    for (int i = 0; i < pointList.Count; i++)
                    {
                        L.Add(1);
                    }
                    for (int i = 0; i < L.Count; i++)
                    {
                        for (int k = 0; k < pointList.Count; k++)
                        {
                            if (i != k)
                                L[i] *= (float)(x - pointList[k].X) / (pointList[i].X - pointList[k].X);
                        }
                    }
                    Operators o = new Operators();
                    o.Operatos = L;
                    OpList.Add(o);
                    Xs.Add(x);

                }

                //Computing the Polynomial P(x) which is y in our curve
                foreach (Operators O in OpList)
                {
                    float y = 0;
                    for (int i = 0; i < pointList.Count; i++)
                    {
                        y += O.Operatos[i] * pointList[i].Y;
                    }

                    Ys.Add(y);
                }
            }
            return Ys;
        }
 

        private static void exitConsole()
        {
            Console.WriteLine("Finished.");
            Console.ReadLine();
        }

        private static List<string> readItemsFromConsole()
        {
            String inputLine = Console.ReadLine();
            Console.WriteLine();

            List<string> items = inputLine.Split(',').Select(p => p.Trim()).ToList();

            return items;
        }
    }
}
