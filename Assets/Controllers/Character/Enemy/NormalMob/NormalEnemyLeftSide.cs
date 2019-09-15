using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyLeftSide : MonoBehaviour
{
    public GameObject enemy;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && enemy.GetComponent<EnemyController>().diChuyen)//Neu nguoi choi vao tam nhin va cho phep di chuyen
        {
            if (enemy.GetComponent<EnemyController>().faceRight)//Neu dang quay ben phai
            {
                enemy.GetComponent<EnemyController>().Flip();
            }
            if (enemy.GetComponent<NormalEnemy>().type == 0)//Neu ke dich la can chien
            {
                enemy.GetComponent<EnemyController>().move = -1;//duoi theo nguoi choi
            }
            if (enemy.GetComponent<NormalEnemy>().type == 1 && enemy.GetComponent<EnemyController>().damagable)//neu la danh xa
                StartCoroutine(enemy.GetComponent<NormalEnemy>().RangedAttack());
        }
        else
            enemy.GetComponent<EnemyController>().move = 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            enemy.GetComponent<EnemyController>().move = 0;
    }
}
