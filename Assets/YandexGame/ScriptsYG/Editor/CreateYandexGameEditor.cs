using UnityEditor;
using UnityEngine;

namespace YG.Insides
{
    public class CreateYandexGameEditor
    {
        [MenuItem("Tools/PluginYG/Create YandexGame Object", false, 101)]
        public static void InsertPrefab()
        {
            string fileLocation = "Assets/YandexGame/Prefabs/YandexGame.prefab";
            UnityEngine.GameObject prefab = AssetDatabase.LoadAssetAtPath(fileLocation, typeof(UnityEngine.GameObject)) as UnityEngine.GameObject;

            if (prefab == null)
                Debug.LogError("Yandex Game prefab not found! It should be located along the way: Assets/YandexGame/Prefabs/YandexGame.prefab\n(en) Yandexgame prefab not found! It should be located along the path: Assets/YandexGame/Prefabs/YandexGame.prefab");
            else
            {
                PrefabUtility.InstantiatePrefab(prefab);
                prefab.transform.position = new Vector3(0f, 0f, 0f);
                Undo.RegisterCreatedObjectUndo(SceneAsset.FindAnyObjectByType<YandexGame>().gameObject, "Create YandexGame");
            }
        }
    }
}