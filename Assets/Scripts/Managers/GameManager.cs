using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const int kMaxActionCount = 10;

    [SerializeField]
    private TapManager _tapManager;
    [SerializeField]
    private SpawnManager _spawnManager;
    [SerializeField]
    private BannerAd _bannerAd;

    private IPlayer _player;

    private int _actionCount;

    private void Awake()
    {
        _actionCount = 0;
        _player = new PlayerController();

        _player.ACTION += Action;

        _spawnManager.SetProperty(_player);
        _tapManager.SetProperty(_player);
        
    }

    private void Start()
    {
        _spawnManager.SpawnAtStart();
    }

    private void Action()
    {
        _actionCount++;
        if (kMaxActionCount == _actionCount)
        {
            _actionCount = 0;
            _bannerAd.ShowBanner();
        }
    }
}
