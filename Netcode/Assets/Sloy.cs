using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.Netcode;

public class Sloy : NetworkBehaviour, IDropHandler
{
    public GameObject Dropped;
    public void OnDrop(PointerEventData eventData)
    {
        Dropped = eventData.pointerDrag;
        CToServerRpc();
    }
    [ServerRpc(RequireOwnership = false)]
    public void CToServerRpc()
    {
        Item item = Dropped.GetComponent<Item>();
        item.ParentAfterDrag = this.transform;
        item.transform.SetParent(this.transform);
    }
}
