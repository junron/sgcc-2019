using System;
using UnityEngine;
using UnityEngine.UI;

public class FallScript : MonoBehaviour
{
  public Text text;
  public GameObject article;
  public GameObject player;
  private int counter;

  private String[] script =
  {
    "You: Wah, Mr Tay! Long time no see ah!",
    "Mr Tay: Yeah! Today I got something ask you.",
    "Mr Tay: After I see this article ah, I very scared leh. \n I don't know if my house is safe from falls!",
    "You: Hmm this sounds quite serious! I didn't know that 1 in 3 trauma cases involve people our age!",
    "You: No fear, Mr Tay! I will help you find all the fall hazards in your house!",
    "Mr Tay: Wah, thank you so much!",
    "Task: Move around the house and help Mr Tay identify 4 fall hazards!"
  };

  private void Start()
  {
    player.GetComponent<MainCharacterController>().inhibit = true;
    text.text = script[counter];
    counter++;
  }

  private void Update()
  {
    if (Input.GetKeyDown("space"))
    {
      if (counter == 7)
      {
        player.GetComponent<MainCharacterController>().inhibit = false;
        return;
      }
      text.text = script[counter];
      counter++;
      if (counter == 3)
      {
        article.SetActive(true);
      }

      if (counter == 5)
      {
        article.SetActive(false);
      }
    }
  }
}