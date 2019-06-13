using UnityEngine;
using UnityEngine.UI;

public class FontManager : MonoBehaviour
{
  [SerializeField] private Button increaseButton;
  [SerializeField] private Button decreaseButton;
  [SerializeField] private Text fontSizeOut;
  public static int fontSize { get; private set; } = 16;

  // Start is called before the first frame update
  void Start()
  {
    fontSizeOut.fontSize = fontSize;
    increaseButton.onClick.AddListener(Increase);
    decreaseButton.onClick.AddListener(Decrease);
  }

  // Update is called once per frame
  private void Increase()
  {
    if (fontSize == 28)
      return;

    fontSize++;
    fontSizeOut.text = fontSize.ToString();
    fontSizeOut.fontSize = fontSize;
  }

  private void Decrease()
  {
    if (fontSize == 14)
      return;

    fontSize--;
    fontSizeOut.text = fontSize.ToString();
    fontSizeOut.fontSize = fontSize;
  }
}