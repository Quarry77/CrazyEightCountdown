using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardPile : MonoBehaviour {

	public bool isFaceDown;
	public int cardCount;
	private List<int> cards;
	private Card activeCard;

	// Use this for initialization
	void Awake()
	{
		this.cards = new List<int>();
		this.activeCard = this.GetComponentInChildren<Card>();
	}
	
	// Update is called once per frame
	void Update()
	{
		this.cardCount = this.cards.Count;
		if (this.cardCount > 0)
		{
			this.activeCard.SetId(cards[0]);
			Renderer[] renderers = this.activeCard.gameObject.GetComponentsInChildren<Renderer>();
			foreach (Renderer r in renderers)
			{
				r.enabled = true;
			}
		}
		else
		{
			Renderer[] renderers = this.activeCard.gameObject.GetComponentsInChildren<Renderer>();
			foreach (Renderer r in renderers)
			{
				r.enabled = false;
			}
		}

		this.GetComponentInChildren<Card>().isFaceDown = this.isFaceDown;
		this.renderPile();
	}
	
	public void OnMouseUpAsButton()
	{
		if (this.name == "Deck")
		{
			GamePlayController.gamePlayController.DeckClick();
		}
		else if (this.name == "Discard")
		{

		}
	}

	// Public Functions
	public void ShuffleNewDeck()
	{
		this.cards.Clear();

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

	public void ShuffleRemainingDeck(List<int> oldCards)
	{
		this.cards.Clear();
		
		while (oldCards.Count > 0)
		{
			int index = Random.Range(0, oldCards.Count);
			cards.Add(oldCards[index]);
			oldCards.RemoveAt(index);
		}
		this.activeCard.SetId(this.cards[0]);
	}

	public int DrawCard()
	{
		if (this.cards.Count > 0)
		{
			int topCard = this.cards[0];
			this.cards.RemoveAt(0);
			return topCard;
		}
		else
		{
			return -1;
		}
	}

	public void AddCardOnTop(int cardId)
	{
		this.cards.Insert(0, cardId);
	}

	// Private Functions
	private void renderPile()
	{

	}
}
