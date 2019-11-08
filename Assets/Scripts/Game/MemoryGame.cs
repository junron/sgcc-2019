using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class MemoryGame : MonoBehaviour
{
  [SerializeField] private GameObject cardPrefab;
  [SerializeField] private Text gameText;
  private static int cardNo = 8;
  private Card[] cards = new Card[cardNo];
  public static int[] cardsChosen = {-1, -1};

  private int pScore, AIScore;//player's/AI's scores
  private bool pTurn; //true if player's turn, false if AI's turn
  private bool end;
  // Start is called before the first frame update
  void Start()
  {
    gameText.text = "Click on a card to choose it";
    InitializeCards();
    pScore = 0;
    AIScore = 0;
    pTurn = true;
    end = false;
  }

  // Update is called once per frame
  void Update()
  { 
    if (end)
    {
      Interstitials.Initiate("Park");
      return;
    }


    //if (!pTurn)
    //{
    //  AITurn();
    //}


    if (cardsChosen[0] != -1 && cardsChosen[1] != -1)
    {
      Check(); //if 2 cards chosen
      //if (pTurn)
      //{
      //  pTurn = false;
      //}
      //else 
      //{
      //  pTurn = true;
      //}
    }


    if (!pTurn)
    {
      gameText.text = "Friend - " + AIScore + "\nYou - " + pScore; //why doesnt this show at the end
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
    //print("[" + cards[0].type + ", " + cards[1].type + ", " + cards[2].type + ", " + cards[3].type 
    //      + ", " + cards[4].type + ", " + cards[5].type + ", " + cards[6].type + ", " + cards[7].type + "]");
  }

  private void Check()
  {
    
    if (cardsChosen[0] == -1 || cardsChosen[1] == -1)
    {
      return;
    }
    float waitTime = 1.0f;
    Card card1 = cards[cardsChosen[0]], card2 = cards[cardsChosen[1]];
    if (card1.type == card2.type)
    {
      //correct!
      Invoke("CorrectPair", waitTime);
      
    }
    else
    {
      Invoke("IncorrectPair", waitTime);
      
    }
    
  }

  void IncorrectPair()
  { //reactions for incorrect pairing
    if (cardsChosen[0] == -1 || cardsChosen[1] == -1)
    {//exit elegantly
      return;
    }
    if (pTurn)
    {
      gameText.text = "Oops! These cards are not matching. Try again!";
    }
    Card card1 = cards[cardsChosen[0]], card2 = cards[cardsChosen[1]];
    cardsChosen[0] = -1;
    cardsChosen[1] = -1;
    card1.chosenSlot = -1;
    card2.chosenSlot = -1;
    card1.SetColor();
    card2.SetColor();
  }

  void CorrectPair()
  { //reactions for correct pairing
    if (cardsChosen[0] == -1 || cardsChosen[1] == -1)
    {//exit elegantly
      return;
    }
    if (pTurn)
    { 
      if (pScore == 0)
      {
        gameText.text = "Well done! You found one matching pair!";
      }
      else
      {
        gameText.text = "Well done! You found another matching pair!";
      }
      pScore++;  
    }
    else
    {
      AIScore++;
    }
    Card card1 = cards[cardsChosen[0]], card2 = cards[cardsChosen[1]];
    card1.deactivated = true;
    card1.gameObject.SetActive(false);
    card2.deactivated = true;
    card2.gameObject.SetActive(false);
    cardsChosen[0] = -1;
    cardsChosen[1] = -1;
    if(pScore + AIScore == cardNo/2)
    {
      end = true;
      gameText.text = "Well done! You collected " + Mathf.Clamp(pScore,1,3) + " gifts from the level! Do you want to repeat it?";
    }
  }
  void Nothing()
  {
    //dummy for pauses
  }

  private void AITurn()
  {//AI doing AI things
    int choice1 = AIChoice();
    cards[choice1].ChooseCard();
    Invoke("Nothing", 1.0f);
    int choice2 = AIChoice();
    cards[choice2].ChooseCard();
    Invoke("Nothing", 1.0f);
  }

  private int AIChoice()
  {
    int choice = Random.Range(0,8);
    while (cards[choice].deactivated || //if removed from game
        cardsChosen[0] == choice || cardsChosen[1] == choice) //if already chosen
    {
      choice = Random.Range(0,8);
    }

    return choice; 
  }

}