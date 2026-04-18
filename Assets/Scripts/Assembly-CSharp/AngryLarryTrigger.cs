using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryLarryTrigger : MonoBehaviour
{
    [SerializeField]
    TutorLarryScript LarryScript;

    [SerializeField]
    AudioSource LarryAudio;

    [SerializeField]
    AudioClip DidntFinishedTalk;

    [SerializeField]
    GameObject[] ObjectsToDissable;
    private void OnTriggerEnter(Collider other)
    {
        if(LarryScript.IsAngry == false)
        {
            LarryScript.IsAngry = true;
        }
        if (LarryScript.IsPrizing)
        {
            LarryScript.IsPrizing = false;
        }
        LarryAudio.Stop();
        LarryAudio.PlayOneShot(DidntFinishedTalk);
        foreach (GameObject obj in ObjectsToDissable)
        {
            obj.SetActive(false);
        }
    }
}
