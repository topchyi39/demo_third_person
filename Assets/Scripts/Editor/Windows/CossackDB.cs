using Containers;
using Extensions;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;

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
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
        }

        [OnInspectorInit("Init")] 
        public ItemsDataContainer itemsDataContainer;

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
            
            tree.SortMenuItemsByName();
            return tree;
        }
    }
}