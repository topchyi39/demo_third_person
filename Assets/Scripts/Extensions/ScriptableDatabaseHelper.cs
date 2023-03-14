using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Extensions
{
    public static class ScriptableDatabaseHelper
    {
        private const string DatabasePath = "Assets/Database";
        private const string ItemPath = "Items";

        /// <summary>
        /// Creates and returns a clone of any given scriptable object.
        /// </summary>
        public static void Clone<T>(this T scriptableObject, string name, Action onDone, Action<string> onError) where T : ScriptableObject
        {
            var path = GetPathByType(scriptableObject);
            var instance = Object.Instantiate(scriptableObject);
            var url = $"{DatabasePath}/{path}/{name}.asset";
            
            var checkedAsset = AssetDatabase.LoadAssetAtPath<T>(url);

            if (checkedAsset != null)
            {
                onError?.Invoke("Name already used");
            }
            
            AssetDatabase.CreateAsset(instance, $"{DatabasePath}/{path}/{name}.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = instance;
            onDone?.Invoke();
        }

        /// <summary>
        /// Delete scriptable object.
        /// </summary>
        /// <param name="objectToDelete"></param>
        /// <typeparam name="T"></typeparam>
        public static void Delete<T>(this T objectToDelete, string name) where T : ScriptableObject
        {
            var asset = objectToDelete.FindInDataBase(name);
            
            if(asset == null) return;
            
            string pathToDelete = AssetDatabase.GetAssetPath(asset);      
            AssetDatabase.DeleteAsset(pathToDelete);
            AssetDatabase.SaveAssets();
        }

        /// <summary>
        /// Load scriptable object from path.
        /// </summary>
        /// <param name="path"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Load<T>(string path) where T : ScriptableObject
        {
            var asset = Resources.Load<T>(path);
            return asset;
        }

        private static T FindInDataBase<T>(this T target, string name) where T : ScriptableObject
        {
            var targetFolder = GetPathByType(target);
            var asset = AssetDatabase.LoadAssetAtPath<T>($"{DatabasePath}/{targetFolder}/{name}.asset");
            return asset;
        }

        private static string GetPathByType<T>(T target) where T : ScriptableObject
        {
            var itemsTypes = typeof(T).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => typeof(T).IsAssignableFrom(x));

            var type = target.GetType();

            if (itemsTypes.Any(t => t == type))
            {
                return ItemPath;
            }

            return String.Empty;
        }
    }
}