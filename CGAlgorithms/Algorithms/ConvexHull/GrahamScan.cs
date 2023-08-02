using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class GrahamScan : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            double minY = points[0].Y;
            int minYIndex = 0;
            for (int i = 1; i < points.Count; i++)
            {
                if (points[i].Y < minY)
                {
                    minY = points[i].Y;
                    minYIndex = i;
                }
            }
            Point temp = points[0];
            points[0] = points[minYIndex];
            points[minYIndex] = temp;

            List<KeyValuePair<Point, double>> sortPoints = new List<KeyValuePair<Point, double>>();
            for (int i = 1; i < points.Count; i++)
            {
                double angle = (180 / Math.PI) * (Math.Atan2((points[i].X - points[0].X), (points[i].Y - points[0].Y)));
                sortPoints.Add(new KeyValuePair<Point, double>(points[i], angle));
            }

            sortPoints.Sort((x, y) => x.Value.CompareTo(y.Value));
            sortPoints.Add(new KeyValuePair<Point, double>(points[0], 0));


            Stack<Point> stack = new Stack<Point>();
            stack.Push(points[0]);
            stack.Push(sortPoints[0].Key);

            for (int i = 1; i < sortPoints.Count; i++)
            {
                Point top = stack.Pop(), previous = stack.First();
                stack.Push(top);
                while (stack.Count > 2 && HelperMethods.CheckTurn(new Line(top, previous), sortPoints[i].Key) != Enums.TurnType.Left)
                {
                    stack.Pop();
                    top = stack.Pop();
                    previous = stack.First();
                    stack.Push(top);
                }
                stack.Push(sortPoints[i].Key);
            }

            while (stack.Any())
                outPoints.Add(stack.Pop());

            outPoints.RemoveAt(outPoints.Count - 1);
        }

        public override string ToString()
        {
            return "Convex Hull - Graham Scan";
        }
    }
}
