using System;
using UnityEngine;

namespace Player.Components.MovementComponents.Data
{
    [Serializable]
    public class MovementLayerData
    {
        [SerializeField] private LayerMask groundLayer;

        public LayerMask GroundLayer => groundLayer;
        
        public bool ContainsLayer(LayerMask layerMask, int layer)
        {
            return (1 << layer & layerMask) != 0;
        }

        public bool IsGroundLayer(int layer)
        {
            return ContainsLayer(groundLayer, layer);
        }
    }
}