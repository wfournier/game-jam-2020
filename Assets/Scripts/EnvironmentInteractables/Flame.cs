using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class Flame : MonoBehaviour
    {
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (_button.signal)
                OpenFlame();
            else
                CloseFlame();
        }

        public void OpenFlame()
        {
            _spriteRenderer.sprite = flameOpened;
        }

        public void CloseFlame()
        {
            _spriteRenderer.sprite = flameClosed;
        }

        #region Declarations --------------------------------------------------

        public Sprite flameClosed;
        public Sprite flameOpened;

        public Button _button;

        private SpriteRenderer _spriteRenderer;

        #endregion
    }
}