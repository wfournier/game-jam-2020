using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Platforms
{
    public class FollowPath : MonoBehaviour
    {
        public enum FollowType
        {
            MoveTowards,
            Lerp
        }

        private IEnumerator<Transform> _currentPoint;
        public float maxDistanceToGoal = .1f;
        public PathDefinition path;
        public float speed = 1;

        public FollowType type = FollowType.MoveTowards;

        public void Start()
        {
            if (path == null)
            {
                Debug.LogError("Path cannot be null", gameObject);
                return;
            }

            _currentPoint = path.GetPathsEnumerator();
            _currentPoint.MoveNext();

            if (_currentPoint == null)
                return;

            if (_currentPoint.Current != null)
                transform.position = _currentPoint.Current.position;
        }

        public void Update()
        {
            if (_currentPoint == null || _currentPoint.Current == null)
                return;

            if (type == FollowType.MoveTowards)
                transform.position = Vector3.MoveTowards(transform.position, _currentPoint.Current.position,
                    Time.deltaTime * speed);
            else if (type == FollowType.Lerp)
                transform.position = Vector3.Lerp(transform.position, _currentPoint.Current.position,
                    Time.deltaTime * speed);


            var distanceSquared = (transform.position - _currentPoint.Current.position).sqrMagnitude;

            if (distanceSquared < maxDistanceToGoal * maxDistanceToGoal)
                _currentPoint.MoveNext();
        }
    }
}