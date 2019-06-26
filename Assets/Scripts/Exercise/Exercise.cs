using System;
using UnityEngine;
using UnityEngine.UI;

public class Exercise :MonoBehaviour
{
  [SerializeField] private Text t;
  [SerializeField] private string[] texts;
  [SerializeField] private int taskStartStringNo;
  [SerializeField] private GameObject target;
  private int textNo;
  private TargetBox targetBox;
  private bool taskCompleted;

  private void Start()
  {
    targetBox = target.GetComponent<TargetBox>();
    t.text = texts[0];
  }

  private void Update()
  {
    if (!Input.GetKeyDown(KeyCode.Space)) return;
//  Block until task is completed;
    if (textNo == taskStartStringNo && !taskCompleted) return;
    textNo++;
    if (textNo >= texts.Length)
    {
      Destroy(this.gameObject);
      return;
    }
    t.text = texts[textNo];
    if (textNo == taskStartStringNo)
    {
      targetBox.onStateChange = completed =>
      {
        taskCompleted = completed;
        t.text = completed ? "Task completed!" : texts[textNo];
        target.GetComponent<SpriteRenderer>().color = completed ? Color.green : Color.black;
      };
    }
  }
}