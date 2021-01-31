using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    public GameObject darkMagicPrefab;
    public GameObject textBoxPrefab;
    public GameObject sequenceTextPrefab;
    public GameObject sirenePrefab;
    public GameObject draculaPrefab;

    private LevelManager levelManager;
    private PauseScript pause;
    private AudioManager audioManager;
    private ParticleSystem batsParticleSystem;
    private CameraController cameraManager;
    private EnemySpawnScript enemySpawnManager;
    private TextBubbleManager textBubbleManager;
    private EnvironmentManager environmentManager;

    private void Awake()
    {
        levelManager = GameObject.Find("GameHandler").GetComponent<LevelManager>();
        pause = GameObject.Find("GameHandler").GetComponent<PauseScript>();
        audioManager = GameObject.Find("GameHandler").GetComponent<AudioManager>();
        cameraManager = GameObject.Find("Main Camera").GetComponent<CameraController>();
        enemySpawnManager = GameObject.Find("GameHandler").GetComponent<EnemySpawnScript>();
        textBubbleManager = GameObject.Find("GameHandler").GetComponent<TextBubbleManager>();
        environmentManager = GameObject.Find("GameHandler").GetComponent<EnvironmentManager>();
    }


    public IEnumerator StartSequence(int level, int phase)
    {
        pause.PauseGame();
        yield return new WaitForSeconds(0.5f);
        if (level == 1)
        {
            if (phase == 1)
            {
                environmentManager.ChangeFogIntensityOverTime(2, 1);
                audioManager.zombies.Stop();
                audioManager.FadeIn(audioManager.sirene, 0.25f);
                yield return new WaitForSeconds(0.25f);
                GameObject sireneLight = Instantiate(sirenePrefab, new Vector3(0, 0, -3), Quaternion.identity);
                sireneLight.name = "sireneLight";
                sireneLight.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);

                yield return new WaitForSeconds(1f);
                GameObject midSequenceText = Instantiate(sequenceTextPrefab, new Vector3(0, 0, -6), Quaternion.identity);
                midSequenceText.name = "midSequenceText";
                midSequenceText.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);

                GameObject textBox = Instantiate(textBoxPrefab, new Vector3(0, 0, -5), Quaternion.identity);
                textBox.name = "textbox";
                textBox.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
                audioManager.spinAmbient.Play();
                yield return new WaitForSeconds(0.6f);
                yield return new WaitForSeconds(0.3f);
                cameraManager.Shake(0.3f, 1, 50);

                yield return new WaitForSeconds(3f);
                audioManager.FadeOut(audioManager.sirene, 2);

                GameObject.Find("midSequenceText").GetComponent<Animator>().SetBool("finish", true);
                yield return new WaitForSeconds(0.5f);
                GameObject.Find("textbox").GetComponent<Animator>().SetBool("finish", true);
                GameObject.Find("sireneLight").GetComponent<Animator>().SetBool("finish", true);

                yield return new WaitForSeconds(1f);
                GameObject magic1 = Instantiate(darkMagicPrefab, new Vector3(-180,-45,69), Quaternion.identity);
                magic1.name = "magic1";
                magic1.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>(),false);
                audioManager.darkMagic01.Play();

                yield return new WaitForSeconds(0.5f);
                GameObject magic2 = Instantiate(darkMagicPrefab, new Vector3(-171, -37, 69), Quaternion.identity);
                magic2.name = "magic2";
                magic2.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>(), false);
                audioManager.darkMagic01.Play();

                yield return new WaitForSeconds(0.5f);
                GameObject magic3 = Instantiate(darkMagicPrefab, new Vector3(-183, -62, 69), Quaternion.identity);
                magic3.name = "magic2";
                magic3.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>(), false);
                audioManager.darkMagic01.Play();
                audioManager.FadeIn(audioManager.Level1Music, 1);

                environmentManager.ChangeFogColor(1, 181, 13, 13);
                GameObject dracula = Instantiate(draculaPrefab, new Vector3(-183, -62, 70), Quaternion.identity);
                dracula.name = "dracula";
                dracula.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>(), false);
                audioManager.draculaLaugh.Play();
                batsParticleSystem = GameObject.Find("batParticles").GetComponent<ParticleSystem>();
                batsParticleSystem.Stop();
                
                yield return new WaitForSeconds(0.5f);

                ParticleSystem.MainModule psMain = batsParticleSystem.main;
                psMain.startLifetime = 25f;
                batsParticleSystem.Play();
                audioManager.batsFlying.Play();
                yield return new WaitForSeconds(1.7f);
                yield return new WaitForSeconds(1);
                textBubbleManager.DisplayBubble(textBubbleManager.fireBubblePrefab, new Vector3(31.9f, -22.5f, 100),2);
                yield return new WaitForSeconds(0.5f);
                enemySpawnManager.DraculaSpawnBat();
                yield return new WaitForSeconds(0.2f);
                batsParticleSystem.Stop();
                audioManager.batsFlying.Stop();
                psMain.startLifetime = 13f;
                environmentManager.ChangeFogIntensityOverTime(1, 0.5f);
                environmentManager.ChangeFogColor(1, 255, 255, 255);
            }

            else if (phase == 2)
            {
                audioManager.zombies.Stop();
                environmentManager.ChangeFogIntensityOverTime(2, 1);
                environmentManager.ChangeFogColor(1, 181, 13, 13);
                GameObject magic1 = Instantiate(darkMagicPrefab, new Vector3(-180, -45, 69), Quaternion.identity);
                magic1.name = "magic1";
                magic1.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>(), false);
                audioManager.darkMagic01.Play();

                yield return new WaitForSeconds(0.5f);
                GameObject magic2 = Instantiate(darkMagicPrefab, new Vector3(-171, -37, 69), Quaternion.identity);
                magic2.name = "magic2";
                magic2.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>(), false);
                audioManager.darkMagic01.Play();

                yield return new WaitForSeconds(0.5f);
                GameObject magic3 = Instantiate(darkMagicPrefab, new Vector3(-183, -62, 69), Quaternion.identity);
                magic3.name = "magic2";
                magic3.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>(), false);
                audioManager.darkMagic01.Play();

                GameObject dracula = Instantiate(draculaPrefab, new Vector3(-183, -62, 70), Quaternion.identity);
                dracula.name = "dracula";
                dracula.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>(), false);
                dracula.GetComponent<Animator>().SetBool("phase2", true);
                batsParticleSystem = GameObject.Find("batParticles").GetComponent<ParticleSystem>();
                batsParticleSystem.Stop();
                audioManager.countDracula01.Play();
                audioManager.Level1Music.volume = 0.25f;
                yield return new WaitForSeconds(5.5f);
                audioManager.draculaLaugh.Play();
                audioManager.Level1Music.volume = 0.5f;
                yield return new WaitForSeconds(0.5f);
                textBubbleManager.DisplayBubble(textBubbleManager.fireBubblePrefab, new Vector3(31.9f, -22.5f, 100), 2);
                for (int i = 0; i < 3; i++)
                {
                    enemySpawnManager.DraculaSpawnBat();
                    yield return new WaitForSeconds(0.2f);
                }
                environmentManager.ChangeFogIntensityOverTime(1, 0.5f);
                environmentManager.ChangeFogColor(1, 255, 255, 255);
            }

            else if (phase == 3)
            {
                audioManager.zombies.Stop();
                GameObject magic1 = Instantiate(darkMagicPrefab, new Vector3(-180, -45, 69), Quaternion.identity);
                magic1.name = "magic1";
                magic1.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>(), false);
                audioManager.darkMagic01.Play();

                yield return new WaitForSeconds(0.5f);
                GameObject magic2 = Instantiate(darkMagicPrefab, new Vector3(-171, -37, 69), Quaternion.identity);
                magic2.name = "magic2";
                magic2.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>(), false);
                audioManager.darkMagic01.Play();

                yield return new WaitForSeconds(0.5f);
                GameObject magic3 = Instantiate(darkMagicPrefab, new Vector3(-183, -62, 69), Quaternion.identity);
                magic3.name = "magic2";
                magic3.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>(), false);
                audioManager.darkMagic01.Play();

                cameraManager.Shake(3.5f, 1, 50);
                environmentManager.ChangeFogIntensityOverTime(1, 1f);
                environmentManager.ChangeFogColor(1, 0, 0, 0);
                
                GameObject dracula = Instantiate(draculaPrefab, new Vector3(-183, -62, 70), Quaternion.identity);
                dracula.name = "dracula";
                dracula.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>(), false);
                dracula.GetComponent<DraculaBossBehaviour>().PlaceBossBar();
                dracula.GetComponent<DraculaBossBehaviour>().SetLifeBar();
                dracula.GetComponent<Animator>().SetBool("phase3", true);
                batsParticleSystem = GameObject.Find("batParticles").GetComponent<ParticleSystem>();
                batsParticleSystem.Stop();
                dracula.GetComponent<DraculaBossBehaviour>().EnableOrbParticles();
                yield return new WaitForSeconds(0.5f);
                audioManager.countDraculaEndYou.Play();
                audioManager.Level1Music.volume = 0.25f;
                yield return new WaitForSeconds(3f);
                environmentManager.ChangeFogIntensityOverTime(1, 0.5f);
                environmentManager.ChangeFogColor(1, 181, 13, 13);
                textBubbleManager.DisplayBubble(textBubbleManager.fireBubblePrefab, new Vector3(31.9f, -22.5f, 100), 2);
                dracula.GetComponent<DraculaBossBehaviour>().StartDraculaBossFight();
                audioManager.IncreaseVolumeOverTime(audioManager.Level1Music, 1, 0.75f);
            }

            else if (phase == 4) //Boss has been defeated
            {
                Debug.Log("OutroDracula");
                yield return new WaitForSeconds(30f);
            }
        }
        yield return new WaitForSeconds(0.2f);
        audioManager.zombies.Play();
        pause.UnpauseGame();
        levelManager.FinishSequence();
    }
}
