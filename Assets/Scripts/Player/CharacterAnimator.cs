using UnityEngine;

namespace Player
{
    public class CharacterAnimator : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        public void SetBool(int key, bool value)
        {
            _animator.SetBool(key, value);
        }

        public void SetFloat(int key, float value)
        {
            _animator.SetFloat(key, value);
        }
        
        public void SetFloatSmooth(int key, float value)
        {
            _animator.SetFloat(key, value, 0.01f, Time.deltaTime);
        }
    }
}