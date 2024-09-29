using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ColorChange : MonoBehaviour
{
    GameObject[] button;
    public int number1;
    public int number2;
    public int number3;
    private int number;
    private float time;
    private int beforenumber1 = -1; //連続で同じボタンが光らないように
    private int beforenumber2 = -1;
    private int beforenumber3 = -1;

    // Start is called before the first frame update
    void Start()
    {
        button = GameObject.FindGameObjectsWithTag("sphere");
        StartCoroutine("ChangetheColor1");
        // Debug.Log(button.Length);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time >= 60.0f)
        {
            StopCoroutine("ChangetheColor1");
            StopCoroutine("ChangetheColor2");
            StopCoroutine("ChangetheColor3");
            for(int i = 0; i < button.Length; i++)
            {
                button[i].GetComponent<Renderer>().material.color = Color.black;
            }
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
        yield return new WaitForSeconds(1);
        // button[number1].GetComponent<Renderer>().material.color = Color.black; //

        // if(time >= 5.0f)
        // {
        //     StartCoroutine("ChangetheColor2");
        //     StopCoroutine("ChangetheColor1");
        // }else
        // {
        //     StartCoroutine("ChangetheColor1");
        // }
        StartCoroutine("ChangetheColor1");

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
        yield return new WaitForSeconds(1);
        // button[number1].GetComponent<Renderer>().material.color = Color.black; 
        // button[number2].GetComponent<Renderer>().material.color = Color.black; 

        yield return new WaitForSeconds(0.5f);

        StartCoroutine("ChangetheColor2");
    }
    IEnumerator ChangetheColor3 () //3つボタンの色を変える
    {
        number1 = Randomnumber(); 
        button[number1].GetComponent<Renderer>().material.color = Color.red; //赤に変更
        yield return new WaitForSeconds(2);
        button[number1].GetComponent<Renderer>().material.color = Color.black; 
    }

    int Randomnumber()
    {
        return number = Random.Range(0,button.Length);
    }
}
