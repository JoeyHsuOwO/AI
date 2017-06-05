using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AI : MonoBehaviour
{

    public GameObject[] Key;
    public GameObject p1Screen;
    public GameObject p2Screen;
    public GameObject p3Screen;
    public bool w1;
    public bool w2;
    public bool w3;
    private int[,] arr;
    private int[,] w;
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
    private int x = 0;
    private int y = 0;
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
    private int minimaxDepth = 6;
    private bool winAI;
    private bool winHuman;
    // Use this for initialization
    void Start()
    {
        p1Screen.SetActive(false);
        p2Screen.SetActive(false);
        p3Screen.SetActive(false);
        arrWidth = num;
        arrHeight = num + 1;
        arr = new int[arrWidth, arrHeight];
        w = new int[arrWidth, arrHeight];
        for (i = 0; i < arrWidth; i++)
        {
            for (j = 0; j < arrHeight; j++)
            {
                arr[i, j] = 0;
                w[i, j] = 0;
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
        if (w1 == true)
        {

            if (Input.GetKeyDown(KeyCode.Space))
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
            Key[n].SetActive(false);
            timeCount += Time.deltaTime;
            if(timeCount >= 2)
            {
                for(n = 0; n < 9;n++)
                {
                    for (m = 0; m < 9; m++)
                    {
                        if(arr[n,m] != 0)
                        {
                            w[n, m] = -1;
                        }
                        else if (arr[n,m] == 0)
                        {
                            w[n, m] = FindWeight(arr, n, m);
                            break;                
                        }
                    }
                }
                PutDown(w);
                timeCount = 0;
            }
        }
    }

    public int FindWeight(int[,] _Matrix , int _row , int _column)
    {
        int x, y;
        
        int flagB = 0;
        int flagR = 0;
        int weight = 0;
        int weightRow = 0;
        int weightColumn = 0;
        int rightSlash = 0;
        int leftSlash = 0;
        int rightrightSlash = 0;
        int leftleftSlash = 0;
        int fourR = 1;
        int fourB = 1;
        int threeR = 200000000;
        int threeB = 30000000;
        int twoR = 90000;
        int twoB = 100000;
        int oneB = 8000;
        int oneR = 700;
        int no = 50;
        
        if(_row < 8)
        {
            if (_Matrix[_row + 1, _column] == 1)
                flagR++;
            else
            {

            }
            if (_Matrix[_row + 1, _column] == -1)
                flagB++;
            else
            {

            }
        }
        if(_row > 0)
        {
            if (_Matrix[_row - 1, _column] == 1)
                flagR++;
            else
            {

            }
            if (_Matrix[_row - 1, _column] == -1)
                flagB++;
            else
            {

            }
        }      
        for (x = _row; x < num - 2; x++)//檢查空格右列
        {
            if (_Matrix[x + 1, _column] == 1 && _Matrix[x + 2  , _column] == 1)
            {
                flagR++;
            }
            else if (_Matrix[x + 1, _column] == -1 && _Matrix[x + 2, _column] == -1 )
            {
                flagB++;
            }
            else
                break;
        }
        for (x = _row; x> 1; x--)//檢查空格左列
        {
            if (_Matrix[x - 1, _column] == 1 && _Matrix[x - 2, _column] == 1)
            {
                flagR++;
            }
            else if (_Matrix[x - 1, _column] == -1 && _Matrix[x - 2, _column] == -1 )
            {
                flagB++;
            }
            else break;
        }
        if (flagR >= 4)
            weight = weight + fourR;
        if (flagB >= 4)
            weight = weight + fourB;        
        if (flagR == 3 && flagB == 0)
            weight = weight + threeR;
        if (flagR == 3 && flagB == 1)
            weight = weight + (threeR + oneB) / 2;
        if (flagR == 3 && flagB == 2)
            weight = weight + (threeR + twoB) / 2;
        if (flagR == 3 && flagB == 3)
            weight = weight + (threeR + threeB) / 2;
        if (flagR == 2 && flagB == 0)
            weight = weight + twoR;
        if (flagR == 2 && flagB == 1)
            weight = weight + (twoR + oneB) / 2;
        if (flagR == 2 && flagB == 2)
            weight = weight + (twoR + twoB) / 2;
        if (flagR == 2 && flagB == 3)
            weight = weight + (twoR + threeB) / 2;
        if (flagR == 1 && flagB == 0)
            weight = weight + oneR;
        if (flagR == 1 && flagB == 1)
            weight = weight + (oneR + oneB) / 2;
        if (flagR == 1 && flagB == 2)
            weight = weight + (oneR + twoB) / 2;
        if (flagR == 1 && flagB == 3)
            weight = weight + (oneR + threeB) / 2;
        if (flagR == 0 && flagB == 0)
            weight = weight + no;
        if (flagR == 0 && flagB == 1)
            weight = weight + oneB;
        if (flagR == 0 && flagB == 2)
            weight = weight + twoB;
        if (flagR == 0 && flagB == 3)
            weight = weight + threeB;

        weightRow = weight;

        flagR = 0;
        flagB = 0;
        weight = 0;

        if(_column > 0)
        {
            if (_Matrix[_row, _column - 1] == 1)
                flagR++;
            else
            {

            }
            if (_Matrix[_row, _column - 1] == -1)
                flagB++;
            else
            {

            }
        }       
        for (y = _column; y > 1; y--)//檢查行
        {
            if (_Matrix[_row, y - 1] == 1 && _Matrix[_row, y - 2] == 1)
            {
                flagR++;
            }
            else if (_Matrix[_row, y - 1] == -1 && _Matrix[_row, y - 2] == -1)
            {
                flagB++;
            }
            else break;
        }
        if (flagR >= 4)
            weight = weight + fourR;
        if (flagB >= 4)
            weight = weight + fourB;
        if (flagR == 3 && flagB == 0)
            weight = weight + threeR;
        if (flagR == 3 && flagB == 1)
            weight = weight + (threeR + oneB) / 2;
        if (flagR == 3 && flagB == 2)
            weight = weight + (threeR + twoB) / 2;
        if (flagR == 3 && flagB == 3)
            weight = weight + (threeR + threeB) / 2;
        if (flagR == 2 && flagB == 0)
            weight = weight + twoR;
        if (flagR == 2 && flagB == 1)
            weight = weight + (twoR + oneB) / 2;
        if (flagR == 2 && flagB == 2)
            weight = weight + (twoR + twoB) / 2;
        if (flagR == 2 && flagB == 3)
            weight = weight + (twoR + threeB) / 2;
        if (flagR == 1 && flagB == 0)
            weight = weight + oneR;
        if (flagR == 1 && flagB == 1)
            weight = weight + (oneR + oneB) / 2;
        if (flagR == 1 && flagB == 2)
            weight = weight + (oneR + twoB) / 2;
        if (flagR == 1 && flagB == 3)
            weight = weight + (oneR + threeB) / 2;
        if (flagR == 0 && flagB == 0)
            weight = weight + no;
        if (flagR == 0 && flagB == 1)
            weight = weight + oneB;
        if (flagR == 0 && flagB == 2)
            weight = weight + twoB;
        if (flagR == 0 && flagB == 3)
            weight = weight + threeB;

        weightColumn = weight;

        flagR = 0;
        flagB = 0;
        weight = 0;

        if(_row < 8 && _column < 8)
        {
            if (_Matrix[_row + 1, _column + 1] == 1)
                flagR++;
            else
            {

            }
            if (_Matrix[_row + 1, _column + 1] == -1)
                flagB++;
            else
            {

            }
        }  
        if(_row > 0 && _column > 0)
        {
            if (_Matrix[_row - 1, _column - 1] == 1)
                flagR++;
            else
            {

            }
            if (_Matrix[_row - 1, _column - 1] == -1)
                flagB++;
            else
            {

            }
        }      
        for (x = _row, y = _column; x < num - 2 && y < num - 2; x++, y++)//檢查右斜線
        {
            if (_Matrix[x + 1, y + 1] == 1 && _Matrix[x+2,y+2] == 1)
            {
                flagR++;
            }
            else if (_Matrix[x + 1, y + 1] == -1 && _Matrix[x + 2, y + 2] == -1)
            {
                flagB++;
            }
            else break;
        }
        for (x = _row, y = _column; x > 1 && y > 1; x--, y--)
        {
            if (_Matrix[x - 1, y - 1] == 1 && _Matrix[x - 2, y - 2] == 1)
            {
                flagR++;
            }
            else if (_Matrix[x - 1 , y - 1] == -1 && _Matrix[x - 2, y - 2] == -1)
            {
                flagB++;
            }
            else break;
        }
        if (flagR >= 4)
            weight = weight + fourR;
        if (flagB >= 4)
            weight = weight + fourB;
        if (flagR == 3 && flagB == 0)
            weight = weight + threeR;
        if (flagR == 3 && flagB == 1)
            weight = weight + (threeR + oneB) / 2;
        if (flagR == 3 && flagB == 2)
            weight = weight + (threeR + twoB) / 2;
        if (flagR == 3 && flagB == 3)
            weight = weight + (threeR + threeB) / 2;
        if (flagR == 2 && flagB == 0)
            weight = weight + twoR;
        if (flagR == 2 && flagB == 1)
            weight = weight + (twoR + oneB) / 2;
        if (flagR == 2 && flagB == 2)
            weight = weight + (twoR + twoB) / 2;
        if (flagR == 2 && flagB == 3)
            weight = weight + (twoR + threeB) / 2;
        if (flagR == 1 && flagB == 0)
            weight = weight + oneR;
        if (flagR == 1 && flagB == 1)
            weight = weight + (oneR + oneB) / 2;
        if (flagR == 1 && flagB == 2)
            weight = weight + (oneR + twoB) / 2;
        if (flagR == 1 && flagB == 3)
            weight = weight + (oneR + threeB) / 2;
        if (flagR == 0 && flagB == 0)
            weight = weight + no;
        if (flagR == 0 && flagB == 1)
            weight = weight + oneB;
        if (flagR == 0 && flagB == 2)
            weight = weight + twoB;
        if (flagR == 0 && flagB == 3)
            weight = weight + threeB;

        rightSlash = weight;

        flagR = 0;
        flagB = 0;
        weight = 0;

        if (_row < 8 && _column > 0)
        {
            if (_Matrix[_row + 1, _column - 1] == 1)
                flagR++;
            else
            {

            }
            if (_Matrix[_row + 1, _column - 1] == -1)
                flagB++;
            else
            {

            }
        }
        if (_row > 0 && _column < 8)
        {
            if (_Matrix[_row - 1, _column + 1] == 1)
                flagR++;
            else
            {

            }
            if (_Matrix[_row - 1, _column + 1] == -1)
                flagB++;
            else
            {

            }
        }
        for (x = _row, y = _column; x < num - 2 && y > 1; x++, y--)//檢查左斜線
        {
            if (_Matrix[x + 1, y - 1] == 1 && _Matrix[x + 2, y - 2] == 1)
            {
                flagR++;
            }
            else if (_Matrix[x + 1, y - 1] == -1 && _Matrix[x + 2, y - 2] == -1)
            {
                flagB++;
            }
            else break;
        }
        for (x = _row, y = _column; x > 1 && y < num - 2; x--, y++)
        {
            if (_Matrix[x - 1, y + 1] == 1 && _Matrix[x - 2, y + 2] == 1)
            {
                flagR++;
            }
            else if (_Matrix[x - 1, y + 1] == -1 && _Matrix[x - 2, y + 2] == -1)
            {
                flagB++;
            }
            else break;
        }

        if (flagR >= 4)
            weight = weight + fourR;
        if (flagB >= 4)
            weight = weight + fourB;
        if (flagR == 3 && flagB == 0)
            weight = weight + threeR;
        if (flagR == 3 && flagB == 1)
            weight = weight + (threeR + oneB) / 2;
        if (flagR == 3 && flagB == 2)
            weight = weight + (threeR + twoB) / 2;
        if (flagR == 3 && flagB == 3)
            weight = weight + (threeR + threeB) / 2;
        if (flagR == 2 && flagB == 0)
            weight = weight + twoR;
        if (flagR == 2 && flagB == 1)
            weight = weight + (twoR + oneB) / 2;
        if (flagR == 2 && flagB == 2)
            weight = weight + (twoR + twoB) / 2;
        if (flagR == 2 && flagB == 3)
            weight = weight + (twoR + threeB) / 2;
        if (flagR == 1 && flagB == 0)
            weight = weight + oneR;
        if (flagR == 1 && flagB == 1)
            weight = weight + (oneR + oneB) / 2;
        if (flagR == 1 && flagB == 2)
            weight = weight + (oneR + twoB) / 2;
        if (flagR == 1 && flagB == 3)
            weight = weight + (oneR + threeB) / 2;
        if (flagR == 0 && flagB == 0)
            weight = weight + no;
        if (flagR == 0 && flagB == 1)
            weight = weight + oneB;
        if (flagR == 0 && flagB == 2)
            weight = weight + twoB;
        if (flagR == 0 && flagB == 3)
            weight = weight + threeB;

        leftSlash = weight;
        weight = 0;

        flagR = 0;
        flagB = 0;
        weight = 0;
        if (_row < 8)
        {
            if (_Matrix[x + 1, y] == 1)
            {
                flagR++;
            }
            else
            {

            }
            if (_Matrix[x + 1, y] == -1)
            {
                flagB++;
            }
            else
            {

            }
        }
            
        for (x = _row, y = _column; x < num - 1 && y > 1; x++, y--)//檢查右邊的右斜線
        {
            if (_Matrix[x+1, y] == 1 && _Matrix[x+2, y-1] == 1)
            {
                flagR++;
            }
            else if (_Matrix[x , y] == -1 && _Matrix[x + 1, y - 1] == -1)
            {
                flagB++;
            }
            else break;
        }
        if (flagR == 3 && flagB == 0)
            weight = 0;
        if (flagR == 2 && flagB == 0)
            weight = weight + twoR / 2;
        if (flagR == 1 && flagB == 0)
            weight = weight + oneR;
        if (flagR == 0 && flagB == 0)
            weight = weight + no;
        if (flagR == 0 && flagB == 1)
            weight = weight + oneB;
        if (flagR == 0 && flagB == 2)
            weight = weight + twoB;
        if (flagR == 0 && flagB == 3)
            weight = weight + threeB;

        rightrightSlash = weight;

        flagR = 0;
        flagB = 0;
        weight = 0;
        if(_row > 0)
        {
            if (_Matrix[x - 1, y] == 1)
            {
                flagR++;
            }
            else
            {

            }
            if (_Matrix[x - 1, y] == -1)
            {
                flagB++;
            }
            else
            {

            }
        }        
        for (x = _row, y = _column; x > 1 && y > 1; x--, y--)//檢查左邊的左斜線
        {
            if (_Matrix[x - 1, y] == 1 && _Matrix[x - 2, y - 1] == 1)
            {
                flagR++;
            }
            else if (_Matrix[x - 1, y] == -1 && _Matrix[x - 2, y - 1] == -1)
            {
                flagB++;
            }
            else break;
        }
        if (flagR == 3 && flagB == 0)
            weight = 0;
        if (flagR == 2 && flagB == 0)
            weight = weight + twoR / 2;
        if (flagR == 1 && flagB == 0)
            weight = weight + oneR;
        if (flagR == 0 && flagB == 0)
            weight = weight + no;
        if (flagR == 0 && flagB == 1)
            weight = weight + oneB;
        if (flagR == 0 && flagB == 2)
            weight = weight + twoB;
        if (flagR == 0 && flagB == 3)
            weight = weight + threeB;
        leftleftSlash = weight;

        weight = weightRow;
        if(rightrightSlash == 0 || leftleftSlash == 0)
        {
            weight = 0;
        }
        else
        {
            if (weightColumn > weight)
                weight = weightColumn;
            if (leftSlash > weight)
                weight = leftSlash;
            if (rightSlash > weight)
                weight = rightSlash;
            if (rightrightSlash > weight)
                weight = rightrightSlash;
            if (leftleftSlash > weight)
                weight = leftleftSlash;
        }
        Debug.Log(weight);
        return weight;
    }

    public void PutDown(int[,] w)
    {
        int max = 0;
        
        for (int a = 0; a < num; a++)
        {
            for (int b = 0; b < num; b++)
            {
                if (w[a, b] > max)
                {
                    x = a;
                    y = b;
                    max = w[a, b];
                }
            }
        }
       
        n = x;
        Key[n].SetActive(true);
        m = y;
        pushSpace = true;
        if(pushSpace == true)
        {                       
                GameObject pieceCloneB = Instantiate(createPieceB, spawnB.position, Quaternion.identity) as GameObject;
                pieceCloneB.transform.parent = collectedPieceB[n].transform;
                pieceCloneB.transform.localPosition = Vector3.zero;
                arr[n, m] = -1;
                pushSpace = false;
                bool isGameover = Win(arr, n, m);
                P2 = false;
                P1 = true;
        }       

    }

    bool Win(int[,] _Matrix, int _row, int _column)
    {
        int total = 0;
        int x, y;
        for (x = _row; x < num - 1; x++)//檢查列
        {
            if (_Matrix[x, _column] == _Matrix[x + 1, _column])
            {
                total = total + _Matrix[_row, _column];
            }
            else break;
        }
        for (x = _row; x > 0; x--)
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

        for (y = _column; y > 0; y--)//檢查行
        {
            if (_Matrix[_row, y] == _Matrix[_row, y - 1])
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
        for (x = _row, y = _column; x > 0 && y < num - 1; x--, y++)
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
