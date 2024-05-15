using UnityEngine;
using TMPro;
using System;

public class MenuLivesBox : MonoBehaviour
{
    [SerializeField] TMP_Text livesCount;
    [SerializeField] TMP_Text livesCooldown;

    [SerializeField] float livesCooldownTime;

    ulong lastTimeFreeLiveReceive;
    int lives;
    int maxLives;

    private void Start()
    {
        UpdateLives();
        CheckLivesOnRelog();
    }

    public void UpdateLives()
    {
        var xmlManager = new XmlManager();
        var saveFile = xmlManager.Load();

        lastTimeFreeLiveReceive = saveFile._lastTimeFreeLiveReceive;
        lives = saveFile._lives;
        maxLives = saveFile._maxLives;
        livesCount.text = lives.ToString();
    }

    private void Update()
    {
        HandleLives();
    }

    void CheckLivesOnRelog()
    {
        var xmlManager = new XmlManager();
        var saveFile = xmlManager.Load();

        if (lastTimeFreeLiveReceive != 0)
        {
            ulong diffInSeconds = ((ulong)DateTime.Now.Ticks - lastTimeFreeLiveReceive) / TimeSpan.TicksPerSecond;

            int relogLives = (int)(diffInSeconds / livesCooldownTime);

            lives += relogLives;
            if (lives > maxLives)
            {
                lives = maxLives;
            }
            saveFile._lives = lives;
            xmlManager.Save(saveFile);

            float deltaSecondsLeft = diffInSeconds % livesCooldownTime;

            if (relogLives >= 1)
            {
                lastTimeFreeLiveReceive = (ulong)DateTime.Now.Ticks - (ulong)(deltaSecondsLeft * TimeSpan.TicksPerSecond);
                saveFile._lastTimeFreeLiveReceive = lastTimeFreeLiveReceive;
                xmlManager.Save(saveFile);
            }

            livesCount.text = lives.ToString(); ;
        }
        else
        {
            saveFile._lastTimeFreeLiveReceive = lastTimeFreeLiveReceive;
            xmlManager.Save(saveFile);
        }

    }

    void HandleLives()
    {
        if (lives < maxLives)
        {
            ulong diffInSeconds = ((ulong)DateTime.Now.Ticks - lastTimeFreeLiveReceive) / TimeSpan.TicksPerSecond;
            float secondsLeft = livesCooldownTime - diffInSeconds;

            if (secondsLeft <= 0)
            {
                lives++;

                var xmlManager = new XmlManager();
                var saveFile = xmlManager.Load();
                saveFile._lives = lives;
                saveFile._lastTimeFreeLiveReceive = lastTimeFreeLiveReceive = (ulong)DateTime.Now.Ticks;
                xmlManager.Save(saveFile);

                livesCooldown.text = "";

                return;
            }

            string livesTimerValue = /*((int)secondsLeft / 3600).ToString() + "h " + */
                ((int)secondsLeft / 60).ToString("00") + ":"
                + (secondsLeft % 60).ToString("00");


            livesCooldown.text = livesTimerValue;
            livesCount.text = lives.ToString();
        }
        else
        {
            livesCooldown.text = "FULL";
        }
    }

    public void MinusLive()
    {
        if (lives > 0)
        {
            var xmlManager = new XmlManager();
            var saveFile = xmlManager.Load();

            lives--;

            saveFile._lives = lives;
            saveFile._lastTimeFreeLiveReceive = lastTimeFreeLiveReceive = (ulong)DateTime.Now.Ticks;
            xmlManager.Save(saveFile);
        }
    }

    public void PlusLive()
    {
        if (lives < maxLives)
        {
            var xmlManager = new XmlManager();
            var saveFile = xmlManager.Load();

            lives++;

            saveFile._lives = lives;
            saveFile._lastTimeFreeLiveReceive = lastTimeFreeLiveReceive = (ulong)DateTime.Now.Ticks;
            xmlManager.Save(saveFile);

            livesCount.text = lives.ToString();
        }
    }

    void OnApplicationQuit()
    {
        //var xmlManager = new XmlManager();
        //var saveFile = xmlManager.Load();
        //saveFile._lives = lives;
        //saveFile._lastTimeFreeLiveReceive = lastTimeFreeLiveReceive = (ulong)DateTime.Now.Ticks;
        //xmlManager.Save(saveFile);
    }
}
