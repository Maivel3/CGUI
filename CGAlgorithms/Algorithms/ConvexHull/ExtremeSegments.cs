using CGUtilities;
using System.Collections.Generic;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class ExtremeSegments : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {


            List<Point> list = new List<Point>();

            for (int i = 0; i < points.Count; i++)
            {
                Point point = points[i];
                if (!list.Contains(point))
                    list.Add(point);
            }

            points = list;

            for (int i = 0; i < points.Count; i++)
            {
                if (points.Count != 1)
                {
                    for (int j = 0; j < points.Count; j++)
                    {

                        if (points[i] == points[j])
                        {
                            continue;
                        }
                        Line l = new Line(points[i], points[j]);
                        bool left = true, right = true, colinear = true;
                        for (int k = 0; k < points.Count; k++)
                        {
                            if (k == i && k == j)
                            {
                                continue;
                            }
                            if (points[k] != points[i] || points[j] != points[k])
                            {
                                switch (HelperMethods.CheckTurn(l, points[k]))
                                {
                                    case Enums.TurnType.Left:
                                        left = false;
                                        break;
                                    case Enums.TurnType.Right:
                                        right = false;
                                        break;
                                }

                                if (HelperMethods.CheckTurn(l, points[k]) == Enums.TurnType.Colinear
                                    && !HelperMethods.PointOnSegment(points[k], points[i], points[j]))
                                    colinear = false;
                            }

                        }
                        if (colinear == false)
                        {
                            continue;
                        }

                        if (!right && !left)
                        {
                            continue;
                        }

                        if (!outPoints.Contains(points[i]))
                            outPoints.Add(points[i]);
                        if (!outPoints.Contains(points[j]))
                            outPoints.Add(points[j]);
                        Line line = new Line(points[i], points[j]);
                        Line line1 = new Line(points[j], points[i]);
                        if (!outLines.Contains(line) && !outLines.Contains(line1))
                        {
                            outLines.Add(new Line(points[i], points[j]));
                        }

                    }
                }
                else
                {
                    outPoints = points;
                    return;
                }


            }



        }

        public override string ToString()
        {
            return "Convex Hull - Extreme Segments";
        }
    }
}