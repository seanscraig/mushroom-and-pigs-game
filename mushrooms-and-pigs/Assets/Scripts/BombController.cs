using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
  public GameObject bombPrefab;
  public KeyCode inputKey = KeyCode.Space;
  public float bombFuseTime = 3f;
  public int bombAmount = 1;

  private int bombsRemaining;

  private void OnEnable()
  {
    bombsRemaining = bombAmount;
  }

  private void Update()
  {
    if (bombsRemaining > 0 && Input.GetKeyDown(inputKey))
    {
      StartCoroutine(PlaceBomb());
    }
  }

  private IEnumerator PlaceBomb()
  {
    Vector2 pos = transform.position;
    pos.x = Mathf.Round(pos.x);
    pos.y = Mathf.Round(pos.y);

    GameObject bomb = Instantiate(bombPrefab, pos, Quaternion.identity);
    bombsRemaining--;

    yield return new WaitForSeconds(bombFuseTime);

    Destroy(bomb);
  }

	private void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.layer == LayerMask.NameToLayer("Bomb")) {
			other.isTrigger = false;
		}
	}
}
