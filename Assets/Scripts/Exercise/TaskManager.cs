using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
  [SerializeField] private Text text;
  [SerializeField] private GameObject target;
  private string promptText;

  private void Start()
  {
    promptText = text.text;
    target.GetComponent<TargetBox>().onStateChange = completed =>
    {
      text.text = completed ? "Task completed!" : promptText;
      target.GetComponent<SpriteRenderer>().color = completed ? Color.green : Color.black;
    };
  }
}