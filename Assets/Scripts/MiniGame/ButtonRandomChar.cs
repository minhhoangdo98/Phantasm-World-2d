using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRandomChar : MonoBehaviour
{
    private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";//chuoi chua cac chu cai
    [SerializeField]
    private float speed = 60f;
    private PracticeTyping pt;
    [SerializeField]
    private GameObject starLight;
    void Start()
    {
        //random so la vi tri trong chuoi
        char t = chars[Random.Range(0, chars.Length)];
        gameObject.GetComponentInChildren<Text>().text = t.ToString();//gan vao button text
        speed = Random.Range(50f, 120f);//random toc do di xuong cua button
        pt = gameObject.GetComponentInParent<PracticeTyping>();
    }

    private void Update()
    {
        Vector2 movPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - speed * Time.deltaTime);//xac dinh vi tri
        gameObject.transform.position = movPos;//di chuyen xuong vi tri do
        //neu di chuyen qua canvas thi destroy
        if (gameObject.transform.position.y <= 200)
        {
            if (pt.score > 0)
                pt.score--;
            Destroy(gameObject);
        }
        //neu bam dung thi cong diem va destroy
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(vKey))
            {
                if (vKey.ToString() == gameObject.GetComponentInChildren<Text>().text)
                {
                    Vector3 pos = new Vector3(0, 0, 0);
                    GameObject star = Instantiate(starLight, pos, Quaternion.identity) as GameObject;//Tao hieu ung chuc mung
                    star.GetComponent<ParticleSystem>().Play();
                    Destroy(star, 2);
                    pt.score++;
                    Destroy(gameObject);
                }
            }
        }
    }

}
