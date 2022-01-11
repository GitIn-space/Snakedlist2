using UnityEngine;

namespace FG
{
    public class Headcollision : MonoBehaviour
    {
        private Controls contr;
        private Snakecontroller snacon;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Snake"))
            {
                contr.Pause();
                collision.collider.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Food"))
            {
                Destroy(collision.gameObject);
                snacon.Eat();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Snake"))
                collision.GetComponent<CircleCollider2D>().isTrigger = false;
        }

        private void Awake()
        {
            contr = gameObject.GetComponentInParent<Controls>();
            snacon = gameObject.GetComponentInParent<Snakecontroller>();
        }
    }
}