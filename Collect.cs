using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//碰撞相关

public class Collect : MonoBehaviour
{
    // Start is called before the first frame update

    //收集特效
    public ParticleSystem CollectEffect;

    //拾取音效
    public AudioClip collectClip;
    void Start()
    {
        
    }

    

    //检测碰撞
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerConstroller pc = collision.GetComponent<PlayerConstroller>();
        if(pc != null)
        {
            if(pc.GetCurrentHealth < pc.GetMaxHealth)
            {
                pc.ChangeHealth(1);

                //特效生成
                Instantiate(CollectEffect, transform.position, Quaternion.identity);

                //播放音效
                AudioManger.instance.AudioPlay(collectClip);

                //物品消失
                Destroy(this.gameObject);
            }
                
        }
    }
}
