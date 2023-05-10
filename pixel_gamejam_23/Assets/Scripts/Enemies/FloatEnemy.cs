using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatEnemy : MonoBehaviour
{
    public float speed;
    public bool isDead = false;
    [Header("Effects")]
    [SerializeField] GameObject deathFx;
    [SerializeField] Sound[] deathSfx;

    Transform player;
    Rigidbody2D rb;

    void Start() {
        player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (!player) {
            rb.gravityScale = 1;
            return;
        }
        RotateTowardPlayer();

        if (isDead) {            
            Dead();
        }
    }

    void FixedUpdate() {
        if (!player) {
            rb.gravityScale = 1;
            return;
        }
        FollowPlayer();
    }

    void FollowPlayer() {
        Vector2 newPos = Vector2.MoveTowards(rb.position, player.position, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }

    void RotateTowardPlayer() {        
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,angle);
    }

    void Dead() {
        FindObjectOfType<Score>().score++;
        FindObjectOfType<HitStop>().Stop(0.0f,0.02f);

        AudioManager.Instance.PlaySFX(deathSfx[Random.Range(0,deathSfx.Length)]);
        Instantiate(deathFx,transform.position,Quaternion.identity);
        
        // ENEMY TYPES
        #region 
        var explosion = GetComponent<Enemy_Explode>();
        if (explosion) explosion.Explosion();

        var ff = GetComponent<Enemy_Force_Field>();
        if (ff) ff.CreateForceField();
        #endregion

        Destroy(gameObject);
    }
}
