using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    Animator anims;
    bool canRestart = false;

    void Start() {
        anims = GetComponent<Animator>();
        anims.enabled = false;
    }

    void Update()
    {
        if (!GameObject.FindWithTag("Player")) {
            Invoke("StartAnims",3f);
        }
        if (canRestart) {            
            if (Input.GetKeyDown(KeyCode.R)){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (Input.GetKeyDown(KeyCode.Escape)) {
                SceneManager.LoadScene(0);
            }
        }
    }

    void StartAnims() {
        anims.enabled = true;
        canRestart = true;
    }
}
