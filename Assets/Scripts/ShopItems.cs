using System.Collections.Generic;
using UnityEngine;

public class ShopItems : MonoBehaviour
{
    public static ShopItems Instance;
    public string itemId;
    public string selectedItem;
    // public List<string> selectedItems;
    // public GameObject productCard;
    
    private void Awake()
    {
        Instance = this;
    }

    // void Start()
    // {
    //     AddItemToSelect();
    // }

    // private void AddItemToSelect()
    // {
    //     selectedItems.Add(selectedItem);
    // }
}
