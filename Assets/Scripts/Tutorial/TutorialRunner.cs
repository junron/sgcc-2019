using UnityEngine;
using UnityEngine.UI;

public class TutorialRunner : MonoBehaviour
{
  [SerializeField] private GameObject blob;
  [SerializeField] private GameObject gift;
  [SerializeField] private Text tutorialText;
  [SerializeField] private Camera cam;
  [SerializeField] private GameObject pMask;
  [SerializeField] private GameObject keys;
  [SerializeField] private GameObject bMask;
  private Tutorial t;


  private void Start()
  {
    Interactable giftObj = gift.GetComponent<Interactable>();
    TutorialMask blobMask = new TutorialMask(blob, false, 4, 4);
    TutorialMask blobMask2 = new TutorialMask(blob, false, 10, 10);
    TutorialMask blobMask3 = new TutorialMask(blob, false, 4, 6);
    TutorialMask giftMask = new TutorialMask(gift, false, 2, 2);
    TutorialMask pathMask = new TutorialMask(null, pMask);
    MainCharacterController blobScript = blob.GetComponent<MainCharacterController>();
    t = new Tutorial(tutorialText, "Falls", cam);
    t.Add(new TutorialComponent(blobMask,
      "This is you. You are an ordinary elderly person. You normally just stay at home and watch TV."));
    t.Add(new TutorialComponent(blobMask, "But today is no ordinary day. It's your grandson's birthday."));
    t.Add(new TutorialComponent(giftMask, "You've prepared a gift for your grandson."));
    t.Add(new TutorialComponent("Let's test out your controls by moving to the gift and picking it up."));
    t.Add(new TutorialComponent(blobMask2, "You can use the arrow keys to move.", () => { keys.SetActive(true); }));
    t.Add(new InteractiveTutorialComponent(
        pathMask,
        "Let's try out the controls! Press the arrow keys to move to the gift.",
        "Yay! You did it!",
        callback =>
        {
          giftObj.SetOnDetect(() =>
          {
            // Prevent interaction with gift from interfering with tutorial
            giftObj.inhibitInteraction = true;
            giftObj.SetOnDetect(null);
            keys.SetActive(false);
            callback();
          });
        }
      )
    );
    t.Add(new InteractiveTutorialComponent(
        pathMask,
        "To pick the gift up, press space.",
        "Yay! You did it!",
        callback =>
        {
          giftObj.inhibitInteraction = false;
          giftObj.callback = () =>
          {
            gift.transform.parent = blob.transform;
            gift.transform.position = new Vector3(-1, 2.2f, 0);
            callback();
          };
        }
      )
    );
    t.Add(new TutorialComponent(blobMask3,"That's right!"));
    t.Add(new TutorialComponent(null,
      "As your assistant, I have added indicators to remind you of your health, wealth and happiness throughout the game.",
      () =>
      {
        bMask.SetActive(false);
      }
    ));
    t.Start();
  }

  // Update is called once per frame
  private void Update()
  {
    if (Input.GetKeyDown("space"))
    {
      t.Next();
    }
  }
}