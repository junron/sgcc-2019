
using UnityEngine;
using UnityEngine.Serialization;

public class KeycodeHandler : MonoBehaviour
{
    [SerializeField] private KeyCode pauseKeyCode = KeyCode.Space;
    [SerializeField] private KeyCode quitKeyCode = KeyCode.Q;
    [SerializeField] private KeyCode settingsKeyCode = KeyCode.S;

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;

    private void Start()
    {
        FontManager.UpdateFont();
    }
    void Update()
    {
        if (pauseKeyCode != KeyCode.None)
        {
            if (Input.GetKeyDown(pauseKeyCode))
            {
                if (!pausePanel.activeSelf)
                {
                    Time.timeScale = 0;
                    pausePanel.SetActive(true);
                }
                else
                {
                    Time.timeScale = 1;
                    pausePanel.SetActive(false);
                }
            }
        }
        if (quitKeyCode != KeyCode.None)
        {
            if (Input.GetKeyDown(quitKeyCode))
            {
                Debug.Log("Quit");
                Application.Quit();
            }
        }
        if (settingsKeyCode != KeyCode.None)
        {
            if (Input.GetKeyDown(settingsKeyCode))
            {
                if (!settingsPanel.activeSelf)
                {
                    Time.timeScale = 0;
                    settingsPanel.SetActive(true);
                }
                else
                {
                    Time.timeScale = 1;
                    settingsPanel.SetActive(false);
                }
            }
        }
    }
}
