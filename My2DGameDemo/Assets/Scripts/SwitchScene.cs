using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;
using System.Collections;

public class SwitchScene : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject SureToExitPanel;
    public AudioMixer audioMixer;
    public Button Pause;
    public Button BombDrop;
    public GameObject myBomb;
    public bool IsInPauseMenu = false;

    GameObject PlayerBottom;
    public void Start()
    {
        Pause.onClick.AddListener(PauseMenu);
        ActionManager.BombAction = new Action<bool>(SetActiveBombButton);
        //������ҿ�����ը����
        BombDrop.onClick.AddListener(SetActiveBomb);
        PlayerBottom = GameObject.Find("PlayerBottom");
    }
    private void Update()
    {
        ESCToGetPauseMenu();
        if (time > 0)
        {
            time -= Time.deltaTime ;
            Bomb.GetComponentInChildren<Text>().text = String.Format("{0:0}", time);
        }
    }
    //���˵�������Ϸ
    public void PlayGame()
    {
        //������Ϸʱ��������
        PlayerPrefs.SetInt("Score", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    //���˵��˳���Ϸ
    public void QuitGame()
    {
        Application.Quit();
    }
    //���˵���ת��MadeBybr0sy����
    public void MadeBybr0sy()
    {
        SceneManager.LoadScene("brosy");
    }
    //���˵�����Introduce����
    public void Introduce()
    {
        SceneManager.LoadScene("Introduce");
    }
    //��Ϸʱ��ͣ
    public void PauseMenu()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    //��Ϸʱ�˳���ͣ
    public void ExitPause()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    //�Ƿ��˳����浯��
    public void SureToExit()
    {
        PausePanel.SetActive(false);
        SureToExitPanel.SetActive(true);
    }
    //��ȷ���˳�
    public void NotSureButton()
    {
        SureToExitPanel.SetActive(false);
        PausePanel.SetActive(true);
    }
    //�������˵�
    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
    //��Ϸ��������
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("BGM", volume);
    }
    //��ESC������Pause�˵�
    public void ESCToGetPauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !IsInPauseMenu)
        {
            PauseMenu();
            IsInPauseMenu = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && IsInPauseMenu)
        {
            ExitPause();
            IsInPauseMenu = false;
        }
    }
    //��ʾը����ť
    public void SetActiveBombButton(bool isHaveBomb)
    {
        BombDrop.gameObject.SetActive(isHaveBomb);
    }

    GameObject Bomb;
    //��������ʾը��
    public void SetActiveBomb()
    {      
        Bomb = GameObject.Instantiate(myBomb, PlayerBottom.transform.position, myBomb.transform.rotation);
        //Bomb.SetActive(true);
        time = 3;
        StartCoroutine(BombExplode());
        SetActiveBombButton(false);
    }
    float time;
     private IEnumerator BombExplode()
    {
        yield return new WaitForSeconds(3);
        Bomb.GetComponent<CapsuleCollider2D>().enabled = true;
        StartCoroutine(BombDisappear());
    }

    private IEnumerator BombDisappear()
    {
        yield return new WaitForSeconds(0.5f);
        Bomb.SetActive(false);
    }


}
