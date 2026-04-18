using System;
using UnityEngine;

// Token: 0x020000BB RID: 187
public class SwingingDoorScript : MonoBehaviour
{
	// Token: 0x06000945 RID: 2373 RVA: 0x0002147A File Offset: 0x0001F87A
	private void Start()
	{
		this.myAudio = base.GetComponent<AudioSource>();
		if(gc.notebooks != MaxNotebooks)
		{
            this.bDoorLocked = true;
        }
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x00021490 File Offset: 0x0001F890
	private void Update()
	{
		if (!this.requirementMet & this.gc.notebooks >= MaxNotebooks)
		{
			this.requirementMet = true;
			this.UnlockDoor();
		}
		if (this.openTime > 0f)
		{
			this.openTime -= 1f * Time.deltaTime;
		}
		if (this.lockTime > 0f)
		{
			this.lockTime -= Time.deltaTime;
		}
		else if (this.bDoorLocked & this.requirementMet)
		{
			this.UnlockDoor();
		}
		if (this.openTime <= 0f & this.bDoorOpen & !this.bDoorLocked)
        {
            SwingDoorAnimator.SetTrigger("Shut");
            this.bDoorOpen = false;
        }
	}
	// Token: 0x06000948 RID: 2376 RVA: 0x000215D8 File Offset: 0x0001F9D8
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
            if (!(this.gc.notebooks < MaxNotebooks))
            {
                if (!bDoorLocked)
                {
                    if (!bDoorOpen)
                    {
                        SwingDoorAnimator.SetTrigger("Open");
                        bDoorOpen = true;
                        openTime = 2;
                        this.myAudio.PlayOneShot(this.doorOpen, 1f);
                        if (other.tag == "Player" && this.baldi.isActiveAndEnabled)
                        {
                            this.baldi.Hear(base.transform.position, 1f);
                        }
                    }
                }
            }
			else
			{
                SwingDoorAnimator.SetTrigger("Rattle");
                this.myAudio.PlayOneShot(this.Rattling, 1f);
            }
        }
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x00021670 File Offset: 0x0001FA70
	public void LockDoor(float time)
    {
        Barrier.SetActive(true);
        this.obstacle.SetActive(true);
		this.bDoorLocked = true;
		this.lockTime = time;
		openTime = 0f;
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x000216C8 File Offset: 0x0001FAC8
	private void UnlockDoor()
	{
		this.obstacle.SetActive(false);
		this.bDoorLocked = false;
		Barrier.SetActive(false);
	}

	// Token: 0x040005D9 RID: 1497
	public GameControllerScript gc;

	// Token: 0x040005DA RID: 1498
	public BaldiScript baldi;

	// Token: 0x040005DC RID: 1500
	public GameObject obstacle;

	public Animator SwingDoorAnimator;

	public GameObject Barrier;

	// Token: 0x040005E3 RID: 1507
	public AudioClip doorOpen;

	// Token: 0x040005E4 RID: 1508
	public AudioClip Rattling;

	// Token: 0x040005E5 RID: 1509
	private float openTime;

	// Token: 0x040005E6 RID: 1510
	private float lockTime;

	// Token: 0x040005E7 RID: 1511
	public bool bDoorOpen;

	// Token: 0x040005E8 RID: 1512
	public bool bDoorLocked;

	// Token: 0x040005E9 RID: 1513
	private bool requirementMet;

	// Token: 0x040005EA RID: 1514
	private AudioSource myAudio;

	public float MaxNotebooks;
}
