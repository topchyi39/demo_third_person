using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class TimeController : MonoBehaviour
    {
        [SerializeField] private float time;


        private void Update()
        {
            Time.timeScale = time;
        }
    }
}