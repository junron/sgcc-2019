using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTutorial : MonoBehaviour
{
  public void LoadTutorial()
  {
    Interstitials.Initiate("Tutorial");
  }
}