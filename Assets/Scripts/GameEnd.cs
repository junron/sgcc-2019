using UnityEngine;

public class GameEnd : MonoBehaviour
{
  private GameObject blob;

  [SerializeField] private GameObject gameEndPanel;

  // Start is called before the first frame update
  private Transform trans;

  void Start()
  {
    Time.timeScale = 1;
    gameEndPanel.SetActive(false);
    trans = GetComponent<Transform>();
  }

  void Update()
  {
    if (Input.GetKeyDown("q"))
    {
      Application.Quit();
    }

    trans.Rotate(0, 0, -40 * Time.deltaTime);
  }

  // Update is called once per frame
  void OnTriggerEnter2D(Collider2D collider)
  {
    gameEndPanel.SetActive(true);
    Time.timeScale = 0;
  }
}