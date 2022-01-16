using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private SaveProgress _saveProgress;
    [SerializeField] private ProgressMenuGUI _progressMenu;
    [SerializeField] private Text _textLevel;
    [SerializeField] private Text _textLife;
    [SerializeField] private Text _textGold;
    [SerializeField] private Text _textDimond;
    [SerializeField] private PlaySound _playSound;

    private int _level;
    private int _life;
    private int _gold;
    private int _dimond;

    public void UpdateDataBase()
    {
        _level = _saveProgress.LevelPlayer;
        _life = _saveProgress.LifePlayer;
        _gold = _saveProgress.AmountGold;
        _dimond = _saveProgress.AmountDimond;
    }

    public void BuyGold()
    {
        _gold += 1000;
        SaveAll();
    }

    public void BuyDimond()
    {
        _dimond += 10;
        SaveAll();
    }

    public void BuyEXP()
    {
        _progressMenu.SetProgressLevel(200);
        _level = _progressMenu.GetLevel();
        SaveAll();
    }

    public void ADSfromShop()
    {
        _gold += 500;
        _dimond += 5;
        _progressMenu.SetProgressLevel(100);
        _level = _progressMenu.GetLevel();
        SaveAll();
    }

    public void BuyMaxiPrice1Dollar()
    {
        _gold += 1000;
        _dimond += 10;
        _progressMenu.SetProgressLevel(200);
        _level = _progressMenu.GetLevel();
        SaveAll();
    }

    public void BuyMaxiPrice2Dollar()
    {
        _gold += 3000;
        _dimond += 30;
        _progressMenu.SetProgressLevel(500);
        _level = _progressMenu.GetLevel();
        SaveAll();
    }

    public void BuyMaxiPrice5Dollar()
    {
        _gold += 10000;
        _dimond += 100;
        _progressMenu.SetProgressLevel(1000);
        _level = _progressMenu.GetLevel();
        SaveAll();
    }

    public void BuyMaxiPrice50Dollar()
    {
        _gold += 1000000;
        _dimond += 10000;
        _progressMenu.SetProgressLevel(100000);
        _level = _progressMenu.GetLevel();
        SaveAll();
    }

    private void PlaySoundOkey()
    {
        _playSound.Play("Okey");
    }

    private void SaveAll()
    {
        _saveProgress.SetGold(_gold);
        _saveProgress.SetDimond(_dimond);
        _saveProgress.SetLevel(_level);
        _saveProgress.Save();
        PlaySoundOkey();
    }

    private void OnGUI()
    {
        _textLevel.text = $"{_level}";
        _textLife.text = $"{_life}";
        _textGold.text = $"{_gold}";
        _textDimond.text = $"{_dimond}";
    }


}
