using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    //Script duoc dung boi object Weapon trong Player Main
    [SerializeField]
    private GameObject weapon1, weapon2, weapon3, player, slash, bullet, weapon1combo1, weapon1combo2, slash2;
    private int combo = 0;
    private float timeStartCombo = 0;
    public bool attackable = true;
    private float delayTime;
    void Start()
    {
        player = gameObject;
        attackable = true;
        delayTime = (float)PlayerPrefs.GetInt("intl") / 20;
        if (delayTime >= 1)
            delayTime = 0.99f;
    }

    void Update()
    {
        if (player.GetComponent<PlayerController>().diChuyen)//Neu cho phep di chuyen
        {
            if (Input.GetButtonDown("Fire1") && attackable)//khi bam Z hoac chuot trai se chem
            {
                if (Time.timeSinceLevelLoad - timeStartCombo > 1.5f)//Neu khong bam danh trong thoi gian qua 1.5 giay thi combo tro lai tu dau
                    combo = 0;
                switch (combo)//combo chem 3 lan khac nhau
                {
                    case 0:
                        StartCoroutine(Attack1());
                        break;
                    case 1:
                        StartCoroutine(Attack1Combo1());
                        break;
                    case 2:
                        StartCoroutine(Attack1Combo2());
                        break;
                }
            }

            if (Input.GetButton("Fire2") && attackable)//khi bam X hoac chuot phai va co du dan se ban
            {
                StartCoroutine(Attack2());
            }

            if (Input.GetButton("Fire3") && attackable)//khi bam C hoac chuot giua se nhay lui
            {
                StartCoroutine(Attack3());
            }
        }

    }

    #region Tan Cong bang vu khi
    IEnumerator Attack1()//danh bang kiem
    {
        timeStartCombo = Time.timeSinceLevelLoad;//cap nhat lai thoi gian dem delay combo
        attackable = false;

        player.GetComponent<PlayerController>().attacktrigger1 = true;
        yield return new WaitForSeconds(0.1f);
        weapon1.SetActive(true);
        if (player.GetComponent<PlayerController>().faceright)//danh ben phai
        {
            Vector3 pos = new Vector3(transform.position.x + 1, transform.position.y);//hieu ung chem vi tri la canh vu khi
            GameObject sla = Instantiate(slash, pos, Quaternion.identity, gameObject.transform) as GameObject;//tao hieu ung chem
            yield return new WaitForSeconds(0.4f);
            Destroy(sla);
        }
        else//danh ben trai
        {
            Vector3 pos = new Vector3(transform.position.x - 1, transform.position.y);//hieu ung chem vi tri la canh vu khi
            GameObject sla = Instantiate(slash, pos, Quaternion.identity, gameObject.transform) as GameObject;//tao hieu ung chem
            yield return new WaitForSeconds(0.4f);
            Destroy(sla);
        }
        weapon1.SetActive(false);
        player.GetComponent<PlayerController>().attacktrigger1 = false;
        yield return new WaitForSeconds(0.2f);

        combo = 1;
        attackable = true;

    }

    IEnumerator Attack1Combo1()//danh bang kiem
    {
        timeStartCombo = Time.timeSinceLevelLoad;//cap nhat lai thoi gian dem delay combo
        attackable = false;

        player.GetComponent<PlayerController>().attacktrigger4 = true;
        yield return new WaitForSeconds(0.2f);
        weapon1combo1.SetActive(true);
        if (player.GetComponent<PlayerController>().faceright)//danh ben phai
        {
            Vector3 pos = new Vector3(transform.position.x + 1, transform.position.y);//hieu ung chem vi tri la canh vu khi
            GameObject sla = Instantiate(slash2, pos, Quaternion.identity, gameObject.transform) as GameObject;//tao hieu ung chem
            sla.GetComponent<Transform>().Rotate(0, 0, 134);
            yield return new WaitForSeconds(0.4f);
            Destroy(sla);
        }
        else//danh ben trai
        {
            Vector3 pos = new Vector3(transform.position.x - 1, transform.position.y);//hieu ung chem vi tri la canh vu khi
            GameObject sla = Instantiate(slash2, pos, Quaternion.identity, gameObject.transform) as GameObject;//tao hieu ung chem
            sla.GetComponent<Transform>().Rotate(0, 0, 134);
            yield return new WaitForSeconds(0.4f);
            Destroy(sla);
        }
        weapon1combo1.SetActive(false);
        player.GetComponent<PlayerController>().attacktrigger4 = false;
        yield return new WaitForSeconds(0.2f);

        combo = 2;
        attackable = true;

    }

    IEnumerator Attack1Combo2()//danh bang kiem
    {
        attackable = false;

        player.GetComponent<PlayerController>().attacktrigger3 = true;
        yield return new WaitForSeconds(0.3f);
        weapon1combo2.SetActive(true);
        if (player.GetComponent<PlayerController>().faceright)//danh ben phai
        {
            Vector3 pos = new Vector3(transform.position.x + 1, transform.position.y);//hieu ung chem vi tri la canh vu khi
            GameObject sla = Instantiate(slash, pos, Quaternion.identity, gameObject.transform) as GameObject;//tao hieu ung chem
            yield return new WaitForSeconds(0.35f);
            Destroy(sla);
        }
        else//danh ben trai
        {
            Vector3 pos = new Vector3(transform.position.x - 1, transform.position.y);//hieu ung chem vi tri la canh vu khi
            GameObject sla = Instantiate(slash, pos, Quaternion.identity, gameObject.transform) as GameObject;//tao hieu ung chem
            yield return new WaitForSeconds(0.35f);
            Destroy(sla);
        }
        weapon1combo2.SetActive(false);
        player.GetComponent<PlayerController>().attacktrigger3 = false;
        yield return new WaitForSeconds(1 - delayTime);

        combo = 0;
        attackable = true;

    }

    IEnumerator Attack2()//danh bang sung
    {
        attackable = false;

        player.GetComponent<PlayerController>().attacktrigger2 = true;
        yield return new WaitForSeconds(0.2f);
        weapon2.SetActive(true);
        if (player.GetComponent<PlayerController>().faceright)//danh ben phai
        {
            Vector3 pos = new Vector3(transform.position.x + 1.5f, transform.position.y + 1.2f);//hieu ung dan
            GameObject bul = Instantiate(bullet, pos, Quaternion.identity) as GameObject;//tao dan ban ra
            yield return new WaitForSeconds(0.3f);
            Destroy(bul, 1.8f);
        }
        else//danh ben trai
        {
            Vector3 pos = new Vector3(transform.position.x - 1.5f, transform.position.y + 1.2f);//hieu ung dan
            GameObject bul = Instantiate(bullet, pos, Quaternion.identity) as GameObject;//tao dan ban ra
            yield return new WaitForSeconds(0.3f);
            Destroy(bul, 1.8f);
        }       
        weapon2.SetActive(false);
        player.GetComponent<PlayerController>().attacktrigger2 = false;
        yield return new WaitForSeconds(1 - delayTime);

        attackable = true;

    }

    IEnumerator Attack3()//dung shotgun
    {
        attackable = false;

        player.GetComponent<PlayerController>().attacktrigger2 = true;
        yield return new WaitForSeconds(0.2f);
        weapon3.SetActive(true);
        if (player.GetComponent<PlayerController>().faceright)//danh ben phai
        {
            for (int i = 0; i < 3 + PlayerPrefs.GetInt("shotgunDam" + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString()); i++)
            {
                Vector3 pos = new Vector3(transform.position.x + 1.5f, transform.position.y + 1.2f);//hieu ung dan
                GameObject bul = Instantiate(bullet, pos, Quaternion.identity) as GameObject;//tao dan ban ra
                bul.transform.Rotate(0, 0, transform.rotation.z + i * 5, Space.Self);//Xoay chieu dan               
                Destroy(bul, 1.8f);
            }
        }
        else//danh ben trai
        {
            for (int i = 0; i < 3 + PlayerPrefs.GetInt("shotgunDam" + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString()); i++)
            {
                Vector3 pos = new Vector3(transform.position.x - 1.5f, transform.position.y + 1.2f);//hieu ung dan
                GameObject bul = Instantiate(bullet, pos, Quaternion.identity) as GameObject;//tao dan ban ra
                bul.transform.Rotate(0, 0, transform.rotation.z + i * 5, Space.Self);
                Destroy(bul, 1.8f);
            }
        }
        yield return new WaitForSeconds(0.3f);
        weapon3.SetActive(false);
        player.GetComponent<PlayerController>().attacktrigger2 = false;
        yield return new WaitForSeconds(1 - delayTime);

        attackable = true;
    }
    #endregion
}
