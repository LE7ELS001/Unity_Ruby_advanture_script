using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;

//角色相关的 移动 生命之类

public class PlayerConstroller : MonoBehaviour
{
    //移动速度设置
    float MoveSpeed = 3.0f;

    //玩家最大生命值
    private int MaxHealth ;

    //玩家当前生命值 
    private int CurrentHealth ;

    //获取当前生命值和最大生命值
    public int GetMaxHealth { get { return MaxHealth; } }
    public int GetCurrentHealth { get { return CurrentHealth; } }


    //无敌时间
    private float InvincibeTime = 2.0f;

    //无敌时间计时器
    private float InvincibleTimer;

    //无敌状态切换
    private bool IsInvincible;

    //===================UI子弹数相关===============
    private int CurrentBulletAmount; //当前子弹数
    
    private int MaxBulletAmount = 99; //最多子弹数

    public int GetCurrentBulletAmount { get { return CurrentBulletAmount; } }

    public int GetMaxBulletAmount { get { return MaxBulletAmount; } }


    //===================玩家音效相关==============

    //受伤音效
    public AudioClip hitClip;

    //发射音效
    public AudioClip launchClip;

    //走路音效
   // public AudioClip walkClip;

    //测试  临时位置
    //private Vector2 tmpLocation;



    //===================玩家朝向相关==============

    private Vector2 LookDir = new Vector2(0, -1); //默认朝向 


    //获取组件
    Rigidbody2D rbody;
    Animator anim;

    //子弹（齿轮）
    public GameObject BulletPrefrab;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        MaxHealth = 5;
        CurrentHealth = 1;
        InvincibleTimer = 0;
        IsInvincible = false;
        UIManager.instance.UpdateHealBar(CurrentHealth, MaxHealth);
        CurrentBulletAmount = 0;
        ChangeBulletAmount(0);
        
    }

    // Update is called once per frame
    void Update()
    {
        //位移
        float MoveX = Input.GetAxisRaw("Horizontal"); //水平方向
        float MoveY = Input.GetAxisRaw("Vertical"); //垂直方向

        Vector2 Position = rbody.position; //获取角色位置
        //tmpLocation = Position;
        //Position.x += MoveX * MoveSpeed * Time.fixedDeltaTime; //水平位移变化
        // Position.y += MoveY * MoveSpeed * Time.fixedDeltaTime; // 垂直位移变化

        Vector2 moveVector = new Vector2(MoveX, MoveY);
        if(moveVector.x!= 0 || moveVector.y!= 0 )
        {
            LookDir = moveVector;
            
        }
        anim.SetFloat("Look X", LookDir.x);
        anim.SetFloat("Look Y", LookDir.y);
        anim.SetFloat("Speed", moveVector.magnitude);

        Position += moveVector * MoveSpeed * Time.fixedDeltaTime;

        //if(tmpLocation != Position)
        //{
        //    AudioManger.instance.AudioPlay(walkClip); 
        //}
        rbody.MovePosition(Position);

        //无敌状态设置
        if(IsInvincible)
        {
            InvincibleTimer -= Time.fixedDeltaTime;
            if(InvincibleTimer < 0 )
            {
                IsInvincible = false;
            }
            
        }

        //按下J键发射子弹
        if(Input.GetKeyDown(KeyCode.J) && CurrentBulletAmount > 0)
        {
            ChangeBulletAmount(-1); //每次攻击-1 子弹

            anim.SetTrigger("Launch");
            AudioManger.instance.AudioPlay(launchClip);
            GameObject bullet = Instantiate(BulletPrefrab, rbody.position + Vector2.up * 0.5f , Quaternion.identity);
            BulletController bc = bullet.GetComponent<BulletController>();
            if(bc!= null)
            {
                bc.BulletMove(LookDir, 500);
            }
        }

        //按E与NPC互动
        if(Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(rbody.position, LookDir, 2f, LayerMask.GetMask("NPC"));
            if(hit.collider!= null)
            {
                NPCManager npc = hit.collider.GetComponent<NPCManager>();
                if(npc!=null)
                {
                    npc.ShowDialog();
                }

            }
        }
    }

    //生命值改变
    public void ChangeHealth(int Amount)
    {
        if(Amount < 0)
        {
            if (IsInvincible)
            {
                return;
            }
            else
            {
                anim.SetTrigger("Hit");
                IsInvincible= true;
                InvincibleTimer = InvincibeTime;
            }
                AudioManger.instance.AudioPlay(hitClip);
        }

        CurrentHealth = Mathf.Clamp(CurrentHealth + Amount, 0, MaxHealth);
        UIManager.instance.UpdateHealBar(CurrentHealth, MaxHealth);
    }

    public void ChangeBulletAmount(int amount)
    {
        CurrentBulletAmount = Mathf.Clamp(CurrentBulletAmount +amount,0, MaxBulletAmount);
        UIManager.instance.UpdateBulletCount(CurrentBulletAmount, MaxBulletAmount);
    }
}
