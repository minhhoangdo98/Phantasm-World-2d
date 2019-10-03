using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcController : MonoBehaviour
{
    public Lily lily;
    public Gideon gideon;
    public Ruth ruth;
    public Main main;
    public GameObject face, portrait1, portrait2;

    public void ThayDoiAvatar(Texture faceImage)
    {
        face.GetComponent<RawImage>().texture = faceImage;
    }

    public void ThayDoiPortrait(Texture faceImage, GameObject portrait, float positionX, float positionY)//Thay doi portrait theo vi tri x, y tinh tu vi tri ban dau
    {
        portrait.GetComponent<RawImage>().texture = faceImage;
        portrait.transform.position = new Vector2(portrait.transform.position.x + positionX, portrait.transform.position.y + positionY);
    }
}
