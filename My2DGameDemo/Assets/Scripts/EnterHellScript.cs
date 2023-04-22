using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterHellScript : MonoBehaviour
{
    public GameObject EnterEDialog;

    private bool PlayerIsHere = false;
    private PlayerMove Player;
    private void Update()
    {
        if (PlayerIsHere && Input.GetKeyDown(KeyCode.E))
        {
            if (Player.Count > PlayerPrefs.GetInt("MaxScore"))
            {
                PlayerPrefs.SetInt("MaxScore", Player.Count);
            }
            PlayerPrefs.SetInt("Score", Player.Count);
            SceneManager.LoadScene("Hell");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            EnterEDialog.SetActive(true);
            Player = collision.gameObject.GetComponent<PlayerMove>();
        }
        PlayerIsHere = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            EnterEDialog.SetActive(false);
        }
        PlayerIsHere = false;
    }
}
