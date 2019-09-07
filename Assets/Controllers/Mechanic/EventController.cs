using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public IEnumerator ChuyenCanh(AudioClip nhacNen, GameObject background, bool activeMenuBackground, bool activeMenu, bool whiteScreen)//Chuyen khung hinh
    {
        enableNext = false;
        yield return new WaitForSeconds(0.5f);
        if(whiteScreen)
            gc.manHinh.fadeOutWhite.SetActive(true);
        else
            gc.manHinh.fadeOut.SetActive(true);
        yield return new WaitForSeconds(1f);
        if(whiteScreen)
        {
            gc.manHinh.whiteScreen.SetActive(true);
            gc.manHinh.fadeOutWhite.SetActive(false);
        }
        else
        {
            gc.manHinh.blackScreen.SetActive(true);
            gc.manHinh.fadeOut.SetActive(false);
        }       
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
        if(whiteScreen)
        {
            gc.manHinh.whiteScreen.SetActive(false);
            gc.manHinh.fadeInWhite.SetActive(true);
        }
        else
        {
            gc.manHinh.blackScreen.SetActive(false);
            gc.manHinh.fadeIn.SetActive(true);
        }
        gc.eve.textNum++;
        PlayStory();
        yield return new WaitForSeconds(1f);
        if (whiteScreen)
            gc.manHinh.fadeInWhite.SetActive(false);
        else
            gc.manHinh.fadeIn.SetActive(false);
    }

    #region event
    public void PlayStory()
    {
        enableNext = true;
        switch (gc.eve.story)
        {
            case 0:
                Intro();
                break;           
        }
    }

    private void Intro()
    {
        switch (gc.eve.textNum)
        {
            case 0:
                gc.music.musicSound.Stop();
                StartCoroutine(ChuyenCanh(null, gc.backGround.hospital, false, false, false));
                break;
            case 1:
                gc.menu.eventCanvas.SetActive(true);
                gc.menu.hoiThoaiPanel.SetActive(true);
                gc.eve.luaChon.SetActive(false);
                gc.eve.facePanel.SetActive(false);              
                gc.eve.talk.text = "...";
                break;
            case 2:
                npc.ThayDoiAvatar(npc.main.confused);
                gc.eve.facePanel.SetActive(true);
                gc.eve.ten.text = gc.stat.Ten;
                gc.eve.talk.text = "[Wh...where am I? Hospital?]";
                break;
            case 3:
                npc.ThayDoiAvatar(npc.gideon.normal);
                gc.eve.ten.text = "???";
                gc.eve.talk.text = "[You finally awake.]";
                break;
            case 4:
                npc.ThayDoiAvatar(npc.main.worry);
                gc.eve.ten.text = gc.stat.Ten;
                gc.eve.talk.text = "[Gildeon! What happen?]";
                break;
            case 5:
                npc.ThayDoiAvatar(npc.gideon.normal);
                gc.eve.ten.text = "Gildeon";
                gc.eve.talk.text = "[Hmm... You don't remember? Are you okay?]";
                StartCoroutine(ChuyenCanh(null, null, false, false, true));
                break;
            case 6:
                npc.ThayDoiAvatar(npc.main.worry);
                gc.eve.ten.text = gc.stat.Ten;
                gc.eve.talk.text = "[Remember what?]";
                break;
            case 7:
                npc.ThayDoiAvatar(npc.main.doubt);
                gc.eve.talk.text = "[Ah! I-I see...]";
                break;
            case 8:
                npc.ThayDoiAvatar(npc.gideon.normal);
                gc.eve.ten.text = "Gildeon";
                gc.eve.talk.text = "[So that's it?]";
                break;
            case 9:
                npc.ThayDoiAvatar(npc.main.angry);
                gc.eve.ten.text = gc.stat.Ten;
                gc.eve.talk.text = "[Yes, it's her and I've been attacked! but this, I mean, I don't believe it, that not right! I-I can't-]";
                break;
            case 10:
                npc.ThayDoiAvatar(npc.gideon.worry);
                gc.eve.ten.text = "Gildeon";
                gc.eve.talk.text = "[Calm down bro! There no need to rush.]";
                break;
            case 11:
                npc.ThayDoiAvatar(npc.main.normal);
                gc.eve.ten.text = gc.stat.Ten;
                gc.eve.talk.text = "[Sorry]";
                break;
            case 19:
                StartCoroutine(ChuyenCanh(null, null, false, false, true));
                break;
            case 20:
                gc.eve.textNum = 0;
                SceneManager.LoadScene(3, LoadSceneMode.Single);
                gc.menu.hoiThoaiPanel.SetActive(false);
                gc.menu.eventCanvas.SetActive(false);
                break;
        }
    }
    #endregion
}
