using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class JSONTest : MonoBehaviour
{
    public InventoryData inven;


    public void OnT()
    {

        string s = JsonUtility.ToJson(inven);
        
    }


}
