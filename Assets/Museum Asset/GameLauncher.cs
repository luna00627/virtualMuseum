using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;

public class GameLauncher:MonoBehaviour{
    public NetworkRunner Runner;
    void Start(){
        Runner = NetworkRunner.GetRunnerForScene(SceneManager.GetActiveScene());
        StartGame();
    }
    async void StartGame(){
        await Runner.StartGame(new StartGameArgs(){
            GameMode=GameMode.Shared,
            SessionName="Museum",
            Scene=SceneManager.GetActiveScene().buildIndex,
        });
    }

    public void shutDownRunner(){
        Runner.Shutdown();
    }

}