using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timercount : MonoBehaviour
{
    private float timer = 60.0f; 
    public Text TimeCounter;
    public ColorChange colorchange;
    bool flagstop;

    [SerializeField] Material white;
    [SerializeField] Material hide;

    public GameObject Plane;

    // private ColorChange startcall;


    // Start is called before the first frame update
    void Start()
    {
        // this.startcall = FindObjectOfType<ColorChange>();
        flagstop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(colorchange.StartCall)
        {
            timer -= Time.deltaTime;
            if(!flagstop)
            {
                TimeCounter.text = "残り時間 : "+timer.ToString("n2");
            }
            else
            {
                TimeCounter.text = "残り時間 ： 0.00"; //0で止めてマイナスにいかないように
            }
            if(timer <= 0.0f)
            {              
                flagstop = true;
                Plane.gameObject.GetComponent<Renderer>().material = white;

            }
        }

        
    }
}
