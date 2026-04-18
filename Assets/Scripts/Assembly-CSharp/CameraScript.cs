using System;
using UnityEngine;

// Token: 0x020000B5 RID: 181
public class CameraScript : MonoBehaviour
{

	// Token: 0x0600092E RID: 2350 RVA: 0x00020D00 File Offset: 0x0001F100
	private void Update()
    {
        MouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
        float xMouse;
        float yMouse = Input.GetAxis("Mouse Y") * MouseSensitivity * 60f * Time.deltaTime;

        xMouse = Input.GetAxis("Mouse X") * MouseSensitivity * 60f * Time.deltaTime;

        xRot -= yMouse;
        xRot = Mathf.Clamp(xRot, -90, 90);

        if (Input.GetButton("Look Behind") && Time.timeScale != 0f)
        {
            lookBehind = Mathf.Lerp(lookBehind, 180, 20 * Time.deltaTime);
        }
        else
        {
            lookBehind = Mathf.Lerp(lookBehind, 0, 20 * Time.deltaTime);
        }

        if (Time.timeScale != 0f)
        {
            if (Input.GetButton("Run"))
			{
				CurrentCamera.fieldOfView = Mathf.Lerp(CurrentCamera.fieldOfView, 82, 5 * Time.deltaTime);
			}
			else
			{
                CurrentCamera.fieldOfView = Mathf.Lerp(CurrentCamera.fieldOfView, 60, 5 * Time.deltaTime);
            }
            base.transform.localRotation = Quaternion.Euler(xRot, (float)this.lookBehind, 0f);
            player.transform.Rotate(Vector3.up * xMouse);
			PointLook = transform.localRotation;
        }
        else
        {
            lookBehind = 0;
        }
    }

	// Token: 0x0600092F RID: 2351 RVA: 0x00020DD8 File Offset: 0x0001F1D8
	private void LateUpdate()
	{
		if (this.ps.gameOver)
		{
			base.transform.position = this.baldi.transform.position + this.baldi.transform.forward * 2f + new Vector3(0f, 5f, 0f); //Puts the camera in front of Baldi
			base.transform.LookAt(new Vector3(this.baldi.position.x, this.baldi.position.y + 5f, this.baldi.position.z)); //Makes the player look at baldi with an offset so the camera doesn't look at the feet
		}
	}

	// Token: 0x040005B0 RID: 1456
	public GameObject player;

	// Token: 0x040005B1 RID: 1457
	public PlayerScript ps;

	// Token: 0x040005B2 RID: 1458
	public Transform baldi;

	// Token: 0x040005B3 RID: 1459
	public float initVelocity;

	// Token: 0x040005B4 RID: 1460
	public float velocity;

	// Token: 0x040005B5 RID: 1461
	public float gravity;

	// Token: 0x040005B6 RID: 1462
	private float lookBehind;

	// Token: 0x040005B8 RID: 1464
	public float jumpHeight;

	// Token: 0x040005B9 RID: 1465
	public Vector3 jumpHeightV3;

	Quaternion PointLook;

	float MouseSensitivity;

	float xRot;

	public Camera CurrentCamera;
}
