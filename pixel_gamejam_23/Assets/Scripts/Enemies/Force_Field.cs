using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force_Field : MonoBehaviour
{
    public float time;
    PlayerController player;
    [SerializeField] Sound[] audios;

    void Start() {
        Invoke("Dead",time);
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        player.isInvincible = true;

        AudioManager.Instance.PlaySFX(audios[Random.Range(0,audios.Length)]);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Enemy")) {
            var enemyCode = col.GetComponent<FloatEnemy>();
            enemyCode.isDead = true;
        }
    }

    void Dead() {
        player.isInvincible = false;
        Destroy(gameObject);
    }
}
