using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.Netcode;

public class Sloy : NetworkBehaviour, IDropHandler
{
    public GameObject Dropped;
    public GameObject Slot;
    public void OnDrop(PointerEventData eventData)
    {
        Dropped = eventData.pointerDrag;
        
        if (IsServer)
        {
            Item item = Dropped.GetComponent<Item>();
            item.ParentAfterDrag = this.transform;
            item.transform.SetParent(this.transform);
        }
        else
        {
            CToServerRpc(Dropped);
        }

    }
    [ServerRpc(RequireOwnership = false)]
    public void CToServerRpc(NetworkObjectReference drroppss)
    {
        Drop(drroppss);
    }

    public void Drop(GameObject Drop)
    {
        
        Item item = Drop.GetComponent<Item>();
        item.ParentAfterDrag = this.transform;
        item.transform.SetParent(this.transform);
    }
}
