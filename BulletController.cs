using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制子弹相关的碰撞 移动
/// </summary>
public class BulletController : MonoBehaviour
{
    private Rigidbody2D rbody;

    //命中音效
    public AudioClip hitClip;
    // Start is called before the first frame update
    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //子弹发射
    public void BulletMove(Vector2 moveDir, float moveForece)
    {
        rbody.AddForce(moveDir * moveForece);
    }

    //碰撞（子弹消失）
     void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController ec = collision.gameObject.GetComponent<EnemyController>();
        if (ec != null)
        {
            ec.Fixed();
            Debug.Log("a");
        }

        AudioManger.instance.AudioPlay(hitClip); //碰撞到的音效

        Destroy(this.gameObject);
    }
}
