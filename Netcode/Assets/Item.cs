using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Unity.Netcode;

public class Item : NetworkBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform ParentAfterDrag;
    public Image image;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IsServer)
        {
            PickUP();
        }
        else
        {
            RpcSendToServerRpc();
        }
       
       
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (IsServer)
        {
            transform.position = Input.mousePosition;
        }
        else
        {
            DuppaServerRpc();
        }

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (IsServer)
        {
            transform.parent = ParentAfterDrag;
            image.raycastTarget = true;
        }
        else
        {
            RpcSentToServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void RpcSendToServerRpc()
    {
        Debug.Log("HUGU");
        PickUP();
    }
    public void PickUP()
    {
        ParentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }
    [ServerRpc(RequireOwnership = false)]
    public void RpcSentToServerRpc()
    {
        Drop();
    }
    public void Drop()
    {
        Debug.Log("Cippa");
        transform.parent = ParentAfterDrag;
        image.raycastTarget = true;
    }

    [ServerRpc(RequireOwnership = false)]
    public void DuppaServerRpc()
    {
        transform.position = Input.mousePosition;
    }
}
