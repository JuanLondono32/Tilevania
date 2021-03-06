using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] float levelSlowFactor = 0.2f;

    IEnumerator Exit()
    {
        Time.timeScale = levelSlowFactor;
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        Time.timeScale = 1;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(Exit());
    }

}
