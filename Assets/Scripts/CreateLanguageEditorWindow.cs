using UnityEditor;
using UnityEngine;

public class CreateLanguageEditorWindow : EditorWindow
{

    private SerializedObject serializedObject;
    private SerializedProperty serializedProperty;

    protected LanguageScriptableObj[] languages;
    public LanguageScriptableObj newLanguage;

    private void OnGUI()
    {
        serializedObject = new SerializedObject(newLanguage);
        serializedProperty = serializedObject.GetIterator();
        serializedProperty.NextVisible(true);
        DrawProperties(serializedProperty);
        if (GUILayout.Button("save"))
        {
            languages = GetAllInstances<LanguageScriptableObj>();
            if (newLanguage.ID==null)
            {
                newLanguage.ID = "Language" + (languages.Length + 1);
            }
            AssetDatabase.CreateAsset(newLanguage, "Assets/Scripts/Language" + (languages.Length + 1) + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Close();

        }

        Apply();
    }
    public static T[] GetAllInstances<T>() where T : LanguageScriptableObj
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
        T[] a = new T[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
        }

        return a;

    }
    protected void DrawProperties(SerializedProperty p)
    {
        while (p.NextVisible(false))
        {
            EditorGUILayout.PropertyField(p, true);
        }
    }
    protected void Apply()
    {
        serializedObject.ApplyModifiedProperties();
    }
}
