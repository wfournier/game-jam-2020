using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.EnvironmentInteractables
{
    public class DoorScript : MonoBehaviour
    {
        private Collider2D _doorCollider;
        private LevelManager _levelManager;
        private GameObject _player;
        private bool _startTime;

        private float _timeout;
        public GameObject keyPromptObject;
        public int keysRequired;

        [Header("Key Prompt Settings")] public float promptDuration = 60f;

        public bool requireCommand = true;
        public string sceneName;
        public Text text;

        // Start is called before the first frame update
        private void Start()
        {
            _startTime = false;
            _timeout = promptDuration;
            _doorCollider = gameObject.GetComponent<BoxCollider2D>();
            _player = GameObject.FindWithTag("Player");
            _levelManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>();
        }

        // Update is called once per frame
        private void Update()
        {
            UnlockDoor();

            CountdownPrompt();
        }

        private void UnlockDoor()
        {
            if (_doorCollider.IsTouching(_player.GetComponent<BoxCollider2D>()))
            {
                if ((InputManager.VerticalDir == InputManager.VerticalDirections.Up || !requireCommand) &&
                    _levelManager.keyCount >= keysRequired)
                {
                    _levelManager.RemoveKey(keysRequired);
                    SceneManager.LoadScene(sceneName);
                }
                else if (_levelManager.keyCount < keysRequired &&
                         InputManager.VerticalDir == InputManager.VerticalDirections.Up)
                {
                    if (!_startTime)
                        _startTime = true;
                }
            }
        }

        private void CountdownPrompt()
        {
            if (!_startTime || keysRequired < _levelManager.keyCount) return;

            if (_timeout >= promptDuration)
            {
                text.text = keysRequired + "";
                keyPromptObject.SetActive(true);
            }

            if (_timeout <= 0f)
            {
                keyPromptObject.SetActive(false);
                _startTime = false;
                _timeout = promptDuration;
            }
            else
            {
                _timeout--;
            }
        }
    }
}