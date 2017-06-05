using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PvsP : MonoBehaviour {
    public GameObject[] Key;
    private int i = 0;
    private int n = 0;
    private int flag = 0;
    public bool isDown;
    public bool isUp;
    // Use this for initialization
    void Start () {
        for(i = 0;i<Key.Length;i++)
        {
            Key[i].SetActive(false);
        }
        Key[0].SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            isDown = true;
        }
        if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            isDown = false;
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            isUp = true;
        }
        if(Input.GetKeyUp(KeyCode.UpArrow))
        {
            isUp = false;
        }

        if(isDown == true)
        {
            if(n != Key.Length - 1)
            {
                Key[n].SetActive(false);
                Key[n + 1].SetActive(true);
                n++;
                flag++;
                isDown = false;
            }
            else if (n == Key.Length - 1)
            {
                Key[n].SetActive(true);
                isDown = false;
            }
        }
        if(isUp == true)
        {
            if(n != 0)
            {
                Key[n].SetActive(false);
                Key[n - 1].SetActive(true);                
                n--;
                flag--;
                isUp = false;
            }
            else if (n == 0)
            {
                Key[n].SetActive(true);
                isUp = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(flag == 0)
            {
                SceneManager.LoadScene("play");
            }
            if(flag == 1)
            {
                SceneManager.LoadScene("Computer");
            }
            if(flag == 2)
            {
                SceneManager.LoadScene("ComputerFirst");
            }
            if(flag == 3)
            {
                Application.Quit();
            }
        }
    }

    
}
