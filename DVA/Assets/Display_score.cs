using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Display_score : MonoBehaviour
{
    public Text display_score;
    // Start is called before the first frame update
    void Start()
    {
        display_score.text = "Your Points: 0 ";
    }

    // Update is called once per frame
    void Update()
    {
        //ScoreManagerからスコアを取得
        display_score.text = "Your Points: " + score.Score.ToString();
        
    }
}
