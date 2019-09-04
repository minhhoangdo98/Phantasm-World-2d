using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PracticeTyping : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonChar;
    [SerializeField]
    private GameObject[] pos;
    private bool enableGenerate = false, playingTying = false;
    [SerializeField]
    private Text scoreText;
    public int score = 0;

    public void KhoiTaoTypingGame()//bat dau game
    {
        pos = new GameObject[gameObject.transform.childCount];//dem so luong vi tri
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = gameObject.transform.GetChild(i).gameObject;//gan cac vi tri vao mang
        }
        enableGenerate = true;
        playingTying = true;
        score = 0;
        scoreText.text = score.ToString();
        
    }

    public void StopTypingGame()//dung game
    {
        enableGenerate = false;
        playingTying = false;
        GameObject[] bt = new GameObject[30];
        bt = GameObject.FindGameObjectsWithTag("ButtonTextRandom");
        for (int i = 0; i < bt.Length; i++)
        {
            Destroy(bt[i]);
        }
    }

    private void Update()
    {
        if(enableGenerate && playingTying)
        {
            StartCoroutine(GeneRateRandomChar());//Khoi tao button o vi tri ngau nhien trong cac vi tri dua vao o mang           
        }
        scoreText.text = score.ToString();
    }
    
    //Tao button xuat hien ngau nhien 1 trong cac vi tri trong mang pos
    IEnumerator GeneRateRandomChar()
    {
        enableGenerate = false;
        int point = Random.Range(0, pos.Length);
        GameObject bc = Instantiate(buttonChar, pos[point].transform.position, Quaternion.identity, gameObject.transform);
        yield return new WaitForSeconds(Random.Range(0.5f, 3f));//Random khoang cach giua cac lan xuat hien
        enableGenerate = true;
    }
}
