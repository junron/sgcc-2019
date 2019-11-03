using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialRunner : MonoBehaviour
{
  [SerializeField] private GameObject blob;
  [SerializeField] private GameObject gift;
  [SerializeField] private Text tutorialText;
  [SerializeField] private Camera cam;
  [SerializeField] private GameObject pMask;
  [SerializeField] private GameObject cursor;
  [SerializeField] private GameObject bMask;
  private int cursor1State;
  private Tutorial t;

  public float speed = 1.5f;
  private Vector3 target;
  private MainCharacterController mainCharacterController;


  private void Start()
  {
    target = transform.position;
    Touchable giftObj = gift.GetComponent<Touchable>();
    TutorialMask blobMask = new TutorialMask(blob, false, 4, 6);
    TutorialMask blobMask2 = new TutorialMask(null, bMask);
    TutorialMask blobMask3 = new TutorialMask(blob, false, 4, 6);
    TutorialMask giftMask = new TutorialMask(gift, false, 10, 10);
    TutorialMask pathMask = new TutorialMask(null, pMask);
    mainCharacterController = blob.GetComponent<MainCharacterController>();
    mainCharacterController.inhibit = true;
    t = new Tutorial(tutorialText, "Falls", cam);
    t.Add(new TutorialComponent(blobMask,
      "This is you. You are an ordinary elderly person. You normally just stay at home and watch TV."));
    t.Add(new TutorialComponent(blobMask, "But today is no ordinary day. It's your grandson's birthday."));
    t.Add(new TutorialComponent(giftMask, "You've prepared a gift for your grandson."));
    t.Add(new TutorialComponent("Let's test out your controls by moving to the gift and picking it up."));
    t.Add(new TutorialComponent(blobMask2, "Click to move, like this.", () =>
    {
      Time.timeScale = 1;
      cursor.SetActive(true);
      cursor1State = 1;
    }));
    t.Add(new InteractiveTutorialComponent(
        pathMask,
        "Let's try out the controls! Press the arrow keys to move to the gift.",
        "Yay! You did it!",
        callback =>
        {
          mainCharacterController.inhibit = false;
          giftObj.callback = () =>
          {
            print("Yay");
            callback();
          };
        }
      )
    );
    t.Add(new TutorialComponent(blobMask3, "That's right!"));
    t.Start();
  }

  // Update is called once per frame
  private void Update()
  {
    if (Input.GetKeyDown("space"))
    {
      t.Next();
    }

    switch (cursor1State)
    {
      case 1:
      {
        Vector3 transformPosition = cursor.transform.position;
        if (Mathf.Approximately(transformPosition.x, -0.8f) && cursor.transform.localScale.x - 5f < 0.1)
        {
          cursor.SetActive(false);
          cursor1State = 2;
          blob.GetComponent<Animator>().speed = 1;
          blob.GetComponent<Animator>().SetInteger("state", 1);
          blob.GetComponent<Rigidbody2D>().AddForce(700 * Vector2.right);
        }

        break;
      }
      case 2:
      {
        if (Math.Abs(blob.transform.position.x + 1.29) < 0.1)
        {
          blob.transform.position = new Vector2(-5.14f, -3.03f);
          mainCharacterController.Reset();
          cursor.SetActive(true);
          cursor1State = 1;
        }

        break;
      }
    }
  }
}