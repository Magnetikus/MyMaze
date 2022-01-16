using UnityEngine;

public class Collection : MonoBehaviour
{

    public enum Name
    {
        Key,
        Gold,
        Map,
        Treasure
    }

    public Name nameObject;

    private GameControler _controller;
    private PlaySound _playSound;

    private void Start()
    {
        _controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControler>();
        _playSound = _controller.GetComponent<PlaySound>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            switch (nameObject)
            {
                case Name.Gold:
                    _controller.MinusGold();
                    _playSound.Play("Gold");
                    break;
                case Name.Key:
                    _controller.MinusKeys();
                    _playSound.Play("Key");
                    break;
                case Name.Map:
                    _controller.MapCollect();
                    _playSound.Play("Map");
                    break;
                case Name.Treasure:
                    _controller.TreasureCollection();
                    _playSound.Play("Treasure");
                    break;
            }
            Destroy(gameObject);
        }
    }

}
