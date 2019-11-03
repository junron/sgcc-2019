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

  public float speed = 1.5f;
  private Vector3 target;


  private void Start()
  {
    target = transform.position;
    Touchable giftObj = gift.GetComponent<Touchable>();
    TutorialMask blobMask = new TutorialMask(blob, false, 4, 4);
    TutorialMask blobMask2 = new TutorialMask(blob, false, 10, 10);
    TutorialMask blobMask3 = new TutorialMask(blob, false, 4, 6);
    TutorialMask giftMask = new TutorialMask(gift, false, 10, 10);
    TutorialMask pathMask = new TutorialMask(null, pMask);
    MainCharacterController blobScript = blob.GetComponent<MainCharacterController>();
    blobScript.inhibit = true;
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
          blobScript.inhibit = false;
          giftObj.callback = () =>
          {
            print("Yay");
            callback();
          };
        }
      )
    );
    t.Add(new TutorialComponent(blobMask3, "That's right!"));
    t.Add(new TutorialComponent(null,
      "As your assistant, I have added indicators to remind you of your health, wealth and happiness throughout the game.",
      () => { bMask.SetActive(false); }
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