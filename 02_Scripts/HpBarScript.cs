using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarScript : MonoBehaviour
{
    Vector3 target = new Vector3(0, 1, 1);
    public float value = 100;
    public float maxValue;


    void Start()
    {
        maxValue = value;
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)){
            value -= 10;
        }     
        if(value <= 0)
        {
            value = 0;
        }
        target = new Vector3(value / maxValue, 1, 1);
        transform.localScale = Vector3.Lerp(transform.localScale, target, Time.deltaTime * 3);
    }
}
