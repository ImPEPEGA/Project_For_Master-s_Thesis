using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using Unity.VisualScripting;

public class ShopController : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip buttonClickSound;
    public GameObject btnBuy;
    public GameObject purchaseMenu;
    public GameObject purchaseErrorMenu;
    public GameObject purchaseMenuImg;
    public GameObject purchaseMenuPrice;
    public GameObject purchaseMenuDescription;
    private GameObject btnProduct;
    public string itemId;

    void Start()
    {
        audioSource = Settings.Instance.audioSource;
        buttonClickSound = Settings.Instance.buttonClickSound;
    }
    public void buyAirplane()
    {
        audioSource.PlayOneShot(buttonClickSound);
        try
        {
            if (PlayerPrefs.GetFloat("PlayerScore", 0) >= float.Parse(purchaseMenuPrice.GetComponent<TextMeshProUGUI>().text.Replace(" ", "")))
            {
                PlayerPrefs.SetFloat("PlayerScore", PlayerPrefs.GetFloat("PlayerScore", 0) - float.Parse(purchaseMenuPrice.GetComponent<TextMeshProUGUI>().text.Replace(" ", "")));
                PlayerPrefs.Save();
                Settings.Instance.SetScore();

                PlayerPrefs.SetInt(itemId, 1);
                PlayerPrefs.Save();
                Settings.Instance.GetSpecificProductInfo(btnProduct);
            } else
            {
                purchaseErrorMenu.SetActive(true);
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("Exception: " + e.Message);
        }
    }

    public void getInfoAbtProduct(GameObject objectToHandle)
    {
        btnProduct = objectToHandle;
        itemId = btnProduct.GetComponent<ShopItems>().itemId;
    }

    public void openPurchaseMenu()
    {
        audioSource.PlayOneShot(buttonClickSound);
        Settings.Instance.GetSpecificProductInfo(btnProduct);
        purchaseMenuImg.GetComponent<UnityEngine.UI.Image>().sprite =
            btnProduct.transform.Find("ProductImg")
            .GetComponent<UnityEngine.UI.Image>()
            .sprite;
        purchaseMenuPrice.GetComponent<TextMeshProUGUI>().text =
            btnProduct
            .transform.Find("PriceBG")
            .transform.Find("Price")
            .GetComponent<TextMeshProUGUI>()
            .text;
        switch (itemId)
        {
            case "TransportAirplane":
                purchaseMenuDescription.GetComponent<TextMeshProUGUI>().text = Settings.Instance.airplaneDescriptions[1];
                break;
            case "JetAirplane":
                purchaseMenuDescription.GetComponent<TextMeshProUGUI>().text = Settings.Instance.airplaneDescriptions[2];
                break;
            case "ComingSoon":
                purchaseMenuDescription.GetComponent<TextMeshProUGUI>().text = Settings.Instance.airplaneDescriptions[3];
                break;
        }
        // purchaseMenuDescription.GetComponent<TextMeshProUGUI>().text =
        purchaseMenu.SetActive(true);
    }
    public void closePurchaseMenu()
    {
        audioSource.PlayOneShot(buttonClickSound);
        closePurchaseErrorMenu();
        purchaseMenu.SetActive(false);
    }
    public void closePurchaseErrorMenu()
    {
        audioSource.PlayOneShot(buttonClickSound);
        purchaseErrorMenu.SetActive(false);
    }
}
