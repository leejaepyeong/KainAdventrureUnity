using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TestSerialize : MonoBehaviour
{
    public ScriptableObject os;

    public SlotPresenter invent;
    public void onClick()
    {
        foreach(var slot in invent.slots)
        {
            string s = JsonUtility.ToJson(slot.item);
        }
       
        string s2 = JsonUtility.ToJson(os);
    }
}
