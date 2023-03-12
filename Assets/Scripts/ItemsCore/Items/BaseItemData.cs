using System;
using UnityEngine;

namespace ItemsCore.Items
{
    [CreateAssetMenu(fileName = "Items/BaseData", menuName = "Items/BaseData", order = 0)]
    public class BaseItemData : ScriptableObject
    {
        [SerializeField] private string name;
        [SerializeField] private bool isStackable;
        [SerializeField] private string id = Guid.NewGuid().ToString();

        public string Name => name;
        public string ID => id;
        public bool IsStackable => isStackable;
    }
}