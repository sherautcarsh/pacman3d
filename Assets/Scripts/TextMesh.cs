using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextMesh : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshPro txt;
    string point;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string poin = PlayerController.score + "";
        point = String.Format("{0:00000}", PlayerController.score);
        txt.text = point;
    }
}
