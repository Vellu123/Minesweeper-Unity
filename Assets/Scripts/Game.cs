using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    public static int tableWidth = 9;
    public static int tableHeight = 9;
    
    public static Cell[,] table = new Cell[tableHeight, tableWidth];
    public Cell cell;

    public int mineCount = 10;

    // The game is won when gameProgress goes to 0.
    // GameProgress is the amount of unopened cells - mineCount
    public int gameProgress;
    
    // Start is called before the first frame update
    void Start()
    {
        populateTable();
        setMines();
        setNumbers();
    }

    void Update()
    {
    }

    void populateTable()
    {
        for (int y = 0; y < tableHeight; y++)
        {
            for (int x = 0; x < tableWidth; x++)
            {
                Cell new_cell = Instantiate(cell, new Vector3(x * 2.0f, y * 2.0f, 0), Quaternion.identity);
                table[y, x] = new_cell;
                table[y, x].minePos = (x, y);
            }
        }
    }

    void setMines()
    {
        int i = 0;
        while (i < mineCount)
        {
            int x = Random.Range(0, tableWidth);
            int y = Random.Range(0, tableHeight);
            Cell cellToMine = table[y, x].GetComponent<Cell>();
            if (!cellToMine.isMine)
            {
                cellToMine.isMine = true;
                i++;
            }
        }
    }

    void setNumbers()
    {
        for (int y = 0; y < tableHeight; y++)
        {
            for (int x = 0; x < tableWidth; x++)
            {
                if (!table[y, x].isMine)
                {
                    check_neighbouring_cells(x, y);
                }
                
            }
        }
    }

    void check_neighbouring_cells(int x, int y)
    {
        for (int j = y - 1; j < y + 2; j++)
        {
            for (int i = x - 1; i < x + 2; i++)
            {
                if ((i == x && j == y) ||
                     !(0 <= j && j < tableHeight) ||
                     !(0 <= i && i < tableWidth))
                {
                    continue;
                }
                if (table[j, i].isMine)
                {
                    table[y, x].neighbouringMines++;
                }
            }
        }
    }

    public void openEmptyArea((int X, int Y) minePos)
    {
        int x = minePos.X;
        int y = minePos.Y;
        for (int j = y - 1; j < y + 2; j++)
        {
            for (int i = x - 1; i < x + 2; i++)
            {

                if ((i == x && j == y) ||
                     !(0 <= j && j < tableHeight) ||
                     !(0 <= i && i < tableWidth))
                {
                    continue;
                }

                Cell cellToOpen = table[j, i].GetComponent<Cell>();
                if (!cellToOpen.isMine && !cellToOpen.isOpened)
                {
                    cellToOpen.openCell();
                }
            }
        }
    }

    public void updateScore()
    {
        int temp = 0;
        for (int j = 0; j < tableHeight; j++)
        {
            for (int i = 0; i < tableWidth; i++)
            {
                if (table[j, i].isOpened && !table[j, i].isMine)
                {
                    temp ++;
                }
            }
        }
        gameProgress = temp;
        Debug.Log(gameProgress);
        if (gameProgress == tableHeight * tableWidth - mineCount)
        {
            Debug.Log("GAME WON");
        }
        
    }
}
