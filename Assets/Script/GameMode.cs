using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public abstract class GameMode
{
    protected const bool Myself = true;
    protected const bool Other = false;
    public abstract CardType FindCardContent(int index);
    public abstract bool Contrast(int card1, int card2);
    public abstract void Settlement();
    public abstract bool GetController();
}

public class OnlineMode : GameMode
{
    public override CardType FindCardContent(int index)
    {
        throw new System.NotImplementedException();
    }

    public override bool Contrast(int card1, int card2)
    {
        throw new System.NotImplementedException();
    }

    public override void Settlement()
    {
        throw new System.NotImplementedException();
    }

    public override bool GetController()
    {
        throw new System.NotImplementedException();
    }
}

public class OfflineMode : GameMode
{
    private bool _controller;
    private readonly CardType[] _cards;

    private int _count = 0;
    public OfflineMode()
    {
        _cards = new CardType[24];
        _InitCards();
        _controller = Myself;
    }
    
    public override CardType FindCardContent(int index)
    {
        return _cards[index];
    }

    public override bool Contrast(int card1, int card2)
    {
        if (_cards[card1] == _cards[card2])
        {
            _count -= 2;
            return true;
        }

        return false;
    }

    public override void Settlement()
    {
        Poncho poncho = GameObject.FindWithTag("Poncho").GetComponent<Poncho>();
        poncho.ChangeColor();
        poncho.AddClick();
        _controller = !_controller;
    }

    public override bool GetController()
    {
        return _controller;
    }

    private void _InitCards(int defend = 2, int load = 3, int load3 = 1, int ricochet = 2, int shoot = 2,
        int shoot2 = 2)
    {
        _count = 0;
        if (defend + load + load3 + ricochet + shoot + shoot2 != 12) return;
        AddCard(CardType.Defend, defend*2);
        AddCard(CardType.Load, load*2);
        AddCard(CardType.Load3, load3*2);
        AddCard(CardType.Ricochet, ricochet*2);
        AddCard(CardType.Shoot, shoot*2);
        AddCard(CardType.Shoot2, shoot2*2);
        _RefleshCards();
    }
    
    private void _RefleshCards()
    {
        System.Random random = new System.Random();
        for (int i = 0; i < 23; i++)
        {
            int ra = random.Next(i, 24);
            // Debug.Log(ra);
            (_cards[i], _cards[ra]) = (_cards[ra], _cards[i]);
        }
    }

    private void AddCard(CardType cardType, int count)
    {
        for(int i=0; i<count; i++) _cards[_count++] = cardType;
    }
}
