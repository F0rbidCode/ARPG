using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using JetBrains.Annotations;
using TMPro;

public class ShopItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    

    ShopItem item;

    public bool dragging;
    public Transform originalParent;
    public Canvas canvas;


    public Slot slot;

    [Header("Child Comonents")]
    public Image image;
    public TextMeshProUGUI nameTag;
    public TextMeshProUGUI descriptionTag;

    public void DoAction()
    {
        if (!dragging)
        {
            Debug.Log("You did " + nameTag.text);
        }
    }


    public void SetItem(ShopItem i)
    {
        item = i;
        if (image)
        {
            if (item)
            {
                image.sprite = item.icon;
                image.color = item.color;
                nameTag.text = item.name;
                descriptionTag.text = item.description;
            }
            gameObject.SetActive(item != null);
        }
    }

    protected void Swap(Slot newParent)
    {
        ShopItemUI other = newParent.item as ShopItemUI;
        if (other)
        {
            ShopItem ours = item;
            ShopItem theirs = other.item;

            slot.UpdateItem(theirs);
            other.slot.UpdateItem(ours);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (originalParent == null) originalParent = transform.parent;
        if (canvas == null) canvas = GetComponentInParent<Canvas>();

        transform.SetParent(canvas.transform, true);
        transform.SetAsLastSibling();

        dragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragging)
            transform.position = eventData.position;
    }

    List<RaycastResult> hits = new List<RaycastResult>();

    public void OnEndDrag(PointerEventData eventData)
    {
        //is there a slot underneath
        Slot slotFound = null;
        EventSystem.current.RaycastAll(eventData, hits);
        foreach (RaycastResult hit in hits)
        {
            Slot s = hit.gameObject.GetComponent<Slot>();
            if(s)
            {
                slotFound = s; //set slot found to the found slot
                Swap(slotFound); //call swap to swap the items contained in each slot
                transform.SetParent(originalParent); //set the orional perant back
                transform.localPosition = Vector3.zero; //reset local transform
            }
            if(!s)//if no slot is found
            {
                transform.SetParent(originalParent); //set the orional perant back
                transform.localPosition = Vector3.zero; //reset local transform
            }
        } 
        dragging = false;
    }
}
