using System;

[Serializable]
public class Actor
{
    public short CardId; // #1 through #N for each card in deck of length N
    public string Origin;
    public string Class;
    public string Name;
    public Attribute Health;
    public Attribute Defense;
    public Attribute Attack;
    public Attribute Speed;
    public Attribute Precision;
    public Attribute ManaCost;
    public string Rarity;
    public short CharacterId;

    public Actor()
    {
        // explicit empty constructor
    }

    public Actor
    (
        short cardId,
        string origin,
        string @class,
        string name,
        Attribute health,
        Attribute defense,
        Attribute attack,
        Attribute speed,
        Attribute precision,
        Attribute manaCost,
        string rarity,
        short characterId
    )
    {
        CardId = cardId;
        Origin = origin;
        Class = @class;
        Name = name;
        Health = health;
        Defense = defense;
        Attack = attack;
        Speed = speed;
        Precision = precision;
        ManaCost = manaCost;
        Rarity = rarity;
        CharacterId = characterId;
    }
}

