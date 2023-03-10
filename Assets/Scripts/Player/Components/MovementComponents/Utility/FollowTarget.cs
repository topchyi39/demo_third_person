using System;
using UnityEngine;

namespace Player.Components.MovementComponents.Utility
{
    public class FollowTarget : MonoBehaviour
    {
        private Transform _transform;
        private Transform _target;
        private Vector3 _offset;
        
        private void Awake()
        {
            _transform = transform;
            _offset = _transform.localPosition;
            _target = _transform.parent;
            _transform.SetParent(null);
        }

        private void Update()
        {
            _transform.position = _target.position + _offset;
        }
    }
}