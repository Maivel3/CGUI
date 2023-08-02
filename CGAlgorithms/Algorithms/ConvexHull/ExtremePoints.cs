using CGUtilities;
using System.Collections.Generic;
namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class ExtremePoints : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {


            for (int point = 0; point < points.Count; point++)
            {
                int flag = 0;
                foreach (Point v in points)
                {
                    foreach (Point v1 in points)
                    {
                        NM(points, ref point, ref flag, v, v1);
                        if (flag == 1)
                            break;
                    }
                    if (flag == 1)
                        break;
                }


            }

            outPoints = points;


        }

        private static void NM(List<Point> points, ref int point, ref int flag, Point v, Point v1)
        {
            for (int c = 0; c < points.Count; c++)
            {
                if (points[point] == v || points[point] == v1)
                {
                    continue;
                }
                if (points[point] != v1 && points[point] != points[c])
                {
                    if (HelperMethods.PointInTriangle(points[point], v, v1, points[c]) != Enums.PointInPolygon.Inside &&
                        HelperMethods.PointInTriangle(points[point], v, v1, points[c]) != Enums.PointInPolygon.OnEdge)
                    {
                        continue;
                    }
                    flag = 1;
                    points.Remove(points[point]);
                    point--;
                    break;
                }

            }
        }

        public override string ToString()
        {
            return "Convex Hull - Extreme Points";
        }
    }
}