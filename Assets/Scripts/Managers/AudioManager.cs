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
    }

    private void Update()
    {
        switch (currentState)
        {
            case GameState.StartGameplay:
                if (!startGameplayAudio.isPlaying)
                {
                    StartGameplayLoop();
                }
                break;

            case GameState.Gameplay:
                if (PlayerDefeated())
                {
                    TransitionToEndGameplay();
                }
                break;

            case GameState.BossFight:
                if (PlayerDefeated())
                {
                    TransitionToBossFightDefeat();
                }
                else if (BossHealthBelowThreshold(66))
                {
                    TransitionToBossSecondStage();
                }
                break;

            case GameState.BossSecondStage:
                if (PlayerDefeated())
                {
                    TransitionToBossFightDefeat();
                }
                else if (BossHealthBelowThreshold(33))
                {
                    TransitionToBossFinalStand();
                }
                break;

            case GameState.BossFinalStand:
                if (PlayerDefeated())
                {
                    TransitionToBossFightDefeat();
                }
                else if (BossHealthBelowThreshold(0))
                {
                    TransitionToBattleEnded();
                }
                break;

            case GameState.BattleEnded:
                // Handle any logic for the battle ended state
                break;

            case GameState.BossFightDefeat:
                // Handle any logic for the boss fight defeat state
                break;
        }
    }

    private void StartGameplayLoop()
    {
        // Start playing "Gameplay Loop" audio in a loop
        gameplayLoopAudio.loop = true;
        gameplayLoopAudio.Play();

        currentState = GameState.Gameplay;
    }

    private bool PlayerDefeated()
    {
        // Replace this with your own logic for player defeat condition
        return false;
    }

    private bool BossHealthBelowThreshold(int threshold)
    {
        // Replace this with your own logic to check if the boss health is below the threshold
        return false;
    }

    private void TransitionToEndGameplay()
    {
        // Fade out "Gameplay Loop" audio
        FadeOutAudio(gameplayLoopAudio);

        // Fade in "End Gameplay" audio
        FadeInAudio(endGameplayAudio);

        currentState = GameState.BattleEnded;
    }

    private void TransitionToBossFight()
    {
        // Fade out "Gameplay Loop" audio
        FadeOutAudio(gameplayLoopAudio);

        // Fade in "Boss Fight" audio
        FadeInAudio(bossFightAudio);

        currentState = GameState.BossFight;
    }

    private void TransitionToBossSecondStage()
    {
        // Fade out "Boss Fight" audio
        FadeOutAudio(bossFightAudio);

        // Fade in "Boss Second Stage" audio
        FadeInAudio(bossSecondStageAudio);

        currentState = GameState.BossSecondStage;
    }

    private void TransitionToBossFinalStand()
    {
        // Fade out "Boss Second Stage" audio
        FadeOutAudio(bossSecondStageAudio);

        // Fade in "Boss Final Stand" audio
        FadeInAudio(bossFinalStandAudio);

        currentState = GameState.BossFinalStand;
    }

    private void TransitionToBattleEnded()
    {
        // Fade out "Boss Final Stand" audio
        FadeOutAudio(bossFinalStandAudio);

        // Fade in "The Battle Has Ended" audio
        FadeInAudio(battleEndedAudio);

        currentState = GameState.BattleEnded;
    }

    private void TransitionToBossFightDefeat()
    {
        // Fade out the currently playing audio (either "Boss Fight", "Boss Second Stage", or "Boss Final Stand")
        FadeOutCurrentAudio();

        // Fade in "Boss Fight Defeat Start" audio
        FadeInAudio(bossFightDefeatStartAudio);

        currentState = GameState.BossFightDefeat;
    }

    private void FadeOutCurrentAudio()
    {
        switch (currentState)
        {
            case GameState.BossFight:
                FadeOutAudio(bossFightAudio);
                break;

            case GameState.BossSecondStage:
                FadeOutAudio(bossSecondStageAudio);
                break;

            case GameState.BossFinalStand:
                FadeOutAudio(bossFinalStandAudio);
                break;
        }
    }

    private void FadeOutAudio(AudioSource audioSource)
    {
        StartCoroutine(FadeOut(audioSource, 1f));
    }

    private void FadeInAudio(AudioSource audioSource)
    {
        StartCoroutine(FadeIn(audioSource, 1f));
    }

    private System.Collections.IEnumerator FadeOut(AudioSource audioSource, float fadeDuration)
    {
        float startVolume = audioSource.volume;
        float startTime = Time.time;

        while (audioSource.volume > 0)
        {
            float elapsedTime = Time.time - startTime;
            float t = elapsedTime / fadeDuration;

            audioSource.volume = Mathf.Lerp(startVolume, 0, t);

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    private System.Collections.IEnumerator FadeIn(AudioSource audioSource, float fadeDuration)
    {
        audioSource.volume = 0;
        audioSource.Play();

        float targetVolume = audioSource.volume;
        float startTime = Time.time;

        while (audioSource.volume < targetVolume)
        {
            float elapsedTime = Time.time - startTime;
            float t = elapsedTime / fadeDuration;

            audioSource.volume = Mathf.Lerp(0, targetVolume, t);

            yield return null;
        }

        audioSource.volume = targetVolume;
    }
}

