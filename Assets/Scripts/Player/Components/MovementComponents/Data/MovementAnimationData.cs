using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player.Components.MovementComponents.Data
{
    [Serializable]
    public class MovementAnimationData
    {
        [SerializeField] private string verticalAxisParameter = "VerticalAxis";
        [SerializeField] private string horizontalAxisParameter = "HorizontalAxis";
        [SerializeField] private string idleParameter = "Idle";
        [SerializeField] private string walkParameter = "Walk";

        private int? verticalAxisKey;
        private int? horizontalAxisKey;
        private int? _idleKey;
        private int? _walkKey;

        public int VerticalKey => verticalAxisKey ??= Animator.StringToHash(verticalAxisParameter);
        public int HorizontalKey => horizontalAxisKey ??= Animator.StringToHash(horizontalAxisParameter);
        public int IdleKey => _idleKey ??= Animator.StringToHash(idleParameter);
        public int WalkKey => _walkKey ??= Animator.StringToHash(walkParameter);

    }
}