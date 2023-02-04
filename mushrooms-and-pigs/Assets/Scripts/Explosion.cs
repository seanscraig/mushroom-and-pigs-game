using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
  public AnimatedSpriteRenderer startSpriteRenderer;
  public AnimatedSpriteRenderer middleSpriteRenderer;
  public AnimatedSpriteRenderer endSpriteRenderer;

  public void SetActiveRenderer(AnimatedSpriteRenderer renderer)
  {
    startSpriteRenderer.enabled = renderer == startSpriteRenderer;
    middleSpriteRenderer.enabled = renderer == middleSpriteRenderer;
    endSpriteRenderer.enabled = renderer == endSpriteRenderer;
  }

  public void SetDirection(Vector2 direction)
  {
    float angle = Mathf.Atan2(direction.y, direction.x);
    transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
  }

  public void DestroyAfterSeconds(float seconds) {
    Destroy(gameObject, seconds);
  }
}
