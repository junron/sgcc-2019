using UnityEngine.UI;

public interface ITutorialComponent
{
  Text textOutput { get; set; }
  bool completed { get; set; }
  void Show();
  void Hide();
}