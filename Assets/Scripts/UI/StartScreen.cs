using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class StartScreen : MonoBehaviour
    {
        public string scene;

        private void Update()
        {
            if (InputManager.JumpButton)
                SceneManager.LoadScene(scene);
        }
    }
}