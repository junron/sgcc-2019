using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
  [SerializeField] private Button continueBtn;
  [SerializeField] private Button quitBtn;

  private void Start()
  {
    this.gameObject.SetActive(false);
    quitBtn.onClick.AddListener(Application.Quit);
    continueBtn.onClick.AddListener(UnPause);

  }

  private void UnPause()
  {
    Time.timeScale = 1;
    this.gameObject.SetActive(false);
  }
}