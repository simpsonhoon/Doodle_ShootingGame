using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBGScript : MonoBehaviour
{
    public float speed = 1.0f;
    SpriteRenderer spr;
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * speed;
        Vector3 pos = transform.position;
        if(pos.x + spr.bounds.size.x / 2 < -8)
        {
            pos.x += spr.bounds.size.x * 3;
            transform.position = pos;
        }
    }
}
