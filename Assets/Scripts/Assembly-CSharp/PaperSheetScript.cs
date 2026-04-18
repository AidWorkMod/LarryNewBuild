using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperSheetScript : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    GameObject Paper;
    [SerializeField]
    GameControllerScript gc;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector2((float)Screen.width / 2, (float)Screen.height / 2));
            if(Physics.Raycast(ray, out hit) && (hit.collider.name == gameObject.name) && Vector3.Distance(player.position, transform.position) < 15)
            {
                GameObject.Destroy(gameObject);
                GameObject.Instantiate(Paper);
                gc.ActivatePaper();
            }
        }
    }
}
