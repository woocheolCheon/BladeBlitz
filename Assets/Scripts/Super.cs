using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Super : MonoBehaviour
{
    private bool isElite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                switch(enemy.enemyType)
                {
                    case Enemy.EnemyTypes.Meteo:

                        CinemachineShake.Instance.ShakeCamera(4f, 0f,0.07f);

                        enemy.KillEnemy();
                        break;

                    case Enemy.EnemyTypes.EliteMeteo:

                        isElite = true;
                        StartCoroutine("ContinuousDamage", enemy);

                        break;
                }
                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.transform.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                if(enemy.enemyType == Enemy.EnemyTypes.EliteMeteo)
                {
                    isElite = false;
                    StopCoroutine("ContinuousDamage");
                }
            }
        }
    }

    private IEnumerator ContinuousDamage(Enemy enemy)
    {
        if (enemy != null && isElite)
        {
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                enemy.KillEnemy();
            }
        }
        
    }

}
