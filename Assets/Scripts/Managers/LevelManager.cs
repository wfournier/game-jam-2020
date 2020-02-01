using System;
using System.Collections;
using Assets.Scripts.Controllers;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Declarations --------------------------------------------------

        public Collider2D startCameraBounds;

        [HideInInspector]
        public PlayerController player;
        
        private Camera _mainCamera;

        public float waitToRespawn;
        public bool deathEffectEnabled;
        public GameObject deathEffect;

        public bool healthBarEnabled;
        public HealthBar healthBar;

        public bool coinsEnabled;
        public bool keysEnabled;

        public int coinCount;
        public int keyCount;

        public Text coinText;
        public Text keyText;

        public GameObject coinsParentObject;
        public GameObject keysParentObject;

        public bool isSoundEnabled;
        #endregion


        #region Public Methods ------------------------------------------------

        public void AddHealth(int value)
        {
            if(healthBarEnabled)
                healthBar.Add(value);
        }

        public void RemoveHealth(int value)
        {
            if (healthBarEnabled)
            {
                healthBar.Remove(value);
                StartCoroutine(InvulnerableCo());
            }
        }

        public void SetHealth(int value)
        {
            if(healthBarEnabled)
                healthBar.Set(value);
        }

        public void SetHealthMax()
        {
            if (healthBar == null) return;

            if (healthBarEnabled)
                healthBar.Set(healthBar.totalHealth);
        }

        public void RespawnPlayer()
        {
            StartCoroutine(RespawnPlayerCo());
            StartCoroutine(InvulnerableCo());
        }

        public void AddCoins(int count)
        {
            if(coinsEnabled)
                SetCoinCount(coinCount + count);
        }

        public void RemoveCoins(int count)
        {
            if(coinsEnabled)
                SetCoinCount(coinCount - count);
        }

        public void SetCoinCount(int count)
        {
            if (coinsEnabled)
            {
                coinCount = count;
                UpdateCoinText();
            }
        }

        public void AddKey(int count)
        {
            if (keysEnabled)
                SetKeyCount(keyCount + count);
        }

        public void RemoveKey(int count)
        {
            if (keysEnabled)
                SetKeyCount(keyCount - count);
        }

        public void SetKeyCount(int count)
        {
            keyCount = count;
            UpdateKeyText();
        }

        #endregion


        #region Private/Protected Methods -------------------------------------

        private void Start()
        {
            isSoundEnabled = false;
            player = FindObjectOfType<PlayerController>();
            _mainCamera = Camera.main;
            healthBar = FindObjectOfType<HealthBar>();

            UpdateUICounters();
        }

        private void Update()
        {
            healthBar.gameObject.SetActive(healthBarEnabled);
            coinsParentObject.gameObject.SetActive(coinsEnabled);
            keysParentObject.gameObject.SetActive(keysEnabled);

            if (healthBar != null && healthBar.currentHealth <= 0 && !player.dead)
                RespawnPlayer();
        }

        private IEnumerator InvulnerableCo()
        {
            player.invulnerable = true;

            var transparent = true;
            var invulnTimer = 0f;
            while (invulnTimer < player.invulnerabilityWindow)
            {
                var alpha = transparent ? 0.5f : 1.0f;
                invulnTimer += player.flashTimer;
                player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
                transparent = !transparent;

                yield return new WaitForSeconds(player.flashTimer);
            }

            player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            player.invulnerable = false;
        }

        private IEnumerator RespawnPlayerCo()
        {
            var playerTransform = player.transform;

            var playerPosition = playerTransform.position;
            var playerRotation = playerTransform.rotation;

            var cameraPosition = _mainCamera.transform.position;
            var effectPosition = player.isInKillZone
                ? new Vector3(playerPosition.x, cameraPosition.y - _mainCamera.orthographicSize)
                : playerPosition;

            player.Kill();
            if(deathEffectEnabled)
                Instantiate(deathEffect, effectPosition, playerRotation);

            yield return new WaitForSeconds(waitToRespawn);

            _mainCamera.GetComponent<CameraController>().cameraBounds = startCameraBounds;
            player.transform.position = player.respawnPosition;
            player.Respawn();

            SetHealthMax();
            SetCoinCount((int) Math.Ceiling((float) coinCount / 2));
        }

        private void UpdateCoinText()
        {
            coinText.text = $"{coinCount}";
        }

        private void UpdateKeyText()
        {
            keyText.text = $"{keyCount}";
        }

        private void UpdateUICounters()
        {
            UpdateCoinText();
            UpdateKeyText();
        }

        #endregion
    }
}