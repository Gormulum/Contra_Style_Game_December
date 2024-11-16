using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PotionChangeImage : MonoBehaviour
{

    RawImage rawImage;
    float offset = 0.1428571428571429f;
    float hold = 0.0f;
    void Start()
    {
        rawImage = GetComponent<RawImage>();
    }

    // Update is called once per frame
    public void ChangeImageDown()
    {
        //GetComponent<Image>().sprite = sprite;

        hold += offset;
        rawImage.uvRect = new Rect(hold, 0, 0.1428571428571429f, 1);
        
    }

    public void ChangeImageUp()
    {
        hold -= offset;
        rawImage.uvRect = new Rect(hold, 0, 0.1428571428571429f, 1);
        
    }
}
