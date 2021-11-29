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

    private void Start()
    {
        _controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControler>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            switch (nameObject)
            {
                case Name.Gold:
                    _controller.MinusGold();
                    
                    break;
                case Name.Key:
                    _controller.MinusKeys();
                    
                    break;
                case Name.Map:
                    _controller.MapCollect();
                    
                    break;
                case Name.Treasure:
                    _controller.TreasureCollection();
                    
                    break;
            }
            Destroy(gameObject);
        }
    }

}
