using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBag : MonoBehaviour
{
    public int bulletCount = 4; //一个包里多少颗子弹

    public ParticleSystem collectEffect; //拾取特效
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerConstroller pc = collision.GetComponent<PlayerConstroller>();
        if (pc != null || pc.GetCurrentBulletAmount < 89)
        {
            pc.ChangeBulletAmount(bulletCount);
            Instantiate(collectEffect,transform.position,Quaternion.identity); //特效
            Destroy(this.gameObject);
        }
    }
}
