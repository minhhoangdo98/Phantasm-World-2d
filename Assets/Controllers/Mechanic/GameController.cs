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
    public bool menuBackground = true;
    public ThoiGian tg;
    public CalendarController calendar;
    private AudioSource sound;
    public AudioClip buttonSound;
    public Music music;
    public PlayerStat stat;

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
        menu.menuList.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        backGround = model.GetComponent<BackGround>();
        yield return new WaitForSeconds(0.1f);
        tg = model.GetComponent<ThoiGian>();
        tg.dayNow = DateTime.Now.Day;
        tg.dayPrev = tg.dayNow - 1;
        yield return new WaitForSeconds(0.1f);
        calendar = GameObject.FindGameObjectWithTag("Calendar").GetComponent<CalendarController>();
        yield return new WaitForSeconds(0.1f);
        music = GameObject.FindGameObjectWithTag("Music").GetComponent<Music>();
        yield return new WaitForSeconds(0.1f);
        GetSelectedDayInCal();
        sound = model.GetComponent<AudioSource>();
        music.musicSound = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        yield return new WaitForSeconds(0.1f);
        ThayDoiNhacNen(music.normalMusic);
        yield return new WaitForSeconds(0.1f);
        stat = model.GetComponent<PlayerStat>();
        stat.LoadStat(PlayerPrefs.GetInt("idTKCurrent"));
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
                    menu.menuButton[18].SetActive(false);
                }
            }           
            if ((int)DateTime.Now.Hour >= 19 && (int)DateTime.Now.Hour <= 23)
            {
                if (backGround.sang != null && backGround.toi != null)
                {
                    backGround.sang.SetActive(false);
                    backGround.toi.SetActive(true);
                    menu.menuButton[18].SetActive(true);
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
                break;
            }
            else
                item.GetComponent<Image>().color = Color.white;
        }
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

}
