using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class CameraController : MonoBehaviour
    {
        #region Declarations --------------------------------------------------
        public PlayerController player;
        public float followAhead;
        public float smoothing;
        public Collider2D cameraBounds;

        private Vector3 _targetPosition;
        private Vector3 _min;
        private Vector3 _max;
        private float _cameraOrthographicSize;
        #endregion


        #region Private/Protected Methods -------------------------------------
        public void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            if (player == null) return;
            
            _min = cameraBounds.bounds.min;
            _max = cameraBounds.bounds.max;
            _cameraOrthographicSize = GetComponent<Camera>().orthographicSize;
            
            if (!player.firstGrounded) return;
            
            var cameraHalfWidth = _cameraOrthographicSize * ((float)Screen.width / Screen.height);

            _targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

            var newX = player.transform.localScale.x > 0f ? 
                _targetPosition.x + followAhead :
                _targetPosition.x - followAhead;

            
            var x = Mathf.Clamp(newX, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);
            var y = Mathf.Clamp(_targetPosition.y, _min.y + _cameraOrthographicSize, _max.y - _cameraOrthographicSize);

            _targetPosition = new Vector3(x, y, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, _targetPosition, smoothing * Time.deltaTime);
        }

        #endregion
    }
}