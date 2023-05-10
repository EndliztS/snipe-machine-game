using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessingToggle : MonoBehaviour
{
    [SerializeField] GameObject pp;

    void Start() {
            int value = PlayerPrefs.GetInt("postProcessing");
            if (value==0) pp.SetActive(false);
            else pp.SetActive(true);
    }
}
