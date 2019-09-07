using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDealDamageTrigger : MonoBehaviour
{
    public EnemyController enemy;
    public int damageAmount;

    private void Start()
    {
        enemy = GetComponentInParent<EnemyController>();
        damageAmount = enemy.str;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if (collision.CompareTag("Player"))//neu la nguoi choi
        {
            collision.SendMessage("TakeDamage", damageAmount, SendMessageOptions.DontRequireReceiver);//gay sat thuong cho nguoi choi
        }
    }
}
