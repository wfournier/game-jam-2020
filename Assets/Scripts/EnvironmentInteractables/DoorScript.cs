using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.EnvironmentInteractables
{
    public class DoorScript : MonoBehaviour
    {
        public string sceneName;
        public int keysRequired;
        public bool requireCommand = true;

        private Collider2D _doorCollider;
        private GameObject _player;
        private LevelManager _levelManager;

        // Start is called before the first frame update
        void Start()
        {
            _doorCollider = gameObject.GetComponent<BoxCollider2D>();
            _player = GameObject.FindWithTag("Player");
            _levelManager = GameObject.FindWithTag("Manager").GetComponent<LevelManager>();
        }

        // Update is called once per frame
        void Update()
        {
            var keysOwned = _levelManager.keyCount;
            if (_doorCollider.IsTouching(_player.GetComponent<BoxCollider2D>()) && 
                (InputManager.VerticalDir == InputManager.VerticalDirections.Up || !requireCommand) &&
                keysOwned >= keysRequired)
            {
                _levelManager.RemoveKey(keysRequired);
                SceneManager.LoadScene(sceneName);
            }
            else if (keysOwned < keysRequired)
            {
                Debug.Log("Not enough keys!");
                //TODO tell players he needs more keys!
            }
        }
    }
}
