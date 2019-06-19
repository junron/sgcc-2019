using UnityEngine;
using UnityEngine.UI;

public class TutorialRunner : MonoBehaviour
{
  [SerializeField] private GameObject blob;
  [SerializeField] private GameObject coin;
  [SerializeField] private GameObject exit;
  [SerializeField] private GameObject pMask;
  [SerializeField] private Text tutorialText;
  [SerializeField] private Camera cam;
  private Tutorial t;


  private void Start()
  {
    TutorialMask blobMask = new TutorialMask(blob, false, 3, 2);
    TutorialMask coinMask = new TutorialMask(coin, true);
    TutorialMask exitMask = new TutorialMask(exit, true);
    TutorialMask pathMask = new TutorialMask(null, pMask);
    MainCharacterController blobScript = blob.GetComponent<MainCharacterController>();
    blobScript.FreezeY();
    t = new Tutorial(tutorialText, "MainScene", cam);
    t.Add(new TutorialComponent(blobMask, "This is Blob. You can control it using the arrow keys."));
    t.Add(
      new TutorialComponent(
        coinMask,
        "This is a coin. Collect coins to gain points!"
      )
    );
    t.Add(
      new InteractiveTutorialComponent(
        pathMask,
        "Lets try out the controls! Press the right arrow key to move blob to the coin!",
        "Yay! You did it!",
        callback =>
        {
          GameObject.Find("coin").GetComponent<Coin>().SetCallback(callback);
        }
      )
    );
    t.Add(
      new TutorialComponent(
        exitMask,
        "This is the exit. Navigate Blob to the exit to complete the game!",
        () => { blob.GetComponent<Transform>().position = new Vector3(4, 3.5f, 0); }
      )
    );
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