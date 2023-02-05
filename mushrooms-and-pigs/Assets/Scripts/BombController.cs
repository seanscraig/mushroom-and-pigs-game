using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
  [Header("Bomb")]
  public GameObject bombPrefab;
  public KeyCode inputKey = KeyCode.Space;
  public float bombFuseTime = 3f;
  public int bombAmount = 1;
  private int bombsRemaining;

  [Header("Explosion")]
  public Explosion explosionPrefab;
  public LayerMask explodeLayerMask;
  public float explosionDuration = 1f;
  public int explosionRadius = 1;

  [Header("Destructible")]
  public Tilemap destructibleTiles;
  public Destructible destructiblePrefab;

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

    pos = bomb.transform.position;
    pos.x = Mathf.Round(pos.x);
    pos.y = Mathf.Round(pos.y);

    Explosion explosion = Instantiate(explosionPrefab, pos, Quaternion.identity);
    explosion.SetActiveRenderer(explosion.startSpriteRenderer);
    explosion.DestroyAfterSeconds(explosionDuration);

    Explode(pos, Vector2.up, explosionRadius);
    Explode(pos, Vector2.down, explosionRadius);
    Explode(pos, Vector2.left, explosionRadius);
    Explode(pos, Vector2.right, explosionRadius);

    Destroy(bomb);
    bombsRemaining++;
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
    {
      other.isTrigger = false;
    }
  }

  private void Explode(Vector2 position, Vector2 direction, int length)
  {
    if (length <= 0)
    {
      return;
    }

    position += direction;

    if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explodeLayerMask))
    {
      ClearDestructible(position);
      return;
    }

    Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
    explosion.SetActiveRenderer(length > 1 ? explosion.middleSpriteRenderer : explosion.endSpriteRenderer);
    explosion.SetDirection(direction);
    explosion.DestroyAfterSeconds(explosionDuration);

    Explode(position, direction, length - 1);
  }

  private void ClearDestructible(Vector2 position)
  {
    Vector3Int cell = destructibleTiles.WorldToCell(position);
    TileBase tile = destructibleTiles.GetTile(cell);

    if (tile != null)
    {
      Instantiate(destructiblePrefab, position, Quaternion.identity);
      destructibleTiles.SetTile(cell, null);
    }
  }

  public void AddBomb()
  {
    bombAmount++;
    bombsRemaining++;
  }
}
