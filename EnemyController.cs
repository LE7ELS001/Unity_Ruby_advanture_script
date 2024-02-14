using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

/// <summary>
/// 敌人相关
/// </summary>

public class EnemyController : MonoBehaviour
{
    //移动速度
    public float speed = 3f;

    //是否是垂直方向移动
    public bool isVertical;

    //移动方向
    private Vector2 MoveDirection;

    //改变方向的时间
    public float ChangeDirectionTime = 2f;

    //改变方向的计时器
    private float ChangeTimer;

    //机器人是否移动开关 和是否修复有关
    private bool IsFix;

    private Animator anim;
    private Rigidbody2D rbody;

    //特效相关
    public ParticleSystem brokenEffect;

    //音效相关==================
    public AudioClip FixClip;
    

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        IsFix = false;

        //控制上还是右
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
            UnityEngine.Debug.Log("当前血量为：  " + pc.GetCurrentHealth);
        }
    }


    /// <summary>
    /// 被修复
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
