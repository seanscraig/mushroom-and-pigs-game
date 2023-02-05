using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public new Rigidbody2D rigidbody { get; private set; }
  public KeyCode inputUp = KeyCode.W;
  // public KeyCode inputUpArrow = KeyCode.UpArrow;
  public KeyCode inputDown = KeyCode.S;
  // public KeyCode inputDownArrow = KeyCode.DownArrow;
  public KeyCode inputLeft = KeyCode.A;
  // public KeyCode inputLeftArrow = KeyCode.LeftArrow;
  public KeyCode inputRight = KeyCode.D;
  // public KeyCode inputRightArrow = KeyCode.RightArrow;
  public AnimatedSpriteRenderer spriteRendererUp;
  public AnimatedSpriteRenderer spriteRendererDown;
  public AnimatedSpriteRenderer spriteRendererLeft;
  public AnimatedSpriteRenderer spriteRendererRight;
  public AnimatedSpriteRenderer spriteRendererDeath;

  public float speed = 5f;

  private Vector2 direction = Vector2.down;
  private AnimatedSpriteRenderer activeSpriteRenderer;

  private void Awake()
  {
    rigidbody = GetComponent<Rigidbody2D>();
    activeSpriteRenderer = spriteRendererDown;
  }

  private void Update()
  {
    if (Input.GetKey(inputUp))
    {
      setDirection(Vector2.up, spriteRendererUp);
    }
    else if (Input.GetKey(inputDown))
    {
      setDirection(Vector2.down, spriteRendererDown);
    }
    else if (Input.GetKey(inputLeft))
    {
      setDirection(Vector2.left, spriteRendererLeft);
    }
    else if (Input.GetKey(inputRight))
    {
      setDirection(Vector2.right, spriteRendererRight);
    }
    else
    {
      setDirection(Vector2.zero, activeSpriteRenderer);
    }
  }

  private void FixedUpdate()
  {
    Vector2 position = rigidbody.position;
    Vector2 translation = direction * speed * Time.fixedDeltaTime;

    rigidbody.MovePosition(position + translation);
  }

  private void setDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer)
  {
    direction = newDirection;

    spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
    spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
    spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
    spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

    activeSpriteRenderer = spriteRenderer;
    activeSpriteRenderer.idle = direction == Vector2.zero;
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
    {
      DeathSequence();
    }
  }

  private void DeathSequence()
  {
    enabled = false;
    GetComponent<BombController>().enabled = false;

    spriteRendererUp.enabled = false;
    spriteRendererDown.enabled = false;
    spriteRendererLeft.enabled = false;
    spriteRendererRight.enabled = false;

    spriteRendererDeath.enabled = true;

    Invoke(nameof(OnDeathSequenceEnded), 1.25f);
  }

  private void OnDeathSequenceEnded()
  {
    gameObject.SetActive(false);
		FindObjectOfType<GameManager>().CheckWinState();
  }
}
