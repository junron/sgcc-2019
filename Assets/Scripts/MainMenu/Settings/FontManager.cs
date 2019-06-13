using UnityEngine;
using UnityEngine.UI;

public class FontManager : MonoBehaviour
{
  [SerializeField] private Button increaseButton;
  [SerializeField] private Button decreaseButton;
  [SerializeField] private Text fontSizeOut;

  // Start is called before the first frame update
  private void Start()
  {
    fontSizeOut.fontSize = Variables.fontSize;
    increaseButton.onClick.AddListener(Increase);
    decreaseButton.onClick.AddListener(Decrease);
  }

  // Update is called once per frame
  private void Increase()
  {
    if (Variables.fontSize == 32)
      return;

    Variables.fontSize++;
    fontSizeOut.text = Variables.fontSize.ToString();
    fontSizeOut.fontSize = Variables.fontSize;
    UpdateFont();
  }

  private void Decrease()
  {
    if (Variables.fontSize == 16)
      return;
    Variables.fontSize--;
    fontSizeOut.text = Variables.fontSize.ToString();
    fontSizeOut.fontSize = Variables.fontSize;
    UpdateFont();
  }

  private void UpdateFont()
  {
    GameObject[] texts = GameObject.FindGameObjectsWithTag("CoreText");
    foreach (GameObject textObj in texts)
    {
      Text text = textObj.GetComponent<Text>();
      text.fontSize = Variables.fontSize;
    }
  }
}