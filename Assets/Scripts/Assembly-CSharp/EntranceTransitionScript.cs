using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceTransitionScript : MonoBehaviour
{
    [SerializeField]
    AudioSource OutdoorsMusic;
    [SerializeField]
    AudioSource SchoolMusic;

    private void OnTriggerEnter(Collider other)
    {
        OutdoorsMusic.Stop();
        SchoolMusic.Play();
        gameObject.SetActive(false);
    }
}
