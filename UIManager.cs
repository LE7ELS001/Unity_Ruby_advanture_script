using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }


    public Image Health; //血条

    public Text BulletCountText; //子弹数



    public void UpdateHealBar(int currenAmount, int MaxAmount)
    {
        Health.fillAmount = (float)currenAmount / (float)MaxAmount;
    }


    /// <summary>
    /// UI更新子弹数
    /// </summary>
    /// <param name="currentAmount"></param>
    /// <param name="maxAmount"></param>
    public void UpdateBulletCount(int currentAmount, int maxAmount)
    {
        BulletCountText.text = currentAmount.ToString() + "/" + maxAmount.ToString();
    }
}
