using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour
{
	const int SPADES 	= 0;
	const int CLUBS 	= 1;
	const int DIAMONDS 	= 2;
	const int HEARTS 	= 3;
	const int ACE 		= 1;
	const int JACK 		= 11;
	const int QUEEN 	= 12;
	const int KING	 	= 13;

	const string SPADES_STRING 		= "Spades";
	const string CLUBS_STRING 		= "Clubs";
	const string DIAMONDS_STRING 	= "Diamonds";
	const string HEARTS_STRING 		= "Hearts";
	const string ACE_STRING 		= "Ace";
	const string JACK_STRING 		= "Jack";
	const string QUEEN_STRING 		= "Queen";
	const string KING_STRING 		= "King";

	public int id;
	public int suit;
	public int value;
	public string cardName;
	public string test = "";
	public bool isFaceDown = false;
	private Sprite[] sprites;
	private string[] spriteNames;

	void Awake()
	{
		this.sprites = Resources.LoadAll<Sprite>("Sprites/Card");
		this.spriteNames = new string[this.sprites.Length];
		for(int i = 0; i < this.spriteNames.Length; i++)
		{
			this.spriteNames[i] = this.sprites[i].name;
		}
	}

	void Start()
	{
		this.suit = this.GetSuitFromId(id);
		this.value = this.GetValueFromId(id);
		this.cardName = this.GetNameFromAttributes(this.suit, this.value);

		this.renderCard();
	}

	public void OnMouseUpAsButton()
	{
		int playerId = this.GetComponentInParent<PlayerHand>().playerId;
		GamePlayController.gamePlayController.TryCard(playerId, this.id);
	}

	void Update()
	{
		this.renderCard();
	}

	// Public Functions
	public int GetId()
	{
		return this.id;
	}

	public void SetId(int id)
	{
		this.id = id;
		this.suit = this.GetSuitFromId(id);
		this.value = this.GetValueFromId(id);
		this.cardName = this.GetNameFromAttributes(this.suit, this.value);

		this.renderCard();
	}
	
	public int GetSuit()
	{
		return this.suit;
	}
	
	public int GetValue()
	{
		return this.value;
	}
	
	public string GetCardName()
	{
		return this.cardName;
	}
	
	public Sprite GetSuitSprite(int suit)
	{
		string spriteName = "CardSpriteSheet_";
		switch (Mathf.Clamp(suit, 0, 3))
		{
			case Card.SPADES:
				spriteName += Card.SPADES_STRING;
				break;
			case Card.CLUBS:
				spriteName += Card.CLUBS_STRING;
				break;
			case Card.DIAMONDS:
				spriteName += Card.DIAMONDS_STRING;
				break;
			case Card.HEARTS:
				spriteName += Card.HEARTS_STRING;
				break;
		}

		return this.sprites[System.Array.IndexOf(this.spriteNames, spriteName)];
	}
	
	public Sprite GetValueSprite(int suit, int value)
	{
		string spriteName = "CardSpriteSheet_";
		switch (Mathf.Clamp(suit, 0, 3))
		{
			case Card.SPADES:
			case Card.CLUBS:
				spriteName += "B";
				break;
			case Card.DIAMONDS:
			case Card.HEARTS:
				spriteName += "R";
				break;
		}
		
		switch (Mathf.Clamp(value, 1, 13))
		{
			case Card.ACE:
				spriteName += Card.ACE_STRING;
				break;
			case Card.JACK:
				spriteName += Card.JACK_STRING;
				break;
			case Card.QUEEN:
				spriteName += Card.QUEEN_STRING;
				break;
			case Card.KING:
				spriteName += Card.KING_STRING;
				break;
			default:
				spriteName += value;
				break;
		}

		return this.sprites[System.Array.IndexOf(this.spriteNames, spriteName)];
	}

	public void setFaceDown(bool faceDown)
	{
		this.isFaceDown = faceDown;

		this.renderCard();
	}

	// Static Functions
	public static int GetIdFromAttributes(int suit, int value)
	{
		return (suit * 13) + value - 1;
	}
	
	public static int GetSuitFromId(int id)
	{
		return id / 13;
	}
	
	public static int GetValueFromId(int id)
	{
		return (id % 13) + 1;
	}

	public static string GetNameFromAttributes(int suit, int value)
	{
		string name = "";
		switch (value)
		{
			case Card.ACE:
				name += Card.ACE_STRING;
				break;
			case Card.JACK:
				name += Card.JACK_STRING;
				break;
			case Card.QUEEN:
				name += Card.QUEEN_STRING;
				break;
			case Card.KING:
				name += Card.KING_STRING;
				break;
			default:
				name += value;
				break;
		}

		name += " of ";

		switch (suit)
		{
			case Card.SPADES:
				name += Card.SPADES_STRING;
				break;
			case Card.CLUBS:
				name += Card.CLUBS_STRING;
				break;
			case Card.DIAMONDS:
				name += Card.DIAMONDS_STRING;
				break;
			case Card.HEARTS:
				name += Card.HEARTS_STRING;
				break;
		}

		return name;
	}

	// Private Functions
	private void renderCard()
	{
		if(this.isFaceDown)
		{
			foreach (SpriteRenderer sr in gameObject.GetComponentsInChildren<SpriteRenderer>())
			{
				if (sr.gameObject.name == "CardBase")
				{
					sr.sprite = this.sprites[System.Array.IndexOf(this.spriteNames, "CardBack")];
				}
				else
				{
					sr.sprite = null;
				}
			}
		}
		else
		{
			Sprite suitSprite = this.GetSuitSprite(suit);
			Sprite valueSprite = this.GetValueSprite(suit, value);
			
			foreach (SpriteRenderer sr in gameObject.GetComponentsInChildren<SpriteRenderer>())
			{
				if (sr.gameObject.name == "CardBase")
				{
					sr.sprite = this.sprites[System.Array.IndexOf(this.spriteNames, "CardBase")];
				}
				else
				{
					if (sr.gameObject.name == "CardValue" || sr.gameObject.name == "CardValue-Inverted")
					{
						sr.sprite = valueSprite;
					}
					else
					{
						sr.sprite = suitSprite;
					}
				}
			}
		}
	}
}
