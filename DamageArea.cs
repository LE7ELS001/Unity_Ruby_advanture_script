using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �˺��������
/// </summary>
public class DamageArea : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerConstroller pc = collision.GetComponent<PlayerConstroller>(); 
        if(pc)
        {
            pc.ChangeHealth(-1);
            
        }
    }
}
