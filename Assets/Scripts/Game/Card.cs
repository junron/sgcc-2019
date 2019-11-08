using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
  public int type;
  private static int CARD_INDEX;
  private static float xK = 0.3f, yK = 0.3f; //stands for x constant and y constant
  private float[] x = {xK * -45, xK * -15, xK * 15, xK * 45}, y = {yK * -15, yK * 15};
  private int index;
  public bool deactivated;
  public int chosenSlot = -1;

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
      ChooseCard();
    }
  }


  public void SetColor()
  {
    if (chosenSlot == -1){
      GetComponent<SpriteRenderer>().color = Color.black;
      return;
    }
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

  public void ChooseCard()
  {
    if (chosenSlot != -1)
    {
      //print("hmm");
      //if already chosen
      MemoryGame.cardsChosen[chosenSlot] = -1; //remove from chosen slot
      chosenSlot = -1;
    }
    else if (chosenSlot == -1)
    {
      //print("hmm3");
      //if not chosen
      if (MemoryGame.cardsChosen[0] == -1)
      {
        //if slot0 not chosen
        //print("slot0 taken");
        MemoryGame.cardsChosen[0] = index;
        chosenSlot = 0;
      }
      else if (MemoryGame.cardsChosen[1] == -1)
      {
        // if slot1 not chosen
        //print("slot1 taken");
        MemoryGame.cardsChosen[1] = index;
        chosenSlot = 1;
      }
    }
    //print("[" + MemoryGame.cardsChosen[0] + ", " + MemoryGame.cardsChosen[1] + "]");
    SetColor();
  }
}