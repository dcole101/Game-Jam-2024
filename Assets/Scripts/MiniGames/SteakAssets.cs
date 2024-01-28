using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SteakAssets : MonoBehaviour
{
    public List<Sprite> sprites;
    int currentSprite = 0;
    
    public void UpdateSprite()
    {
        currentSprite++;
        GetComponent<Image>().sprite = sprites[currentSprite];
    }

    public void ResetSprite()
    {
        currentSprite = 0;
        GetComponent<Image>().sprite = sprites[0];
    }
}
