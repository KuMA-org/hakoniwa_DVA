using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_cal : MonoBehaviour
{
    [SerializeField] GameObject Hand;
    [SerializeField] OVRHand MYHand;
    [SerializeField] OVRSkeleton MYHandSkelton;
    [SerializeField] GameObject IndexSphere;
    [SerializeField] Material _material;
    [SerializeField] Material hand_material_ture;
    [SerializeField] Material hand_material_false;


    public int colorcheck = 0; // sphere's color  1 = red, 2 = blue


    Vector3 indexTipPos;
    Quaternion indexTipRotate;
    public bool flag_setscore;

    public AudioClip se;
    AudioSource audiosource1;
    //public int Score;
    public bool flag_reset;

    // public Text scoreText; //Text用変数
    //private int score = 0; //スコア計算用変数

    // Start is called before the first frame update
    void Start()
    {
        flag_setscore = false;
        flag_reset = true;
        // AudioSourceコンポーネント取得
        audiosource1 = GetComponent<AudioSource>();
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
        if(flag_reset == true)
        {
            Hand.GetComponent<Renderer>().material = hand_material_ture;
        }
        else
        {
            Hand.GetComponent<Renderer>().material = hand_material_false;
        }
        
    }

    //sphereとhandsphereの接触でポイント加算
    void OnTriggerEnter(Collider other)
    {
        // Hit judgement with Spheres
        // if sphere color is red
        if (other.gameObject.tag=="sphere" && other.gameObject.GetComponent<Renderer>().material.color == Color.red  && flag_reset == true)
        {
            //Score += 1;
            other.gameObject.GetComponent<Renderer>().material.color = Color.black;
            
            flag_setscore = true;
            colorcheck = 1;
            audiosource1.PlayOneShot(se);
        }

        // if sphere color is blue
        if (other.gameObject.tag == "sphere" && other.gameObject.GetComponent<Renderer>().material.color == Color.blue && flag_reset == true)
        {
            other.gameObject.GetComponent<Renderer>().material = _material;

            flag_setscore = true;
            colorcheck = 2;
            audiosource1.PlayOneShot(se);
           
        }
        //SetScore();
    }

    // void SetScore()
    // {
    //     scoreText.text = string.Format( "Score:{0}", score );
    // }
}
