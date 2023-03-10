using UnityEngine;

namespace Player.Components.MovementComponents.States.Data
{
    [CreateAssetMenu(menuName = "Cossack/Movement/MovementData", fileName = "MovementData", order = 0)]
    public class MovementData : ScriptableObject
    {
        [SerializeField] private float baseSpeed;
        [SerializeField] private MovementStateData walkData;


        public float BaseSpeed => baseSpeed;
        public MovementStateData WalkData => walkData;
    }
}