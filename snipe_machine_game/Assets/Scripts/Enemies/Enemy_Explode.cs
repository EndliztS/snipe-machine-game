using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Explode : MonoBehaviour
{
    [SerializeField] Explode explosion;

    public void Explosion() {
        Instantiate(explosion,transform.position,Quaternion.identity);
    }
}
