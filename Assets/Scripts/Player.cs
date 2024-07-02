using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public UIManager uiManager;
    public GameManager gameManager;

    public enum BodyPattern
    {
        Idle,
        Jump,
        SuperJump,
        Shield,
        Attack_Type_1,
        Attack_Type_2,
        Attack_Type_3,
        Attack_Type_4,
        DeadMotion,
        Dead,
        Super_Attack
    }

    public enum AttackPattern
    {
        Attack_Type_1,
        Attack_Type_2,
        Attack_Type_3,
        Attack_Type_4
    }

    public GameObject[] bodyAssets;
    public GameObject[] attackAssets;

    public float jumpForce;

    public bool isEnemyHit;
    private bool isGround;
    private bool isShield;
    private bool isDead;

    public bool isSuperJump;
    public bool isSuperAttack;
    

    private bool canAttack = true;

    private Rigidbody2D rb;

    private int attackingId;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        BodyStatus(BodyPattern.Idle);
    }


    public void AttackButton()
    {
        if(canAttack)
        {
            if(isDead)
            {
                return;
            }

            if (isSuperJump || isSuperAttack)
            {
                return;
            }

            StopCoroutine("AttackCoolDown");
            StartCoroutine("AttackCoolDown");

            attackingId++;

            switch (attackingId)
            {
                case 1:
                    BodyStatus(BodyPattern.Attack_Type_1);
                    AttackStatus(AttackPattern.Attack_Type_1);
                    break;
                case 2:
                    BodyStatus(BodyPattern.Attack_Type_2);
                    AttackStatus(AttackPattern.Attack_Type_2);
                    break;
                case 3:
                    BodyStatus(BodyPattern.Attack_Type_3);
                    AttackStatus(AttackPattern.Attack_Type_3);
                    break;
                case 4:
                    BodyStatus(BodyPattern.Attack_Type_4);
                    AttackStatus(AttackPattern.Attack_Type_4);
                    break;
            }

            if (attackingId == 4) //공격 패턴 개수
            {
                attackingId = 0;
            }
        }
    }

    private void AttackStatus(AttackPattern attackStatus)
    {
        for (var i = 0; i < attackAssets.Length; i++)
        {
            attackAssets[i].SetActive(false);
        }
        attackAssets[(int)attackStatus].SetActive(true);
    }

    private IEnumerator AttackCoolDown()
    {
        canAttack = false;

        yield return new WaitForSeconds(0.15f);

        canAttack = true;

        for (var i = 0; i < attackAssets.Length; i++)
        {
            attackAssets[i].SetActive(false);
        }

        if (isGround && !isDead)
        {
            yield return new WaitForSeconds(0.15f);
            BodyStatus(BodyPattern.Idle);
            attackingId = 0;
        }
    }

    public void ShieldButton()
    {
        if (isSuperJump || isSuperAttack)
        {
            return;
        }

        isShield = true;
        attackingId = 0;

        BodyStatus(BodyPattern.Shield);
        
        StartCoroutine("CloseShield");
    }

    private IEnumerator CloseShield()
    {
        yield return new WaitForSeconds(0.6f);
        isShield = false;
        if (isGround)
        {
            DefaultBody();
        }
    }

    public void JumpButton()
    {
        if(!isGround)
        {
            return;
        }

        if (isSuperJump || isSuperAttack)
        {
            return;
        }

        attackingId = 0;

        uiManager.JumpCount();

        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        BodyStatus(BodyPattern.Jump);

        isGround = false;
    }

    private void BodyStatus(BodyPattern bodyStatus)
    {
        for(var i = 0; i< bodyAssets.Length;i++)
        {
            bodyAssets[i].SetActive(false);
        }

        bodyAssets[(int)bodyStatus].SetActive(true);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead)
        {
            return;
        }

        if (collision.gameObject.transform.CompareTag("Ground"))
        {
            if(isSuperAttack || isSuperJump)
            {
                return;
            }
            DefaultBody();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(isDead)
        {
            return;
        }

        if(isSuperJump || isSuperAttack)
        {
            return;
        }

        if (collision.gameObject.transform.CompareTag("Enemy"))
        {
            if(isShield)
            {
                return;
            }

            isEnemyHit = true;
        }

        if(collision.gameObject.transform.CompareTag("Ground"))
        {
            isGround = true;
        }

        if(isEnemyHit && isGround)
        {
            StartCoroutine("DeadPlayer");
            CinemachineShake.Instance.ShakeCamera(5f, 3f, 0.7f);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.transform.CompareTag("Enemy"))
        {
            isEnemyHit = false;
        }

        if (collision.gameObject.transform.CompareTag("Ground"))
        {
            isGround = false;
        }
    }

    private IEnumerator DeadPlayer()
    {
        gameManager.gameOver = true;

        isDead = true;

        uiManager.ShowResultScreen("Fail");

        BodyStatus(BodyPattern.DeadMotion);

        yield return new WaitForSeconds(2f);

        rb.simulated = false;

        yield return new WaitForSeconds(0.6f);

        BodyStatus(BodyPattern.Dead);
        CinemachineShake.Instance.ShakeCamera(5f, 3f, 0.5f);
    }

    private void DefaultBody()
    {
        BodyStatus(BodyPattern.Idle);
    }

    public void SuperJump()
    {
        if(isDead)
        {
            return;
        }

        attackingId = 0;

        isSuperJump = true;

        rb.AddForce(new Vector2(0, jumpForce * 0.9f), ForceMode2D.Impulse);
        BodyStatus(BodyPattern.SuperJump);

        rb.isKinematic = true;

        StartCoroutine("EndSuperJump");
    }

    private IEnumerator EndSuperJump()
    {
        yield return new WaitForSeconds(1f);

        
        isSuperJump = false;

        rb.isKinematic = false;
        BodyStatus(BodyPattern.Jump);
    }

    public void SuperAttack()
    {
        if (isDead)
        {
            return;
        }

        isSuperAttack = true;



        BodyStatus(BodyPattern.Super_Attack);


        StartCoroutine("EndSuperAttack");

    }

    private IEnumerator EndSuperAttack()
    {
        yield return new WaitForSeconds(0.8f);
        CinemachineShake.Instance.ShakeCamera(5f,3f, 1f);

        yield return new WaitForSeconds(0.7f);

        isSuperJump = false;

        isSuperAttack = false;


        if (isGround)
        {
            BodyStatus(BodyPattern.Idle);
        }
        else
        {
            BodyStatus(BodyPattern.Jump);
        }
    }
}
