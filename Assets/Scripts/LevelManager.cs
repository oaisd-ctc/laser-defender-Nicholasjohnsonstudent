using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float delay = 0f;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    public void LoadGame()
    {
        Debug.Log("heeeeeeeeee");
        scoreKeeper.ResetScore();
        Debug.Log("hiiiiiiiiii");
        SceneManager.LoadScene(1);
    }
    public void LoadMainMenu()
    {
        // StartCoroutine(WaitAndLoad(0, delay));
        SceneManager.LoadScene(0);
    }
    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad(2, delay));
    }
    public void QuitGame()
    {
        Debug.Log("Quiting");
        Application.Quit();
    }
    IEnumerator WaitAndLoad(int sceneIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndex);
    }
}
