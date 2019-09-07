using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public int str, hp;
    public float speed = 150f, maxSpeed = 3, jumpPow = 350f, move = 0;
    public bool grounded = true, faceRight = true, attacktrigger1 = false, takeDam = false, death = false, attacktrigger2 = false, attacktrigger3 = false, attacktrigger4 = false, flipable = true, jumpable = true, backMove = false, invisible = false;
    public Rigidbody2D r2;
    public Animator anim;
    private AudioSource audioSource;
    public bool diChuyen;

    void Start()
    {
        r2 = gameObject.GetComponent<Rigidbody2D>();//Lay nhan vat
        anim = gameObject.GetComponent<Animator>();//Bien chua animation cho Player
        audioSource = gameObject.GetComponent<AudioSource>();//Chua am thanh de chay
        diChuyen = true;//co the di chuyen
    }

    void Update()
    {
        anim.SetBool("Grounded", grounded);//animation khi dung yen tren mat dat (grounded = true)
        anim.SetFloat("Speed", Mathf.Abs(r2.velocity.x)); // Mathf.abs: tra ve gia tri duong ; r2.velocity.x: toc do hien tai, animation khi chay
        anim.SetBool("AttackTrigger1", attacktrigger1);
        anim.SetBool("AttackTrigger2", attacktrigger2);
        anim.SetBool("AttackTrigger3", attacktrigger3);
        anim.SetBool("AttackTrigger4", attacktrigger4);
        anim.SetBool("TakeDamage", takeDam);//animation khi nhan sat thuong
        anim.SetBool("Death", death);//animation khi death

    }

    private void FixedUpdate()
    {
        if (diChuyen)
        {
            r2.AddForce(Vector2.right * speed * move);//di chuyen
            //Ham gioi han toc do di chuyen
            if (r2.velocity.x > maxSpeed) //Gioi han toc do di ve ben phais
                r2.velocity = new Vector2(maxSpeed, r2.velocity.y);
            if (r2.velocity.x < -maxSpeed)// Gioi han toc do di ve ben trai
                r2.velocity = new Vector2(-maxSpeed, r2.velocity.y);

            if (flipable)
            {
                if (move > 0 && !faceRight)
                {
                    Flip();//Goi ham dao chieu 
                }
                if (move < 0 && faceRight)
                {
                    Flip();
                }
            }

            if (grounded)
            {
                r2.velocity = new Vector2(r2.velocity.x * 0.7f, r2.velocity.y);
            }

            if (!grounded)
            {
                r2.velocity = new Vector2(r2.velocity.x * 0.7f, r2.velocity.y);
            }
        }
        else
        {
            move = 0;
            r2.AddForce(Vector2.right * speed * move);
        }
    }

    public void Flip() // Chuyen huong nhan vat
    {
        faceRight = !faceRight;
        gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
    }

    public IEnumerator JumpDelay()
    {
        jumpable = false;
        yield return new WaitForSeconds(1.5f);
        jumpable = true;
    }

    #region NhanSatThuong
    public void TakeDamage(int damageAmount)//Nhan sat thuong
    {
        if(!invisible)
        {
            takeDam = true;
            diChuyen = false;
            hp -= damageAmount;
            StartCoroutine(TakeDam());
        }
    }

    IEnumerator TakeDam()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;//thay doi mau de mo phong nhan sat thuong
        yield return new WaitForSeconds(0.4f);//doi 0.4s
        if (hp <= 0)
        {
            takeDam = false;
            diChuyen = false;
            death = true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            takeDam = false;
            diChuyen = true;//di chuyen lai binh thuong
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;//tro lai mau nhu cu
        }
    }
    #endregion
}
