using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;

public class GameLauncherShopping:MonoBehaviour{
    public NetworkRunner Runner;
    void Start(){
        Runner = NetworkRunner.GetRunnerForScene(SceneManager.GetActiveScene());
        StartGame();
    }
    async void StartGame(){
        await Runner.StartGame(new StartGameArgs(){
            GameMode=GameMode.Shared,
            SessionName="Shopping",
            Scene=SceneManager.GetActiveScene().buildIndex,
        });
    }

    public void shutDownRunner(){
        Runner.Shutdown();
    }

}