using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Script duoc dung boi prefebs bullet
    public int damageAmount;
    public float speed = 15f;
    public PlayerController player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        damageAmount = player.tHGameController.gc.stat.Str;//Sat thuong dua tren chi so Str
        if (player.faceright)//Xac dinh huong ban
            gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * speed;
        else
            gameObject.GetComponent<Rigidbody2D>().velocity = -transform.right * speed;

    }


    #region Gay sat thuong cho ke dich hoac cham mat dat va bien mat
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dat"))//neu la Dat
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))//neu la ke dich
        {            
            collision.SendMessage("TakeDamage", damageAmount);//gay sat thuong cho ke dich
            Destroy(gameObject);
        }
    }

    #endregion
}
