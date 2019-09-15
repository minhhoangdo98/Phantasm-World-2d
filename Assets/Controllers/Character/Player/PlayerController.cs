using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //script duoc dung boi  main
    public float speed = 150f, maxspeed = 1, jumpPow = 350f, defaultSpeed;
    public bool grounded = true, faceright = false, doubleJump = false, attacktrigger1 = false, takeDam = false, death = false, attacktrigger2 = false, attacktrigger3 = false, attacktrigger4 = false;
    public Rigidbody2D r2;
    public Animator anim;
    public AudioClip Nhay;
    private AudioSource audioSource;
    public bool diChuyen, traiPhai, lenXuong, dieuKhien = true;
    public THGameController tHGameController;
    public int hp;
    public Slider hpSlider;


    void Start()
    {
        r2 = gameObject.GetComponent<Rigidbody2D>();//Lay nhan vat
        anim = gameObject.GetComponent<Animator>();//Bien chua animation cho Player
        audioSource = gameObject.GetComponent<AudioSource>();//Chua am thanh de chay
        audioSource.clip = Nhay;//am thanh nhay
        diChuyen = true;//co the di chuyen
        traiPhai = true;
        lenXuong = false;
        defaultSpeed = speed;//speed ban dau
        hp = PlayerPrefs.GetInt("currentHp" + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString());//chi so hp hien tai
        //thanh mau
        hpSlider.minValue = 0;
        hpSlider.maxValue = PlayerPrefs.GetInt("maxHp" + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString()); ;
        hpSlider.value = hp;
    }


    void Update()
    {
        anim.SetBool("Grounded", grounded);//animation khi dung yen tren mat dat (grounded = true)
        anim.SetFloat("Speed", Mathf.Abs(r2.velocity.x)); // Mathf.abs: tra ve gia tri duong ; r2.velocity.x: toc do hien tai, animation khi chay
        anim.SetBool("AttackTrigger1", attacktrigger1);//animation attack 1
        anim.SetBool("AttackTrigger2", attacktrigger2);//animation attack 2
        anim.SetBool("AttackTrigger3", attacktrigger3);//animation attack 3
        anim.SetBool("AttackTrigger4", attacktrigger4);//animation attack 4
        anim.SetBool("TakeDamage", takeDam);//animation khi nhan sat thuong
        anim.SetBool("Death", death);//animation khi death
        hpSlider.value = hp;//luon luon cap nhat thanh mau

        if(!tHGameController.thBattle || tHGameController.isGameOver || tHGameController.isWin)//Kiem tra de cho phep dieu khien nhan vat
        {
            dieuKhien = false;
        }
        if(tHGameController.thBattle)
        {
            dieuKhien = true;
        }

        if (Input.GetButtonDown("Jump") && diChuyen && dieuKhien) // neu nut an xuong cua nguoi choi la Space va dang cho phep di chuyen (diChuyen = true)
        {
            gameObject.GetComponent<GroundCheck>();//Goi ham kiem tra xem Player co dang dung tren mat dat hay khong
            if (grounded)//neu dang dung tren mat dat
            {
                grounded = false;//cho grounded = false tuc la nguoi choi se nhay len khong
                doubleJump = true;//co the nhay tiep lan 2
                audioSource.clip = Nhay;
                audioSource.Play();//Play am thanh khi nhay
                r2.AddForce(Vector2.up * jumpPow);//thay doi vi tri nhan vat len tren dua vao jumpPow
            }
            else//Nguoc lai, Neu khong dung tren mat dat
            {
                if (doubleJump)//neu chua nhay lan 2
                {
                    doubleJump = false;//nhay tiep lan 2 va khong the nhay them nua
                    r2.velocity = new Vector2(r2.velocity.x, 0);
                    audioSource.clip = Nhay;
                    audioSource.Play();
                    r2.AddForce(Vector2.up * jumpPow * 0.8f);
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (diChuyen && traiPhai && dieuKhien)//neu cho phep di chuyen (diChuyen = true)
        {
            float h = Input.GetAxis("Horizontal");//Lay thong tin nut bam la nut mui ten (Phai: 1, Trai: -1)
            r2.AddForce(Vector2.right * speed * h);//Thay doi vi tri nhan vat dua vao speed va h

            //Ham gioi han toc do di chuyen
            if (r2.velocity.x > maxspeed) //Gioi han toc do di ve ben phais
                r2.velocity = new Vector2(maxspeed, r2.velocity.y);
            if (r2.velocity.x < -maxspeed)// Gioi han toc do di ve ben trai
                r2.velocity = new Vector2(-maxspeed, r2.velocity.y);


            if (h > 0 && !faceright)//Neu h > 0 tuc la ben phai va player dang quay ve ben trai va chua trong trang thai tan cong
            {
                Flip();//Goi ham dao chieu 
            }
            if (h < 0 && faceright)//Neu h < 0 tuc la ben trai va player dang quay ve ben phai va chua trong trang thai tan cong
            {
                Flip();
            }

            if (grounded)
            {
                r2.velocity = new Vector2(r2.velocity.x * 0.7f, r2.velocity.y);
            }

            if (!grounded)
            {
                r2.velocity = new Vector2(r2.velocity.x * 0.7f, r2.velocity.y);
            }


            if (Input.GetKey(KeyCode.LeftShift) && h != 0)//khi nhan giu Shift se chay nhanh
            {
                speed = defaultSpeed + 200f + (float)tHGameController.gc.stat.Dex * 5;
            }
            else
                speed = defaultSpeed;
            if (!Input.GetKey(KeyCode.LeftShift))//khi khong nhan shift se tro ve toc do ban dau
            {
                speed = defaultSpeed;
            }
        }
    }

    public void Flip() // Chuyen huong nhan vat
    {
        faceright = !faceright;
        gameObject.GetComponent<Transform>().localScale = new Vector3(-gameObject.GetComponent<Transform>().localScale.x, 1, 1);
    }

    #region Nhan Sat Thuong
    public void TakeDamage(int damageAmount)//Nhan sat thuong
    {
        takeDam = true;
        diChuyen = false;
        hp -= damageAmount;
        StartCoroutine(TakeDam());
    }

    IEnumerator TakeDam()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;//thay doi mau de mo phong nhan sat thuong
        yield return new WaitForSeconds(0.4f);//doi 0.4s
        if (hp <= 0 && !tHGameController.isGameOver)//Neu het mau thi gameover
        {
            takeDam = false;
            tHGameController.isGameOver = true;
            diChuyen = false;
            r2.AddForce(Vector2.right * 0);
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
