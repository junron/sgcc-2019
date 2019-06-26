using UnityEngine;
using UnityEngine.UI;

public class TreeHider : MonoBehaviour
{
  [SerializeField] private GameObject tree, noTree;
  private bool hasTree = true;

  private void Start()
  {
    GetComponent<Button>().onClick.AddListener(() =>
    {
      hasTree ^= true;
      tree.SetActive(hasTree);
      noTree.SetActive(!hasTree);
    });
  }
}
