using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUI : MonoBehaviour
{
    [Header("UI Ԫ��")]
    public Image cardImage;
    public TextMeshProUGUI cardNameText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI descriptionText;
    public Button addButton;

    private CardData cardData;
    private bool isInDeck;

    public void Initialize(CardData data, bool showAddButton)
    {
        cardData = data;
        isInDeck = !showAddButton;

        // ����UI
        cardImage.sprite = data.icon;
        cardNameText.text = data.cardName;
        costText.text = data.cost.ToString();
        descriptionText.text = data.description;


        // ���ð�ť
        addButton.gameObject.SetActive(showAddButton);
        if (showAddButton)
        {
            addButton.onClick.AddListener(AddToDeck);
        }
    }

    private void AddToDeck()
    {
        bool success = DeckManager.Instance.AddCardToDeck(cardData);
        if (success)
        {
            // �������һЩ�Ӿ�����
        }
    }
}