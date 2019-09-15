using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TittleController : MonoBehaviour
{
    EventSystem system;
    public GameObject fadeIn, blackScreen, titleScreen;


    void Start()
    {
        system = EventSystem.current;
        titleScreen.SetActive(false);
        blackScreen.SetActive(true);
        StartCoroutine(FadeInScreenblack());
    }

    private IEnumerator FadeInScreenblack()//lam man hinh sang len va hien title screen
    {
        yield return new WaitForSeconds(1f);
        blackScreen.SetActive(false);
        fadeIn.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        fadeIn.SetActive(false);
        titleScreen.SetActive(true);
    }

    void Update()
    {
        //Tab key to next input field
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();

            if (next != null)
            {

                InputField inputfield = next.GetComponent<InputField>();
                if (inputfield != null)
                    inputfield.OnPointerClick(new PointerEventData(system));  

                system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
            }

        }

    }

    public void Thoat()
    {
        Application.Quit();
    }
}
