using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour
{

    [HideInInspector]
    public float SpeedMultiplier = 1;

    public float maxHealth = 1000;
    public float health = 1000;
    public int bagsEaten = 0;
    public float itemSpeed;
    public int difficulty;
    public Rigidbody2D Sköldpadda;
    public bool dead = false;
    public TMPro.TMP_Text bagscountText;
    public float mastervolume = 100;
    public AudioSource Musicsource;
    public float score;
    public float highScore = 0;
    public PlayerController playerController;

    public AudioClip[] sounds;

    public void ResetGame()
    {
        dead = false;
        bagsEaten = 0;
        health = maxHealth;
    }

    void Start()
    {
        Debug.Log(Application.persistentDataPath + @"\hs.dat");
        DontDestroyOnLoad(gameObject);
        StartCoroutine(StartUpdateSecond());
        StartCoroutine(CountSeconds());
        highScore = LoadHighScore();
    }

    IEnumerator StartUpdateSecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            health -= 10 + bagsEaten * 10;
        }
    }

    public void PlaySound(AudioClip clip, float volume = 0.8f)
    {
        StartCoroutine(PlaySoundCoroutine(clip, volume * (mastervolume/100)));
    }

    IEnumerator PlaySoundCoroutine(AudioClip clip, float volume)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.Play();
        yield return new WaitForSeconds(clip.length);
        Destroy(source);
    }

    public IEnumerator CountSeconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            score += 0.1f;
        }
    }

    public void SaveHighScore()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(Application.persistentDataPath + @"\hs.dat", FileMode.Create);

            Save s = new Save();
            s.HighScore = highScore;

            bf.Serialize(fs, s);
            fs.Close();
        }
        catch
        {

        }

        
    }

    public float LoadHighScore()
    {
        try
        {
            if (File.Exists(Application.persistentDataPath + @"\hs.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = new FileStream(Application.persistentDataPath + @"\hs.dat", FileMode.Open);

                Save s = bf.Deserialize(fs) as Save;
                fs.Close();

                return s.HighScore;
            }
            else
            {
                SaveHighScore();
                return highScore;
            }
        }
        catch
        {
            return 0;
        }
        
    }

    public void KillAllPlasticBags()
    {
        foreach (GameObject bag in GameObject.FindGameObjectsWithTag("PlasticBags"))
        {
            Destroy(bag);
        }
    }

}

[System.Serializable]
class Save
{
    public float HighScore;
}
