using UnityEngine;
using UnityEngine.UI;
using Utils;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Text coinText;

    private void Awake() => Instance = this;

    private void Start()
    {
        coinText.text = PlayerPrefs.GetInt("Coin").ToMoneyFormat();
    }


    public void CoinCalculator(int coin)
    {
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + coin);
        coinText.text = PlayerPrefs.GetInt("Coin").ToMoneyFormat();
    }
}