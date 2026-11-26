using UnityEngine;
using UnityEngine.InputSystem;

public class playerMoveScript : MonoBehaviour
{

    public enum objectType
    {
        Offense, Defense
    }

    [SerializeField] private objectType type;

    private InputAction moveAction;

    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (type == objectType.Offense)
        {
            moveAction = InputSystem.actions.FindAction("Move Offense");
        }
        else if (type == objectType.Defense)
        {
            moveAction = InputSystem.actions.FindAction("Move Defense");
        }

        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(moveAction.ReadValue<Vector2>().x, moveAction.ReadValue<Vector2>().y, 0) * Time.deltaTime * moveSpeed;
    }
}
