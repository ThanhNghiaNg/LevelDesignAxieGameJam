using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public List<Item> Inventory;
    public int slotLimit = 10;
    public Transform container;
    public Transform ItemCard;
    private float startX = -270f;
    private float startY = 55f;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        ItemCard.gameObject.SetActive(false);
    }

    private void Start()
    {
        int index = 0;
        foreach (Item item in Inventory)
        {
            if (item == null)
            {
                CreateCardButton(null, null, index);
            }
            else
            {
                int itemIndex = Inventory.IndexOf(item);
                CreateCardButton(Inventory[itemIndex].sprite, Inventory[itemIndex].quatity, index);
            }
            index++;

        }
    }

    private bool IsExist(Item item)
    {
        foreach (Item inventoryItem in Inventory)
        {
            if (inventoryItem == null)
            {
                continue;
            }
            else
            {
                if (item.id == inventoryItem.id)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void addItem(Item item)
    {
        if (IsExist(item))
        {
            int index = Inventory.IndexOf(item);
            Inventory[index].quatity++;
        }
        else
        {
            Inventory.Add(item);
            int index = Inventory.IndexOf(item);
            Inventory[index].quatity = 1;
        }
    }

    public void removeItem(Item item)
    {
        if (IsExist(item) && item.quatity > 1)
        {
            int index = Inventory.IndexOf(item);
            Inventory[index].quatity--;
        }
        else if (IsExist(item) && item.quatity == 1)
        {
            int index = Inventory.IndexOf(item);
            Inventory[index].quatity = 0;
            Inventory.Remove(item);
        }
    }

    private void CreateCardButton(Sprite itemSprite, int? quatity, int index)
    {
        Transform axieCardTransform = Instantiate(ItemCard, container);
        RectTransform axieCardRectTransform = axieCardTransform.GetComponent<RectTransform>();
        if (index <= 4)
        {
            axieCardRectTransform.anchoredPosition = new Vector2(startX, startY);
            startX += 130;
            if (index == 4)
            {
                startX = -270f;
            }
        }
        else if (index >= 5)
        {
            startY = -80f;
            axieCardRectTransform.anchoredPosition = new Vector2(startX, startY);
            startX += 130;
        }


        if (itemSprite != null)
        {
            axieCardTransform.Find("Image").GetComponent<Image>().sprite = itemSprite;
            axieCardTransform.Find("quitity").GetComponent<TextMeshProUGUI>().SetText(quatity.ToString());
        }
        else
        {
            axieCardTransform.Find("Image").GetComponent<Image>().sprite = null;
            axieCardTransform.Find("Image").GetComponent<Image>().color = new Color32(255, 255, 255, 0);
            axieCardTransform.Find("quatity").GetComponent<TextMeshProUGUI>().SetText("");
        }
        axieCardTransform.gameObject.SetActive(true);
    }

    public void UpdateInventory()
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
        int index = 0;
        foreach (Item item in Inventory)
        {
            if (item == null)
            {
                CreateCardButton(null, null, index);
            }
            else
            {
                int itemIndex = Inventory.IndexOf(item);
                CreateCardButton(Inventory[itemIndex].sprite, Inventory[itemIndex].quatity, index);
            }
            index++;
        }
    }

}
