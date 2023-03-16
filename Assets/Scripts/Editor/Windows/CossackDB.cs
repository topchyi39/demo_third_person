using Containers;
using Extensions;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor.Windows
{
    public class CossackDB : OdinMenuEditorWindow
    {
        private const string Container = "Containers/";
        private const string ItemsContainer = "ItemsDataContainer";
        
        [MenuItem("Cossack/CossackDB")]
        private static void OpenWindow()
        {
            var window = GetWindow<CossackDB>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1000, 800);
        }

        [SerializeField]
        private ItemsDataContainer itemsDataContainer;

        [OnInspectorInit]
        public void Init()
        {
            itemsDataContainer = ScriptableDatabaseHelper.Load<ItemsDataContainer>(Container + ItemsContainer);
        }
        
        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree(supportsMultiSelect: true)
            {
                { "ItemsWindow", this,  EditorIcons.Folder },
            };
            
            return tree;
        }
    }
}