using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDealDamageTrigger : MonoBehaviour
{
    public EnemyController enemy;
    public int damageAmount;
    [SerializeField]
    private GameObject datEffect;
    [SerializeField]
    private bool effectOnDat;
    [SerializeField]
    private float posX, posY;//vi tri x va y cua hieu ung

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

        if(collision.CompareTag("Dat") && effectOnDat)
        {
            Vector2 pos = PositionSetting(enemy.transform.gameObject, posX, posY);
            GameObject e = Instantiate(datEffect, pos, Quaternion.identity) as GameObject;
            Destroy(e, 1);
            Destroy(gameObject);
        }

    }

    private Vector2 PositionSetting(GameObject o, float xPos, float yPos)//Tao hieu ung tai vi tri vat bi tac dong cong them xPos va yPos
    {
        Vector3 posi = new Vector3(o.transform.position.x + xPos, o.transform.position.y + yPos);
        return posi;
    }
}
