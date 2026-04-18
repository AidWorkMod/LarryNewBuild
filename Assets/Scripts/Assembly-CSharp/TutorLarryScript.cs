using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorLarryScript : MonoBehaviour
{
    [SerializeField]
    AudioSource AudioDevice;

    [SerializeField]
    AudioClip Larry_Hi;

    [SerializeField]
    AudioClip Larry_Nice;

    [SerializeField]
    AudioClip Larry_Coin;

    [SerializeField]
    float DistanceTalking;

    [SerializeField]
    Transform Player;

    [SerializeField]
    bool TriggeredHi;

    [SerializeField]
    bool LetEmTalk;

    [SerializeField]
    public bool IsAngry;

    [SerializeField]
    public bool IsPrizing;

    [SerializeField]
    bool GivePrize;

    [SerializeField]
    Animator Larry_Ani;

    [SerializeField]
    GameControllerScript gc;

    [SerializeField]
    GameObject Trigger0;

    [SerializeField]
    GameObject Trigger1;


    void Update()
    {
        if (Vector3.Distance(transform.position, Player.position) < DistanceTalking)
        {
            if (gc.notebooks == 1 && GivePrize == false)
            {
                Trigger0.SetActive(true);
                Trigger1.SetActive(true);
                AudioDevice.Stop();
                AudioDevice.PlayOneShot(Larry_Coin);
                IsPrizing = true;
                GivePrize = true;
            }
            if (TriggeredHi == false)
            {
                StartTalking();
            }
        } 
       if(LetEmTalk == true)
        {
            if(AudioDevice.isPlaying == true)
            {
                if(IsAngry == true)
                {
                    Larry_Ani.SetBool("IsYappingAngry", true);
                    Larry_Ani.SetBool("IsYappingHappy", false);
                    Larry_Ani.SetBool("IsYapping", false);
                }
                else
                {   if (IsPrizing == true)
                    {
                        Larry_Ani.SetBool("IsYappingHappy", true);
                        Larry_Ani.SetBool("IsYapping", false);
                        Larry_Ani.SetBool("IsYappingAngry", false);
                    }
                    else
                    {
                        Larry_Ani.SetBool("IsYapping", true);
                        Larry_Ani.SetBool("IsYappingHappy", false);
                        Larry_Ani.SetBool("IsYappingAngry", false);
                    }
                }
            }
            else
            {
                Trigger0.SetActive(false);
                Trigger1.SetActive(false);
                if (IsAngry == true)
                {
                    Larry_Ani.SetBool("IsYappingAngry", false);
                }
                else
                {
                    if (IsPrizing == true)
                    {
                        Larry_Ani.SetBool("IsYappingHappy", false);
                    }
                    else
                    {
                        Larry_Ani.SetBool("IsYapping", false);
                    }
                }
            }
        }
    }

    void StartTalking()
    {
        StartCoroutine(Yap());
        IEnumerator Yap()
        {
            TriggeredHi = true;
            Larry_Ani.SetTrigger("Wave");
            AudioDevice.PlayOneShot(Larry_Hi);
            yield return new WaitForSeconds(2f);
            LetEmTalk = true;
        }
    }
}
