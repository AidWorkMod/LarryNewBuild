using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLockTrigger : MonoBehaviour
{
    [SerializeField]
    DoorScript door;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            door.LockDoor(0, false);
            GameObject.Destroy(gameObject);
        }
    }
}
