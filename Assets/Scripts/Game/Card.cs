using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
  public int type;
  private static int CARD_INDEX;
  private static float xK = 0.8f, yK = 0.1f; //stands for x constant and y constant
  private float[] x = {xK * -45, xK * -15, xK * 15, xK * 45}, y = {yK * -45, yK * -15, yK * 15, yK * 45};
  private int index;
  public bool deactivated;
  private int chosenSlot = -1;

  private static int[] types = {0, 0, 1, 1, 2, 2, 3, 3, -1};

  private void SetType()
  {
    int temp = 8;
    while (types[temp] == -1){
      temp = Random.Range(0,8);
    }
    type = types[temp];
    types[temp] = -1;
  }
  void Awake()
  {
    this.transform.position = new Vector2(x[CARD_INDEX % 4], y[CARD_INDEX / 4]);
    SetType();
    SetColor();
    index = CARD_INDEX;
    CARD_INDEX++;
  }

  void OnMouseOver()
  {
    if (deactivated)
    {
      return;
    }

    if (Input.GetMouseButtonDown(0))
    {
      if (chosenSlot != -1)
      {
        //if already chosen
        MemoryGame.cardsChosen[chosenSlot] = -1; //remove from chosen slot
        flip();
      }
      else if (chosenSlot == -1)
      {
        //if not chosen
        if (MemoryGame.cardsChosen[0] == -1)
        {
          //if slot0 not chosen
          MemoryGame.cardsChosen[0] = index;
          chosenSlot = 0;
        }
        else if (MemoryGame.cardsChosen[1] != -1)
        {
          // if slot1 not chosen
          MemoryGame.cardsChosen[1] = index;
          chosenSlot = 1;
        }

        flip();
      }
    }
  }


  public void flip()
  {
    if (chosenSlot != -1)
    {
      chosenSlot = -1;
      GetComponent<SpriteRenderer>().color = Color.black;
    }

    else if (chosenSlot == -1)
    {
      SetColor();
    }
  }

  private void SetColor()
  {
    switch (type)
    {
      case 0:
        GetComponent<SpriteRenderer>().color = Color.white;
        break;
      case 1:
        GetComponent<SpriteRenderer>().color = Color.red;
        break;
      case 2:
        GetComponent<SpriteRenderer>().color = Color.green;
        break;
      case 3:
        GetComponent<SpriteRenderer>().color = Color.blue;
        break;
    }
  }
}