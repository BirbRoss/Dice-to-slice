using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class musicHaunter : MonoBehaviour
{
    int curScene;

    private void Awake()
    {
        Object.DontDestroyOnLoad(gameObject);
        curScene = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (curScene != SceneManager.GetActiveScene().buildIndex)
        {
            Transform cam = FindObjectOfType<Camera>().transform;
            transform.position = cam.position;
        }
    }
}
