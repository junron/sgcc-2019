using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGame : MonoBehaviour
{   
    [SerializeField] private GameObject cardPrefab;    
    private GameObject[] cardlst = new GameObject[8];
    public static int[] cardsChosen = new int[2];
    
    

    // Start is called before the first frame update
    void Start()
    {
        cardlst = new GameObject[8];
        InitializeCards();
    }

    // Update is called once per frame
    void Update()
    {
        if (cardsChosen[0] != -1 && cardsChosen[1] != -1){
                    check(); //if 2 cards chosen
                }
    }

    //color code and pair the cards
    private void InitializeCards(){
        for (int i = 0; i < 8; i ++){
            
            GameObject card = Instantiate(cardPrefab);
            cardlst[i] = card;
        }
    }

    public void check(){
        if (cardsChosen[0] == -1 ||cardsChosen[1] == -1){return;}
//        Card card1 = cardlst[cardsChosen[0]], card2 =cardlst[cardsChosen[1]];
//        if(card1.type == card2.type){
           //correct!
//            card1.deactivated = true;
//            card1.card.SetActive(false);
//        }
//        else{
//            cardsChosen[0] = -1; cardsChosen[1] = -1;
//            card1.flip(); card2.flip();
//        }
    }
}
