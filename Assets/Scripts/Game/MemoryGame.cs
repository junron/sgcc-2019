using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MemoryGame : MonoBehaviour
{
  [SerializeField] private GameObject cardPrefab;
  private static int cardNo = 8;
  private Card[] cards = new Card[cardNo];
  public static int[] cardsChosen = new int[2];


  // Start is called before the first frame update
  void Start()
  {
    InitializeCards();
  }

  // Update is called once per frame
  void Update()
  {
    if (cardsChosen[0] != -1 && cardsChosen[1] != -1)
    {
      Check(); //if 2 cards chosen
    }
  }

  //color code and pair the cards
  private void InitializeCards()
  {
    for (int i = 0; i < cardNo; i++)
    {
      GameObject card = Instantiate(cardPrefab);
      cards[i] = card.GetComponent<Card>();
      Assert.IsNotNull(cards[i]);
    }
  }

  private void Check()
  {
    if (cardsChosen[0] == -1 || cardsChosen[1] == -1)
    {
      return;
    }

    Card card1 = cards[cardsChosen[0]], card2 = cards[cardsChosen[1]];
    if (card1.type == card2.type)
    {
      //correct!
      card1.deactivated = true;
      card1.gameObject.SetActive(false);
    }
    else
    {
      cardsChosen[0] = -1;
      cardsChosen[1] = -1;
      card1.flip();
      card2.flip();
    }
  }
}