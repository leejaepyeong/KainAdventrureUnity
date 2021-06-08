using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new user inven context", menuName = "User/Inven")]
public class UserInvenContext : ScriptableObject
{
    public int Money;
    // 추가적인 것들

    // 오직 단 하나의 인벤만 존제한다고 할 경우
    private void Awake()
    {
        Money = PlayerPrefs.GetInt(nameof(Money), 0);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt(nameof(Money), Money);
        PlayerPrefs.Save();
    }
}
