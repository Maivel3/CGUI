using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//final final !!
namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class DivideAndConquer : Algorithm
    {
        public List<Point> Merge(List<Point> a, List<Point> b)
        {

            int n1 = a.Count, n2 = b.Count;
            int indx1 = 0, indx2 = 0;

            //Extreme Points
            for (int i = 1; i < n1; i++)
            {
                if (a[i].X > a[indx1].X)
                {
                    indx1 = i;
                }
                else if (a[i].X == a[indx1].X && a[i].Y > a[indx1].Y)
                {
                    indx1 = i;
                }

            }

            for (int i = 1; i < n2; i++)
            {
                if (b[i].X < b[indx2].X)
                {
                    indx2 = i;
                }
                else if (b[i].X == b[indx2].X && b[i].Y < b[indx2].Y)
                {
                    indx2 = i;
                }

            }

            //-------------------------------------------------------------------------------------------//

            //1st Side (indx1 -> extreme a | indx2 -> extreme b)
            int up_a = indx1, up_b = indx2;
            int low_a = indx1, low_b = indx2;
            bool found = false;


            Line line;
            Enums.TurnType turnType;
            while (!found)
            {
                found = true;

                line = new Line(b[up_b], a[up_a]);
                turnType = HelperMethods.CheckTurn(line, a[(up_a + 1) % n1]);

                while (turnType == Enums.TurnType.Right)
                {
                    up_a = (up_a + 1) % n1;
                    found = false;
                    line = new Line(b[up_b], a[up_a]);
                    turnType = HelperMethods.CheckTurn(line, a[(up_a + 1) % n1]);
                }

                line = new Line(b[up_b], a[up_a]);
                turnType = HelperMethods.CheckTurn(line, a[(up_a + 1) % n1]);

                if (found && turnType == Enums.TurnType.Colinear)
                {
                    up_a = (up_a + 1) % n1;
                }


                line = new Line(a[up_a], b[up_b]);
                turnType = HelperMethods.CheckTurn(line, b[(n2 + up_b - 1) % n2]);
                while (turnType == Enums.TurnType.Left)
                {
                    up_b = (n2 + up_b - 1) % n2;
                    found = false;
                    line = new Line(a[up_a], b[up_b]);
                    turnType = HelperMethods.CheckTurn(line, b[(n2 + up_b - 1) % n2]);
                }

                line = new Line(a[up_a], b[up_b]);
                turnType = HelperMethods.CheckTurn(line, b[(up_b + n2 - 1) % n2]);
                if (found && turnType == Enums.TurnType.Colinear)
                {
                    up_b = (up_b + n2 - 1) % n2;
                }

            }

            //-------------------------------------------------------------------------------------------------------------------//
            //2nd Side
            found = false;
            while (!found)
            {
                found = true;
                line = new Line(b[low_b], a[low_a]);
                turnType = HelperMethods.CheckTurn(line, a[(low_a + n1 - 1) % n1]);

                while (turnType == Enums.TurnType.Left)
                {
                    low_a = (low_a + n1 - 1) % n1;
                    found = false;
                    line = new Line(b[low_b], a[low_a]);
                    turnType = HelperMethods.CheckTurn(line, a[(low_a + n1 - 1) % n1]);
                }

                line = new Line(b[low_b], a[low_a]);
                turnType = HelperMethods.CheckTurn(line, a[(low_a + n1 - 1) % n1]);
                if (found && turnType == Enums.TurnType.Colinear)
                {
                    low_a = (low_a + n1 - 1) % n1;
                }

                line = new Line(a[low_a], b[low_b]);
                turnType = HelperMethods.CheckTurn(line, b[(low_b + 1) % n2]);

                while (turnType == Enums.TurnType.Right)
                {
                    low_b = (low_b + 1) % n2;
                    found = false;
                    line = new Line(a[low_a], b[low_b]);
                    turnType = HelperMethods.CheckTurn(line, b[(low_b + 1) % n2]);
                }

                line = new Line(a[low_a], b[low_b]);
                turnType = HelperMethods.CheckTurn(line, b[(low_b + 1) % n2]);

                if (found && turnType == Enums.TurnType.Colinear)
                {
                    low_b = (low_b + 1) % n2;
                }


            }

            //---------------------------------------------------------------------------------------------------//

            //final result
            List<Point> out_points = new List<Point>();

            int x = up_a;

            out_points.Add(a[up_a]);
            while (x != low_a)
            {
                Console.WriteLine("X = " + x);
                x = (x + 1) % n1;
                out_points.Add(a[x]);
            }

            x = low_b;
            if (!out_points.Contains(b[low_b]))
            {
                out_points.Add(b[low_b]);
            }

            while (x != up_b)
            {
                x = (x + 1) % n2;
                if (!out_points.Contains(b[x]))
                {
                    out_points.Add(b[x]);
                }

            }


            return out_points;
        }

        public List<Point> divideAndConquer(List<Point> L)
        {
            if (L.Count == 1)
            {
                return L;
            }

            List<Point> left = new List<Point>();
            List<Point> right = new List<Point>();

            for (int i = 0; i < L.Count / 2; i++)
            {
                left.Add(L[i]);
            }

            for (int i = L.Count / 2; i < L.Count; i++)
            {
                right.Add(L[i]);
            }

            List<Point> finalLeft = divideAndConquer(left);
            List<Point> finalRight = divideAndConquer(right);

            return Merge(finalLeft, finalRight);
        }


        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {

            points = points.OrderBy(x => x.Y).ToList();
            points = points.OrderBy(x => x.X).ToList();

            outPoints = new List<Point>();
            outPoints = divideAndConquer(points);

        }

        public override string ToString()
        {
            return "Convex Hull - Divide & Conquer";
        }

    }
}
