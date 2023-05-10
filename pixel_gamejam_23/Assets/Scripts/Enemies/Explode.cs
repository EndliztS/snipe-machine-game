using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float delay;
    public float radius;
    [SerializeField] GameObject explodeFx;

    void Start() {
        Invoke("StartExplode",delay);
    }

    void StartExplode() {
        Instantiate(explodeFx,transform.position,Quaternion.identity);
        Collider2D[] entities = Physics2D.OverlapCircleAll(transform.position,radius);

        foreach (Collider2D i in entities) {
            var enemy = i.GetComponent<FloatEnemy>();
            var player = i.GetComponentInParent<PlayerController>();

            if (enemy) enemy.isDead = true;
            if (player) {
                if (player.isInvincible) continue;
                player.Dead();
            }
        }

        Destroy(gameObject);
    }
}
