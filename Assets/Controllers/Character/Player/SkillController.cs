using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    //Script duoc dung boi object Weapon trong Player Main
    [SerializeField]
    private GameObject weapon1, weapon2, weapon3, player, slash, bullet, weapon1combo1, weapon1combo2, slash2, skill4Icon, skill5Icon, skill6Icon, waterSkill, electricSkill, darkSkill;
    [SerializeField]
    private int combo = 0, skill4Unlock = 0, skill5Unlock = 0, skill6Unlock = 0;
    private float timeStartCombo = 0;
    public bool attackable = true;
    private float delayTime;
    void Start()
    {
        player = gameObject;
        attackable = true;
        delayTime = (float)PlayerPrefs.GetInt("dex" + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString()) / 40;
        if (delayTime >= 0.5f)
            delayTime = 0.49f;
        //lay gia tri skill unlock, =0 chua unlock, =1 da unlock
        skill4Unlock = PlayerPrefs.GetInt("skill4Unlock");
        skill5Unlock = PlayerPrefs.GetInt("skill5Unlock");
        skill6Unlock = PlayerPrefs.GetInt("skill6Unlock");
    }

    void Update()
    {
        #region unlock Skill
        if (skill4Unlock != 0)
            skill4Icon.SetActive(true);
        else
            skill4Icon.SetActive(false);
        if (skill5Unlock != 0)
            skill5Icon.SetActive(true);
        else
            skill5Icon.SetActive(false);
        if (skill6Unlock != 0)
            skill6Icon.SetActive(true);
        else
            skill6Icon.SetActive(false);
        #endregion

        if (player.GetComponent<PlayerController>().diChuyen && player.GetComponent<PlayerController>().dieuKhien)//Neu cho phep di chuyen
        {
            if (Input.GetButtonDown("Fire1") && attackable && player.GetComponent<PlayerController>().sta >= 10)//khi bam Z hoac chuot trai se chem
            {
                if (Time.timeSinceLevelLoad - timeStartCombo > 0.8f)//Neu khong bam danh trong thoi gian qua 1.5 giay thi combo tro lai tu dau
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
                player.GetComponent<PlayerController>().sta -= 10;
            }

            if (Input.GetButton("Fire2") && attackable)//khi bam S 
            {
                StartCoroutine(Attack2());
            }

            if (Input.GetButton("Fire3") && attackable)//khi bam D 
            {
                StartCoroutine(Attack3());
            }

            if (Input.GetButton("Fire4") && attackable && player.GetComponent<PlayerController>().mana >= 7 && skill4Unlock==1)//khi bam z
            {
                StartCoroutine(UseWaterSkill());
                player.GetComponent<PlayerController>().mana -= 7;
            }

            if (Input.GetButton("Fire5") && attackable && player.GetComponent<PlayerController>().mana >= 14 && skill5Unlock == 1)//khi bam z
            {
                StartCoroutine(UseElectricSkill());
                player.GetComponent<PlayerController>().mana -= 14;
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
            Destroy(sla, 0.4f);
        }
        else//danh ben trai
        {
            Vector3 pos = new Vector3(transform.position.x - 1, transform.position.y);//hieu ung chem vi tri la canh vu khi
            GameObject sla = Instantiate(slash, pos, Quaternion.identity, gameObject.transform) as GameObject;//tao hieu ung chem
            Destroy(sla, 0.4f);
        }
        player.GetComponent<PlayerController>().attacktrigger1 = false;
        yield return new WaitForSeconds(0.3f);
        weapon1.SetActive(false);
        combo = 1;
        attackable = true;

    }

    IEnumerator Attack1Combo1()//danh bang kiem
    {
        timeStartCombo = Time.timeSinceLevelLoad;//cap nhat lai thoi gian dem delay combo
        attackable = false;
        player.GetComponent<PlayerController>().attacktrigger4 = true;
        yield return new WaitForSeconds(0.1f);
        weapon1combo1.SetActive(true);
        if (player.GetComponent<PlayerController>().faceright)//danh ben phai
        {
            Vector3 pos = new Vector3(transform.position.x + 1, transform.position.y);//hieu ung chem vi tri la canh vu khi
            GameObject sla = Instantiate(slash2, pos, Quaternion.identity, gameObject.transform) as GameObject;//tao hieu ung chem
            sla.GetComponent<Transform>().Rotate(0, 0, 134);
            Destroy(sla, 0.4f);
        }
        else//danh ben trai
        {
            Vector3 pos = new Vector3(transform.position.x - 1, transform.position.y);//hieu ung chem vi tri la canh vu khi
            GameObject sla = Instantiate(slash2, pos, Quaternion.identity, gameObject.transform) as GameObject;//tao hieu ung chem
            sla.GetComponent<Transform>().Rotate(0, 0, 134);
            Destroy(sla, 0.4f);
        }
        player.GetComponent<PlayerController>().attacktrigger4 = false;
        yield return new WaitForSeconds(0.3f);
        weapon1combo1.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        combo = 2;
        attackable = true;

    }

    IEnumerator Attack1Combo2()//danh bang kiem
    {
        attackable = false;
        player.GetComponent<PlayerController>().attacktrigger3 = true;
        yield return new WaitForSeconds(0.1f);
        weapon1combo2.SetActive(true);
        if (player.GetComponent<PlayerController>().faceright)//danh ben phai
        {
            Vector3 pos = new Vector3(transform.position.x + 1, transform.position.y);//hieu ung chem vi tri la canh vu khi
            GameObject sla = Instantiate(slash, pos, Quaternion.identity, gameObject.transform) as GameObject;//tao hieu ung chem
            Destroy(sla, 0.4f);
        }
        else//danh ben trai
        {
            Vector3 pos = new Vector3(transform.position.x - 1, transform.position.y);//hieu ung chem vi tri la canh vu khi
            GameObject sla = Instantiate(slash, pos, Quaternion.identity, gameObject.transform) as GameObject;//tao hieu ung chem
            Destroy(sla, 0.4f);
        }
        yield return new WaitForSeconds(0.3f);
        player.GetComponent<PlayerController>().attacktrigger3 = false;
        weapon1combo2.SetActive(false);
        yield return new WaitForSeconds(0.5f - delayTime);
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
        yield return new WaitForSeconds(0.5f - delayTime);

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
                bul.transform.Rotate(0, 0, transform.rotation.z + i * 10, Space.Self);//Xoay chieu dan               
                Destroy(bul, 0.25f);
            }
        }
        else//danh ben trai
        {
            for (int i = 0; i < 3 + PlayerPrefs.GetInt("shotgunDam" + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString()); i++)
            {
                Vector3 pos = new Vector3(transform.position.x - 1.5f, transform.position.y + 1.2f);//hieu ung dan
                GameObject bul = Instantiate(bullet, pos, Quaternion.identity) as GameObject;//tao dan ban ra
                bul.transform.Rotate(0, 0, transform.rotation.z + i * -10, Space.Self);
                Destroy(bul, 0.25f);
            }
        }
        yield return new WaitForSeconds(0.3f);
        weapon3.SetActive(false);
        player.GetComponent<PlayerController>().attacktrigger2 = false;
        yield return new WaitForSeconds(0.5f - delayTime);

        attackable = true;
    }
    #endregion

    #region Dung Skill
    IEnumerator UseWaterSkill()
    {
        attackable = false;
        player.GetComponent<PlayerController>().attacktrigger4 = true;
        yield return new WaitForSeconds(0.2f);
        if (player.GetComponent<PlayerController>().faceright)//danh ben phai
        {
            Vector3 pos = new Vector3(transform.position.x + 1.5f, transform.position.y);
            GameObject bul = Instantiate(waterSkill, pos, Quaternion.identity) as GameObject;
        }
        else//danh ben trai
        {
            Vector3 pos = new Vector3(transform.position.x - 1.5f, transform.position.y);
            GameObject bul = Instantiate(waterSkill, pos, Quaternion.identity) as GameObject;
        }
        yield return new WaitForSeconds(0.3f);
        player.GetComponent<PlayerController>().attacktrigger4 = false;
        yield return new WaitForSeconds(0.5f - delayTime);
        attackable = true;
    }

    IEnumerator UseElectricSkill()
    {
        attackable = false;
        player.GetComponent<PlayerController>().attacktrigger3 = true;
        yield return new WaitForSeconds(0.2f);
        if (player.GetComponent<PlayerController>().faceright)//danh ben phai
        {
            Vector3 pos = new Vector3(transform.position.x + 1.5f, transform.position.y);
            GameObject bul = Instantiate(electricSkill, pos, Quaternion.identity) as GameObject;
        }
        else//danh ben trai
        {
            Vector3 pos = new Vector3(transform.position.x - 1.5f, transform.position.y);
            GameObject bul = Instantiate(electricSkill, pos, Quaternion.identity) as GameObject;
        }
        yield return new WaitForSeconds(0.3f);
        player.GetComponent<PlayerController>().attacktrigger3 = false;
        yield return new WaitForSeconds(0.5f - delayTime);
        attackable = true;
    }
    #endregion
}
