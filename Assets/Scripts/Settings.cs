using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public static Settings Instance;
    public float score = 0f;
    public GameObject btnBuy;
    public GameObject btnSelect;
    public TextMeshProUGUI scoreText;
    public List<GameObject> products;
    // public Dictionary<string, int> airplanesInfo;
    public List<GameObject> airplanes;
    public List<string> airplaneDescriptions = new List<string>
    {
        "A classic biplane with excellent maneuverability and a vintage design.",
        "A sturdy cargo plane capable of carrying heavy loads over long distances.",
        "A high-performance jet with advanced avionics and supersonic capabilities.",
        "This item is under development."
    };
    public string selectedAirplane;
    public AudioSource audioSource;
    public AudioClip buttonClickSound;

    private void Start()
    {
        score = 200000f;
        PlayerPrefs.SetFloat("PlayerScore", score);
        PlayerPrefs.Save();

        if (SceneManager.GetActiveScene().name != "LevelScene")
        {
            SetScore();
        }
        else
        {
            SpawnSelectedAirplane();
        }

        if (SceneManager.GetActiveScene().name == "Shop")
        {
            GetProductsInfo();
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnSelectedAirplane()
    {
        foreach (GameObject airplane in airplanes)
        {
            if (PlayerPrefs.GetInt(airplane.GetComponent<AirplanesId>().airplaneId, 0) == 1)
            {
                airplane.SetActive(true);
                Debug.Log($"Selected Airplane: {airplane.name}");
            }
            else
            {
                airplane.SetActive(false);
            }
        }
        // foreach (GameObject airplane in airplanes)
        // {
        //     foreach (KeyValuePair<string, int> pair in airplanesInfo)
        //     {
        //         if (airplane.name == pair.Key && pair.Value == 1)
        //         {
        //             Debug.Log($"(Level)Item: {pair.Key}, Value: {pair.Value}");
        //         }
        //     }
        // }
    }

    /*SETTINGS SHOP*/
    #region SettingsShop
    public void GetProductsInfo()
    {
        foreach (GameObject product in products)
        {
            // PlayerPrefs.SetInt(product.GetComponent<ShopItems>().itemId, 0);
            // PlayerPrefs.Save();

            // Debug.Log($"(Settings)Item id: {product.GetComponent<ShopItems>().itemId}, Value: {PlayerPrefs.GetInt(product.GetComponent<ShopItems>().itemId, 0)}");
            GetSpecificProductInfo(product);

        }
    }

    public void GetSpecificProductInfo(GameObject product)
    {
        if (PlayerPrefs.GetInt(product.GetComponent<ShopItems>().itemId, 0) == 1)
        {
            // Debug.Log("(Settings)Purchased item found: " + product.GetComponent<ShopItems>().itemId);
            btnBuy.GetComponentInChildren<TextMeshProUGUI>().text = "purchased";
            product.GetComponentInChildren<TextMeshProUGUI>().text = "purchased";
            btnBuy.GetComponent<Button>().interactable = false;
        }
        else if (product.GetComponent<ShopItems>().itemId == "ComingSoon")
        {
            btnBuy.GetComponentInChildren<TextMeshProUGUI>().text = "coming soon";
            btnBuy.GetComponent<Button>().interactable = false;
        }
        else
        {
            // Debug.Log("(Settings)Unpurchased item found: " + product.GetComponent<ShopItems>().itemId);
            btnBuy.GetComponentInChildren<TextMeshProUGUI>().text = "buy";
            btnBuy.GetComponent<Button>().interactable = true;
        }
    }

    public void SetScore()
    {
        scoreText.text = PlayerPrefs.GetFloat("PlayerScore", 0f).ToString();
    }
    #endregion SettingsShop

    /*SETTINGS INVENTORY*/
    #region SettingsInventory

    #endregion SettingsInventory

    /*DEVELOPER METHODS*/
    #region Developer Methods
    public void DeveloperAddScore()
    {
        score += 100000f;
        PlayerPrefs.SetFloat("PlayerScore", score);
        PlayerPrefs.Save();
        SetScore();
    }

    public void DeveloperResetProducts()
    {
        foreach (GameObject product in products)
        {
            PlayerPrefs.SetInt(product.GetComponent<ShopItems>().itemId, 0);
            PlayerPrefs.Save();
        }
        GetProductsInfo();
    }

    public static implicit operator Settings(PauseMenu v)
    {
        throw new NotImplementedException();
    }
    #endregion Developer Methods
}
