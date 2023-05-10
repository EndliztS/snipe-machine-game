using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Force_Field : MonoBehaviour
{
    [SerializeField] Force_Field ff;

    public void CreateForceField() {
        var player = GameObject.FindWithTag("Player").transform;
        Transform instance = Instantiate(ff,player.position,Quaternion.identity).transform;
        instance.SetParent(player);
        instance.transform.localPosition = Vector2.zero;

        Destroy(gameObject);
    }
}
