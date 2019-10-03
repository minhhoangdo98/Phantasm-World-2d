using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    public void BtnChoiLai()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("lastScene"), LoadSceneMode.Single);//Khi bam button choi lai thi load scene vua moi bi chet
    }

    public void BtnDangXuat()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);//Khi bam button Dang xuat thi quay ve man hinh dang nhap
    }

}
