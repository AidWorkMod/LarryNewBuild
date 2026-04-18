using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000B9 RID: 185
public class DoorScript : MonoBehaviour
{
	// Token: 0x0600093A RID: 2362 RVA: 0x0002114F File Offset: 0x0001F54F
	private void Start()
	{
		this.myAudio = base.GetComponent<AudioSource>();
	}

	// Token: 0x0600093B RID: 2363 RVA: 0x00021170 File Offset: 0x0001F570
	private void Update()
	{
		if (LockTimerActive)
		{
            if (this.lockTime > 0f) // If the lock time is greater then 0, decrease lockTime
            {
                this.lockTime -= 1f * Time.deltaTime;
            }
            else if (this.bDoorLocked) //If the door is locked, unlock it
            {
                this.UnlockDoor();
            }
        }
		if (this.openTime > 0f) // If the open time is greater then 0, decrease lockTime Decrease open time
		{
			this.openTime -= 1f * Time.deltaTime;
		}
		if (this.openTime <= 0f & this.bDoorOpen)
		{
			ShutDoor();
		}
		if (Input.GetMouseButtonDown(0) && Time.timeScale != 0f && !bDoorOpen) //If the door is left clicked and the game isn't paused
		{
			Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider == Trigger & Vector3.Distance(this.player.position, base.transform.position) < this.openingDistance))
			{
				if (this.baldi.isActiveAndEnabled & this.silentOpens <= 0)
				{
					this.baldi.Hear(base.transform.position, 1f); //If the door isn't silent, Baldi hears the door with a priority of 1.
				}
				if (!bDoorLocked)
				{
                    this.OpenDoor();
                }
				else
				{
                    DoorAnimator.SetTrigger("Rattle");
                    this.myAudio.PlayOneShot(this.Jiggle, 1f); //Play the door close sound
                }
				if (this.silentOpens > 0) //If the door is silent
				{
					this.silentOpens--; //Decrease the amount of opens the door will stay quite for.
				}
			}
		}
	}

	// Token: 0x0600093C RID: 2364 RVA: 0x0002135C File Offset: 0x0001F75C
	public void OpenDoor()
	{
		if (this.silentOpens <= 0 && !this.bDoorOpen) //Play the door sound if the door isn't silent
		{
			this.myAudio.PlayOneShot(this.doorOpen, 1f);
		}
		Barrier.SetActive(false);
		this.bDoorOpen = true; //Set the door open status to false
		DoorAnimator.SetTrigger("Open");
        this.openTime = 3f; //Set the open time to 3 seconds
	}

	public void ShutDoor()
    {
        if (this.silentOpens <= 0) //If the door isn't silent
        {
            this.myAudio.PlayOneShot(this.doorClose, 1f); //Play the door close sound
        }
        DoorAnimator.SetTrigger("Shut");
		bDoorOpen = false;
		StartCoroutine(ActivateBarrier());
		IEnumerator ActivateBarrier()
		{
			yield return new WaitForSeconds(0.5f);
            Barrier.SetActive(true);
			StopCoroutine(ActivateBarrier());
			yield break;
        }
    }

	// Token: 0x0600093D RID: 2365 RVA: 0x000213E2 File Offset: 0x0001F7E2
	private void OnTriggerStay(Collider other)
	{
		if (!this.bDoorLocked & other.CompareTag("NPC")) //Open the door if it isn't locked and it is an NPC
		{
			this.OpenDoor();
		}
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x00021404 File Offset: 0x0001F804
	public void LockDoor(float time, bool hastimer) //Lock the door for a specified amount of time
    {
        this.myAudio.PlayOneShot(this.Click, 1f); //Play the door close sound
        LockTimerActive = hastimer;
		openTime = 0f;
		this.bDoorLocked = true;
		if (LockTimerActive)
		{
            this.lockTime = time;
        }
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x00021414 File Offset: 0x0001F814
	public void UnlockDoor() //Unlock the door
    {
        this.myAudio.PlayOneShot(this.Unclick, 1f); //Play the door close sound
        this.bDoorLocked = false;
	}

    // Token: 0x06000940 RID: 2368 RVA: 0x0002141D File Offset: 0x0001F81D
	public bool DoorLocked
	{
		get
		{
			return this.bDoorLocked;
		}
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x00021425 File Offset: 0x0001F825
	public void SilenceDoor() //Set the amount of times the door can be open silently
	{
		this.silentOpens = 4;
	}

	// Token: 0x040005C3 RID: 1475
	public float openingDistance;

	// Token: 0x040005C4 RID: 1476
	public Transform player;

	// Token: 0x040005C5 RID: 1477
	public BaldiScript baldi;

	public Animator DoorAnimator;

	public BoxCollider Trigger;

	public GameObject Barrier;

	// Token: 0x040005CB RID: 1483
	public AudioClip doorOpen;

	// Token: 0x040005CC RID: 1484
	public AudioClip doorClose;

	public AudioClip Click;

	public AudioClip Unclick;

	public AudioClip Jiggle;

	// Token: 0x040005CF RID: 1487
	private bool bDoorOpen;

	// Token: 0x040005D0 RID: 1488
	public bool bDoorLocked;

	// Token: 0x040005D1 RID: 1489
	public int silentOpens;

	// Token: 0x040005D2 RID: 1490
	private float openTime;

	// Token: 0x040005D3 RID: 1491
	public float lockTime;

	// Token: 0x040005D4 RID: 1492
	private AudioSource myAudio;

	bool LockTimerActive;

}
