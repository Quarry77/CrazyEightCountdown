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
			GameObject newPlayer = (GameObject)Instantiate(Resources.Load("Prefabs/Player"), new Vector3(0, -4.5f, 0), new Quaternion());
			this.players.Add(newPlayer.GetComponent<PlayerHand>());
			this.players[i].level = 8;
			this.players[i].playerId = i;

			while (this.players[i].GetCardCount() < this.players[i].level)
			{
				this.players[i].DrawCard(this.deck.DrawCard());
			}
		}

		this.activePlayer = 0;
	}

	public void TryCard(int playerId, int cardId)
	{
		if (this.activePlayer == playerId) //&& this.isValidPlay(cardId))
		{
			if (this.players[playerId].PlayCard(cardId) >= 0)
			{
				this.discard.AddCardOnTop(cardId);
			}
		}
	}
}
