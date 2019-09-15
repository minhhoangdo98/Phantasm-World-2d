using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKi : MonoBehaviour
{
    public EnemyController enemy;
    public int damageAmount;
    public int moveSpeed = 15;
    [SerializeField]
    private GameObject effect;
    [SerializeField]
    private float posX, posY;//vi tri x va y cua hieu ung
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
        if (collision.CompareTag("Player"))//neu la nguoi choi
        {
            collision.SendMessage("TakeDamage", damageAmount);//gay sat thuong cho nguoi choi
            Vector2 pos = PositionSetting(collision.transform.gameObject, posX, posY);
            GameObject e = Instantiate(effect, pos, Quaternion.identity, collision.transform) as GameObject;
            Destroy(e, 1.5f);
            Destroy(gameObject);
        }
    }

    private Vector2 PositionSetting(GameObject o, float xPos, float yPos)//Tao hieu ung tai vi tri vat bi tac dong cong them xPos va yPos
    {
        Vector3 posi = new Vector3(o.transform.position.x + xPos, o.transform.position.y + yPos);
        return posi;
    }
}
