using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShotScript : MonoBehaviour
{

    public float speed = 5;

    void Start()
    {

    }


    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    public void DestroyGameObject()
    {
        ObjectPoolManager.instance.bossShot.Destroy(gameObject);
    }


}
