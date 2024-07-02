using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnDropController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private UIManager uiManager;
    private Player player;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        player = FindObjectOfType<Player>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject hoveredObject = eventData.pointerDrag;

        if (hoveredObject != null && uiManager != null)
        {
            if (hoveredObject.name == "JumpButton")
            {
                if(player.isSuperAttack)
                {
                    return;
                }
                uiManager.SuperJumpReset();
                player.SuperJump();
            }
            else if (hoveredObject.name == "AttackButton")
            {
                if (player.isSuperJump)
                {
                    return;
                }
                uiManager.SuperAttackReset();
                player.SuperAttack();
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
