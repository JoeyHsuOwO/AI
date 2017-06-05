using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class moveChoice : MonoBehaviour
{

    public GameObject[] Key;
    public GameObject p1Screen;
    public GameObject p2Screen;
    public GameObject p3Screen;
    public bool w1;
    public bool w2;
    public bool w3;
    private int[,] arr;
    private int num = 9;
    private int winNum = 4;
    private int arrWidth;
    private int arrHeight;
    public bool Isleft = false;
    public bool Isright = false;
    private int i = 0;
    private int j = 0;
    private int n = 0;
    private int m = 0;
    private int count = 0;
    private int totalStep = 0;
    private float timeCount = 0;
    public bool pushSpace = false;
    public bool P1;
    public bool P2;
    public GameObject P1key;
    public GameObject P2key;
    public GameObject[] collectedPieceR;
    public GameObject[] collectedPieceB;
    public Transform spawnR;
    public Transform spawnB;
    public GameObject createPieceR;
    public GameObject createPieceB;
    // Use this for initialization
    void Start()
    {
        p1Screen.SetActive(false);
        p2Screen.SetActive(false);
        p3Screen.SetActive(false);
        arrWidth = num;
        arrHeight = num + 1;
        arr = new int[arrWidth, arrHeight];
        for (i = 0; i < arrWidth; i++)
        {
            for (j = 0; j < arrHeight; j++)
            {
                arr[i, j] = 0;
            }
        }
        P1 = true;
        P1key.SetActive(true);
        P2key.SetActive(false);
        for (i = 0; i < collectedPieceR.Length; i++)
        {

        }

        for (i = 0; i < Key.Length; i++)
        {
            Key[i].SetActive(false);
        }
        Key[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        if(w1 == true)
        {
           
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("main");
                w1 = false;
            }
        }
        if (w2 == true)
        {
          
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("main");
                w2 = false;
            }
        }
        if (w3 == true)
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("main");
                w3 = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Isleft = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            Isleft = false;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Isright = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            Isright = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pushSpace = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            pushSpace = false;
        }

        if (P1 == true)
        {
            P1key.SetActive(true);
            P2key.SetActive(false);
        }
        if (P2 == true)
        {
            P2key.SetActive(true);
            P1key.SetActive(false);
        }

        if (Isleft == true)
        {
            if (n == 0)
            {
                Key[n].SetActive(true);
                Isleft = false;
            }
            else if (n != 0)
            {
                Key[n].SetActive(false);
                Key[n - 1].SetActive(true);
                spawnR.transform.position = Key[n].transform.position;
                spawnB.transform.position = Key[n].transform.position;
                n--;
                m = 0;
                Isleft = false;
            }
        }
        if (Isright == true)
        {
            if (n != Key.Length - 1)
            {
                Key[n].SetActive(false);
                Key[n + 1].SetActive(true);
                n++;
                m = 0;
                spawnB.transform.position = Key[n].transform.position;
                spawnR.transform.position = Key[n].transform.position;
                Isright = false;
            }
            else if (n == Key.Length - 1)
            {
                Key[n].SetActive(true);
                Isright = false;
            }
        }
        if (P1 == true)
        {
            timeCount += Time.deltaTime;
            if (timeCount <= 20)
            {

                if (pushSpace == true)
                {
                    if (arr[n, m] == 0 && m < 9)
                    {
                        GameObject pieceCloneR = Instantiate(createPieceR, spawnR.position, Quaternion.identity) as GameObject;
                        pieceCloneR.transform.parent = collectedPieceR[n].transform;
                        pieceCloneR.transform.localPosition = Vector3.zero;
                        pushSpace = false;
                        P1 = false;
                        P2 = true;
                        arr[n, m] = 1;
                        bool isGameover = Win(arr, n, m);
                        timeCount = 0;

                    }
                    else if (arr[n, m] == 1 || arr[n, m] == -1 && m < 9)
                    {
                        m++;
                    }
                    else if (m >= 9)
                    {
                        Debug.Log("不能再放了");
                    }
                }
            }
            else
            {
                P1 = false;
                P2 = true;
                timeCount = 0;
            }

        }
        if (P2 == true)
        {
            timeCount += Time.deltaTime;
            if (timeCount <= 20)
            {

                if (pushSpace == true)
                {
                    if (arr[n, m] == 0 && m < 9)
                    {
                        GameObject pieceCloneB = Instantiate(createPieceB, spawnB.position, Quaternion.identity) as GameObject;
                        pieceCloneB.transform.parent = collectedPieceB[n].transform;
                        pieceCloneB.transform.localPosition = Vector3.zero;
                        pushSpace = false;
                        P1 = true;
                        P2 = false;
                        arr[n, m] = -1;
                        bool isGameover = Win(arr, n, m);
                        timeCount = 0;

                    }
                    else if (arr[n, m] == 1 || arr[n, m] == -1 && m < 9)
                    {
                        m++;
                    }
                    else if (m >= 9)
                    {
                        Debug.Log("不能再放了");
                    }
                }
            }
            else
            {
                P1 = true;
                P2 = false;
                timeCount = 0;
            }

        }


    }

    bool Win(int[,] _Matrix, int _row, int _column)
    {
        int total = 0;
        int x, y;
        for( x = _row; x< num-1 ;x++)//檢查列
        {
            if (_Matrix[x, _column] == _Matrix[x + 1, _column])
            {
                total = total + _Matrix[_row, _column];
            }
            else break;
        }
        for( x = _row ; x > 0; x--)
        {
            if (_Matrix[x, _column] == _Matrix[x - 1, _column])
            {
                total = total + _Matrix[_row, _column];
            }
            else break;
        }
            if (total > 3)
            {
                Winner(2);
                return true;
            }
            else if (total == 3)
            {
                Winner(1);
                return true;
            }
            else if (total < -3)
            {
                Winner(1);
                return true;
            }
            else if (total == -3)
            {
                Winner(2);
                return true;
            }


        total = 0;

        for(y = _column; y > 0; y--)//檢查行
        {
            if(_Matrix[_row,y] == _Matrix[_row,y - 1])
            {
                total = total + _Matrix[_row, _column];
            }
            else break;
        }
            if (total >= 4)
            {
                Winner(2);
                return true;
            }
            else if (total == 3)
            {
                Winner(1);
                return true;
            }
            else if (total <= -4)
            {
                Winner(1);
                return true;
            }
            else if (total == -3)
            {
                Winner(2);
                return true;
            }

        total = 0;
        for (x = _row, y = _column; x < num - 1 && y < num - 1; x++, y++)//檢查右斜線
        {
            if (_Matrix[x, y] == _Matrix[x + 1, y + 1])
            {
                total = total + _Matrix[_row, _column];
            }
            else break;
        }
        for (x = _row, y = _column; x > 0 && y > 0; x--, y--)
        {
            if (_Matrix[x, y] == _Matrix[x - 1, y - 1])
            {
                total = total + _Matrix[_row, _column];
            }
            else break;
        }
            if (total >= 4)
            {
                Winner(2);
                return true;
            }
            else if (total == 3)
            {
                Winner(1);
                return true;
            }
            else if (total <= -4)
            {
                Winner(1);
                return true;
            }
            else if (total == -3)
            {
                Winner(2);
                return true;
            }
        

        total = 0;
        for (x = _row, y = _column; x < num - 1 && y > 0; x++, y--)//檢查左斜線
        {
            if (_Matrix[x, y] == _Matrix[x + 1, y - 1])
            {
                total = total + _Matrix[_row, _column];
            }
            else break;
        }
        for(x = _row,y = _column; x > 0 && y < num - 1;x-- , y++)
        {
            if (_Matrix[x, y] == _Matrix[x - 1, y + 1])
            {
                total = total + _Matrix[_row, _column];
            }
            else break;
        }
             
            if (total >= 4)
            {
                Winner(2);
                return true;
            }
            else if (total == 3)
            {
                Winner(1);
                return true;
            }
            else if (total <= -4)
            {
                Winner(1);
                return true;
            }
            else if (total == -3)
            {
                Winner(2);
                return true;
            }

        totalStep++;
        if (totalStep == arrWidth * arrHeight - 1)
        {
            Winner(3);
            return true;
        }
        return false;
    }
    
    void Winner(int player)
    {
        if (player == 1)
        {
            p1Screen.SetActive(true);
            w1 = true;
        }
        else if (player == 2)
        {
            p2Screen.SetActive(true);
            w2 = true;
        }
        else
        {
            p3Screen.SetActive(true);
            w3 = true;
        }
    }

    
}

