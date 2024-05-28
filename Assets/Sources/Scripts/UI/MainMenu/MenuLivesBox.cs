using UnityEngine;
using TMPro;
using System;

public class MenuLivesBox : MonoBehaviour
{
    [SerializeField] TMP_Text livesCount;
    [SerializeField] TMP_Text livesCooldown;

    [SerializeField] float livesCooldownTime;

    public int Lives { get; private set; }

    ulong lastTimeFreeLiveReceive;
    int maxLives;

    private void Start()
    {
        var xmlManager = new XmlManager();
        var saveFile = xmlManager.Load();

        lastTimeFreeLiveReceive = saveFile._lastTimeFreeLiveReceive;
        Lives = saveFile._lives;
        maxLives = saveFile._maxLives;

        UpdateLives(Lives);
        CheckLivesOnRelog();
    }

    public void UpdateLives(int lives)
    {
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

            Lives += relogLives;
            if (Lives > maxLives)
            {
                Lives = maxLives;
            }
            saveFile._lives = Lives;
            xmlManager.Save(saveFile);

            float deltaSecondsLeft = diffInSeconds % livesCooldownTime;

            if (relogLives >= 1)
            {
                lastTimeFreeLiveReceive = (ulong)DateTime.Now.Ticks - (ulong)(deltaSecondsLeft * TimeSpan.TicksPerSecond);
                saveFile._lastTimeFreeLiveReceive = lastTimeFreeLiveReceive;
                xmlManager.Save(saveFile);
            }

            livesCount.text = Lives.ToString(); ;
        }
        else
        {
            saveFile._lastTimeFreeLiveReceive = lastTimeFreeLiveReceive;
            xmlManager.Save(saveFile);
        }

    }

    void HandleLives()
    {
        if (Lives < maxLives)
        {
            ulong diffInSeconds = ((ulong)DateTime.Now.Ticks - lastTimeFreeLiveReceive) / TimeSpan.TicksPerSecond;
            float secondsLeft = livesCooldownTime - diffInSeconds;

            if (secondsLeft <= 0)
            {
                Lives++;

                var xmlManager = new XmlManager();
                var saveFile = xmlManager.Load();
                saveFile._lives = Lives;
                saveFile._lastTimeFreeLiveReceive = lastTimeFreeLiveReceive = (ulong)DateTime.Now.Ticks;
                xmlManager.Save(saveFile);

                livesCooldown.text = "";

                return;
            }

            string livesTimerValue = /*((int)secondsLeft / 3600).ToString() + "h " + */
                ((int)secondsLeft / 60).ToString("00") + ":"
                + (secondsLeft % 60).ToString("00");


            livesCooldown.text = livesTimerValue;
            livesCount.text = Lives.ToString();
        }
        else
        {
            livesCooldown.text = "FULL";
        }
    }

    public void MinusLive()
    {
        if (Lives > 0)
        {
            var xmlManager = new XmlManager();
            var saveFile = xmlManager.Load();

            Lives--;

            saveFile._lives = Lives;
            saveFile._lastTimeFreeLiveReceive = lastTimeFreeLiveReceive = (ulong)DateTime.Now.Ticks;
            xmlManager.Save(saveFile);
        }
    }

    public void PlusLive()
    {
        if (Lives < maxLives)
        {
            var xmlManager = new XmlManager();
            var saveFile = xmlManager.Load();

            Lives++;

            saveFile._lives = Lives;
            saveFile._lastTimeFreeLiveReceive = lastTimeFreeLiveReceive = (ulong)DateTime.Now.Ticks;
            xmlManager.Save(saveFile);

            UpdateLives(Lives);
        }
    }
}
