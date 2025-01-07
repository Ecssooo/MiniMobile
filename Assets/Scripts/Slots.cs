using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slots : MonoBehaviour, IDropHandler
{
    [SerializeField] private Transform _slotInWorld;

    [SerializeField] private bool _slotInShop;
    
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop");
        if(!_slotInShop)
        {
            GameObject dropped = eventData.pointerDrag;
            DragObject dragObject = dropped.GetComponent<DragObject>();
            this.GetComponent<Image>().enabled = false;
            Instantiate(dragObject.ObjectPrefab, _slotInWorld);
        }
    }
}
