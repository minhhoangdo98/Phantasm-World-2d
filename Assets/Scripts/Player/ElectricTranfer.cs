using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricTranfer : MonoBehaviour
{
    private GameObject player;
    public int moveSpeed = 10, damageAmount;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        damageAmount = (int)PlayerPrefs.GetInt("intl" + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString()) * 5;
        if (player.GetComponent<PlayerController>().faceright)//Xac dinh huong ban
            gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * moveSpeed;
        else
            gameObject.GetComponent<Rigidbody2D>().velocity = -transform.right * moveSpeed;
        Destroy(gameObject, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 t;
        if(collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
        {
            t = collision.transform.position;
            collision.transform.position = player.transform.position;
            player.transform.position = t;
            collision.SendMessage("TakeDamage", damageAmount, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Dat"))
            Destroy(gameObject);
    }
}
