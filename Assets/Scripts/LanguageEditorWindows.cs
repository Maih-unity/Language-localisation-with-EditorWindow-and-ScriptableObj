using UnityEditor;
using UnityEngine;

public class LanguageEditorWindows : EditorWindow
{
    protected SerializedObject serializedObject;
    protected SerializedProperty serializedProperty;

    protected LanguageScriptableObj[] languages;
    protected string selectedPropertyPach;
    protected string selectedProperty;

    [MenuItem("Window/Languages")]
    protected static void ShowWindow()
    {
        GetWindow<LanguageEditorWindows>("Languages");
    }

    private void OnGUI()
    {
        languages = GetAllInstances<LanguageScriptableObj>();
        if (languages.Length>0)
        {
            serializedObject = new SerializedObject(languages[0]);
        }
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));
        DrawSliderBar(languages);
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
        if (selectedProperty != null)
        {
            for (int i = 0; i < languages.Length; i++)
            {
                if (languages[i].ID == selectedProperty)
                {

                    serializedObject = new SerializedObject(languages[i]);
                    serializedProperty = serializedObject.GetIterator();
                    serializedProperty.NextVisible(true);
                    DrawProperties(serializedProperty);

                }

            }

        }
        else
        {
            EditorGUILayout.LabelField("select an item from the list");
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        Apply();
        
    }

    protected void DrawProperties(SerializedProperty p)
    {
        while (p.NextVisible(false))
        {
            EditorGUILayout.PropertyField(p, true);
        }
        Repaint();
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
    protected void DrawSliderBar(LanguageScriptableObj[]prop)
    {
        foreach (LanguageScriptableObj p in prop)
        {
            if (GUILayout.Button(p.ID))
            {
                selectedPropertyPach = p.ID;
            }

        }
        if (!string.IsNullOrEmpty(selectedPropertyPach))
        {
            selectedProperty = selectedPropertyPach;
        }
        if (GUILayout.Button("New Language"))
        {

            LanguageScriptableObj newLanguage = LanguageScriptableObj.CreateInstance<LanguageScriptableObj>();
            CreateLanguageEditorWindow newLanguageWindow = GetWindow<CreateLanguageEditorWindow>("New Language");
            newLanguageWindow.newLanguage = newLanguage;
        }
    }

    protected void Apply()
    {
        if (languages.Length>0)
        {
            serializedObject.ApplyModifiedProperties();
        }
    }
}
