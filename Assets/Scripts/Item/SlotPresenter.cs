using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotPresenter : MonoBehaviour
{
    public List<Slot> slots;

    private void Awake()
    {
        slots = GetComponentsInChildren<Slot>().ToList();
        
    }
}
