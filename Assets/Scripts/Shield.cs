using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                Rigidbody2D enemyRB = enemy.GetComponent<Rigidbody2D>();

                enemyRB.velocity = Vector2.zero;
                enemyRB.angularVelocity = 0;

                enemyRB.AddForce(new Vector2(0, 700), ForceMode2D.Impulse);

                CinemachineShake.Instance.ShakeCamera(4f, 0f ,0.07f);
            }
        }
        
    }
}
