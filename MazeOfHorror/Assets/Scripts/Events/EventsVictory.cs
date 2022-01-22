using UnityEngine;

public class EventsVictory : MonoBehaviour
{
    private WinMenu _winMenu;

    private void Start()
    {
        _winMenu = gameObject.GetComponentInParent<WinMenu>();
    }

    public void MidleGold()
    {
        _winMenu.Gold();
    }

    public void EndGold()
    {
        _winMenu.EndGold();
    }

    public void MidleTreasure()
    {
        _winMenu.Treasure();
    }

    public void EndTreasure()
    {
        _winMenu.EndTreasure();
    }

    public void MidleLife()
    {
        _winMenu.Life();
    }

    public void EndLife()
    {
        _winMenu.EndLife();
    }

    public void MidleRecord()
    {
        _winMenu.MidleRecord();
    }

    public void EndRecord()
    {
        _winMenu.EndRecord();
    }

    public void EndKrest()
    {
        _winMenu.EndKrest();
    }
}
