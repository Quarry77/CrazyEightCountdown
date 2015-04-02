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

	private int id;
	private int suit;
	private int value;
	private string name;

	public Card (int id)
	{
		this.id = id;
		this.suit = this.GetSuitFromId(id);
		this.value = this.GetValueFromId(id);
		this.name = this.GetNameFromAttributes(this.suit, this.value);
	}
	
	public Card (int suit, int value)
	{
		this.id = this.GetIdFromAttributes(suit, value);
		this.suit = suit;
		this.value = value;
		this.name = this.GetNameFromAttributes(this.suit, this.value);
	}

	public int GetId()
	{
		return this.id;
	}

	public void SetId(int id)
	{
		this.id = id;
		this.suit = this.GetSuitFromId(id);
		this.value = this.GetValueFromId(id);
		this.name = this.GetNameFromAttributes(this.suit, this.value);
	}
	
	public int GetSuit()
	{
		return this.suit;
	}
	
	public int GetValue()
	{
		return this.value;
	}
	
	public string GetName()
	{
		return this.name;
	}
	
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
}
