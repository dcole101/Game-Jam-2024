using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssetContainer : MonoBehaviour
{
    public Sprite defaultSprite;
    public Sprite altSprite;

    public void switchSprite()
    {
        if (GetComponent<Image>().sprite == defaultSprite)
        {
            GetComponent<Image>().sprite = altSprite;
        }
        else
        {
            GetComponent<Image>().sprite = defaultSprite;
        }
    }

    public void ResetSprite()
    {
        GetComponent<Image>().sprite = defaultSprite;
    }
}
