using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWaterKi : MonoBehaviour
{
    public int damageAmount, moveSpeed = 15;
    private GameObject player;
    void Start()
    {
        damageAmount = (int)PlayerPrefs.GetInt("intl" + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString());//Sat thuong mac dinh
        gameObject.GetComponent<Rigidbody2D>().velocity = -transform.right * moveSpeed;
    }

    #region Gay sat thuong cho ke dich
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))//neu la ke dich
        {
            collision.SendMessage("TakeDamage", damageAmount, SendMessageOptions.DontRequireReceiver);//gay sat thuong cho ke dich
            Destroy(gameObject);
        }
        if (collision.CompareTag("Dat"))
            Destroy(gameObject);
    }

    #endregion
}
