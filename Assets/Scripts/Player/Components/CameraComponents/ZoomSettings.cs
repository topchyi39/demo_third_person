using System;
using UnityEngine;

namespace Player.Components.CameraComponents
{
    [Serializable]
    public class ZoomSettings
    {
        [SerializeField] private float zoomSensitivity = 2f;
        [SerializeField] private float smoothing = 5f;
        [Space]
        [SerializeField] private float defaultDistance = 2f;
        [SerializeField] private float minDistance = 1f;
        [SerializeField] private float maxDistance = 3f;

        public float ZoomSensitivity => zoomSensitivity;
        public float Smoothing => smoothing;
        public float DefaultDistance => defaultDistance;
        public float MinDistance => minDistance;
        public float MaxDistance => maxDistance;
    }
}