using System;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
  // Start is called before the first frame update
  public GameObject player;
  [SerializeField] private HealthBar coinIndicator;
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

    coinIndicator.barValue++;
    callback?.Invoke();
    Destroy(this.gameObject);
  }

  public void SetCallback(Action callback)
  {
    this.callback = callback;
  }
}