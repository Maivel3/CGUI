using CGUtilities;
using System;
using System.Collections.Generic;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class JarvisMarch : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {


            double minimumX = 200, minimumX_Y = 0;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].X < minimumX)
                {
                    minimumX = points[i].X;
                    minimumX_Y = points[i].Y;

                }
            }

            Point minimunpoint = new Point(minimumX, minimumX_Y);
            Point secpoint = new Point(minimumX, minimumX_Y - 1);
            Point firstpoint = minimunpoint;



            outPoints.Add(minimunpoint);
            for (int j = 0; j < points.Count; j++)
            {

                double largest_a = 0;
                Point nextpoint = minimunpoint;
                double distance = 0;
                double largest_d = 0;
                for (int i = 0; i < points.Count; i++)
                {


                    Point vectorAB = new Point(minimunpoint.X - secpoint.X, minimunpoint.Y - secpoint.Y);
                    Point vectorAC = new Point(points[i].X - minimunpoint.X, points[i].Y - minimunpoint.Y);

                    double Crossprduct = HelperMethods.CrossProduct(vectorAB, vectorAC);
                    //double magnitude = (vectorAB.X * vectorAC.X) + (vectorAB.Y * vectorAC.Y);
                    double cosangle = ((vectorAB.X * vectorAC.X) + (vectorAB.Y * vectorAC.Y));
                    //double x=  ((Math.Sqrt(Math.Pow((vectorAB.X), 2) + Math.Pow((vectorAB.Y), 2))) *
                    //(Math.Sqrt(Math.Pow((vectorAC.X), 2) + Math.Pow((vectorAC.Y), 2))));
                    //double sinangle = Crossprduct / magnitude;
                    //double dot = (vectorAB.X * vectorAC.X )+ (vectorAB.Y + vectorAC.Y);
                    // double det = (vectorAB.X * vectorAC.Y) - (vectorAB.Y * vectorAC.X);
                    // double angle1 = Math.Atan2(secpoint.Y - minimunpoint.Y, secpoint.X - minimunpoint.X) * (180 / Math.PI);
                    //double dotproduct = (vectorAB.X * vectorAC.X) + (vectorAB.Y * vectorAB.Y);
                    //double magnitudeX = Math.Sqrt(Math.Pow(vectorAB.X, 2) * Math.Pow(vectorAC.X, 2));
                    //double magnitudeY = Math.Sqrt(Math.Pow(vectorAB.Y, 2) * Math.Pow(vectorAC.Y, 2));
                    //double magnitude = magnitudeX * magnitudeY;


                    //double angle = (180 / Math.PI) * Math.Asin(dotproduct/magnitude);
                    double angle = (180 / Math.PI) * Math.Atan2(Crossprduct, cosangle);
                    if (angle < 0)
                        angle = angle + (2 * 180);

                    distance = Math.Sqrt(Math.Pow(minimunpoint.X - points[i].X, 2)
                        + Math.Pow(minimunpoint.Y - points[i].Y, 2));
                    if (angle > largest_a)
                    {

                        largest_a = angle;
                        largest_d = distance;
                        nextpoint = points[i];
                    }
                    else if (angle == largest_a)
                    {
                        if (distance > largest_d)
                        {
                            largest_d = distance;
                            nextpoint = points[i];
                        }

                    }


                }
                if (firstpoint.X == nextpoint.X && firstpoint.Y == nextpoint.Y)
                {
                    break;
                }
                Line line = new Line(nextpoint, minimunpoint);
                Line line1 = new Line(minimunpoint, nextpoint);
                if (!outLines.Contains(line))
                    if (!outLines.Contains(line1))
                    {
                        outLines.Add(new Line(nextpoint, minimunpoint));
                    }
                if (!outPoints.Contains(nextpoint))
                    outPoints.Add(nextpoint);

                secpoint = minimunpoint;
                minimunpoint = nextpoint;



            }




        }

        public override string ToString()
        {
            return "Convex Hull - Jarvis March";
        }
    }
}