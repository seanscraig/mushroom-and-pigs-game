using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }

    private Vector2 direction = Vector2.down;

    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputUpArrow =  KeyCode.UpArrow;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputDownArrow =  KeyCode.DownArrow;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputLeftArrow =  KeyCode.LeftArrow;
    public KeyCode inputRight = KeyCode.D;
    public KeyCode inputRightArrow =  KeyCode.RightArrow;

    public float speed = 5f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Input.GetKey(inputUp)||Input.GetKey(inputUpArrow)) {
            setDirection(Vector2.up);
        } else if (Input.GetKey(inputDown)||Input.GetKey(inputDownArrow)) {
            setDirection(Vector2.down);
        } else if (Input.GetKey(inputLeft)||Input.GetKey(inputLeftArrow)) {
            setDirection(Vector2.left);
        } else if (Input.GetKey(inputRight)||Input.GetKey(inputRightArrow)) {
            setDirection(Vector2.right);
        } else {
            setDirection(Vector2.zero);
        }
    }

    private void FixedUpdate() 
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);
    }

    private void setDirection(Vector2 newDirection) 
    {
        direction = newDirection;
    }
}
