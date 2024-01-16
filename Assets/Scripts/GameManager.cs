using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int[,] values2048 = new int[4, 4];
    [SerializeField] private TextMeshProUGUI[] valueText = new TextMeshProUGUI[16];
    [SerializeField] private GameObject parentObject;
    private bool moved = false;

    // DOES NOT CHECK IF SOMETHING IS IN THE WAY!!!!!!!!!!!!!
    private void Start()
    {
        ResetValues();
        Debug.LogError("This code does not yet check if something is in the way!");

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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            values2048[1, 0] = 2;
            Debug.Log(Equals(values2048[1, 0], 2));
            UpdateBoard();
        }
        if (Input.anyKey) // Check if any key is pressed
        {
            CheckInput();
            UpdateBoard();
        }


    }
    private void UpdateBoard()
    {
        //Update the board
        for (int i = 0; i < valueText.Length; i++)
        {
            valueText[i].text = values2048[i / 4, i % 4].ToString();
        }
    }
    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveUp();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            MoveDown();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
        }
        if (moved)
        {
            SpawnNewValue();
            moved = false;
        }
    }
    private void SpawnNewValue()
    {
        // Spawn a new value
        // Check if there is a free space
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
        else
        {
            // Game Over
            Debug.Log("Game Over");
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



    // Move Methods (MoveLeft, MoveRight, MoveUp, MoveDown)
    // Might be able to combine them into one method (optimization)
    private void MoveLeft()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 1; j < 4; j++)
            {
                if (values2048[i, j] != 0)
                {
                    if (j - 3 >= 0 && values2048[i, j] == values2048[i, j - 3])
                    {
                        values2048[i, j - 3] *= 2;
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (j - 2 >= 0 && values2048[i, j] == values2048[i, j - 2])
                    {
                        values2048[i, j - 2] *= 2;
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (j - 1 >= 0 && values2048[i, j] == values2048[i, j - 1])
                    {
                        values2048[i, j - 1] *= 2;
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }

                    if (j - 3 >= 0 && values2048[i, j - 3] == 0)
                    {
                        values2048[i, j - 3] = values2048[i, j];
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (j - 2 >= 0 && values2048[i, j - 2] == 0)
                    {
                        values2048[i, j - 2] = values2048[i, j];
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (j - 1 >= 0 && values2048[i, j - 1] == 0)
                    {
                        values2048[i, j - 1] = values2048[i, j];
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                }
            }
        }
    }

    private void MoveUp()
    {
        for (int j = 0; j < 4; j++)
        {
            for (int i = 1; i < 4; i++)
            {
                if (values2048[i, j] != 0)
                {
                    if (i - 3 >= 0 && values2048[i, j] == values2048[i - 3, j])
                    {
                        values2048[i - 3, j] *= 2;
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (i - 2 >= 0 && values2048[i, j] == values2048[i - 2, j])
                    {
                        values2048[i - 2, j] *= 2;
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (i - 1 >= 0 && values2048[i, j] == values2048[i - 1, j])
                    {
                        values2048[i - 1, j] *= 2;
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }

                    if (i - 3 >= 0 && values2048[i - 3, j] == 0)
                    {
                        values2048[i - 3, j] = values2048[i, j];
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (i - 2 >= 0 && values2048[i - 2, j] == 0)
                    {
                        values2048[i - 2, j] = values2048[i, j];
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (i - 1 >= 0 && values2048[i - 1, j] == 0)
                    {
                        values2048[i - 1, j] = values2048[i, j];
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                }
            }
        }
    }

    private void MoveDown()
    {
        for (int j = 0; j < 4; j++)
        {
            for (int i = 2; i >= 0; i--)
            {
                if (values2048[i, j] != 0)
                {
                    if (i + 3 < values2048.GetLength(0) && values2048[i, j] == values2048[i + 3, j])
                    {
                        values2048[i + 3, j] *= 2;
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (i + 2 < values2048.GetLength(0) && values2048[i, j] == values2048[i + 2, j])
                    {
                        values2048[i + 2, j] *= 2;
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (i + 1 < values2048.GetLength(0) && values2048[i, j] == values2048[i + 1, j])
                    {
                        values2048[i + 1, j] *= 2;
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }

                    if (i + 3 < values2048.GetLength(0) && values2048[i + 3, j] == 0)
                    {
                        values2048[i + 3, j] = values2048[i, j];
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (i + 2 < values2048.GetLength(0) && values2048[i + 2, j] == 0)
                    {
                        values2048[i + 2, j] = values2048[i, j];
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (i + 1 < values2048.GetLength(0) && values2048[i + 1, j] == 0)
                    {
                        values2048[i + 1, j] = values2048[i, j];
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                }
            }
        }
    }

    private void MoveRight()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 2; j >= 0; j--)
            {
                if (values2048[i, j] != 0)
                {
                    if (j + 3 < values2048.GetLength(1) && values2048[i, j] == values2048[i, j + 3])
                    {
                        values2048[i, j + 3] *= 2;
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (j + 2 < values2048.GetLength(1) && values2048[i, j] == values2048[i, j + 2])
                    {
                        values2048[i, j + 2] *= 2;
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (j + 1 < values2048.GetLength(1) && values2048[i, j] == values2048[i, j + 1])
                    {
                        values2048[i, j + 1] *= 2;
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (values2048[i, j] == values2048[i, j + 1])
                    {
                        values2048[i, j + 1] *= 2;
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }

                    if (j + 3 < values2048.GetLength(1) && values2048[i, j + 3] == 0)
                    {
                        values2048[i, j + 3] = values2048[i, j];
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (j + 2 < values2048.GetLength(1) && values2048[i, j + 2] == 0)
                    {
                        values2048[i, j + 2] = values2048[i, j];
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                    if (j + 1 < values2048.GetLength(1) && values2048[i, j + 1] == 0)
                    {
                        values2048[i, j + 1] = values2048[i, j];
                        values2048[i, j] = 0;
                        moved = true;
                        continue;
                    }
                }
            }
        }
    }
}
