using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTutorial : MonoBehaviour
{
  public void LoadTutorial()
  {
    SceneManager.LoadScene("Tutorial");
  }
}