using UnityEngine;
using UnityEngine.UI;

public class OpenSettings : MonoBehaviour
{
  [SerializeField] private Button settingsBtn;
  [SerializeField] private GameObject settingsMenu;
  // Start is called before the first frame update
  private void Start()
  {
    settingsMenu.SetActive(false);
    settingsBtn.onClick.AddListener(LoadSettings);
  }

  private void LoadSettings()
  {
    settingsMenu.SetActive(true);
  }
}