using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_cal : MonoBehaviour
{
    [SerializeField] OVRHand MYHand;
    [SerializeField] OVRSkeleton MYHandSkelton;
    [SerializeField] GameObject IndexSphere;
    [SerializeField] Material _material;


    Vector3 indexTipPos;
    Quaternion indexTipRotate;
    public bool flag_setscore;

    public AudioClip se;
    AudioSource audiosource1;
    //public int Score;

    // public Text scoreText; //Text用変数
    //private int score = 0; //スコア計算用変数

    // Start is called before the first frame update
    void Start()
    {
        flag_setscore = false;
        // AudioSourceコンポーネント取得
        audiosource1 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MYHand.IsTracked)
        {
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

        if (other.gameObject.tag == "sphere")
        {
            //other.gameObject.GetComponent<Renderer>().material.color = Color.white;
            //flag_setscore = true;
            //Debug.Log("sawatta!!");
            if (other.gameObject.GetComponent<Renderer>().material.color == Color.red)
            {
                flag_setscore = true;
                other.gameObject.GetComponent<Renderer>().material = _material;
                audiosource1.PlayOneShot(se);
            }
        }
        //SetScore();
    }

    // void SetScore()
    // {
    //     scoreText.text = string.Format( "Score:{0}", score );
    // }
}

