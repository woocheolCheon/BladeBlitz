using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button jumpButton;
    public Button defenseButton;
    public Button attackButton;

    public GameObject superJumpImage;
    public GameObject superAttackImage;

    public GameObject failedReloadScreen;
    public GameObject successReloadScreen;

    private bool canSuperJump = false;
    private bool canSuperAttack = false;

    public GameObject introScreen;


    public void ShowIntro()
    {
        StartCoroutine("IntroPanel");
    }

    private IEnumerator IntroPanel()
    {
        introScreen.SetActive(true);
        yield return new WaitForSeconds(8f);
        introScreen.SetActive(false);

    }

    public void ShowResultScreen(string status)
    {
        StartCoroutine("ReloadScreen", status);
    }

    private IEnumerator ReloadScreen(string status)
    {
        jumpButton.enabled = false;
        defenseButton.enabled = false;
        attackButton.enabled = false;
        yield return new WaitForSeconds(3.5f);

        if(status == "Fail")
        {
            failedReloadScreen.SetActive(true);
        }
        else if(status == "Success")
        {
            successReloadScreen.SetActive(true);
        }
    }

    public void JumpCount()
    {
        ProgressController progressController = jumpButton.GetComponent<ProgressController>();
        progressController.OnCount();

        canSuperJump = progressController.isMax;

        if(canSuperJump)
        {
            superJumpImage.SetActive(true);
        }
    }

    public void KillCount()
    {
        ProgressController progressController = attackButton.GetComponent<ProgressController>();
        progressController.OnCount();

        canSuperAttack = progressController.isMax;

        if (canSuperAttack)
        {
            superAttackImage.SetActive(true);
        }
    }

    public void SuperJumpReset()
    {
        ProgressController progressController = jumpButton.GetComponent<ProgressController>();
        progressController.Reset();
        superJumpImage.SetActive(false);
    }

    public void SuperAttackReset()
    {
        ProgressController progressController = attackButton.GetComponent<ProgressController>();
        progressController.Reset();
        superAttackImage.SetActive(false);
    }
}
