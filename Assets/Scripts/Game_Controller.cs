
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Game_Controller : MonoBehaviour
{
    [Header("Endscreens")]
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject endScreenText;

    void Awake() {
        Time.timeScale = 1;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    public void YouLose() {
        StartCoroutine(ScreenLoader(loseScreen));

    }

    public void YouWin() {
        StartCoroutine(ScreenLoader(winScreen));
    }

    IEnumerator ScreenLoader(GameObject endScreen) {
        Image endScreenImage = endScreen.GetComponent<Image>();
        Color ScreenColor = endScreenImage.color;
        for (float alpha = 0f; alpha <= 1.1; alpha += .05f) {
            ScreenColor.a = alpha;
            endScreenImage.color = ScreenColor;
            yield return new WaitForSecondsRealtime(.05f);
        }
        if (endScreen == winScreen) {
            endScreenText.GetComponent<TMP_Text>().SetText("Completion Time: " + Time.realtimeSinceStartup.ToString("F2") + " Seconds");
            yield return new WaitForSecondsRealtime(10);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else {
            endScreenText.GetComponent<TMP_Text>().SetText("You Died");
            yield return new WaitForSecondsRealtime(3);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
