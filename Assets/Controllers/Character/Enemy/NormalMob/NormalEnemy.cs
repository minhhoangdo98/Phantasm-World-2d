using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
    //Script duoc su dung cho cac enemy thuong
    public int type;//Enemy type = 0 (Can chien), = 1 (Danh xa), = 2 (Do Don)
    public float stopMoveTime, eKbackPower=200f;
    [SerializeField]
    private EnemyController ec;
    public GameObject attackEffect, gun, bullet;

    private void Start()
    {
        ec = gameObject.GetComponent<EnemyController>();
    }

    #region Gay sat thuong cho nguoi choi
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (type == 0)//Neu ke dich la can chien
        {
            if (collision.collider.CompareTag("Player") && !ec.thgc.isWin && !ec.thgc.isGameOver && ec.damagable && ec.hoatDong)//Neu doi tuong cham vao la nguoi choi va chua win game, chua gameover va cho phep gay sat thuong
            {
                ec.damagable = false;
                Vector3 pos = new Vector3(collision.transform.position.x, collision.transform.position.y);
                StartCoroutine(AttackClose());
                GameObject ae = Instantiate(attackEffect, pos, Quaternion.identity, collision.transform) as GameObject;//tao hieu ung danh trung nguoi choi
                Destroy(ae, 0.5f);
                StartCoroutine(StopMove(false));//Khi tan cong ke dich phai dung lai trong khoang thoi gian ngan
                KBack(150f);//bi knockback
                collision.collider.SendMessage("TakeDamage", ec.str);//nguoi choi nhan sat thuong
            }
        }
    }

    IEnumerator AttackClose()
    {
        ec.attacktrigger1 = true;//trigger cho animation attack1
        yield return new WaitForSeconds(0.5f);
        ec.attacktrigger1 = false;
    }

    public IEnumerator RangedAttack()
    {
        ec.damagable = false;
        ec.attacktrigger2 = true;
        if (ec.faceRight)//danh ben phai
        {
            if (!gun.GetComponent<SpriteRenderer>().flipX)//Neu vu khi dang quay ve ben trai thi lat nguoc lai
            {
                gun.transform.position = new Vector2(transform.position.x + 0.7f, transform.position.y);
                gun.GetComponent<SpriteRenderer>().flipX = true;
            }
            yield return new WaitForSeconds(0.2f);
            gun.SetActive(true);
            Vector3 pos = new Vector3(transform.position.x + 1.5f, transform.position.y + 1.2f);//hieu ung chem vi tri la canh vu khi
            GameObject bul = Instantiate(bullet, pos, Quaternion.identity, gameObject.transform) as GameObject;//tao hieu ung
            yield return new WaitForSeconds(0.3f);
            Destroy(bul, 1.8f);
        }
        else//danh ben trai
        {
            if (gun.GetComponent<SpriteRenderer>().flipX)//Neu vu khi dang quay ve ben phai
            {
                gun.transform.position = new Vector2(transform.position.x - 0.7f, transform.position.y);
                gun.GetComponent<SpriteRenderer>().flipX = false;
            }
            yield return new WaitForSeconds(0.2f);
            gun.SetActive(true);
            Vector3 pos = new Vector3(transform.position.x - 1.5f, transform.position.y + 1.2f);//hieu ung dan vi tri la canh vu khi
            GameObject bul = Instantiate(bullet, pos, Quaternion.identity, gameObject.transform) as GameObject;//tao dan ban ra
            yield return new WaitForSeconds(0.3f);
            Destroy(bul, 1.8f);
        }
        gun.SetActive(false);
        ec.attacktrigger2 = false;
        yield return new WaitForSeconds(stopMoveTime);
        ec.damagable = true;
    }
    #endregion

    #region Nhan sat thuong 
    void TakeDamage(int damageAmount)
    {
        ec.hp -= damageAmount;
        if (gameObject.CompareTag("Enemy"))
            EnemyKBack(eKbackPower);//Day lui ke dich, ngoai tru boss
        if (ec.hp <= 0 && !ec.death)
        {
            ec.diChuyen = false;
            ec.damagable = false;
            ec.death = true;
            ec.thgc.gc.stat.AddStat(4, ec.cash);
            Destroy(gameObject, 1);
            return;
        }
        StartCoroutine(StopMove(true));

    }

    public IEnumerator StopMove(bool takeDam)//ngung di chuyen
    {
        ec.diChuyen = false;
        if (takeDam)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.3f);
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        yield return new WaitForSeconds(stopMoveTime);
        ec.diChuyen = true;
        ec.damagable = true;
    }

    #endregion

    #region Day lui
    public void KBack(float power)//Nguoi choi bi day lui
    {
        if (!ec.thgc.player.GetComponent<PlayerController>().faceright)//kiem tra xem nguoi choi co doi huong hay khong, nguoi choi dang quay ve phia nao
        {
            ec.thgc.player.GetComponent<Rigidbody2D>().AddForce(new Vector2(power, ec.thgc.player.GetComponent<Rigidbody2D>().velocity.y + 100f));//bi luc tac dung day lui
            ec.thgc.player.GetComponent<PlayerController>().diChuyen = false;//khong the di chuyen trong luc bi day lui
        }
        else
        {
            ec.thgc.player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-power, ec.thgc.player.GetComponent<Rigidbody2D>().velocity.y + 100f));
            ec.thgc.player.GetComponent<PlayerController>().diChuyen = false;
        }
    }

    public void EnemyKBack(float power)//ke dich bi day lui
    {
        if (ec.faceRight)//kiem tra xem ke dich co doi huong hay khong, ke dich dang quay ve phia nao
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-power, gameObject.GetComponent<Rigidbody2D>().velocity.y + 100f));//bi luc tac dung day lui 150f
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(power, gameObject.GetComponent<Rigidbody2D>().velocity.y + 100f));
        }
    }
    #endregion
}
