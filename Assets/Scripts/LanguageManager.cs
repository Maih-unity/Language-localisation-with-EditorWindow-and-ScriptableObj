using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{

    [SerializeField]
    private bool isTMPRO;
    [SerializeField]
    private string region;
    private LanguageScriptableObj[] languages;
    [SerializeField]
    private int textID;

    private void OnEnable()
    {
        languages = GetAllInstances<LanguageScriptableObj>();
        foreach (LanguageScriptableObj language in languages)
        {
            if (language.Region==region)
            {
                if (isTMPRO)
                {
                    gameObject.GetComponent<TextMeshProUGUI>().font = language.FontAsset;
                    gameObject.GetComponent<TextMeshProUGUI>().text = language.GetText(textID);
                    gameObject.GetComponent<TextMeshProUGUI>().fontSize = language.GetFontSize(textID);

                }
                else
                {
                    gameObject.GetComponent<Text>().font = language.Font;
                    gameObject.GetComponent<Text>().text= language.GetText(textID);
                    gameObject.GetComponent<Text>().fontSize = language.GetFontSize(textID);
                }

            }
            

        }
       
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
}
