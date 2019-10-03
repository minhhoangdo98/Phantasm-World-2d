using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class THGameController : MonoBehaviour
{
    public bool isGameOver = false, isWin = false, thBattle;
    public GameController gc;
    public GameObject player, clearText;
    public int gameType, time;//0: kill all enemy, 1:run to finish point, 2: survival in ammount of time
    [SerializeField]
    private GameObject allEnemy;
    public GameObject thCanvas;

    void Start()
    {
        gc.thPlay = true;
        StartCoroutine(KiemTraScene());
    }

    IEnumerator KiemTraScene()//cho load xong het thi moi load cai nay
    {
        yield return new WaitForSeconds(3);
        switch (gc.eve.story)//kiem tra story
        {
            case 0:
                BatDauManChoiTuDau(4, gc.music.level0);//Kiem tra scene hien tai va thay doi nhac nen tuong ung
                BatDauManChoiTuDau(6, gc.music.challenge);
                break;
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
                BatDauManChoiTuDau(7, gc.music.level1);
                break;
        }
    }

    void Update()
    {
        if (isGameOver && gameType != 3)
            StartCoroutine(Gameover());
        if (isGameOver || isWin)
            thBattle = false;
        if (gc.loadComplete)
        {
            switch (gameType)
            {
                case 0://Kill all enemy type
                    if (allEnemy.transform.childCount <= 0 && !isWin)//Win khi tieu diet toan bo enemy
                    {
                        isWin = true;
                        switch (gc.eve.story)//Kiem tra story
                        {
                            case 0:
                                gc.GetComponent<GameController>().eve.textNum = 20;
                                gc.GetComponent<EventController>().PlayStory();
                                break;
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                            case 10:
                            case 11:
                            case 12:
                            case 13:
                                StartCoroutine(Clear(2, 1000));
                                break;
                        }
                    }
                    break;
                case 1://objective type

                    break;
                case 2://survival type

                    break;
                case 3://misc
                    switch (gc.eve.story)
                    {
                        case 0:
                            if (isGameOver)
                            {
                                player.GetComponent<PlayerController>().hp += 10;
                                isGameOver = false;
                                thBattle = false;
                                gc.GetComponent<GameController>().eve.textNum = 33;
                                gc.GetComponent<EventController>().PlayStory();
                            }
                            break;
                    }
                    break;
            }
                    
        }
    }

    IEnumerator Gameover()
    {
        gc.manHinh.fadeOut.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        PlayerPrefs.SetInt("lastScene", SceneManager.GetActiveScene().buildIndex);//luu scene hien tai de load lai
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);//chuyen sang scene gameover
    }

    private void BatDauManChoiTuDau(int sceneIndex, AudioClip nhacNenChoManChoi)
    {
        if (SceneManager.GetActiveScene().buildIndex == sceneIndex)
        {
            if (nhacNenChoManChoi != null)
                gc.ThayDoiNhacNen(nhacNenChoManChoi);
        }
    }

    private IEnumerator Clear(int sceneToLoad, int cashEarn)
    {
        yield return new WaitForSeconds(1f);
        clearText.SetActive(true);
        gc.stat.AddStat(4, cashEarn);
        yield return new WaitForSeconds(2f);
        StartCoroutine(gc.FadeOutScreenblack());
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}
