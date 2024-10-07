using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class score : MonoBehaviour
{
    public int Score;
    public TextMeshProUGUI scoreText; //Text用変数
    public Score_cal score_calRight;
    public Score_cal score_calLeft;
    public move_testhand score_testhand;
    public Score_cal resetR;
    public Score_cal resetL;

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
        if(score_calRight.flag_setscore == true || score_calLeft.flag_setscore == true || score_testhand.flag_setscore == true)
        {
            Score += 100;
            SetScore();
            if (score_calLeft.flag_setscore == true)
            {
                resetL.flag_reset = false;
            }
            if (score_calRight.flag_setscore == true)
            {
                resetR.flag_reset = false;
            }
            score_calRight.flag_setscore = false;
            score_calLeft.flag_setscore = false;
            score_testhand.flag_setscore = false;

        }

    }

    void SetScore()
    {
        scoreText.text = string.Format( "Score:{0}", Score );
        //scoreText.text = string.Format( "Score:{0}", score_calLeft.Score );
    }
}
