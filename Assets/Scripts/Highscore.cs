using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{
    // Start is called before the first frame update
    
    public TextMeshPro txt,txt1;
    string point;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        int point = PlayerController.score;
        
        if (point > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore",point);
        }
        txt.text = "Highscore: " + PlayerPrefs.GetInt("HighScore");
    }
}
