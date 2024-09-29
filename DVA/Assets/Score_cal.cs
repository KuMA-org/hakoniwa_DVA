using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_cal : MonoBehaviour
{
    [SerializeField] OVRHand MYHand;
    [SerializeField] OVRSkeleton MYHandSkelton;
    [SerializeField] GameObject IndexSphere;
    

    Vector3 indexTipPos;
    Quaternion indexTipRotate;
    public bool flag_setscore;

    //public int Score;
    
    // public Text scoreText; //Text用変数
    //private int score = 0; //スコア計算用変数

    // Start is called before the first frame update
    void Start()
    {
        flag_setscore = false;
        //Score   = 0;
       // SetScore();   //初期スコアを代入して表示
    }

    // Update is called once per frame
    void Update()
    {
        if (MYHand.IsTracked){
            indexTipPos = MYHandSkelton.Bones[(int)OVRSkeleton.BoneId.Hand_Middle1].Transform.position;
            indexTipRotate = MYHandSkelton.Bones[(int)OVRSkeleton.BoneId.Hand_Middle1].Transform.rotation;
            IndexSphere.transform.position = indexTipPos;
            IndexSphere.transform.rotation = indexTipRotate;
        }
        
    }

    //sphereとhandsphereの接触でポイント加算
    void OnTriggerEnter(Collider other)
    {
            //Debug.Log("sawa!!");

        if (other.gameObject.tag=="sphere" && other.gameObject.GetComponent<Renderer>().material.color == Color.red)
        {
            //Score += 1;
            other.gameObject.GetComponent<Renderer>().material.color = Color.black;
            
            flag_setscore = true;
            //Debug.Log("sawatta!!");
        }
       //SetScore();
    }

    // void SetScore()
    // {
    //     scoreText.text = string.Format( "Score:{0}", score );
    // }
}
