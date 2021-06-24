using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapChange : MonoBehaviour
{
    public AudioClip sound;
    public Text mapText;

    void ChangeMap()
    {
        Event.instanse.audio.clip = sound;
        Event.instanse.audio.Play();

        mapText.text = "- " + gameObject.name + " -";
        mapText.gameObject.SetActive(true);

        Invoke("Disappear",2.8f);
    }

    void Disappear()
    {
        mapText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            ChangeMap();
    }


}
