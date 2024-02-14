using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;

//��ɫ��ص� �ƶ� ����֮��

public class PlayerConstroller : MonoBehaviour
{
    //�ƶ��ٶ�����
    float MoveSpeed = 3.0f;

    //����������ֵ
    private int MaxHealth ;

    //��ҵ�ǰ����ֵ 
    private int CurrentHealth ;

    //��ȡ��ǰ����ֵ���������ֵ
    public int GetMaxHealth { get { return MaxHealth; } }
    public int GetCurrentHealth { get { return CurrentHealth; } }


    //�޵�ʱ��
    private float InvincibeTime = 2.0f;

    //�޵�ʱ���ʱ��
    private float InvincibleTimer;

    //�޵�״̬�л�
    private bool IsInvincible;

    //===================UI�ӵ������===============
    private int CurrentBulletAmount; //��ǰ�ӵ���
    
    private int MaxBulletAmount = 99; //����ӵ���

    public int GetCurrentBulletAmount { get { return CurrentBulletAmount; } }

    public int GetMaxBulletAmount { get { return MaxBulletAmount; } }


    //===================�����Ч���==============

    //������Ч
    public AudioClip hitClip;

    //������Ч
    public AudioClip launchClip;

    //��·��Ч
   // public AudioClip walkClip;

    //����  ��ʱλ��
    //private Vector2 tmpLocation;



    //===================��ҳ������==============

    private Vector2 LookDir = new Vector2(0, -1); //Ĭ�ϳ��� 


    //��ȡ���
    Rigidbody2D rbody;
    Animator anim;

    //�ӵ������֣�
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
        //λ��
        float MoveX = Input.GetAxisRaw("Horizontal"); //ˮƽ����
        float MoveY = Input.GetAxisRaw("Vertical"); //��ֱ����

        Vector2 Position = rbody.position; //��ȡ��ɫλ��
        //tmpLocation = Position;
        //Position.x += MoveX * MoveSpeed * Time.fixedDeltaTime; //ˮƽλ�Ʊ仯
        // Position.y += MoveY * MoveSpeed * Time.fixedDeltaTime; // ��ֱλ�Ʊ仯

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

        //�޵�״̬����
        if(IsInvincible)
        {
            InvincibleTimer -= Time.fixedDeltaTime;
            if(InvincibleTimer < 0 )
            {
                IsInvincible = false;
            }
            
        }

        //����J�������ӵ�
        if(Input.GetKeyDown(KeyCode.J) && CurrentBulletAmount > 0)
        {
            ChangeBulletAmount(-1); //ÿ�ι���-1 �ӵ�

            anim.SetTrigger("Launch");
            AudioManger.instance.AudioPlay(launchClip);
            GameObject bullet = Instantiate(BulletPrefrab, rbody.position + Vector2.up * 0.5f , Quaternion.identity);
            BulletController bc = bullet.GetComponent<BulletController>();
            if(bc!= null)
            {
                bc.BulletMove(LookDir, 500);
            }
        }

        //��E��NPC����
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

    //����ֵ�ı�
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
