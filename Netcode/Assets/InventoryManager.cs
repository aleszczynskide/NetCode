using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Networking;

public class InventoryManager : NetworkBehaviour
{
    public Sloy[] slots;
    public GameObject gameItemPrefab;

    public void AddItem()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Sloy slot = slots[i];
            Item itemInSlot = slot.GetComponentInChildren<Item>();
            if (itemInSlot == null)
            {
                SpawnItem(slot);
                return;
            }
        }
    }

    public void SpawnItem(Sloy slot)
    {
        GameObject newItem = Instantiate(gameItemPrefab, slot.transform);
        Item inventoryItem = newItem.GetComponent<Item>();
        inventoryItem.GetComponent<NetworkObject>().Spawn(true);
        inventoryItem.ParentAfterDrag = slot.transform;
        inventoryItem.transform.SetParent(slot.transform);
    }

    [ServerRpc(RequireOwnership = false)]
    public void RpcSendToServerRpc(string message)
    {
        Debug.Log("Client received message: " + message);
        AddItem();
    }
}
