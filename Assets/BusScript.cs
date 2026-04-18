using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BusScript : MonoBehaviour
{
    float chance;

    BoxCollider Trigger;

    [SerializeField]
    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        Trigger = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        chance = UnityEngine.Random.Range(0.012f, 0.057f);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray EyePoint = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(EyePoint, out hit) && hit.collider == Trigger && Vector3.Distance(transform.position, Player.position) <= 15)
            {
                chance = UnityEngine.Random.Range(0, Mathf.RoundToInt(chance * 2000));
                Debug.Log(chance);
                if (chance > 100)
                {
                   SceneManager.LoadSceneAsync("Secret");
                }
            }
        }
    }
}
