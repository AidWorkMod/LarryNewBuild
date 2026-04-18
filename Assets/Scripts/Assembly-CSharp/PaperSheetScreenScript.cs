using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperSheetScreenScript : MonoBehaviour
{
    [SerializeField]
    GameControllerScript gc;
    [SerializeField]
    string GameManagerName;
    void Start()
    {
        gc = GameObject.Find(GameManagerName).GetComponent<GameControllerScript>();
    }
    public void DeletePaper()
    {
        gc.DeactivatePaper(gameObject);
    }
}
