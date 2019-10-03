using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //Script duoc dung boi prefebs bullet
    public int damageAmount;
    public float speed = 15f;
    public EnemyController ene;
    void Start()
    {
        ene = GetComponentInParent<EnemyController>();
        damageAmount = ene.str;//Sat thuong dua tren chi so Str
        if (ene.faceRight)//Xac dinh huong ban
            gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * speed;
        else
            gameObject.GetComponent<Rigidbody2D>().velocity = -transform.right * speed;

    }


    #region Gay sat thuong cho nguoi choi hoac cham mat dat va bien mat
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dat"))//neu la Dat
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Player"))//neu la nguoi choi
        {
            collision.SendMessage("TakeDamage", damageAmount);//gay sat thuong cho ke dich
            Destroy(gameObject);
        }
    }

    #endregion
}
