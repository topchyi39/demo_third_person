
#if UNITY_EDITOR
using System;
using System.Linq;
using System.Reflection;
using ModestTree;
using UnityEditor;
using UnityEngine;
using Screen = UI.Screens.Screen;

namespace UI.Editor
{
    [CustomEditor(typeof(UIManager))]
    public class UIManagerEditor : UnityEditor.Editor
    {
        private int _index;
        private UIManager _target;

        private Type[] _types;
        private string[] _typeNames;

        private void OnEnable()
        {
            _target = target as UIManager;
            _types = Assembly.GetAssembly(typeof(Screen)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Screen))).ToArray();
            _typeNames = _types.Select(myType => myType.Name).ToArray();
        }

        public override void OnInspectorGUI()
        {
            _index = EditorGUILayout.Popup("Default Screen", _index, _typeNames);
            if (_types[_index].FullName != _target.GetDefaultScreenTypeName())
            {
                _target.SetDefaultScreen(_types[_index].FullName);
                EditorUtility.SetDirty(_target);
            }
            base.OnInspectorGUI();
        }
    }
}
#endif