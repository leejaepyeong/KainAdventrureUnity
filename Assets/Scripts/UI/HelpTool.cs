using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="HelpTool",menuName ="Info/HepTool")]
public class HelpTool : ScriptableObject
{

    public string HelpTiltle;

    [TextArea]
    public string[] HelpTxt;

    public Image[] HelpImg;

}
