using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    public static int Score;
    public Text scoreText; //Text用変数
    public Score_cal score_calRight;
    public Score_cal score_calLeft;
    //Score_cal Score;
    //Score_cal flag_setscore;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        SetScore();   //初期スコアを代入して表示
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Score.score);
        if(score_calRight.flag_setscore == true || score_calLeft.flag_setscore == true)
        {
            Score += 1;
            SetScore();
            score_calRight.flag_setscore = false;
            score_calLeft.flag_setscore = false;
        }

    }

    void SetScore()
    {
        scoreText.text = string.Format( "Score:{0}", Score );
        //scoreText.text = string.Format( "Score:{0}", score_calLeft.Score );
    }
}
