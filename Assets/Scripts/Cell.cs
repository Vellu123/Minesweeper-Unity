using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{

    public bool isMine = false;
    public bool isOpened = false;
    public (int X, int Y) minePos = (-1, -1);
    public int neighbouringMines = 0;
    public Text mineText;
    public SpriteRenderer flagImg;
    
    bool flagState = false;

    public GameObject game;

    void OnMouseOver() {
        if (Input.GetMouseButtonDown(0))
        {
            openCell();
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("MOUSE2");
            if (flagState)
            {
                setFlag(false);
            }
            else if (!flagState)
            {
                setFlag(true);
            }

        }
    }

    public void openCell()
    {
        isOpened = true;
        mineText.text = neighbouringMines.ToString();
        if (neighbouringMines == 0)
        {
            game.GetComponent<Game>().openEmptyArea(minePos);
        }
        if (isMine)
        {
            GetComponent<Renderer>().material.color = new Color(0,0,0);
        }
        game.GetComponent<Game>().updateScore();
    }

    void setFlag(bool state)
    {
        flagImg.gameObject.SetActive(state);
        flagState = state;
    }
}
