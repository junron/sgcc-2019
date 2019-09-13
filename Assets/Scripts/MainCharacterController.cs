using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
  public float speed = 3.0f;

  public bool yFrozen;

  public bool xFrozen;

  // Should actually be called randomize
  private Vector3 currentState = Vector3.up;
  private readonly Vector3[] possibleStates = {Vector3.up, Vector3.down, Vector3.right, Vector3.left};
  private Rigidbody2D rb2d;

  [SerializeField] private Collider2D e;
  private Vector3 target;
  private bool _isTargetNull = true;
  private bool _isCameraNotNull;
  private Camera _camera;

  // Start is called before the first frame update
  void Start()
  {
    _camera = Camera.main;
    _isCameraNotNull = _camera != null;
    rb2d = GetComponent<Rigidbody2D>();
    rb2d.freezeRotation = true;
  }

  // Update is called once per frame
  void Update()
  {
    Time.timeScale = 1;
    if (Input.GetMouseButtonDown(0))
    {
      if (_isCameraNotNull)
      {
        target = _camera.ScreenToWorldPoint(Input.mousePosition);
        print("Target reassigned");
      }

      target.z = this.transform.position.z;
      _isTargetNull = false;
    }

    if (_isTargetNull) return;
    //    Prevent jiggling when reach destination
    if ((target - transform.position).sqrMagnitude < 0.005)
    {
      transform.position = target;
      return;
    }

    Vector3 direction = (target - transform.position).normalized;
    rb2d.velocity = direction * speed;
  }

  public void FreezeY()
  {
    yFrozen = true;
  }

  public void FreeY()
  {
    yFrozen = false;
  }

  public void FreezeX()
  {
    xFrozen = true;
  }

  public void FreeX()
  {
    xFrozen = false;
  }
}