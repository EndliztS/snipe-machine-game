using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Sniper : MonoBehaviour
{
    [Header("Stats")]
    public float shootRate;
    public int damage;

    [Header("References")]
    [SerializeField] Transform shootPoint;

    [Header("Effects")]
    [SerializeField] TrailRenderer bulletTracer;
    [SerializeField] ParticleSystem shootFx;
    [SerializeField] GameObject hitFx;

    [Header("Audios")]
    [SerializeField] Sound shootSfx;

    [Header("Cameras Shake")]
    [SerializeField] float magnitude;
    [SerializeField] float roughness;
    [SerializeField] float fadeIn;
    [SerializeField] float fadeOut;

    private bool readyToShoot = true;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0) && readyToShoot) Shoot();
    }

    void Shoot() {
        readyToShoot = false;
            
        shootFx.Play();
        CameraShaker.Instance.ShakeOnce(magnitude,roughness,fadeIn,fadeOut);
        AudioManager.Instance.PlaySFX(shootSfx);

        RaycastHit2D hit = Physics2D.Raycast(shootPoint.position,shootPoint.right);        
        if (hit) {
            var trail = Instantiate(bulletTracer,shootPoint.position,Quaternion.identity);
            trail.AddPosition(shootPoint.position);
            trail.transform.position = hit.point;

            Instantiate(hitFx,hit.point,Quaternion.LookRotation(hit.normal));

            var enemy = hit.collider.gameObject;
            if (enemy.CompareTag("Enemy")) {
                if (enemy.GetComponent<FloatEnemy>()) enemy.GetComponent<FloatEnemy>().isDead=true;
            }
        }

        Invoke("ResetShoot",shootRate);
    }

    void ResetShoot() {
        readyToShoot = true;
    }
}
