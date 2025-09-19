using System;
using System.Media;
using System.Threading.Tasks;

namespace PACountdown.Windows.Services;

public class AudioService : IAudioService
{
    private readonly SoundPlayer _tickSoundPlayer;
    private readonly SoundPlayer _finalTickSoundPlayer;
    private DateTime _lastTickPlayTime = DateTime.MinValue;
    private readonly object _lock = new object();

    public AudioService()
    {
        // Use Windows system sounds or embed custom sounds
        _tickSoundPlayer = new SoundPlayer();
        _finalTickSoundPlayer = new SoundPlayer();

        // You can load custom sound files here:
        // _tickSoundPlayer.SoundLocation = "Resources/tick.wav";
        // _finalTickSoundPlayer.SoundLocation = "Resources/final.wav";
    }

    public void PlayTickSound()
    {
        var now = DateTime.Now;
        if (now.Subtract(_lastTickPlayTime).TotalMilliseconds < 1000)
            return;

        _lastTickPlayTime = now;

        Task.Run(() =>
        {
            try
            {
                lock (_lock)
                {
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception)
            {
                try { Console.Beep(800, 80); } catch { }
            }
        });
    }

    public void PlayFinalTickSound()
    {
        Task.Run(() =>
        {
            try
            {
                lock (_lock)
                {
                    SystemSounds.Exclamation.Play();
                }
            }
            catch (Exception)
            {
                try { Console.Beep(1000, 300); } catch { }
            }
        });
    }

    public void TestSound()
    {
        PlayTickSound();
    }
}