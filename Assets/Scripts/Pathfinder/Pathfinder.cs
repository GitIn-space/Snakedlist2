using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FG
{
    public class Pathfinder
    {
        [SerializeField] private Vector2Int dims = new Vector2Int(9, 6);

        private Transform[] edges = new Transform[4];
        private static Mygrid grid;

        static Pathfinder()
        {
            grid = GameObject.Find("Controller").GetComponent<Mygrid>();
        }

        public static List<Vector3> Astar(Vector3 origin, Vector3 end, List<Vector3> obs = null, bool nonempty = false)
        {
            Tile start = grid.Gettile(origin); 
            Tile goal = grid.Gettile(end);

            Tile current = new Tile();
            List<Tile> open = new List<Tile>();
            List<Tile> closed = new List<Tile>();
            int cost = 0;

            if (obs != null && obs.Count > 0)
                grid.Setobstacles(obs);

            open.Add(start);

            while (open.Count > 0)
            {
                float lowest = open.Min(l => l.costdistance);
                current = open.First(l => l.costdistance == open.Min(l => l.costdistance));

                closed.Add(current);

                open.Remove(current);

                if (closed.FirstOrDefault(l => l.loc.x == goal.loc.x && l.loc.y == goal.loc.y) != null)
                    break;

                List<Tile> neighbours = current.neighbours.Where(l => l.passable).ToList();
                cost++;

                foreach (Tile neighbourtile in neighbours)
                {
                    if (closed.FirstOrDefault(l => l.loc.x == neighbourtile.loc.x && l.loc.y == neighbourtile.loc.y) != null)
                        continue;

                    if (open.FirstOrDefault(l => l.loc.x == neighbourtile.loc.x && l.loc.y == neighbourtile.loc.y) == null)
                    {
                        neighbourtile.cost = cost;
                        neighbourtile.distance = Getdistance(neighbourtile, goal);
                        neighbourtile.costdistance = neighbourtile.cost + neighbourtile.distance;
                        neighbourtile.parent = current;

                        open.Insert(0, neighbourtile);
                    }
                    else
                    {
                        if (cost + neighbourtile.distance < neighbourtile.costdistance)
                        {
                            neighbourtile.cost = cost;
                            neighbourtile.costdistance = neighbourtile.cost + neighbourtile.distance;
                            neighbourtile.parent = current;
                        }
                    }
                }
            }
            List<Vector3> path = new List<Vector3>();
            while (current != start)
            {
                path.Add(current.loc);
                current = current.parent;
            }
            path.Reverse();

            if (obs != null && obs.Count > 0)
                grid.Resetobstacles(obs);

            if (nonempty && path.Count == 0)
                return Astar(origin, end);

            return path;
        }

        private static float Getdistance(Tile subject, Tile target)
        {
            return Vector3.Distance(subject.loc, target.loc);
        }
    }
}