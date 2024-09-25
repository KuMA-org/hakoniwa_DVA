using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_cal : MonoBehaviour
{
    [SerializeField] OVRHand MYRightHand;
    [SerializeField] OVRSkeleton MYRightSkelton;
    [SerializeField] GameObject IndexSphere;

    Vector3 indexTipPos;
    Quaternion indexTipRotate;

    [SerializeField] OVRHand MYLeftHand;
    [SerializeField] OVRSkeleton MYLeftSkelton;
    [SerializeField] GameObject IndexSphere2;

    Vector3 indexTipPos2;
    Quaternion indexTipRotate2;

    public Text scoreText; //Text用変数
    private int score = 0; //スコア計算用変数

    // Start is called before the first frame update
    void Start()
    {
        score   = 0;
        SetScore();   //初期スコアを代入して表示
    }

    // Update is called once per frame
    void Update()
    {
        indexTipPos = MYRightSkelton.Bones[(int)OVRSkeleton.BoneId.Hand_Middle1].Transform.position;
        indexTipRotate = MYRightSkelton.Bones[(int)OVRSkeleton.BoneId.Hand_Middle1].Transform.rotation;
        IndexSphere.transform.position = indexTipPos;
        IndexSphere.transform.rotation = indexTipRotate;

        indexTipPos2 = MYLeftSkelton.Bones[(int)OVRSkeleton.BoneId.Hand_Middle1].Transform.position;
        indexTipRotate2 = MYLeftSkelton.Bones[(int)OVRSkeleton.BoneId.Hand_Middle1].Transform.rotation;
        IndexSphere2.transform.position = indexTipPos2;
        IndexSphere2.transform.rotation = indexTipRotate2;
    }

    //sphereとhandsphereの接触でポイント加算
    void OnCollisionEnter( Collision collision )
    {
        string yourTag  = collision.gameObject.tag;
        Debug.Log("sawa!!");
        if( yourTag == "sphere" )
        {
            score += 1;
            Debug.Log("sawatta!!");
        }

        SetScore();
    }
    // void OnTriggerEnter(Collider other)
    // {
    //         Debug.Log("sawa!!");

    //     if (other.CompareTag("sphere"))
    //     {
    //         score += 1;
    //         Debug.Log("sawatta!!");
    //     }
    //     SetScore();
    // }

    void SetScore()
    {
        scoreText.text = string.Format( "Score:{0}", score );
    }
}
