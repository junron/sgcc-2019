using UnityEngine;
using UnityEngine.UI;

public class ExitSettings : MonoBehaviour
{
    [SerializeField] private Button exitBtn;
    [SerializeField] private GameObject settingsMenu;
    // Start is called before the first frame update
    void Start()
    {
        settingsMenu.SetActive(false);
        exitBtn.onClick.AddListener(Exit);
    }

    private void Exit()
    {
        settingsMenu.SetActive(false);
    }
}
