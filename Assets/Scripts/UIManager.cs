using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;
    public Rigidbody PlayerRB;
    [SerializeField]
    private TMP_Text coinsText, distText, speedText;
    [SerializeField]
    private GameObject lossPanel;
    private int coins;

    private void OnEnable()
    {

        Instance = this;
        coins = PlayerPrefs.GetInt("Coins", 0);
        coinsText.text = "Coins: " + coins;
        distText.text = "Distance: " + 0;
        speedText.text = "Speed: " + 0;

    }

    private void FixedUpdate()
    {

        speedText.text = "Speed: " + (PlayerRB.velocity.z > 0.01f ? PlayerRB.velocity.z : 0);

    }

    private void OnApplicationQuit()
    {

        PlayerPrefs.SetInt("Coins", coins);

    }

    public void CollectedCoin()
    {

        coins++;
        coinsText.text = "Coins: " + coins;

    }

    public void DistanceUpdate(int _dist)
    {

        distText.text = "Distance: " + _dist;

    }

    public void Lost()
    {

        lossPanel.SetActive(true);

    }

    public void ReloadScene()
    {

        PlayerPrefs.SetInt("Coins", coins);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void PauseGame()
    {

        Time.timeScale = Time.timeScale > 0 ? 0 : 1;

    }

}
