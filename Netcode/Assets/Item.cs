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
            ParentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            image.raycastTarget = false;
        }
        else
        {
          
        }
  
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.parent = ParentAfterDrag;
        image.raycastTarget = true;
    }
 
    [ServerRpc(RequireOwnership = false)]
    public void RpcSendToServerRpc()
    {
        Debug.Log("Synchronizacja kurwo");
    }
}
