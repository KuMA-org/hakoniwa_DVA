using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_testhand : MonoBehaviour
{
    public bool flag_setscore;
    public int colorcheck = 0; // 1 = red, 2 = blue

    // Start is called before the first frame update
    void Start()
    {
        flag_setscore = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(-0.0025f,0.0f,0.0f);
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(0.0025f,0.0f,0.0f);
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(0.0f,0.0025f,0.0f);
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(0.0f,-0.0025f,0.0f);
        }
    }
    void OnTriggerEnter(Collider other)
    {
            //Debug.Log("sawa!!");

        if (other.gameObject.tag=="sphere" && other.gameObject.GetComponent<Renderer>().material.color == Color.red)
        {
            other.gameObject.GetComponent<Renderer>().material.color = Color.black;

            flag_setscore = true;
            colorcheck = 1;
            //Debug.Log("sawatta!!");
        }

        if (other.gameObject.tag=="sphere" && other.gameObject.GetComponent<Renderer>().material.color == Color.blue)
        {
            other.gameObject.GetComponent<Renderer>().material.color = Color.black;

            flag_setscore = true;
            colorcheck = 2;
            //Debug.Log("sawatta!!");
        }
    }
}
