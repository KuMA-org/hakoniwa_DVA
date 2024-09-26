using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    public Text scoreText; //Text用変数
    Score_cal Score;
    Score_cal flag_setscore;

    // Start is called before the first frame update
    void Start()
    {
        
        SetScore();   //初期スコアを代入して表示
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Score.score);
        if(flag_setscore == true)
        {
            SetScore();
            flag_setscore = false;
        }

    }

    void SetScore()
    {
        scoreText.text = string.Format( "Score:{0}", Score );
    }
}
