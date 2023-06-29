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
    public AudioSource bossFightDefeatStartAudio;
    public AudioSource bossFightDefeatLoopAudio;
    public AudioSource battleEndedAudio;

    public float bossFightHealthThreshold = 66f;
    public float bossSecondStageHealthThreshold = 33f;

    private enum GameState
    {
        Gameplay,
        BossFight,
        BossSecondStage,
        BossFinalStand,
        BossFightDefeat
    }

    private GameState currentState = GameState.Gameplay;

    private void Start()
    {
        // Start playing the "Start Gameplay" audio
        startGameplayAudio.Play();
    }

    private void Update()
    {
        switch (currentState)
        {
            case GameState.Gameplay:
                // Check if "Start Gameplay" audio has finished playing
                if (!startGameplayAudio.isPlaying)
                {
                    // Start playing "Gameplay Loop" audio in a loop
                    gameplayLoopAudio.Play();
                }

                // Check if player is defeated
                // Replace this with your own logic for player defeat condition
                if (PlayerDefeated())
                {
                    // Transition to end gameplay state
                    TransitionToEndGameplay();
                }
                break;

            case GameState.BossFight:
                // Check if player is defeated during boss fight
                if (PlayerDefeated())
                {
                    // Transition to boss fight defeat state
                    TransitionToBossFightDefeat();
                }
                else if (BossHealthBelowThreshold(bossFightHealthThreshold))
                {
                    // Transition to boss second stage
                    TransitionToBossSecondStage();
                }
                break;

            case GameState.BossSecondStage:
                // Check if player is defeated during boss second stage
                if (PlayerDefeated())
                {
                    // Transition to boss fight defeat state
                    TransitionToBossFightDefeat();
                }
                else if (BossHealthBelowThreshold(bossSecondStageHealthThreshold))
                {
                    // Transition to boss final stand
                    TransitionToBossFinalStand();
                }
                break;

            case GameState.BossFinalStand:
                // Check if player is defeated during boss final stand
                if (PlayerDefeated())
                {
                    // Transition to boss fight defeat state
                    TransitionToBossFightDefeat();
                }
                else if (BossHealthBelowThreshold(0f))
                {
                    // Transition to battle ended state
                    TransitionToBattleEnded();
                }
                break;

            case GameState.BossFightDefeat:
                // Check if "Boss Fight Defeat Start" audio has finished playing
                if (!bossFightDefeatStartAudio.isPlaying)
                {
                    // Start playing "Boss Fight Defeat Loop" audio in a loop
                    bossFightDefeatLoopAudio.Play();
                }
                break;
        }
    }

    private bool PlayerDefeated()
    {
        // Replace this with your own logic for player defeat condition
        return false;
    }

    private bool BossHealthBelowThreshold(float threshold)
    {
        // Replace this with your own logic to check boss health
        return false;
    }

    private void TransitionToEndGameplay()
    {
        // Fade out "Gameplay Loop" audio
        FadeOutAudio(gameplayLoopAudio);

        // Fade in "End Gameplay" audio
        FadeInAudio(endGameplayAudio);

        currentState = GameState.Gameplay;
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

        currentState = GameState.Gameplay;
    }

    private void TransitionToBossFightDefeat()
    {
        // Fade out currently playing audio
        FadeOutCurrentAudio();

        // Start playing "Boss Fight Defeat Start" audio
        bossFightDefeatStartAudio.Play();

        currentState = GameState.BossFightDefeat;
    }

    private void FadeOutAudio(AudioSource audioSource)
    {
        StartCoroutine(FadeOut(audioSource, 1f));
    }

    private void FadeInAudio(AudioSource audioSource)
    {
        StartCoroutine(FadeIn(audioSource, 1f));
    }

    private void FadeOutCurrentAudio()
    {
        switch (currentState)
        {
            case GameState.Gameplay:
                FadeOutAudio(gameplayLoopAudio);
                break;

            case GameState.BossFight:
                FadeOutAudio(bossFightAudio);
                break;

            case GameState.BossSecondStage:
                FadeOutAudio(bossSecondStageAudio);
                break;

            case GameState.BossFinalStand:
                FadeOutAudio(bossFinalStandAudio);
                break;

            case GameState.BossFightDefeat:
                FadeOutAudio(bossFightDefeatLoopAudio);
                break;
        }
    }

    private IEnumerator FadeOut(AudioSource audioSource, float fadeDuration)
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

    private IEnumerator FadeIn(AudioSource audioSource, float fadeDuration)
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
