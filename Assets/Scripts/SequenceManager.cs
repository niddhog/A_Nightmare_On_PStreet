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

    private void Awake()
    {
        levelManager = GameObject.Find("GameHandler").GetComponent<LevelManager>();
        pause = GameObject.Find("GameHandler").GetComponent<PauseScript>();
        audioManager = GameObject.Find("GameHandler").GetComponent<AudioManager>();
    }

    public IEnumerator StartSequence(int level, int phase)
    {
        pause.PauseGame();
        if (level == 1)
        {
            if (phase == 1)
            {
                audioManager.zombies.Stop();
                audioManager.sirene.Play();
                yield return new WaitForSeconds(1f);

                GameObject sireneLight = Instantiate(sirenePrefab, new Vector3(0, 0, -3), Quaternion.identity);
                sireneLight.name = "sireneLight";
                sireneLight.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);

                yield return new WaitForSeconds(1f);

                GameObject textBox = Instantiate(textBoxPrefab, new Vector3(0, 0, -5), Quaternion.identity);
                textBox.name = "textbox";
                textBox.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);

                yield return new WaitForSeconds(1f);

                GameObject midSequenceText = Instantiate(sequenceTextPrefab, new Vector3(0, 0, -6), Quaternion.identity);
                midSequenceText.name = "midSequenceText";
                midSequenceText.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);

                yield return new WaitForSeconds(4f);
                audioManager.sirene.Stop();

                GameObject.Find("midSequenceText").GetComponent<Animator>().SetBool("finish", true);
                yield return new WaitForSeconds(0.5f);
                GameObject.Find("textbox").GetComponent<Animator>().SetBool("finish", true);
                GameObject.Find("sireneLight").GetComponent<Animator>().SetBool("finish", true);

                yield return new WaitForSeconds(1f);

                GameObject magic1 = Instantiate(darkMagicPrefab, new Vector3(-180,-45,70), Quaternion.identity);
                magic1.name = "magic1";
                magic1.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>(),false);
                audioManager.darkMagic01.Play();

                yield return new WaitForSeconds(0.5f);
                GameObject magic2 = Instantiate(darkMagicPrefab, new Vector3(-171, -37, 70), Quaternion.identity);
                magic2.name = "magic2";
                magic2.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>(), false);
                audioManager.darkMagic01.Play();

                yield return new WaitForSeconds(0.5f);
                GameObject magic3 = Instantiate(darkMagicPrefab, new Vector3(-183, -62, 70), Quaternion.identity);
                magic3.name = "magic2";
                magic3.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>(), false);
                audioManager.darkMagic01.Play();

                GameObject dracula = Instantiate(draculaPrefab, new Vector3(-183, -62, 70), Quaternion.identity);
                dracula.name = "dracula";
                dracula.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>(), false);
                audioManager.draculaLaugh.Play();
                batsParticleSystem = GameObject.Find("batParticles").GetComponent<ParticleSystem>();
                batsParticleSystem.Stop();
                yield return new WaitForSeconds(0.5f);
                batsParticleSystem.Play();
                audioManager.batsFlying.Play();
                yield return new WaitForSeconds(1.5f);
                batsParticleSystem.Stop();
                audioManager.batsFlying.Stop();


            }
            else if (phase == 2)
            {

            }
            else if (phase == 3)
            {

            }
        }
        yield return new WaitForSeconds(3f);
        audioManager.zombies.Play();
        pause.UnpauseGame();
        levelManager.FinishSequence();
    }
}
