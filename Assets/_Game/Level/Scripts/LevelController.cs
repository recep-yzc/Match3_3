using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.Level.Scripts
{
    public class LevelController : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}