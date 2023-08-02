using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class QuickHull : Algorithm
    {
        public List<Point> quickHull(List<Point> points, Point XMin, Point XMAx, bool turnType)//N^2

        {//if turn type right turn type=true
            //else left =false

            double indx = -1, max = -1;

            for (int i = 0; i < points.Count; i++)
            {
                double Distance = Math.Abs(((XMAx.Y - XMin.Y) * (points[i].X - XMin.X))-((points[i].Y - XMin.Y) * (XMAx.X - XMin.X)) );
        
                if (turnType ==true)
                {
                    if (CGUtilities.HelperMethods.CheckTurn(new Line(XMin.X, XMin.Y, XMAx.X, XMAx.Y), points[i]) == Enums.TurnType.Right && Distance > max)
                    {
                        indx = i;
                        max = Distance;
                    }
                }
                else
                {
                    if (CGUtilities.HelperMethods.CheckTurn(new Line(XMin.X, XMin.Y, XMAx.X, XMAx.Y), points[i]) == Enums.TurnType.Left && Distance > max)
                    {
                        indx = i;
                        max = Distance;
                    }
                }

            }

            List<Point> extreme = new List<Point>();

            if (indx == -1)
            {
                extreme.Add(XMin);
                extreme.Add(XMAx);
                return extreme;

            }

            List<Point> points1, points2;

            Enums.TurnType type = CGUtilities.HelperMethods.CheckTurn(new Line(points[(int)indx].X, points[(int)indx].Y, XMin.X, XMin.Y), XMAx);
            if ( type==Enums.TurnType.Right)
            {
                points1 = quickHull(points,points[(int)indx], XMin,false);

            }
            else
            {
                points1 = quickHull(points, points[(int)indx], XMin, true);

            }
         type = CGUtilities.HelperMethods.CheckTurn(new Line(points[(int)indx].X, points[(int)indx].Y, XMAx.X, XMAx.Y), XMin);

            if (type == Enums.TurnType.Right)
            {
                points2 = quickHull(points, points[(int)indx], XMAx, false);

            }
            else
            {
                points2 = quickHull(points, points[(int)indx], XMAx, true);

            }
            for (int i = 0; i < points2.Count; ++i)
                points1.Add(points2[i]);
            return points1;
         
        }

        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {

            double MaxX = 0, MaxY = 0, MinX = 1000000000, MinY = 10000000000;
            Point XMAx = new Point(0,0);

            Point XMin = new Point(0, 0);
          
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].X > MaxX)
                {
                    MaxX = points[i].X;
                    XMAx = points[i];

                }
                if (points[i].Y > MaxY)
                {
                    MaxY = points[i].Y;
                
                }
                if (points[i].X < MinX)
                {
                    MinX = points[i].X;
                    XMin = points[i];

                }
                if (points[i].Y < MinY)
                {
                    MinY = points[i].Y;
                   
                }
            }

            List<Point> right = quickHull(points, XMin, XMAx, true);
            List<Point> left = quickHull(points, XMin, XMAx, false);
            for (int i = 1; i < left.Count; i++)
                right.Add(left[i]);

            for (int i = 0; i < right.Count; i++)
            {
                if (!outPoints.Contains(right[i]))
                    outPoints.Add(right[i]);
            }



        }

        public override string ToString()
        {
            return "Convex Hull - Quick Hull";
        }
    }
}
