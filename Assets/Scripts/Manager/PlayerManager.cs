using UnityEngine;

namespace Ria
{
    [System.Serializable]
    public sealed class PlayerManager : ChildManager
    {
        [SerializeField, Tooltip("プレイヤー１のデータ")]
        private PlayerScriptableObject playerScriptObj1;
        [SerializeField, Tooltip("プレイヤー２のデータ")]
        private PlayerScriptableObject playerScriptObj2;
    
        private PlayerActor mPlayer1 = null;
        private PlayerActor mPlayer2 = null;

        protected override void OnInit()
        {
            mPlayer1 = new PlayerActor(PlayerID.Player1, playerScriptObj1, "player1", new Vector3(-500, -500));
            mPlayer2 = new PlayerActor(PlayerID.Player2, playerScriptObj2, "player2", new Vector3(500, -500));

            mPlayer1.Create();
            mPlayer2.Create();
        }

        protected override void OnRun()
        {
            mPlayer1.Run();
            mPlayer2.Run();
        }

    }
}