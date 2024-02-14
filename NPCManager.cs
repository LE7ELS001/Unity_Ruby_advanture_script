using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour
{
    public GameObject dialogImage; //对话

    public GameObject TipImage; //按键提示

    public float showTime = 4; //框的显示时间

    public float showTimer; //计时器

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

    //显示对话框
    public void ShowDialog()
    {
        showTimer = showTime;
        dialogImage.SetActive(true) ;
        TipImage.SetActive(false) ;
        
    }
}
