using System;
using Extensions;
using ItemsCore.Items.Data.OtherData;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ItemsCore.Items.Data.Base
{
    [InlineEditor]
    public abstract class BaseItemData : ScriptableObject
    {
        private string _lastName = String.Empty;
        private const string ItemPath = "Items";
        
        [HorizontalGroup("Base", Width = 150)]
        
        [VerticalGroup("Base/Left")]
        [LabelWidth(90)]
        [HideLabel, Title("Name", Bold = true, HorizontalLine = false, TitleAlignment = TitleAlignments.Centered)]
        public string name;
        [VerticalGroup("Base/Left")]
        [PreviewField(145)]
        [HideLabel]
        public Sprite icon;
        
        [VerticalGroup("Base/Right")]
        [TextArea(5,5)]
        [HideLabel, Title("Description", Bold = true, HorizontalLine = false, TitleAlignment = TitleAlignments.Centered)]
        public string description;
        
        [HorizontalGroup("Base/Right/Lower")]
        
        [VerticalGroup("Base/Right/Lower/Middle")]
        [LabelWidth(50)] 
        [ReadOnly]
        public int id;
        
        [VerticalGroup("Base/Right/Lower/Middle")]
        [LabelWidth(50)] 
        [Range(0, 30)]
        public float weight;
        
        [VerticalGroup("Base/Right/Lower/Middle")]
        [LabelWidth(50)] 
        [Range(0, 10000)]
        public float cost;
        
        [VerticalGroup("Base/Right/Lower/Middle")]
        [LabelWidth(50)] 
        public RarityType rarity;

        private string _buttonName = NothingToSave;
        
        private const string NeedToSave = "Save";
        private const string NothingToSave = "Nothing to save";
        
        [Button("$_buttonName", Icon = SdfIconType.Save, IconAlignment = IconAlignment.LeftOfText)]
        public void Save()
        {
            if (!Validate()) return;

            if (!_lastName.Equals(name))
            {
                this.Delete(_lastName);
            }
            
            this.Clone(name, () =>
                {
                    _lastName = name;
                    _buttonName = NothingToSave;
                },
                s =>
                {
                    _buttonName = s;
                });
        }

        private void OnValidate()
        {
            _buttonName = NeedToSave;
        }

        protected bool Validate()
        {
            bool isValidated = !String.IsNullOrEmpty(name);
            return isValidated;
        }
    }
}