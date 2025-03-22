using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UITextSO", menuName = "Scriptable Objects/UITextSO")]
public class UITextSO : ScriptableObject
{
    public string Name;
    public List<LocalizedText> Translations;
}

[System.Serializable]
public class LocalizedText {
    public Language Language;
    public string Text;
}