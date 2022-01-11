using UnityEngine;
using UnityEngine.SceneManagement;

namespace FG
{
    public class Controls : MonoBehaviour
    {
        [SerializeField] private GameObject text;

        public void Pause()
        {
            Time.timeScale = 0;
            text.SetActive(true);
        }
        private void OnReset()
        {
            if (Time.timeScale == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
            }
        }
    }
}