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
        gc.menu.hoiThoaiPanel.SetActive(false);
    }

    public IEnumerator ChuyenCanh(AudioClip nhacNen, GameObject background, bool activeMenuBackground, bool activeMenu, bool whiteScreen)//Chuyen khung hinh
    {
        enableNext = false;
        gc.backGround.backgroundCanvas.SetActive(true);
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
                StartCoroutine(ChuyenCanh(gc.music.mysterial, gc.backGround.hospital, false, false, false));
                break;
            case 1:
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
                gc.eve.talk.text = "[Gildeon! What happened?]";
                break;
            case 5:
                npc.ThayDoiAvatar(npc.gideon.normal);
                gc.eve.ten.text = "Gildeon";
                gc.eve.talk.text = "[Hmm... You don't remember anything? Are you okay?]";
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
                gc.eve.talk.text = "[You rushed to that place without telling anyone and then the police found you unconscious, what happened after all? Did you find the culprit?]";
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
                gc.eve.talk.text = "[Sorry, It happened so quick!]";
                break;
            case 12:
                npc.ThayDoiAvatar(npc.gideon.smile);
                gc.eve.ten.text = "Gildeon";
                gc.eve.talk.text = "[Tell me more!]";
                break;
            case 13:
                npc.ThayDoiAvatar(npc.main.normal);
                gc.eve.ten.text = gc.stat.Ten;
                gc.eve.talk.text = "[As you know, last night when that accident happened, I was there and...]";
                break;
            case 14:
                StartCoroutine(ChuyenCanh(null, null, false, false, true));
                break;
            case 15:
                gc.eve.textNum = 0;
                SceneManager.LoadScene(4, LoadSceneMode.Single);
                gc.menu.hoiThoaiPanel.SetActive(false);
                break;
            case 16:
                gc.menu.hoiThoaiPanel.SetActive(true);
                gc.eve.luaChon.SetActive(false);
                gc.eve.facePanel.SetActive(true);
                gc.menu.hoiThoaiPanel.transform.position = new Vector2(gc.menu.hoiThoaiPanel.transform.position.x, gc.menu.hoiThoaiPanel.transform.position.y + 400f);
                npc.ThayDoiAvatar(npc.main.confused);
                gc.eve.ten.text = gc.stat.Ten.ToString();
                gc.eve.talk.text = "[!!]";
                break;
            case 17:
                gc.eve.talk.text = "[What is that thing?]";
                break;
            case 18:
                gc.eve.talk.text = "[A ghost?]";
                break;
            case 19:
                gc.menu.hoiThoaiPanel.transform.position = new Vector2(gc.menu.hoiThoaiPanel.transform.position.x, gc.menu.hoiThoaiPanel.transform.position.y - 400f);
                gc.menu.hoiThoaiPanel.SetActive(false);
                enableNext = false;
                gc.eve.textNum = 0;
                gc.thgc.thBattle = true;
                break;
            case 20:
                StartCoroutine(ChuyenCanh(null, gc.backGround.introStage, false, false, false));
                gc.thgc.thCanvas.SetActive(false);
                break;
            case 21:
                gc.menu.hoiThoaiPanel.SetActive(true);
                gc.eve.luaChon.SetActive(false);
                gc.eve.facePanel.SetActive(true);               
                npc.ThayDoiAvatar(npc.main.normal);
                gc.eve.ten.text = gc.stat.Ten.ToString();
                gc.eve.talk.text = "[It disappears! Is that thing real? And what's more, the surroundings are strange!]";
                break;
            case 22:
                gc.eve.facePanel.SetActive(false);
                gc.eve.talk.text = "There is someone appears behind you.";
                break;
            case 23:
                gc.eve.facePanel.SetActive(false);
                gc.eve.ten.text = "???";
                gc.eve.talk.text = "[You don't belong here! Leave now! I don't want to fight you!]";
                break;
            case 24:
                gc.eve.facePanel.SetActive(true);
                gc.eve.ten.text = gc.stat.Ten.ToString();
                gc.eve.talk.text = "[What? Who are you?]";
                break;
            case 25:
                gc.eve.facePanel.SetActive(false);
                gc.eve.ten.text = "???";
                gc.eve.talk.text = "[...]";
                break;
            case 26:
                gc.eve.facePanel.SetActive(true);
                gc.eve.ten.text = gc.stat.Ten.ToString();
                gc.eve.talk.text = "[You are suspicious, Put down your weapons and come with me!]";
                break;
            case 27:
                gc.eve.facePanel.SetActive(false);
                gc.eve.ten.text = "???";
                gc.eve.talk.text = "[Go away!]";
                break;
            case 28:
                gc.eve.facePanel.SetActive(true);
                npc.ThayDoiAvatar(npc.main.angry);
                gc.eve.ten.text = gc.stat.Ten.ToString();
                gc.eve.talk.text = "[You look familiar! Are you... that person?]";
                break;
            case 29:
                gc.eve.facePanel.SetActive(false);
                gc.eve.ten.text = "???";
                gc.eve.talk.text = "[You leave me no choice but to knock you down!]";
                break;
            case 30:
                gc.eve.facePanel.SetActive(true);
                npc.ThayDoiAvatar(npc.main.angry);
                gc.eve.ten.text = gc.stat.Ten.ToString();
                gc.eve.talk.text = "[I won't let you do that!]";
                break;
            case 31:
                StartCoroutine(ChuyenCanh(null, null, false, false, false));
                break;
            case 32:
                gc.eve.textNum = 0;
                SceneManager.LoadScene(6, LoadSceneMode.Single);
                gc.menu.hoiThoaiPanel.SetActive(false);
                gc.thgc.thCanvas.SetActive(true);
                break;
            case 33:
                StartCoroutine(ChuyenCanh(gc.music.mysterial, gc.backGround.hospital, false, false, true));
                gc.thgc.thCanvas.SetActive(false);
                break;
            case 34:
                gc.menu.hoiThoaiPanel.SetActive(true);
                gc.eve.luaChon.SetActive(false);
                gc.eve.facePanel.SetActive(true);
                npc.ThayDoiAvatar(npc.main.normal);
                gc.eve.ten.text = gc.stat.Ten.ToString();
                gc.eve.talk.text = "[And then I lost consciousness, that's all as I remember!]";
                break;
            case 35:
                npc.ThayDoiAvatar(npc.main.confused);
                gc.eve.talk.text = "[Is this real? Magic, ghost, elemental? I don't believe it! But... I have witnessed it with my own eyes, or is that my nightmar?]";
                break;
            case 36:
                npc.ThayDoiAvatar(npc.gideon.normal);
                gc.eve.ten.text = "Gildeon";
                gc.eve.talk.text = "[I trust you, my friend! This could be something, but don't push yourself too hard, you are injured and need to rest, just leave it to me, okay?]";
                break;
            case 37:
                npc.ThayDoiAvatar(npc.main.normal);
                gc.eve.ten.text = gc.stat.Ten.ToString();
                gc.eve.talk.text = "[OK, When I recover, we will continue to investigate!]";
                break;
            case 38:
                npc.ThayDoiAvatar(npc.gideon.normal);
                gc.eve.ten.text = "Gildeon";
                gc.eve.talk.text = "[Right, Until then, rest well!]";
                break;
            case 39:
                npc.ThayDoiAvatar(npc.main.normal);
                gc.eve.ten.text = gc.stat.Ten.ToString();
                gc.eve.talk.text = "[Bye!]";
                break;
            case 40:
                Complete();
                SceneManager.LoadScene(2, LoadSceneMode.Single);
                break;
        }
    }
    #endregion
}
