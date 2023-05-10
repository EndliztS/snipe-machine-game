using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float yMin, yMax;
    [SerializeField] float xMin, xMax;
    [SerializeField] Entity[] entities;

    void Start() {
        foreach (Entity i in entities) {
            StartCoroutine(StartSpawn(i));
        }
    }

    IEnumerator StartSpawn(Entity entity) {
        var obj = entity.obj;
        var time = entity.rate;
        var limit = entity.limitRate;
        var dec = entity.decRate;
        yield return new WaitForSeconds(entity.startTime);
        StartCoroutine(Spawn(obj,time,limit,dec));
    }

    IEnumerator Spawn(GameObject obj, float time, float limit, float dec) {
        if (!GameObject.FindWithTag("Player")) yield break;
        var randomX = Random.Range(xMin,xMax);
        var randomY = Random.Range(yMin,yMax);
        Instantiate(obj,new Vector2(randomX,randomY),Quaternion.identity);
        yield return new WaitForSeconds(time);
        if (time>limit) time -= dec;
        StartCoroutine(Spawn(obj,time,limit,dec));
    }
}
