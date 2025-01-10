using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slots : MonoBehaviour, IDropHandler
{
    [SerializeField] private Transform _slotInWorld;
    [SerializeField] private bool _slotInShop;
    
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DragObject dragObject = dropped.GetComponent<DragObject>();
        if(!_slotInShop && dropped.transform.childCount == 0 && GameManager.Instance.MoneyController.MoneyBanq >= dragObject.Cost)
        {
            this.GetComponent<Image>().enabled = false;
            Instantiate(dragObject.ObjectPrefab, _slotInWorld);
            GameManager.Instance.MoneyController.SubMoney(dragObject.Cost);
            TowerController.Instance.TowerList.Add(dragObject.ObjectPrefab.GetComponentInChildren<TowerAttack>());
        }
    }
}
