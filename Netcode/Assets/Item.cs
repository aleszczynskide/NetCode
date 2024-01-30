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
        AToServerRpc();
    }
    public void OnDrag(PointerEventData eventData)
    {
        BToServerRpc();
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        CToServerRpc();
    }
    [ServerRpc(RequireOwnership = false)]
    public void AToServerRpc()
    {
        ParentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }
    [ServerRpc(RequireOwnership = false)]
    public void BToServerRpc()
    {
        transform.position = Input.mousePosition;
    }
    [ServerRpc(RequireOwnership = false)]
    public void CToServerRpc()
    {
        transform.parent = ParentAfterDrag;
        image.raycastTarget = true;
    }
}
