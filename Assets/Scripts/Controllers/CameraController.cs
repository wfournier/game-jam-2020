using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class CameraController : MonoBehaviour
    {
        #region Declarations --------------------------------------------------
        public GameObject target;
        public float followAhead;
        public float smoothing;
        public BoxCollider2D cameraBounds;

        private Vector3 _targetPosition;
        private Vector3 _min;
        private Vector3 _max;
        private float _cameraOrthographicSize;
        #endregion


        #region Private/Protected Methods -------------------------------------
        public void Start()
        {
            _min = cameraBounds.bounds.min;
            _max = cameraBounds.bounds.max;
            _cameraOrthographicSize = GetComponent<Camera>().orthographicSize;
        }

        // Update is called once per frame
        private void Update()
        {
            var cameraHalfWidth = _cameraOrthographicSize * ((float)Screen.width / Screen.height);

            _targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);

            var newX = target.transform.localScale.x > 0f ? 
                _targetPosition.x + followAhead :
                _targetPosition.x - followAhead;

            
            var x = Mathf.Clamp(newX, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);
            var y = Mathf.Clamp(_targetPosition.y, _min.y + _cameraOrthographicSize, _max.y + cameraHalfWidth);

            _targetPosition = new Vector3(x, y, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, _targetPosition, smoothing * Time.deltaTime);
        }

        #endregion
    }
}