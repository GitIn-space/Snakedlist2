using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Foodcontroller : MonoBehaviour
    {
        [SerializeField] private GameObject foodfab;

        public Vector3 Spawn(List<Vector3> obs)
        {
            Vector3Int foodpos = new Vector3Int(Random.Range(0, 18), Random.Range(0, 12), 0);
            while (obs.Contains(foodpos))
                foodpos = new Vector3Int(Random.Range(0, 18), Random.Range(0, 12), 0);

            return Instantiate(foodfab, foodpos, Quaternion.identity, transform).transform.position;
        }
    }
}