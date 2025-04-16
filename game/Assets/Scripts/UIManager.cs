using System.Collections;
using UnityEngine;
using Hyb.Utils;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : ManualSingletonMono<UIManager>
{
    //Transition
    [SerializeField] private GameObject transition;

    //Menu
    [SerializeField] private GameObject menu;

    //Game
    [SerializeField] private GameObject game;
    [SerializeField] private GameObject loss;
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject pass;

    //public Joystick moveStick;
    public Button ControlLeft;
    public Button ControlRight;
    public Button ControlUp;
    public Button ControlDown;
    public void MenuScene()
    {
        this.menu.SetActive(true);
        this.game.SetActive(false);
        this.loss.SetActive(false);
        this.pause.SetActive(false);
        this.pass.SetActive(false);
    }

    public void GameScene()
    {

        this.menu.SetActive(false);
        this.game.SetActive(true);
        this.loss.SetActive(false);
        this.pause.SetActive(false);
        this.pass.SetActive(false);

    }

    public void LossScene()
    {
        this.loss.SetActive(true);
    }

    public void PauseScene()
    {
        this.pause.SetActive(true);
    }

    public void PassScene()
    {
        this.pass.SetActive(true);

    }

    public IEnumerator OpenLoadScene()
    {

        GameManager.Instance.state = GameManager.GameState.Loading;
        this.transition.GetComponent<Animator>().Play("EndTransition");
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator CloseLoadScene(GameManager.GameState state)
    {
        GameManager.Instance.state = state;
        this.transition.GetComponent<Animator>().Play("StartTransition");
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator WaitLoadScene(string sceneName, GameManager.GameState state)
    {
        yield return StartCoroutine(OpenLoadScene());
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            yield return null;
        }
        yield return StartCoroutine(CloseLoadScene(state));
    }

    public IEnumerator LoadScene(string sceneName, GameManager.GameState state)
    {

        yield return StartCoroutine(WaitLoadScene(sceneName, state));
    }

    public void StartCoroutineWrapper(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }




}
