using UnityEngine;

namespace old_0609
{
    public class PlayerManager : MonoBehaviour
    {
        #region Member on Inspector
        [SerializeField, Tooltip("プレイヤー１のデータ")]
        private PlayerScriptableObject playerScriptObj1;
        [SerializeField, Tooltip("プレイヤー２のデータ")]
        private PlayerScriptableObject playerScriptObj2;
        #endregion

        #region Member
        private PlayerActor mPlayer1 = null;
        private PlayerActor mPlayer2 = null;
        #endregion

        #region Main Function
        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            mPlayer1 = new PlayerActor(PlayerID.Player1, playerScriptObj1, "player1", new Vector3(-500, -500));
            mPlayer2 = new PlayerActor(PlayerID.Player2, playerScriptObj2, "player2", new Vector3(500, -500));
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            mPlayer1.OnUpdate();
            mPlayer2.OnUpdate();
        }
        #endregion
    }
}