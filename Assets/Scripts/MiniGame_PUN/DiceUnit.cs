using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceUnit : MonoBehaviour
{
    public List<GameObject> FoundObjects;
    public GameObject Target;

    private void Start()
    {
        if(gameObject.tag == "Player1")
            FoundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player2"));
        if (gameObject.tag == "Player2")
            FoundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player1"));
    }
}
