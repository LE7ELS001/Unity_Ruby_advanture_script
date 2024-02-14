using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBag : MonoBehaviour
{
    public int bulletCount = 4; //һ��������ٿ��ӵ�

    public ParticleSystem collectEffect; //ʰȡ��Ч
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerConstroller pc = collision.GetComponent<PlayerConstroller>();
        if (pc != null || pc.GetCurrentBulletAmount < 89)
        {
            pc.ChangeBulletAmount(bulletCount);
            Instantiate(collectEffect,transform.position,Quaternion.identity); //��Ч
            Destroy(this.gameObject);
        }
    }
}
