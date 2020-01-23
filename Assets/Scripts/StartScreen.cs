using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class StartScreen : MonoBehaviour
    {

        public SceneAsset scene;

        // Update is called once per frame
        void Update()
        {
            if (!Input.GetMouseButtonDown(0))
                return;

            SceneManager.LoadScene(scene.name);
        }
    }
}
