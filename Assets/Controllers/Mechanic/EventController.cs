using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    private GameController gc;
    [SerializeField]
    private NpcController npc;

    public bool enableNext = false;

    void Start()
    {
        gc = gameObject.GetComponent<GameController>();
    }

    #region luu, load cot truyen
    public void LuuCotTruyen()
    {
        PlayerPrefs.SetInt("story" + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString(), gc.eve.story);
    }

    public void LoadCotTruyen()
    {
        gc.eve.story = PlayerPrefs.GetInt("story" + "tk" + PlayerPrefs.GetInt("idTKCurrent").ToString());
    }
    #endregion

    #region button
    public void LuaChon1()
    {
        switch (gc.eve.story)
        {
            case 0:

                break;
        }
    }
    public void LuaChon2()
    {
        switch (gc.eve.story)
        {
            case 0:

                break;          
        }
    }
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && enableNext)
        {
            gc.eve.textNum++;
            PlayStory();
        }
    }
    public void Complete()//Hoan thanh 1 story
    {
        enableNext = false;
        gc.eve.textNum = 0;
        gc.eve.story++;
        LuuCotTruyen();
        gc.menu.eventCanvas.SetActive(false);
    }

    public IEnumerator ChuyenCanh(AudioClip nhacNen, GameObject background, bool activeMenuBackground, bool activeMenu)//Chuyen khung hinh
    {
        enableNext = false;
        yield return new WaitForSeconds(0.5f);
        gc.manHinh.fadeOut.SetActive(true);
        yield return new WaitForSeconds(1f);
        gc.manHinh.blackScreen.SetActive(true);
        gc.manHinh.fadeOut.SetActive(false);
        gc.menu.menuCanvas.SetActive(activeMenu);
        gc.menuBackground = activeMenuBackground;
        if (background != null)
        {
            gc.backGround.TatToanBoBackGround();
            background.SetActive(true);
        }
        else
        {
            gc.backGround.TatToanBoBackGround();
        }
        yield return new WaitForSeconds(1f);
        if (nhacNen != null)
        {
            gc.ThayDoiNhacNen(nhacNen);
        }
        gc.manHinh.blackScreen.SetActive(false);
        gc.manHinh.fadeIn.SetActive(true);
        gc.eve.textNum++;
        PlayStory();
        yield return new WaitForSeconds(1f);
        gc.manHinh.fadeIn.SetActive(false);
    }

    #region event
    public void PlayStory()
    {
        enableNext = true;
        switch (gc.eve.story)
        {
            case 0:
                
                break;           
        }
    }
    #endregion
}
