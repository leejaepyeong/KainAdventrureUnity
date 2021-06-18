using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="NPC", menuName ="NPC/NPCInfo")]
public class NPCDB : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string helpComment;
#endif

    [TextArea]
    public string charterName;
    public Sprite charcterImg;
}
