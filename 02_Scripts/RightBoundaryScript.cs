using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightBoundaryScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)          // 물체를 안 죽이고 지나갔을 경우에도 자동으로 파괴되어야함
    {
        if (collision.tag == "PlayerShot")
        {
            ShotScript playerShot = collision.GetComponent<ShotScript>();
            playerShot.DestroyGameObject();
        }
        
    }
}
