using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressController : MonoBehaviour
{
    public Image progressBar;

    private int currentCount = 0;

    public int maxCount;

    public bool isMax;

    public void OnCount()
    {
        if(isMax)
        {
            return;
        }

        currentCount++;

        if(currentCount <= maxCount)
        {
            progressBar.fillAmount = (float)currentCount / maxCount;

            if(currentCount == maxCount)
            {
                isMax = true;
            }
        }
    }

    public void Reset()
    {
        currentCount = 0;
        progressBar.fillAmount = 0;
        isMax = false;
    }

}
