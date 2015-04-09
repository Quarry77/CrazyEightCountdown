using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GamePlayController : MonoBehaviour {
	
	public static GamePlayController gamePlayController;
	public GameObject game;
	public CardPile deck;
	public CardPile discard;
	public int playerCount;
	public List<PlayerHand> players;
	public int activePlayer;
	
	void Awake ()
	{
		if (gamePlayController == null)
		{
			DontDestroyOnLoad(gameObject);
			gamePlayController = this;
		}
		else if (gamePlayController != this)
		{
			Destroy(gameObject);
		}

		this.players = new List<PlayerHand>();
	}

	// Use this for initialization
	void Start ()
	{
		this.playerCount = Mathf.Clamp(this.playerCount, 1, 4);
		this.StartNewGame(this.playerCount);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void StartNewGame(int playerCount)
	{
		Destroy(game);
		this.game = (GameObject)Instantiate(Resources.Load("Prefabs/Game"), new Vector3(0, 0, 0), new Quaternion());
		foreach (CardPile pile in game.GetComponentsInChildren<CardPile>())
		{
			if (pile.name == "Deck")
			{
				pile.ShuffleNewDeck();
				this.deck = pile;
			}
			else
			{
				this.discard = pile;
			}
		}

		for (int i = 0; i < playerCount; i++)
		{
			GameObject newPlayer;
			bool isVertical = false;
			switch (i)
			{
				case 0:
					newPlayer = (GameObject)Instantiate(Resources.Load("Prefabs/Player"), new Vector3(0, -4.5f, 0), new Quaternion());
					break;
				case 1:
					if (playerCount == 2)
					{
						newPlayer = (GameObject)Instantiate(Resources.Load("Prefabs/Player"), new Vector3(0, 4.5f, 0), Quaternion.Euler(0, 0, 180));
					}
					else
					{
						newPlayer = (GameObject)Instantiate(Resources.Load("Prefabs/Player"), new Vector3(-9.0f, 0, 0), Quaternion.Euler(0, 0, 90));
						isVertical = true;
					}
					break;
				case 2:
					if (playerCount == 3)
					{
						newPlayer = (GameObject)Instantiate(Resources.Load("Prefabs/Player"), new Vector3(0, 4.5f, 0), Quaternion.Euler(0, 0, 180));
					}
					else
					{
						newPlayer = (GameObject)Instantiate(Resources.Load("Prefabs/Player"), new Vector3(9.0f, 0, 0), Quaternion.Euler(0, 0, -90));
						isVertical = true;
					}
					break;
				default:
					newPlayer = (GameObject)Instantiate(Resources.Load("Prefabs/Player"), new Vector3(9.0f, 0, 0), Quaternion.Euler(0, 0, -90));
					isVertical = true;
					break;
			}

			this.players.Add(newPlayer.GetComponent<PlayerHand>());
			this.players[i].level = 8;
			this.players[i].playerId = i;
			this.players[i].isVertical = isVertical;

			while (this.players[i].GetCardCount() < this.players[i].level)
			{
				this.players[i].DrawCard(this.deck.DrawCard());
			}
		}

		this.discard.AddCardOnTop(this.deck.DrawCard());

		this.activePlayer = 0;
	}

	public void TryCard(int playerId, int cardId)
	{
		if (this.activePlayer == playerId && this.IsValidPlay(cardId))
		{
			if (this.players[playerId].PlayCard(cardId) >= 0)
			{
				this.discard.AddCardOnTop(cardId);
			}
		}
	}

	public void DeckClick()
	{
		if (this.deck.cardCount > 0)
		{
			this.players[activePlayer].DrawCard(this.deck.DrawCard());
		}
		else
		{
			bool checkSameVal = true;
			List<int> shuffleableCards = new List<int>();
			int topCardVal = Card.GetValueFromId(this.discard.cards[0]);
			for (int i = 1; i < this.discard.cards.Count; i++)
			{
				if (checkSameVal)
				{
					if (Card.GetValueFromId(this.discard.cards[i]) == topCardVal)
					{
						continue;
					}
					else
					{
						checkSameVal = false;
					}
				}
				shuffleableCards.Add(this.discard.cards[i]);
			}

			this.deck.ShuffleRemainingDeck(shuffleableCards);
		}
	}

	private bool IsValidPlay(int cardId)
	{
		int newSuit = Card.GetSuitFromId(cardId);
		int newValue = Card.GetValueFromId(cardId);
		int oldSuit = Card.GetSuitFromId(this.discard.PeekTopCardId());
		int oldValue = Card.GetValueFromId(this.discard.PeekTopCardId());

		bool isValid = false;
		if (oldSuit == newSuit)
		{
			isValid = true;
		}
		else if (oldValue == newValue)
		{
			isValid = true;
		}

		return isValid;
	}
}
