using System;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
  // Start is called before the first frame update
  public GameObject player;
  [SerializeField] private Text t;
  private Action callback;
  bool playerGetCoin;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject == player) playerGetCoin = true;
  }

  // Update is called once per frame
  private void Update()
  {
    if (!playerGetCoin) return;

    string text = t.text;
    text = text.Replace("Coins: ", "");
    Debug.Log(text);
    int coins = Int32.Parse(text);
    t.text = "Coins: " + (coins + 1);
    callback?.Invoke();
    Destroy(gameObject);
  }

  public void SetCallback(Action callback)
  {
    this.callback = callback;
  }
}