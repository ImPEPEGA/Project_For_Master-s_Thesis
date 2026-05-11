using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip buttonClickSound;
    public GameObject productMenuBtn;
    public GameObject productMenuImg;
    public GameObject productMenuName;
    public GameObject productMenuDescription;
    public GameObject productMenuBtnSelect;
    private string defaultAirplane = "BiplaneAirplane";
    public string itemId;
    public string selectedItem;

    // public int _isSelected;

    void Start()
    {
        audioSource = Settings.Instance.audioSource;
        buttonClickSound = Settings.Instance.buttonClickSound;
        PlayerPrefs.SetInt(defaultAirplane, 1);
        PlayerPrefs.Save();
        // ResetItemsInfo();
        checkSelectedItem();
        checkBoughtItems();
        LogItemsInfo();
    }

    void checkBoughtItems()
    {
        foreach (GameObject product in Settings.Instance.products)
        {
            if (PlayerPrefs.GetInt(product.GetComponent<ShopItems>().itemId, 0) == 1)
            {
                product.SetActive(true);
            } else
            {
                product.SetActive(false);
            }
        }
    }

    public void checkSelectedItem()
    {
        foreach (GameObject product in Settings.Instance.products) {
            Debug.Log($"(Inventory)Selected name: {product.GetComponent<ShopItems>().selectedItem}, Value: {PlayerPrefs.GetInt(product.GetComponent<ShopItems>().selectedItem, 0)}");
        }
    }
    //--!!!--VOPROS--!!!--//
    public void SelectItem()
    {
        audioSource.PlayOneShot(buttonClickSound);
        foreach (GameObject product in Settings.Instance.products)
        {
            PlayerPrefs.SetInt(product.GetComponent<ShopItems>().selectedItem, 0);
            PlayerPrefs.Save();

            // productMenuBtnSelect.GetComponentInChildren<TextMeshProUGUI>().text = "Select";
            // productMenuBtnSelect.GetComponent<Button>().interactable = true;
        }

        PlayerPrefs.SetInt(selectedItem, 1);
        PlayerPrefs.Save();

        productMenuBtnSelect.GetComponentInChildren<TextMeshProUGUI>().text = "selected";
        productMenuBtnSelect.GetComponent<Button>().interactable = false;

        // Settings.Instance.selectedAirplane = itemId;

        // PlayerPrefs.SetInt("isSelect", 1);
        // PlayerPrefs.Save();
        // Settings.Instance.airplanesInfo.Add(itemId, PlayerPrefs.GetInt("isSelect", 0));
    }

    /* Testing check info about items */
    #region LogItemsInfo
    void LogItemsInfo()
    {
        foreach (GameObject product in Settings.Instance.products)
        {
            Debug.Log($"(Inventory)Items name: {product.GetComponent<ShopItems>().itemId}, Value: {PlayerPrefs.GetInt(product.GetComponent<ShopItems>().itemId, 0)}");
        }
    }
    #endregion LogItemsInfo

    private void ResetItemsInfo()
    {
        foreach (GameObject product in Settings.Instance.products)
        {
            PlayerPrefs.SetInt(product.GetComponent<ShopItems>().selectedItem, 0);
            PlayerPrefs.Save();
        }
    }

    public void openProductMenu(GameObject objectToHandle)
    {
        audioSource.PlayOneShot(buttonClickSound);
        itemId = objectToHandle.GetComponent<ShopItems>().itemId;
        selectedItem = objectToHandle.GetComponent<ShopItems>().selectedItem;

        checkSelectedItem(objectToHandle);

        productMenuImg.GetComponent<UnityEngine.UI.Image>().sprite =
            objectToHandle.transform.Find("ProductImg")
            .GetComponent<UnityEngine.UI.Image>()
            .sprite;

        productMenuName.GetComponent<TextMeshProUGUI>().text =
            objectToHandle
            .transform.Find("NameBG")
            .transform.Find("Name")
            .GetComponent<TextMeshProUGUI>()
            .text;
        
        switch (itemId)
        {
            case "BiplaneAirplane":
                productMenuDescription.GetComponent<TextMeshProUGUI>().text = Settings.Instance.airplaneDescriptions[0];
                break;
            case "TransportAirplane":
                productMenuDescription.GetComponent<TextMeshProUGUI>().text = Settings.Instance.airplaneDescriptions[1];
                break;
            case "JetAirplane":
                productMenuDescription.GetComponent<TextMeshProUGUI>().text = Settings.Instance.airplaneDescriptions[2];
                break;
            case "ComingSoon":
                productMenuDescription.GetComponent<TextMeshProUGUI>().text = Settings.Instance.airplaneDescriptions[3];
                break;
        }
        productMenuBtn.SetActive(true);
    }

    private void checkSelectedItem (GameObject item)
    {
        if (PlayerPrefs.GetInt(item.GetComponent<ShopItems>().selectedItem, 0) == 1)
        {
            productMenuBtnSelect.GetComponentInChildren<TextMeshProUGUI>().text = "selected";
            productMenuBtnSelect.GetComponent<Button>().interactable = false;
        }
        else
        {
            productMenuBtnSelect.GetComponentInChildren<TextMeshProUGUI>().text = "select";
            productMenuBtnSelect.GetComponent<Button>().interactable = true;
        }
    }

    public void closeProductMenu()
    {
        audioSource.PlayOneShot(buttonClickSound);
        productMenuBtn.SetActive(false);
    }
}
