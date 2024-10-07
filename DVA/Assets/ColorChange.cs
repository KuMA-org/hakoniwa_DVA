using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ColorChange : MonoBehaviour
{
    [SerializeField] Material _material;

    GameObject[] button;
    public int number1;
    public int number2;
    public int number3;
    private int number;
    private float time;
    private int beforenumber1 = -1; //連続で同じボタンが光らないように
    private int beforenumber2 = -1;
    private int beforenumber3 = -1;

    public Text CountText;
    float countdown = 3f;
    int count;
    public bool StartCall;

    // Start is called before the first frame update
    void Start()
    {
        button = GameObject.FindGameObjectsWithTag("sphere");
        // Debug.Log(button.Length);
        StartCall = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(countdown >= 0)
        {
            countdown -= Time.deltaTime;
            count = (int)countdown;
            if(count == 0)
            {
                CountText.text = "Start!";
            }
            else
            {
                CountText.text = count.ToString();                
            }
        }
        if(countdown <= 0)
        {
            CountText.text = "";
            time += Time.deltaTime;
            if(!StartCall)
            {
                StartCall = true;
                StartCoroutine("ChangetheColor1");
            }
        }

        if(time >= 60.0f)
        {
            StopCoroutine("ChangetheColor1");
            StopCoroutine("ChangetheColor2");
            StopCoroutine("ChangetheColor3");
            for(int i = 0; i < button.Length; i++)
            {
                button[i].GetComponent<Renderer>().material = _material;
            }
            CountText.text = "Finish!";
        }
    }

    IEnumerator ChangetheColor1 () //はじめの段階，1つボタンの色を変える
    {
        //前光ったボタンとすでに光っているボタンは選ばない，ランダムで一つ選択
        while(number1 == beforenumber1 || button[number1].GetComponent<Renderer>().material.color == Color.red) 
        {
            number1 = Randomnumber(); 
        }
        beforenumber1 = number1;
        button[number1].GetComponent<Renderer>().material.color = Color.red; //赤に変更
        yield return new WaitForSeconds(2);
        button[number1].GetComponent<Renderer>().material = _material; //

        if(time >= 5.0f)
        {
            StartCoroutine("ChangetheColor2");
            StopCoroutine("ChangetheColor1");
        }else
        {
            StartCoroutine("ChangetheColor1");
        }
        // StartCoroutine("ChangetheColor1");

        yield return new WaitForSeconds(0.5f);
    }
    IEnumerator ChangetheColor2 () //2つボタンの色を変える
    {
        while(number1 == beforenumber1 || button[number1].GetComponent<Renderer>().material.color == Color.red)
        {
            number1 = Randomnumber(); 
        }
        beforenumber1 = number1;
        number2 = number1;
        while(number1 == number2 || beforenumber2 == number2 || button[number2].GetComponent<Renderer>().material.color == Color.red)
        {
            number2 = Randomnumber(); 
        }
        beforenumber2 = number2;

        button[number1].GetComponent<Renderer>().material.color = Color.red; //赤に変更
        button[number2].GetComponent<Renderer>().material.color = Color.red; 
        yield return new WaitForSeconds(1.5f);
        button[number1].GetComponent<Renderer>().material = _material; 
        button[number2].GetComponent<Renderer>().material = _material; 

        yield return new WaitForSeconds(0.5f);

        if(time >= 30.0f)
        {
            StartCoroutine("ChangetheColor3");
            StopCoroutine("ChangetheColor2");
        }else
        {
            StartCoroutine("ChangetheColor2");
        }

        // StartCoroutine("ChangetheColor2");
    }
    IEnumerator ChangetheColor3 () //3つボタンの色を変える
    {
        while(number1 == beforenumber1 || button[number1].GetComponent<Renderer>().material.color == Color.red)
        {
            number1 = Randomnumber(); 
        }
        beforenumber1 = number1;
        number2 = number1;
        number3 = number1;
        while(number1 == number2 || beforenumber2 == number2 || button[number2].GetComponent<Renderer>().material.color == Color.red)
        {
            number2 = Randomnumber(); 
        }
        beforenumber2 = number2;
        while(number1 == number3 || number2 == number3 || beforenumber3 == number3 || button[number3].GetComponent<Renderer>().material.color == Color.red)
        {
            number3 = Randomnumber(); 
        }
        beforenumber3 = number3;

        button[number1].GetComponent<Renderer>().material.color = Color.red; //赤に変更
        button[number2].GetComponent<Renderer>().material.color = Color.red; 
        button[number3].GetComponent<Renderer>().material.color = Color.red; 
        yield return new WaitForSeconds(1.5f);
        button[number1].GetComponent<Renderer>().material = _material; 
        button[number2].GetComponent<Renderer>().material = _material; 
        button[number3].GetComponent<Renderer>().material = _material; 

        yield return new WaitForSeconds(0.5f);

        StartCoroutine("ChangetheColor3");
    }

    int Randomnumber()
    {
        return number = Random.Range(0,button.Length);
    }
        // timer += Time.deltaTime;

        // if (timer > 1.0f)
        // {
        //     before_number = number;
        //     if (before_number >= 0)
        //     {
        //         button[before_number].GetComponent<Renderer>().material = _material;
        //     }

        //     number = Random.Range(0, button.Length);

        //     Debug.Log("before_number" + before_number);

        //     while (number == before_number)
        //     {
        //         number = Random.Range(0, button.Length);

        //     }
        //     Debug.Log("number" + number);
        //     Change();
        //     timer = 0;
        // }

    // void Change()
    // {

    //     button[number].GetComponent<Renderer>().material.color = Color.red;
    // }
    public bool Called
    {
        get {return StartCall;}
    }
}