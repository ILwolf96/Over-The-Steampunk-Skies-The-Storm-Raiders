using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource startGameplayAudio;
    public AudioSource gameplayLoopAudio;
    public AudioSource endGameplayAudio;
    public AudioSource bossFightAudio;
    public AudioSource bossSecondStageAudio;
    public AudioSource bossFinalStandAudio;
    public AudioSource battleEndedAudio;
    public AudioSource bossFightDefeatStartAudio;
    public AudioSource bossFightDefeatLoopAudio;

    private enum GameState
    {
        StartGameplay,
        Gameplay,
        BossFight,
        BossSecondStage,
        BossFinalStand,
        BattleEnded,
        BossFightDefeat
    }

    private GameState currentState;

    private void Start()
    {
        currentState = GameState.StartGameplay;

        // Start playing the "Start Gameplay" audio
        startGameplayAudio.Play();

        // Start the coroutine to wait for the "Start Gameplay" audio to finish playing
        StartCoroutine(StartGameplayLoopCoroutine());
    }

    private System.Collections.IEnumerator StartGameplayLoopCoroutine()
    {
        // Wait for the "Start Gameplay" audio to finish playing
        while (startGameplayAudio.isPlaying)
        {
            yield return null;
        }

        // Start playing the "Gameplay Loop" audio in a loop
        gameplayLoopAudio.loop = true;
        gameplayLoopAudio.Play();

        currentState = GameState.Gameplay;
    }

    // Rest of the code remains unchanged...
}

