using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    [SerializeField] private Text t;
    private Action callback;
    bool playerGetCoin;

     void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject==player)  playerGetCoin = true;
        }
    // Update is called once per frame
    void Update()
    {
        if(playerGetCoin){
            string text = t.text;
            text = text.Replace("Coins: ","");
            Debug.Log(text);
            int coins = Int32.Parse(text);
            t.text = "Coins: "+(coins+1);
            if(this.callback!=null){
                callback();
            }
            Destroy(gameObject);
        }
    }
    public void setCallback(Action callback){
        this.callback = callback;
    }
}
