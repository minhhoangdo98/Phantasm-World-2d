using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public GameObject model;
    public bool loadComplete = false;
    public Menu menu;
    public ManHinh manHinh;
    public Event eve;
    public BackGround backGround;
    public bool menuBackground = true, thPlay = false;
    public ThoiGian tg;
    public CalendarController calendar;
    private AudioSource sound;
    public AudioClip buttonSound;
    public Music music;
    public PlayerStat stat;
    public THGameController thgc;

    private void Start()
    {
        StartCoroutine(LoadObject());
    }

    IEnumerator LoadObject()
    {
        model = GameObject.FindGameObjectWithTag("Model");
        manHinh = model.GetComponent<ManHinh>();    
        manHinh.loadingScreen.SetActive(true);
        yield return new WaitForEndOfFrame();
        menu = model.GetComponent<Menu>();
        yield return new WaitForSeconds(0.1f);
        eve = model.GetComponent<Event>();
        eve.textNum = 0;
        yield return new WaitForSeconds(0.1f);
        backGround = model.GetComponent<BackGround>();
        yield return new WaitForSeconds(0.1f);
        tg = model.GetComponent<ThoiGian>();
        tg.dayNow = DateTime.Now.Day;
        tg.dayPrev = tg.dayNow - 1;
        yield return new WaitForSeconds(0.1f);
        if (!thPlay)
        {
            calendar = GameObject.FindGameObjectWithTag("Calendar").GetComponent<CalendarController>();
            yield return new WaitForSeconds(0.1f);
            GetSelectedDayInCal();
        }
        music = GameObject.FindGameObjectWithTag("Music").GetComponent<Music>();
        yield return new WaitForSeconds(0.1f);
        sound = model.GetComponent<AudioSource>();
        music.musicSound = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        yield return new WaitForSeconds(0.1f);
        if (!thPlay)
            ThayDoiNhacNen(music.normalMusic);
        yield return new WaitForSeconds(0.1f);
        stat = model.GetComponent<PlayerStat>();
        stat.LoadStat(PlayerPrefs.GetInt("idTKCurrent"));
        gameObject.GetComponent<EventController>().LoadCotTruyen();
        yield return new WaitForSeconds(0.1f);
        loadComplete = true;
        manHinh.loadingScreen.SetActive(false);
    }

    private void Update()
    {
        if (loadComplete)
        {
            Clock();
        }
    }

    #region MenuControll
    void Clock()//dong ho thoi gian thuc theo thoi gian he thong
    {
        if (menuBackground == true)
        {
            tg.timeNow = DateTime.Now.ToLongTimeString();
            tg.clockText.text = tg.timeNow;
            if ((int)DateTime.Now.Hour >= 0 && (int)DateTime.Now.Hour <= 18)
            {
                if (backGround.sang != null && backGround.toi != null)
                {
                    backGround.sang.SetActive(true);
                    backGround.toi.SetActive(false);
                    menu.menuButton[19].SetActive(false);
                }
            }           
            if ((int)DateTime.Now.Hour >= 19 && (int)DateTime.Now.Hour <= 23)
            {
                if (backGround.sang != null && backGround.toi != null)
                {
                    backGround.sang.SetActive(false);
                    backGround.toi.SetActive(true);
                    menu.menuButton[19].SetActive(true);
                }
            }
        }
        else
        {
            backGround.sang.SetActive(false);
            backGround.toi.SetActive(false);
        }
    }

    public void AnHienMenu(GameObject subMenu)
    {
        menu.TatToanBoMidMenu(subMenu);
        subMenu.SetActive(!subMenu.activeInHierarchy);
        sound.clip = buttonSound;
        sound.Play();
    }

    public void GetSelectedDayInCal()
    {
        foreach (GameObject item in calendar._dateItems)
        {
            if (item.GetComponentInChildren<Text>().text == tg.dayNow.ToString() && calendar._monthNumText.text == DateTime.Now.Month.ToString() && calendar._yearNumText.text == DateTime.Now.Year.ToString())
            {
                item.GetComponent<Image>().color = Color.yellow;
            }
            else
                GetDeadLineDay(item);
                
        }
    }

    public void GetDeadLineDay(GameObject item)
    {
        for (int i = 1; i <= PlayerPrefs.GetInt("demPlan" + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString()); i++)
        {
            if (item.GetComponentInChildren<Text>().text == PlayerPrefs.GetInt("deadlineDay" + i.ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString()).ToString() && calendar._monthNumText.text == PlayerPrefs.GetInt("deadlineMonth" + i.ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString()).ToString() && calendar._yearNumText.text == PlayerPrefs.GetInt("deadlineYear" + i.ToString() + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString()).ToString())
            {
                item.GetComponent<Image>().color = Color.green;
                return;
            }
        }
        item.GetComponent<Image>().color = Color.white;
    }

    public void OnDateItemClick()
    {
        menu.mBPanel.SetActive(true);
    }

    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    #endregion

    #region Nhac
    public void ThayDoiNhacNen(AudioClip nhac)
    {
        music.musicSound.Stop();
        music.musicSound.clip = nhac;
        music.musicSound.Play();
    }
    #endregion

    #region HieuUng
    public void TaoHieuUng(GameObject hieuUng, float lifeTime, Vector3 pos)
    {
        GameObject par = Instantiate(hieuUng, pos, Quaternion.identity);
        Destroy(par, lifeTime);
    }
    #endregion

    #region screenEvent
    public IEnumerator FadeOutScreenWhite()//Lam man hinh trang dan
    {
        yield return new WaitForSeconds(0.5f);
        manHinh.fadeOutWhite.SetActive(true);
        yield return new WaitForSeconds(1f);
        manHinh.whiteScreen.SetActive(true);
        manHinh.fadeOutWhite.SetActive(false);
    }

    public IEnumerator FadeInScreenWhite()//lam man hinh giam do trang xuong
    {
        yield return new WaitForSeconds(0.5f);
        manHinh.whiteScreen.SetActive(false);
        manHinh.fadeInWhite.SetActive(true);
        yield return new WaitForSeconds(1f);
        manHinh.fadeInWhite.SetActive(false);
    }

    public IEnumerator FadeOutScreenblack()//Lam man hinh toi dan
    {
        yield return new WaitForSeconds(0.5f);
        manHinh.fadeOut.SetActive(true);
        yield return new WaitForSeconds(1f);
        manHinh.blackScreen.SetActive(true);
        manHinh.fadeOut.SetActive(false);
    }

    public IEnumerator FadeInScreenblack()//lam man hinh sang len
    {
        yield return new WaitForSeconds(0.5f);
        manHinh.blackScreen.SetActive(false);
        manHinh.fadeIn.SetActive(true);
        yield return new WaitForSeconds(1f);
        manHinh.fadeIn.SetActive(false);
    }
    #endregion

    #region SuKien 
    public void MenuIconClicked()//kiem tra story de unlock cac chuc nang
    {
        switch (eve.story)
        {
            case 0://neu phan mo dau thi lock tat ca chuc nang ngoai tru cot truyen
                for(int i = 12; i < menu.menuButton.Length; i++)
                {
                    menu.menuButton[i].SetActive(true);
                }
                menu.menuButton[13].SetActive(false);
                break;
            case 1:
                menu.menuButton[12].SetActive(true);
                for (int i = 13; i < 19; i++)
                {
                    menu.menuButton[i].SetActive(false);
                }
                menu.menuButton[20].SetActive(true);
                break;
            case 2:
                for (int i = 12; i < 19; i++)
                {
                    menu.menuButton[i].SetActive(false);
                }
                menu.menuButton[20].SetActive(true);
                break;
        }
    }

    public void BtnEvent()
    {
        switch (eve.story)
        {
            case 0:
                eve.textNum = 0;
                gameObject.GetComponent<EventController>().PlayStory();
                break;
            case 1:
                eve.textNum = 0;
                gameObject.GetComponent<EventController>().PlayStory();
                break;
            case 2:
                eve.textNum = 0;
                gameObject.GetComponent<EventController>().PlayStory();
                break;
                
        }
    }

    public void BtnMission()
    {
        
    }

    #endregion

    #region Debug
    public void SetStoryUpOrDown(int num)
    {
        eve.story = num;
        gameObject.GetComponent<EventController>().LuuCotTruyen();
    }

    public void UnlockAllSkill()
    {
        PlayerPrefs.SetInt("skill4Unlock", 1);
        PlayerPrefs.SetInt("skill5Unlock", 1);
        PlayerPrefs.SetInt("skill6Unlock", 1);
    }
    #endregion

}
