using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{

    [SerializeField]public int nextLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))StartCoroutine(LoadNextLevel());
    }
    IEnumerator LoadNextLevel()
    {
        FindAnyObjectByType<GameSession>().setLevel(nextLevel);
        yield return new WaitForSecondsRealtime(1);
        FindAnyObjectByType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextLevel);

    }

}
