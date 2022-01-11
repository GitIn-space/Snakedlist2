using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Mygrid : MonoBehaviour
    {
        [SerializeField] private GameObject edge;
        [SerializeField] private Vector2Int dims;

        private Tile[,] grid;
        private readonly Transform[] edges = new Transform[4];

        public Tile Gettile(Vector3 pos)
        {
            return grid[(int) pos.x, (int) pos.y];
        }

        public void Setobstacles(List<Vector3> obs)
        {
            foreach (Vector3 each in obs)
                grid[(int) each.x, (int) each.y].passable = false;
        }

        public void Resetobstacles(List<Vector3> obs)
        {
            foreach (Vector3 each in obs)
                grid[(int) each.x, (int) each.y].passable = true;
        }

        private void Awake()
        {
            dims += Vector2Int.one;

            grid = new Tile[dims.x, dims.y];

            for (int c = 0; c < this.dims.x; c++)
                for (int q = 0; q < this.dims.y; q++)
                    grid[c, q] = new Tile(new Vector3(c, q), true);

            int xpos = 0;
            int ypos = 0;
            for (int c = 0; c < this.dims.x; c++)
                for (int q = 0; q < this.dims.y; q++)
                {
                    xpos = c == 0 ? dims.x - 1 : c - 1;
                    ypos = q == 0 ? dims.y - 1 : q - 1;

                    grid[c, q].neighbours = new[] { grid[(c + 1) % dims.x, q], grid[xpos, q], grid[c, (q + 1) % dims.y], grid[c, ypos] };
                }

            transform.position = new Vector3(dims.x / 2, dims.y / 2);

            dims += Vector2Int.one * 2;

            edges[0] = Instantiate(edge, new Vector3(dims.x / 2, 0, 0) + transform.position, Quaternion.identity, transform).transform;
            edges[0].localScale = new Vector3(1, dims.y, 1);

            edges[1] = Instantiate(edge, new Vector3(-dims.x / 2, 0, 0) + transform.position, Quaternion.identity, transform).transform;
            edges[1].localScale = new Vector3(1, -dims.y, 1);

            edges[2] = Instantiate(edge, new Vector3(0, dims.y / 2, 0) + transform.position, Quaternion.identity, transform).transform;
            edges[2].localScale = new Vector3(dims.x, 1, 1);

            edges[3] = Instantiate(edge, new Vector3(0, -dims.y / 2, 0) + transform.position, Quaternion.identity, transform).transform;
            edges[3].localScale = new Vector3(-dims.x, 1, 1);

            dims -= Vector2Int.one * 2;
        }
    }
}