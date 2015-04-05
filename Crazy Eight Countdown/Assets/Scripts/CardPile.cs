using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardPile : MonoBehaviour {

	public bool isFaceDown;
	public int cardCount;
	private List<int> cards = new List<int>();
	private Card activeCard;

	// Use this for initialization
	void Start ()
	{
		this.activeCard = this.GetComponentInChildren<Card>();
		this.shuffleNewDeck();
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.cardCount = this.cards.Count;
		if(this.cardCount > 0)
		{
			this.activeCard.SetId(cards[0]);
		}
		this.renderPile();
	}

	// Public Functions
	public void shuffleNewDeck()
	{
		cards.Clear();

		List<int> newCards = new List<int>();
		for (int i = 0; i < 52; i++)
		{
			newCards.Add(i);
		}

		while (newCards.Count > 0)
		{
			int index = Random.Range(0, newCards.Count);
			cards.Add(newCards[index]);
			newCards.RemoveAt(index);
		}
		this.activeCard.SetId(this.cards[0]);
	}

	// Private Functions
	private void renderPile()
	{

	}
}
