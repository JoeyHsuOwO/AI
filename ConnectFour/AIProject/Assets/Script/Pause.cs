using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
    public bool isPaused;
    public GameObject pauseMenu;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR || UNITY_STANDALONE
        MouseInput();   // 滑鼠偵測
        if (isPaused)
        {
            pauseMenu.SetActive(true);

            Time.timeScale = 0f;
        }
        else
        {
            pauseMenu.SetActive(false);

            Time.timeScale = 1f;
        }

    }
    void MouseInput()
    {

        if (Input.GetMouseButtonDown(0))
        {



        }
    }
    public void push()
    {
        isPaused = !isPaused;
    }
    public void Resume()
    {
        isPaused = false;

    }

    public void RestartLevel(string Restart)
    {
        isPaused = false;

        SceneManager.LoadScene(Restart);
    }
    public void BackToHome(string BackHome)
    {
        isPaused = false;

        SceneManager.LoadScene("main");
    }
}
#endif