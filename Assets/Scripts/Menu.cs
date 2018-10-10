using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        StartCoroutine(WaitToQuit());
    }

    private IEnumerator WaitToQuit() {
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }
}
