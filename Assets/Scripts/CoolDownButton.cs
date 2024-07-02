using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownButton : MonoBehaviour
{
    public float coolDownTime;
    private float currentTime;
    private bool isCoolDown = false;

    public Image coolDownImage;

    public Button currentButton;

    private void Update()
    {
        if(isCoolDown)
        {
            currentTime += Time.deltaTime;
            float fillAmount = 1 - currentTime / coolDownTime;
            coolDownImage.fillAmount = Mathf.Clamp01(fillAmount);

            if (currentTime >= coolDownTime)
            {
                currentTime = 0f;
                isCoolDown = false;
                currentButton.enabled = true;
            }
        }
    }

    public void StartCoolDown()
    {
        currentButton.enabled = false;
        isCoolDown = true;
    }
}
