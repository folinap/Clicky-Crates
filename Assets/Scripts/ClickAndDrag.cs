using UnityEngine;


[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
public class ClickAndDrag : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Camera _cam;
    [SerializeField] private TrailRenderer _trail;
    [SerializeField] private BoxCollider _col;
    [SerializeField] private Vector3 _mousePos;
    private bool _swiping = false;

    void Awake()
    {
        _trail.enabled = false;
        _col.enabled = false;
    }
    void UpdateMousePosition()
    {
        _mousePos = _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        transform.position = _mousePos;
    }

    void UpdateComponents()
    {
        _trail.enabled = _swiping;
        _col.enabled = _swiping;
    }

    void Update()
    {
        if (_gameManager.IsGameActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _swiping = true;
                UpdateComponents();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _swiping = false;
                UpdateComponents();
            }
            if (_swiping)
            {
                UpdateMousePosition();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    { 
        Target _target = collision.gameObject.GetComponent<Target>();
        if (_target)
        {
            _target.DestroyTarget();
        }
    }
}
