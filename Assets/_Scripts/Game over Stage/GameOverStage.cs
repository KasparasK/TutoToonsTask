using UnityEngine;

public class GameOverStage : MonoBehaviour , IStage
{
    public GameOverScreen gameOverScreen;
    public void Initialize()
    {
        gameOverScreen.Show();
    }
    public void Finish()
    {
        gameOverScreen.Hide();

    }

}
