using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKi : MonoBehaviour
{
    public EnemyController enemy;
    public int damageAmount;
    public int moveSpeed = 15;
    void Start()
    {
        enemy = GetComponentInParent<EnemyController>();
        damageAmount = enemy.str / 2;
        if (enemy.faceRight)//Xac dinh huong ban
            gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * moveSpeed;
        else
            gameObject.GetComponent<Rigidbody2D>().velocity = -transform.right * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dat"))//neu la Dat
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Player"))//neu la nguoi choi
        {
            collision.SendMessage("TakeDamage", damageAmount);//gay sat thuong cho nguoi choi
            Destroy(gameObject);
        }
    }
}
