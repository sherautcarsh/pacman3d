using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Names : MonoBehaviour
{
    public TextMeshPro txt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string quote = "Enter your name :";
        string name = Console.ReadLine();
        
        txt.text = quote + name;
    }
}
