using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

/// <summary>
/// �������
/// </summary>

public class EnemyController : MonoBehaviour
{
    //�ƶ��ٶ�
    public float speed = 3f;

    //�Ƿ��Ǵ�ֱ�����ƶ�
    public bool isVertical;

    //�ƶ�����
    private Vector2 MoveDirection;

    //�ı䷽���ʱ��
    public float ChangeDirectionTime = 2f;

    //�ı䷽��ļ�ʱ��
    private float ChangeTimer;

    //�������Ƿ��ƶ����� ���Ƿ��޸��й�
    private bool IsFix;

    private Animator anim;
    private Rigidbody2D rbody;

    //��Ч���
    public ParticleSystem brokenEffect;

    //��Ч���==================
    public AudioClip FixClip;
    

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        IsFix = false;

        //�����ϻ�����
        MoveDirection = isVertical? Vector2.up : Vector2.right;

        ChangeTimer = ChangeDirectionTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFix)
        {
            return;
        }
        ChangeTimer -= Time.deltaTime;
        if (ChangeTimer < 0)
        {
            MoveDirection *= -1;
            ChangeTimer = ChangeDirectionTime;
        }
        Vector2 position = rbody.position;
        position.x += MoveDirection.x * speed * Time.deltaTime;
        position.y += MoveDirection.y * speed * Time.deltaTime;
        rbody.MovePosition(position);

        anim.SetFloat("MoveX", MoveDirection.x);
        anim.SetFloat("MoveY", MoveDirection.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerConstroller pc = collision.gameObject.GetComponent<PlayerConstroller>(); 
        if(pc != null)
        {
            pc.ChangeHealth(-1);
            UnityEngine.Debug.Log("��ǰѪ��Ϊ��  " + pc.GetCurrentHealth);
        }
    }


    /// <summary>
    /// ���޸�
    /// </summary>
    public void Fixed()
    {
        rbody.simulated = false;
        anim.SetTrigger("Fix");
        IsFix = true;
        AudioManger.instance.AudioPlay(FixClip);

        if(brokenEffect.isPlaying)
        {
            brokenEffect.Stop();
        }
    }
    
}
