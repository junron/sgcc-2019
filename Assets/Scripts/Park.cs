using UnityEngine;
using UnityEngine.SceneManagement;

public class Park : MonoBehaviour
{
  [SerializeField] private Interactable chua;

  private void Start()
  {
    chua.callback = () => { SceneManager.LoadScene("Exercise");};
  }
}
