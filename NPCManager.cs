using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour
{
    public GameObject dialogImage; //�Ի�

    public GameObject TipImage; //������ʾ

    public float showTime = 4; //�����ʾʱ��

    public float showTimer; //��ʱ��

    void Start()
    {
      dialogImage.SetActive(false);
      TipImage.SetActive(true);
        showTimer = -1;
    }

    // Update is called once per frame
    void Update()
    {
            showTimer -= Time.deltaTime;
            if(showTimer < 0)
            {
                dialogImage.SetActive(false);
                TipImage.SetActive(true);
            }
    }

    //��ʾ�Ի���
    public void ShowDialog()
    {
        showTimer = showTime;
        dialogImage.SetActive(true) ;
        TipImage.SetActive(false) ;
        
    }
}
