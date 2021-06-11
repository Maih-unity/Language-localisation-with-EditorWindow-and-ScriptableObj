using UnityEngine;
using TMPro;


[CreateAssetMenu(menuName ="Languages")]
public class LanguageScriptableObj : ScriptableObject
{
    private string id;
    [SerializeField]
    private TMP_FontAsset tmpFontAsset;
    [SerializeField]
    private Font font;
    [SerializeField]
    private string region;
    [SerializeField]
    private string[] texts;
    [SerializeField]
    private int[] fontSize;



    public string ID
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
        }
    }

    public string GetText(int id)
    {
        return texts[id];
    }

    public int GetFontSize(int id)
    {
        return fontSize[id];
    }
    public string Region
    {
        get
        {
            return region;
        }
        set
        {
            region = value;
        }
    }

    public Font Font
    {
        get
        {
            return font;
        }
    }
    public TMP_FontAsset FontAsset
    {
        get
        {
            return tmpFontAsset;
        }
    }
    public string[] Texts
    {
        get
        {
            return texts;
        }
    }
    public int[] FontSize
    {
        get
        {
            return fontSize;
        }
    }
}
