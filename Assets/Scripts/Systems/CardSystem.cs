using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSystem : MonoBehaviour
{
    [SerializeField] private int maxHand;

    private List<Card> drawPile = new();
    private List<Card> hand = new();
    private List<Card> cards = new();


}
