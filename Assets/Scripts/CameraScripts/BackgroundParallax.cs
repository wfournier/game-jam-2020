using UnityEngine;

namespace Assets.Scripts.CameraScripts
{
    public class BackgroundParallax : MonoBehaviour
    {
        [Tooltip("The position of the camera in the previous frame.")]
        private Vector3 _previousCamPos;

        public Transform[] backgrounds;

        [Tooltip("How much less each successive layer should parallax.")]
        public float parallaxReductionFactor;

        [Tooltip("The proportion of the camera's movement to move the backgrounds by.")]
        public float parallaxScale;

        public float smoothing;

        private void Start()
        {
            _previousCamPos = transform.position;
        }

        private void Update()
        {
            // The parallax is the opposite of the camera movement since the previous frame multiplied by the scale.
            var parallax = (_previousCamPos.x - transform.position.x) * parallaxScale;

            for (var i = 0; i < backgrounds.Length; i++)
            {
                // ... set a target x position which is their current position plus the parallax multiplied by the reduction.
                var backgroundTargetPosX = backgrounds[i].position.x + parallax * (i * parallaxReductionFactor + 1);
                // Create a target position which is the background's current position but with it's target x position.
                var backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y,
                    backgrounds[i].position.z);
                // Lerp the background's position between itself and it's target position.
                backgrounds[i].position =
                    Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
            }

            // Set the previousCamPos to the camera's position at the end of this frame.
            _previousCamPos = transform.position;
        }
    }
}