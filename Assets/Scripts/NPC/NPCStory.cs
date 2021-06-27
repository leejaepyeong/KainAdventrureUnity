using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName ="Story",menuName ="Info/story")]
public class NPCStory : ScriptableObject
{
    [TextArea]
    public string[] StoryTxt;   // story Text
    public NPCDB[] npcDB; // npc Info

    public int[] stroyTurn;
}
