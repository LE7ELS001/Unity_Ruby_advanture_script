using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ײ���

public class Collect : MonoBehaviour
{
    // Start is called before the first frame update

    //�ռ���Ч
    public ParticleSystem CollectEffect;

    //ʰȡ��Ч
    public AudioClip collectClip;
    void Start()
    {
        
    }

    

    //�����ײ
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerConstroller pc = collision.GetComponent<PlayerConstroller>();
        if(pc != null)
        {
            if(pc.GetCurrentHealth < pc.GetMaxHealth)
            {
                pc.ChangeHealth(1);

                //��Ч����
                Instantiate(CollectEffect, transform.position, Quaternion.identity);

                //������Ч
                AudioManger.instance.AudioPlay(collectClip);

                //��Ʒ��ʧ
                Destroy(this.gameObject);
            }
                
        }
    }
}
