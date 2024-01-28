using UnityEngine;
using UnityEngine.UI;

public class RandomNickname : MonoBehaviour
{
    // Array of typical human names
    private string[] humanNames = { "John", "Jane", "Michael", "Emily", "David", "Sophia", "Chris", "Emma", "Alex", "Olivia" };

    [SerializeField] private Text nickText;

    private void Start()
    {
        GenerateRandomNickname(humanNames[Random.Range(0,humanNames.Length)]);
    }

    private void GenerateRandomNickname(string playerName)
    {
        string randomNickname = GenerateRandomNicknameWithNumbers(playerName);
        nickText.text = randomNickname;
    }

    private string GenerateRandomNicknameWithNumbers(string playerName)
    {
        // Generate two random numbers
        int randomNumber1 = Random.Range(10, 100);
        int randomNumber2 = Random.Range(10, 100);

        // Combine playerName with the random numbers to create the nickname
        string randomNickname = playerName + randomNumber1 + randomNumber2;

        return randomNickname;
    }
}
