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
        //告诉玩家可以扔炸弹了
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
    //主菜单进入游戏
    public void PlayGame()
    {
        //载入游戏时清零数据
        PlayerPrefs.SetInt("Score", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    //主菜单退出游戏
    public void QuitGame()
    {
        Application.Quit();
    }
    //主菜单跳转至MadeBybr0sy场景
    public void MadeBybr0sy()
    {
        SceneManager.LoadScene("brosy");
    }
    //主菜单进入Introduce场景
    public void Introduce()
    {
        SceneManager.LoadScene("Introduce");
    }
    //游戏时暂停
    public void PauseMenu()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    //游戏时退出暂停
    public void ExitPause()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    //是否退出界面弹出
    public void SureToExit()
    {
        PausePanel.SetActive(false);
        SureToExitPanel.SetActive(true);
    }
    //不确定退出
    public void NotSureButton()
    {
        SureToExitPanel.SetActive(false);
        PausePanel.SetActive(true);
    }
    //返回主菜单
    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
    //游戏音量调整
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("BGM", volume);
    }
    //按ESC键导出Pause菜单
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
    //显示炸弹按钮
    public void SetActiveBombButton(bool isHaveBomb)
    {
        BombDrop.gameObject.SetActive(isHaveBomb);
    }

    GameObject Bomb;
    //场景中显示炸弹
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
