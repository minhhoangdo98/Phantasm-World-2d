using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LilyMaskedController : MonoBehaviour
{
    //script duoc su dung boi boss lilyMasked
    [SerializeField]
    private EnemyController lilyMasked;
    public bool skillKi = true;
    public GameObject ki, thunderBall;
    [SerializeField]
    private Slider hpSlider;

    private void Start()
    {
        hpSlider.minValue = 0;
        hpSlider.maxValue = lilyMasked.hp;
        hpSlider.value = lilyMasked.hp;
    }

    private void Update()
    {
        if (skillKi && lilyMasked.damagable && lilyMasked.hoatDong)//su dung skill
            StartCoroutine(SkillKi1());
        hpSlider.value = lilyMasked.hp;
    }

    IEnumerator SkillKi1()
    {
        skillKi = false;
        yield return new WaitForSeconds(2);
        lilyMasked.GetComponent<EnemyController>().attacktrigger2 = true;
        yield return new WaitForSeconds(0.3f);
        if(lilyMasked.grounded)
            if (lilyMasked.GetComponent<EnemyController>().faceRight)//danh ben phai
            {
                Vector3 pos = new Vector3(lilyMasked.transform.position.x + 1, lilyMasked.transform.position.y);
                GameObject k1 = Instantiate(thunderBall, pos, Quaternion.identity, lilyMasked.transform) as GameObject;
                GameObject k2 = Instantiate(thunderBall, pos, Quaternion.identity, lilyMasked.transform) as GameObject;
                k2.transform.Rotate(0, 0, transform.rotation.z - 30, Space.Self);
                Destroy(k1, 2);
                Destroy(k2, 2);
            }
            else//danh ben trai
            {
                Vector3 pos = new Vector3(lilyMasked.transform.position.x - 1, lilyMasked.transform.position.y);
                GameObject k1 = Instantiate(thunderBall, pos, Quaternion.identity, lilyMasked.transform) as GameObject;
                GameObject k2 = Instantiate(thunderBall, pos, Quaternion.identity, lilyMasked.transform) as GameObject;
                k2.transform.Rotate(0, 0, transform.rotation.z + 30, Space.Self);
                Destroy(k1, 2);
                Destroy(k2, 2);
            }
        else
            for (int i = 0; i < 5; i++)
            {
                Vector3 pos = new Vector3(lilyMasked.transform.position.x - 1.5f, lilyMasked.transform.position.y);
                GameObject k = Instantiate(ki, pos, Quaternion.identity, lilyMasked.transform) as GameObject;
                if (lilyMasked.faceRight)
                    k.transform.Rotate(0, 0, transform.rotation.z + i * 15, Space.Self);
                else
                {
                    k.transform.Rotate(0, 0, transform.rotation.z + i * -15, Space.Self);
                    k.transform.localScale = new Vector2(-1, 1);
                }
                Destroy(k, 2);
            }
        yield return new WaitForSeconds(0.2f);
        lilyMasked.GetComponent<EnemyController>().attacktrigger2 = false;
        skillKi = true;
    }

}
