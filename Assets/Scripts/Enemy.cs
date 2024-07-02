using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public TextMeshPro countText;
    public int eliteHp;

    public enum EnemyTypes
    {
        Meteo,
        EliteMeteo
    }

    public EnemyTypes enemyType;

    private Animator animator;

    private Rigidbody2D rb;

    private UIManager uiManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        uiManager = FindObjectOfType<UIManager>();

        if (enemyType == EnemyTypes.EliteMeteo)
        {
            rb = GetComponent<Rigidbody2D>();
            countText.text = eliteHp.ToString();
        }
    }

    private void OnEnable()
    {
        animator.SetBool("isCrash", false);
    }


    public void KillEnemy()
    {
        switch (enemyType)
        {
            case EnemyTypes.Meteo:

                animator.SetBool("isCrash", true);
                StartCoroutine("DisableEnemy");

                break;
            case EnemyTypes.EliteMeteo:

                eliteHp--;

                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0;

                rb.AddForce(new Vector2(0, 100), ForceMode2D.Impulse);

                if (eliteHp <= 0)
                {
                    eliteHp = 0;
                    animator.SetBool("isCrash", true);
                    StartCoroutine("DisableEnemy");
                }

                countText.text = eliteHp.ToString();

                break;
        }
    }

    private IEnumerator DisableEnemy()
    {
        yield return new WaitForSeconds(0.12f);

        SpawnManager spawnManager = FindObjectOfType<SpawnManager>();

        if(spawnManager !=null)
        {
            spawnManager.EnemyDied(gameObject);
        }

        uiManager.KillCount();

        gameObject.SetActive(false);
    }

}
