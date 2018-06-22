using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    private PlayerManager playerManager;
    [SerializeField]
    private EnemyManager enemyManager;

    private void Start ()
    {
        playerManager.Init();
        enemyManager.Init();

    }
	
	private void Update ()
    {
        playerManager.Run();
        enemyManager.Run();

    }
}
