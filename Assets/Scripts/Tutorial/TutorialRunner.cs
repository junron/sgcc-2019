using UnityEngine;
using UnityEngine.UI;

public class TutorialRunner : MonoBehaviour
{
  [SerializeField] private GameObject blob;
  [SerializeField] private GameObject coin;
  [SerializeField] private GameObject kitchen;
  [SerializeField] private GameObject pMask;
  [SerializeField] private Text tutorialText;
  [SerializeField] private Camera cam;
  Tutorial t;

  public static bool completed = true;


  private void Start()
  {
    TutorialMask blobMask = new TutorialMask(blob, false, 3, 2);
    TutorialMask coinMask = new TutorialMask(coin, true);
    TutorialMask kitchenMask = new TutorialMask(kitchen, false, 1, 1);
    TutorialMask pathMask = new TutorialMask(null, pMask);
    MainCharacterController blobScript = blob.GetComponent<MainCharacterController>();
    blobScript.FreezeY();
    t = new Tutorial(tutorialText, "MainScene", cam);
    t.Add(new TutorialComponent(blobMask, "This is Blob. You can control it using the arrow keys."));
    
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
        kitchenMask,
        "This is the kitchen. Navigate Blob to the kitchen to make breakfast!"
      )//how to sense when reach the entrance lel
    );
    
    t.Start();
  }

  // Update is called once per frame
  private void Update()
  {
  if (completed)
    {
    return;
    }
    if (Input.GetKeyDown("space"))
    {
      t.Next();
    }
  }
}