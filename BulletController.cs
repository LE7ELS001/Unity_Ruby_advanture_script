using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����ӵ���ص���ײ �ƶ�
/// </summary>
public class BulletController : MonoBehaviour
{
    private Rigidbody2D rbody;

    //������Ч
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

    //�ӵ�����
    public void BulletMove(Vector2 moveDir, float moveForece)
    {
        rbody.AddForce(moveDir * moveForece);
    }

    //��ײ���ӵ���ʧ��
     void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController ec = collision.gameObject.GetComponent<EnemyController>();
        if (ec != null)
        {
            ec.Fixed();
            Debug.Log("a");
        }

        AudioManger.instance.AudioPlay(hitClip); //��ײ������Ч

        Destroy(this.gameObject);
    }
}
