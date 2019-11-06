using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Exercise :MonoBehaviour
{
  [SerializeField] private Text tPlayer;
  [SerializeField] private string[] texts;
  [SerializeField] private int taskStartStringNo;
  [SerializeField] private GameObject target;
  [SerializeField] private Animator animator;
  [SerializeField] private Button demoButton;

  private int textNo;
  private TargetBox targetBox;
  private bool taskCompleted;

  private void Start()
  {
    Debug.Log(Variables.fontSize);
    targetBox = target.GetComponent<TargetBox>();
    demoButton.onClick.AddListener(RunDemo);
    SetText(texts[0]);
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
    SetText(texts[textNo]);
    if (textNo == 8)
    {
      StartCoroutine(WaitAndDo(0.75f, RunDemo));
    }
    if (textNo != taskStartStringNo) return;
    target.SetActive(true);
    StartCoroutine(WaitAndDo(2, () => { SetText("Move your left leg into the black box"); }));

    targetBox.onStateChange = completed =>
    {
      if (completed && !taskCompleted)
      {
      }
      if (completed) taskCompleted = true;

      tPlayer.text = taskCompleted ? "Task completed!" : "Move your left leg into the black box";
      target.GetComponent<SpriteRenderer>().color = completed ? Color.green : Color.black;
    };
  }

  private void SetText(string text)
  {
    tPlayer.text = text;
  }

  private void RunDemo()
  {
    demoButton.gameObject.SetActive(true);
    animator.SetTrigger("StartDemo");
  }
  IEnumerator WaitAndDo (float time, Action action) {
    yield return new WaitForSeconds (time);
    action();
  }
}