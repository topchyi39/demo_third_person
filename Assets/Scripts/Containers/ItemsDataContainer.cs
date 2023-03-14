using System;
using Extensions;
using ItemsCore.Items.Data.Base;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace Containers
{
    [InlineEditor]
    [CreateAssetMenu(fileName = "DataContainer/ItemsDataContainer", menuName = "DataContainer/ItemsDataContainer", order = 0)]
    public class ItemsDataContainer : DataContainer<BaseItemData>
    {
        public override void AfterListChanges(CollectionChangeInfo info)
        {
            int index = elements.IndexOf((BaseItemData)info.Value);

            if(info.ChangeType is CollectionChangeType.Add or CollectionChangeType.Insert)
            {
                var targetObject = elements[index];
                targetObject.id = Guid.NewGuid().GetHashCode();
            }
        }

        public override void BeforeListChanges(CollectionChangeInfo info)
        {
            if(info.ChangeType is CollectionChangeType.RemoveIndex or CollectionChangeType.RemoveValue)
            {
                var targetObject = elements[info.Index];
                targetObject.Delete(targetObject.name);
            }
        }
    }
}