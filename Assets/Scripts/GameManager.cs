using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    public int[,] values2048 = new int[4, 4];
    [SerializeField] private TextMeshProUGUI[] valueText = new TextMeshProUGUI[16];
    [SerializeField] private GameObject parentObject;
    [SerializeField] private Image gameOverImage;
    [SerializeField] private TextMeshProUGUI gameOverText;
    private float timer = 0;
    private bool moved = false;
    private int spawnCount = 1;
    [SerializeField] private TextMeshProUGUI spawnCountText;
    [SerializeField] private Slider spawnCountSlider;
    private void Start()
    {
        gameOverImage.CrossFadeAlpha(0, 0, false);
        ResetValues();


        //Find all the value text
        valueText = parentObject.GetComponentsInChildren<TextMeshProUGUI>();

        // Set Max Frame Rate to 60
        Application.targetFrameRate = 60;

        // Spawn 2 new values
        for (int i = 0; i < 2; i++)
        {
            SpawnNewValue();
        }
        UpdateBoard();
    }

    public void UpdateBoard()
    {
        //Update the board
        for (int i = 0; i < valueText.Length; i++)
        {
            valueText[i].text = values2048[i / 4, i % 4].ToString();
            if (values2048[i / 4, i % 4] == 0)
            {
                valueText[i].text = "";
            }
            else if (values2048[i / 4, i % 4] == 2)
            {
                valueText[i].color = new Color32(238, 228, 218, 255);
            }
            else if (values2048[i / 4, i % 4] == 4)
            {
                valueText[i].color = new Color32(237, 224, 200, 255);
            }
            else if (values2048[i / 4, i % 4] == 8)
            {
                valueText[i].color = new Color32(242, 177, 121, 255);
            }
            else if (values2048[i / 4, i % 4] == 16)
            {
                valueText[i].color = new Color32(245, 149, 99, 255);
            }
            else if (values2048[i / 4, i % 4] == 32)
            {
                valueText[i].color = new Color32(246, 124, 95, 255);
            }
            else if (values2048[i / 4, i % 4] == 64)
            {
                valueText[i].color = new Color32(246, 94, 59, 255);
            }
            else if (values2048[i / 4, i % 4] == 128)
            {
                valueText[i].color = new Color32(237, 207, 114, 255);
            }
            else if (values2048[i / 4, i % 4] == 256)
            {
                valueText[i].color = new Color32(237, 204, 97, 255);
            }
            else if (values2048[i / 4, i % 4] == 512)
            {
                valueText[i].color = new Color32(237, 200, 80, 255);
            }
            else if (values2048[i / 4, i % 4] == 1024)
            {
                valueText[i].color = new Color32(237, 197, 63, 255);
            }
            else if (values2048[i / 4, i % 4] == 2048)
            {
                valueText[i].color = new Color32(237, 194, 46, 255);
            }

        }
    }
    public void CheckInput(int Selected)
    {
        if (Selected == 1)
        {
            MoveUp(false);
        }
        else if (Selected == 2)
        {
            MoveDown(false);
        }
        else if (Selected == 3)
        {
            MoveLeft(false);
        }
        else if (Selected == 4)
        {
            MoveRight(false);
        }
        if (moved)
        {
            SpawnNewValue();
            moved = false;
        }
        else
        {
            MoveDown(true);
            MoveUp(true);
            MoveLeft(true);
            MoveRight(true);
            if (!moved)
            {
                gameOverImage.CrossFadeAlpha(1, 1, false);
                timer = 0.01f;
            }
            else
            {
                moved = false;
            }
        }
        UpdateBoard();
    }
    private void FixedUpdate()
    {
        if (timer > 0) { timer += Time.deltaTime; }
        if (timer > 1)
        {
            gameOverText.text = "Game Over!";
        }
        if (timer > 8)
        {
            timer = 0;
            gameOverText.text = "";
            gameOverImage.CrossFadeAlpha(0, 1, false);
            ResetValues();
            for (int i = 0; i < 2; i++)
            {
                spawnCount = 1;
                SpawnNewValue();
            }
            spawnCount = ((int)spawnCountSlider.value);
            UpdateBoard();
        }
    }
    private void SpawnNewValue()
    {
        // Spawn a new value
        // Check if there is a free space

        for (int k = 0; k < spawnCount; k++)
        {
            bool freeSpace = false;
            for (int i = 0; i < values2048.GetLength(0); i++)
            {
                for (int j = 0; j < values2048.GetLength(1); j++)
                {
                    if (values2048[i, j] == 0)
                    {
                        freeSpace = true;
                        break;
                    }

                }
            }
            if (freeSpace)
            {
                // Find a random free space
                int x = Random.Range(0, 4);
                int y = Random.Range(0, 4);
                while (values2048[x, y] != 0)
                {
                    x = Random.Range(0, 4);
                    y = Random.Range(0, 4);
                }
                // Spawn a new value
                values2048[x, y] = 2;

            }
        }
    }
    private void ResetValues()
    {
        // Set all values to 0
        for (int i = 0; i < values2048.GetLength(0); i++)
        {
            for (int j = 0; j < values2048.GetLength(1); j++)
            {
                values2048[i, j] = 0;
            }
        }
    }
    public void SpawnNewSlider()
    {
        spawnCount = ((int)spawnCountSlider.value);
        spawnCountText.text = "Spawn Count: " + spawnCount;
    }


    // Move Methods (MoveLeft, MoveRight, MoveUp, MoveDown)
    // Might be able to combine them into one method (optimization)
    // Very Long and Repetitive
    private void MoveLeft(bool FalseCheck)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 1; j < 4; j++)
            {
                if (values2048[i, j] != 0)
                {

                    if (j - 3 >= 0 && values2048[i, j] == values2048[i, j - 3] && values2048[i, j - 2] == 0 && values2048[i, j - 1] == 0)
                    {
                        if (FalseCheck)
                        {
                            moved = true;
                            return;
                        }
                        values2048[i, j - 3] *= 2;
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }


                    if (j - 2 >= 0 && values2048[i, j] == values2048[i, j - 2] && values2048[i, j - 1] == 0)
                    {
                        if (FalseCheck)
                        {
                            moved = true;
                            return;
                        }
                        values2048[i, j - 2] *= 2;
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (j - 1 >= 0 && values2048[i, j] == values2048[i, j - 1])
                    {
                        if (FalseCheck)
                        {
                            moved = true;
                            return;
                        }
                        values2048[i, j - 1] *= 2;
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }

                    if (j - 3 >= 0 && values2048[i, j - 3] == 0 && values2048[i, j - 2] == 0 && values2048[i, j - 1] == 0)
                    {
                        if (FalseCheck)
                        {
                            moved = true;
                            return;
                        }
                        values2048[i, j - 3] = values2048[i, j];
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (j - 2 >= 0 && values2048[i, j - 2] == 0 && values2048[i, j - 1] == 0)
                    {
                        if (FalseCheck)
                        {
                            moved = true;
                            return;
                        }
                        values2048[i, j - 2] = values2048[i, j];
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (j - 1 >= 0 && values2048[i, j - 1] == 0)
                    {
                        if (FalseCheck)
                        {
                            moved = true;
                            return;
                        }
                        values2048[i, j - 1] = values2048[i, j];
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                }
            }
        }
    }

    private void MoveUp(bool FalseCheck)
    {
        for (int j = 0; j < 4; j++)
        {
            for (int i = 1; i < 4; i++)
            {
                if (values2048[i, j] != 0)
                {
                    if (i - 3 >= 0 && values2048[i, j] == values2048[i - 3, j] && values2048[i - 2, j] == 0 && values2048[i - 1, j] == 0)
                    {
                        if (FalseCheck)
                        {
                            moved = true;
                            return;
                        }
                        values2048[i - 3, j] *= 2;
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (i - 2 >= 0 && values2048[i, j] == values2048[i - 2, j] && values2048[i - 1, j] == 0)
                    {
                        if (FalseCheck)
                        {
                            moved = true;
                            return;
                        }
                        values2048[i - 2, j] *= 2;
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (i - 1 >= 0 && values2048[i, j] == values2048[i - 1, j])
                    {
                        if (FalseCheck)
                        {
                            moved = true;
                            return;
                        }
                        values2048[i - 1, j] *= 2;
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }

                    if (i - 3 >= 0 && values2048[i - 3, j] == 0 && values2048[i - 2, j] == 0 && values2048[i - 1, j] == 0)
                    {
                        if (FalseCheck)
                        {
                            moved = true;
                            return;
                        }
                        values2048[i - 3, j] = values2048[i, j];
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (i - 2 >= 0 && values2048[i - 2, j] == 0 && values2048[i - 1, j] == 0)
                    {
                        if (FalseCheck)
                        {
                            moved = true;
                            return;
                        }
                        values2048[i - 2, j] = values2048[i, j];
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (i - 1 >= 0 && values2048[i - 1, j] == 0)
                    {
                        if (FalseCheck)
                        {
                            moved = true;
                            return;
                        }
                        values2048[i - 1, j] = values2048[i, j];
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                }
            }
        }
    }

    private void MoveDown(bool FalseCheck)
    {
        for (int j = 0; j < 4; j++)
        {
            for (int i = 2; i >= 0; i--)
            {
                if (values2048[i, j] != 0)
                {
                    if (i + 3 < values2048.GetLength(0) && values2048[i, j] == values2048[i + 3, j] && values2048[i + 2, j] == 0 && values2048[i + 1, j] == 0)
                    {
                        if (FalseCheck)
                        {
                            moved = true;
                            return;
                        }
                        values2048[i + 3, j] *= 2;
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (i + 2 < values2048.GetLength(0) && values2048[i, j] == values2048[i + 2, j] && values2048[i + 1, j] == 0)
                    {
                        if (FalseCheck)
                        {
                            moved = true;
                            return;
                        }
                        values2048[i + 2, j] *= 2;
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (i + 1 < values2048.GetLength(0) && values2048[i, j] == values2048[i + 1, j])
                    {
                        if (FalseCheck)
                        {
                            moved = true;
                            return;
                        }
                        values2048[i + 1, j] *= 2;
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }

                    if (i + 3 < values2048.GetLength(0) && values2048[i + 3, j] == 0 && values2048[i + 2, j] == 0 && values2048[i + 1, j] == 0)
                    {
                        if (FalseCheck)
                        {
                            moved = true;
                            return;
                        }
                        values2048[i + 3, j] = values2048[i, j];
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (i + 2 < values2048.GetLength(0) && values2048[i + 2, j] == 0 && values2048[i + 1, j] == 0)
                    {
                        if (FalseCheck)
                        {
                            moved = true;
                            return;
                        }
                        values2048[i + 2, j] = values2048[i, j];
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (i + 1 < values2048.GetLength(0) && values2048[i + 1, j] == 0)
                    {
                        if (FalseCheck)
                        {
                            moved = true;
                            return;
                        }
                        values2048[i + 1, j] = values2048[i, j];
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                }
            }
        }
    }

    private void MoveRight(bool FalseCheck)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 2; j >= 0; j--)
            {
                if (values2048[i, j] != 0)
                {
                    if (j + 3 < values2048.GetLength(1) && values2048[i, j] == values2048[i, j + 3] && values2048[i, j + 2] == 0 && values2048[i, j + 1] == 0)
                    {
                        if (FalseCheck)
                        {
                            moved = true;
                            return;
                        }
                        values2048[i, j + 3] *= 2;
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (j + 2 < values2048.GetLength(1) && values2048[i, j] == values2048[i, j + 2] && values2048[i, j + 1] == 0)
                    {
                        if (FalseCheck)
                        {
                            moved = true;
                            return;
                        }
                        values2048[i, j + 2] *= 2;
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (j + 1 < values2048.GetLength(1) && values2048[i, j] == values2048[i, j + 1])
                    {
                        if (FalseCheck)
                        {
                            moved = true;
                            return;
                        }
                        values2048[i, j + 1] *= 2;
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }

                    if (j + 3 < values2048.GetLength(1) && values2048[i, j + 3] == 0 && values2048[i, j + 2] == 0 && values2048[i, j + 1] == 0)
                    {
                        if (FalseCheck)
                        {
                            moved = true;
                            return;
                        }
                        values2048[i, j + 3] = values2048[i, j];
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (j + 2 < values2048.GetLength(1) && values2048[i, j + 2] == 0 && values2048[i, j + 1] == 0)
                    {
                        if (FalseCheck)
                        {
                            moved = true;
                            return;
                        }
                        values2048[i, j + 2] = values2048[i, j];
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (j + 1 < values2048.GetLength(1) && values2048[i, j + 1] == 0)
                    {
                        if (FalseCheck)
                        {
                            moved = true;
                            return;
                        }
                        values2048[i, j + 1] = values2048[i, j];
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                }
            }
        }
    }
    //combing the move methods into one method
    private void MoveAny(int iTimes, int jTimes, bool FalseCheck)
    {
        // Move right = jTimes = 1, iTimes = 0
        // Move left = jTimes = -1, iTimes = 0
        // Move up = jTimes = 0, iTimes = -1
        // Move down = jTimes = 0, iTimes = 1
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (values2048[i, j] != 0)
                {
                    for (int k = 1; k < 4; k++)
                    {
                        if (i + (k * iTimes) < values2048.GetLength(0) && j + (k * jTimes) < values2048.GetLength(1))
                        {
                            if (values2048[i, j] == values2048[i + (k * iTimes), j + (k * jTimes)])
                            {
                                if (values2048[i + ((k - 1) * iTimes), j + ((k - 1) * jTimes)] == 0)
                                {
                                    if (FalseCheck)
                                    {
                                        moved = true;
                                        return;
                                    }
                                    values2048[i + (k * iTimes), j + (k * jTimes)] *= 2;
                                    values2048[i, j] = 0;
                                    moved = true;
                                    continue;
                                }
                            }
                            if (values2048[i + ((k - 1) * iTimes), j + ((k - 1) * jTimes)] == 0)
                            {
                                if (FalseCheck)
                                {
                                    moved = true;
                                    return;
                                }
                                values2048[i + ((k - 1) * iTimes), j + ((k - 1) * jTimes)] = values2048[i, j];
                                values2048[i, j] = 0;
                                moved = true;
                                continue;
                            }
                        }

                    }
                }
            }
        }
    }
}
