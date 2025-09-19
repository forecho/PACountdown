namespace PACountdown.Windows.Services;

public interface IAudioService
{
    void PlayTickSound();
    void PlayFinalTickSound();
    void TestSound();
}