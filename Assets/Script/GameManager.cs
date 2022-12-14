using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class GameManager
{
    private bool _online;
    private GameMode _gameMode = new OfflineMode();
    private static GameManager _instance;
    private int _count = 0;
    private int[] _selectIndex = new int[2];
    private MonoStub _monoStub;
    private GameManager()
    {
        _monoStub = new GameObject().AddComponent<MonoStub>();
    }
    public static GameManager Instance()
    {
        return _instance ??= new GameManager();
    }

    public CardType FindCardContent(int index)
    {
        CardType res = _gameMode.FindCardContent(index);
        _selectIndex[_count] = index;
        _count++;
        if (_count == 2)
        {
            _count = 0;
            _monoStub.StartCoroutine(Settlement());
        }
        return res;
    }

    public bool Contrast(int card1, int card2)
    {
        return _gameMode.Contrast(card1, card2);
    }
    
    IEnumerator Settlement()
    {
        yield return new WaitForSeconds(1);
        
        EventHandler.CallCardSettlement(Contrast(_selectIndex[0], _selectIndex[1]));

        yield return new WaitForSeconds(0.3f);
        
        _gameMode.Settlement();

    }

    public bool ControllerIsMyself()
    {
        return _gameMode.GetController();
    }
}