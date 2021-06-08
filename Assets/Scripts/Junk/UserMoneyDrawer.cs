using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserMoneyDrawer : MonoBehaviour
{
    public UserInvenContext Context;
    public Text Text;

    private void Update()
    {
        if (Context != null)
            Text.text = Context.Money.ToString();
    }
}
