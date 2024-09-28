using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    GameObject[] button;
    public int number;
    int before_number;
    [SerializeField] Material _material;

    float timer;
    // Start is called before the first frame update
    void Start()
    {
        button = GameObject.FindGameObjectsWithTag("sphere");
        timer = 0;
        number = -1; // first time not change color
        Debug.Log("lenght" + button.Length);
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if (timer > 1.0f)
        {
            before_number = number;
            if (before_number >= 0)
            {
                button[before_number].GetComponent<Renderer>().material = _material;
            }

            number = Random.Range(0, button.Length);

            Debug.Log("before_number" + before_number);

            while (number == before_number)
            {
                number = Random.Range(0, button.Length);

            }
            Debug.Log("number" + number);
            Change();
            timer = 0;
        }



    }

    void Change()
    {

        button[number].GetComponent<Renderer>().material.color = Color.red;
    }
}