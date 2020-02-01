using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class Button : MonoBehaviour
    {
        #region Declarations --------------------------------------------------


        public Sprite buttonOpened;
        public Sprite buttonClosed;

        public int buttonNumber;    //Numéro du bouton
        public bool signal;         //Envoie un signal true ou false
        public bool order;          //Indique si le bouton fait partie d'un ordre 

        private SpriteRenderer _spriteRenderer;
        private AudioSource _audio;

        #endregion

        // Start is called before the first frame update
        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _audio = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        { }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (!signal)
                {
                    _audio.Play();
                }           
                _spriteRenderer.sprite = buttonClosed;
                signal = true;
            }
        }

        public void buttonReset()
        {
            _spriteRenderer.sprite = buttonOpened;
            signal = false;
        }
    }
}
