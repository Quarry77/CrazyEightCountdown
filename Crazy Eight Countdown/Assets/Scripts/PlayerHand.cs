using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHand : MonoBehaviour {

	private List<Card> cards;
	public bool isVertical;
	public int level;
	public int playerId;

	// Use this for initialization
	void Awake ()
	{
		this.cards = new List<Card>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void DrawCard(int cardId)
	{
		if (cardId >= 0)
		{
			Quaternion quat = new Quaternion();
			if (this.isVertical)
			{
				quat = Quaternion.Euler(0,0,-90);
			}
			GameObject newCardObj = (GameObject)Instantiate(Resources.Load("Prefabs/Card"), new Vector3(transform.localPosition.x, transform.localPosition.y), quat);
			newCardObj.transform.parent = gameObject.transform;
			Card newCard = newCardObj.GetComponent<Card>();
			newCard.SetId(cardId);
			this.cards.Add(newCard);

			this.reorientCards();
		}
	}

	public int PlayCard(int cardId)
	{
		for (int i = 0; i < this.cards.Count; i++)
		{
			if (this.cards[i].GetId() == cardId)
			{
				Card card = this.cards[i];
				this.cards.RemoveAt(i);
				Destroy(card.gameObject);
				
				this.reorientCards();
				return cardId;
			}
		}

		return -1;
	}

	public int GetCardCount()
	{
		return this.cards.Count;
	}

	private void reorientCards()
	{
		if (this.isVertical && false)
		{
			for (int i = 0; i < this.cards.Count; i++)
			{
				float newY = (-0.2f * this.cards.Count) + (i * 0.4f);
				Vector3 oldPos = cards[i].transform.localPosition;
				Vector3 newPos = new Vector3(oldPos.x, newY, i * -0.1f);
				cards[i].transform.localPosition = newPos;
			}
		}
		else
		{
			for (int i = 0; i < this.cards.Count; i++)
			{
				float newX = (-0.6f * this.cards.Count) + (i * 1.2f);
				Vector3 oldPos = cards[i].transform.localPosition;
				Vector3 newPos = new Vector3(newX, oldPos.y, i * -0.1f);
				cards[i].transform.localPosition = newPos;
			}
		}
	}
}
