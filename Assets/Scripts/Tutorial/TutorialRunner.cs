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
  private int cursorState2;
  private Tutorial t;

  private MainCharacterController mainCharacterController;
  private TutorialComponent tutorialComponent;
  private InteractiveTutorialComponent freeGift;
  private CameraClamp cameraClamp;


  private void Start()
  {
    Touchable giftObj = gift.GetComponent<Touchable>();
    TutorialMask blobMask = new TutorialMask(blob, false, 4, 6);
    TutorialMask blobMask2 = new TutorialMask(null, bMask);
    TutorialMask blobMask3 = new TutorialMask(blob, false, 4, 6);
    TutorialMask giftMask = new TutorialMask(gift, false, 10, 10);
    TutorialMask pathMask = new TutorialMask(null, pMask);
    cameraClamp = blob.GetComponentInChildren<CameraClamp>();
    cameraClamp.FreezeMovement();
    mainCharacterController = blob.GetComponent<MainCharacterController>();
    mainCharacterController.inhibitInhibit = true;
    t = new Tutorial(tutorialText, "Park", cam);
    t.Add(new TutorialComponent(blobMask,
      "This is you. You are an ordinary elderly person. You normally just stay at home and watch TV."));
    t.Add(new TutorialComponent(blobMask, "But today is no ordinary day. It's your grandson's birthday."));
    t.Add(new TutorialComponent(giftMask, "You've prepared a gift for your grandson."));
    t.Add(new TutorialComponent("Let's test out your controls by moving to the gift and picking it up."));
    tutorialComponent = new TutorialComponent(blobMask2, "Click to move, like this.", () =>
    {
      Time.timeScale = 1;
      cursor.SetActive(true);
      cursor1State = 1;
    }) {completed = false};
    t.Add(tutorialComponent);
    freeGift = new InteractiveTutorialComponent(
      pathMask,
      "Let's try out the controls! Click to move to the gift.",
      "Yay! You did it!",
      callback =>
      {
        cursor1State = 0;
        blob.transform.position = new Vector2(-5.14f, -3.03f);
        mainCharacterController.Reset();
        mainCharacterController.inhibitInhibit = false;
        cursor.SetActive(false);
        cursorState2 = 1;
        cursor.SetActive(true);
        giftObj.callback = ()=>
        {
          callback();
          Variables.tutorialGifts = 1;
          Variables.giftDisplay.UpdateGifts();
        };
      }
    );
    t.Add(freeGift);
    t.Add(new TutorialComponent(blobMask3, "You can see the total number of gifts you have earned in the top left corner."));
    t.Start();
  }

  // Update is called once per frame
  private void Update()
  {
    if (Input.GetKeyDown("space"))
    {
      t.Next();
    }

    if (cursorState2 == 1)
    {
      Vector3 transformPosition = cursor.transform.position;
      if (Mathf.Approximately(transformPosition.x, -0.8f) && cursor.transform.localScale.x - 5f < 0.1)
      {
        cursor.SetActive(false);
        cursor.SetActive(true);
      }

      if (Math.Abs(blob.transform.position.x + 1.3) < 1)
      {
        // Player has succeeded in first move
        cursor.SetActive(false);
        cursor.SetActive(true);
        cursor.GetComponent<Animator>().SetBool("cursor2", true);
        cameraClamp.Unfreeze();
        cursorState2 = 2;
      }
    }
    else if (freeGift.completed)
    {
      cursor.SetActive(false);
      mainCharacterController.inhibitInhibit = true;
    }

    if (cursor1State == 1)
    {
      Vector3 transformPosition = cursor.transform.position;
      if (!Mathf.Approximately(transformPosition.x, -0.8f) || !(cursor.transform.localScale.x - 5f < 0.1)) return;
      cursor.SetActive(false);
      cursor1State = 2;
      blob.GetComponent<Animator>().speed = 1;
      blob.GetComponent<Animator>().SetInteger("state", 1);
      blob.GetComponent<Rigidbody2D>().AddForce(700 * Vector2.right);
    }
    else if (cursor1State == 2)
    {
      if (!(Math.Abs(blob.transform.position.x + 1.29) < 0.1)) return;
      blob.transform.position = new Vector2(-5.14f, -3.03f);
      mainCharacterController.Reset();
      cursor.SetActive(true);
      tutorialComponent.completed = true;
      cursor1State = 1;
    }
  }
}