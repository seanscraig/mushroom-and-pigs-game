using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
  public enum ItemType
  {
    ExtraBomb,
    BlastRadius,
    SpeedIncrease
  }

  public ItemType type;

  public int amountOfMushroomsToAdd;

  private void OnItemPickup(GameObject player)
  {
    switch (type)
    {
      case ItemType.ExtraBomb:
        player.GetComponent<BombController>().AddMushrooms(amountOfMushroomsToAdd);
        break;
      case ItemType.BlastRadius:
        player.GetComponent<BombController>().explosionRadius++;
        break;
      case ItemType.SpeedIncrease:
        player.GetComponent<PlayerController>().speed++;
        break;
    }

    Destroy(gameObject);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      OnItemPickup(other.gameObject);
    }
  }
}
