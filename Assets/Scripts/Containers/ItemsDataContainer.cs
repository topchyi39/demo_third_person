using System;
using System.Linq;
using Extensions;
using ItemsCore.Items.Data.Base;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Containers
{
    [InlineEditor]
    [CreateAssetMenu(fileName = "DataContainer/ItemsDataContainer", menuName = "DataContainer/ItemsDataContainer", order = 0)]
    public class ItemsDataContainer : DataContainer<BaseItemData>
    {
        protected override void Initialize()
        {
            elements.RemoveAll(element => element == null);
            
            foreach (var element in elements)
            {
                element.onNameValidation += OnNameValidation;
                element.onNameChanged += AssetNameChanged;
            }
        }

        protected override void Disable()
        {
            foreach (var element in elements)
            {
                element.onNameValidation -= OnNameValidation;
                element.onNameChanged -= AssetNameChanged;
            }

            CheckForUnnamedElement();
        }

        private void CheckForUnnamedElement()
        {
            var nonNamedElements = elements
                .Where(e => String.IsNullOrEmpty(e.itemName))
                .Select(e => e)
                .ToList();

            foreach (var element in nonNamedElements)
            {
                element.Delete("Item");
            }
        }

        public override void AfterListChanges(CollectionChangeInfo info)
        {
            var targetElement = (BaseItemData)info.Value;
            int index = elements.IndexOf(targetElement);

            if(info.ChangeType is CollectionChangeType.Add or CollectionChangeType.Insert)
            {
                var targetObject = elements[index];
                var asset = targetObject.Clone("Item");
                elements.Remove(targetObject);
                elements.Add(asset);
                
                asset.onNameValidation += OnNameValidation;
                asset.onNameChanged += AssetNameChanged;
                asset.SetNewID(GenerateID());
            }
        }

        private int GenerateID()
        {
            if (elements.Count == 0)
            {
                return 0;
            }
            
            var maxId = elements.Max(e => e.id);
            return ++maxId;
        }

        public override void BeforeListChanges(CollectionChangeInfo info)
        {
            if(info.ChangeType is CollectionChangeType.RemoveIndex or CollectionChangeType.RemoveValue)
            {
                var targetObject = elements[info.Index];
                targetObject.onNameChanged -= AssetNameChanged;
                targetObject.Delete(targetObject.itemName);
            }
        }

        private bool OnNameValidation(string itemName, int id)
        {
            if (elements.Count == 1) return true;
            
            var items = elements
                .Where(i => 
                    !String.IsNullOrEmpty(i.itemName) && 
                    i.itemName.Equals(itemName) && 
                    i.id != id)
                .Select(i => i)
                .ToArray();

            return items.Length <= 0;
        }
        
        private void AssetNameChanged(BaseItemData itemData, string value)
        {
            var path = AssetDatabase.GetAssetPath(itemData);

            if (String.IsNullOrEmpty(path))
            {
                var asset = itemData.Clone(value);
                asset.SetName(value);
                asset.onNameValidation += OnNameValidation;
                asset.onNameChanged += AssetNameChanged;
                asset.SetNewID(GenerateID());
                elements.Remove(itemData);
                elements.Add(asset);
                return;
            }
            
            AssetDatabase.RenameAsset(path, value);
        }
    }
}