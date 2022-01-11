using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Snakecontroller : MonoBehaviour
    {
        [SerializeField] private GameObject bodyfab;
        [SerializeField] private GameObject snakefab;
        [SerializeField] private float snakeinterval = 1f;

        private Slinklist<Transform> snake = new Slinklist<Transform>();
        private Pathfinder pathfinder;
        private List<Vector3> path = new List<Vector3>();

        private float eaten = 1f;
        private Vector3 food;

        private Foodcontroller foocon;

        public void Eat()
        {
            snake.Insert(1, Instantiate(bodyfab, snake[0].position, Quaternion.identity).transform);
            eaten++;

            List<Vector3> obs = Getobstacles();
            food = foocon.Spawn(obs);
            path = Pathfinder.Astar(snake[0].position, food, obs, true);
        }

        private List<Vector3> Getobstacles()
        {
            List<Vector3> obs = new List<Vector3>();
            foreach (Transform each in snake)
                obs.Add(each.position);

            return obs;
        }

        private IEnumerator Move()
        {
            Vector2 temp;
            while (true)
            {
                if (path.Count == 0)
                    path = Pathfinder.Astar(snake[0].position, food, Getobstacles(), true);

                temp = path[0];
                path.RemoveAt(0);
                foreach (Transform each in snake)
                    (each.position, temp) = (temp, each.position);

                yield return new WaitForSeconds(snakeinterval);
            }
        }

        private void Awake()
        {
            foocon = gameObject.GetComponent<Foodcontroller>();

            snake.Add(Instantiate(snakefab, transform.position, Quaternion.identity, transform).transform);
            pathfinder = new Pathfinder();
        }

        private void Start()
        {
            food = foocon.Spawn(Getobstacles());

            path = Pathfinder.Astar(snake[0].transform.position, food);

            StartCoroutine(Move());
        }
    }
}