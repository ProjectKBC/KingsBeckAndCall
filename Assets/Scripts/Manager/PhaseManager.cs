using UnityEngine;

namespace Ria
{
    public enum GAME_PHASE
    {
        Loading,
        Run,
        Pose,
    }

    // 各managerがPhaseManager.GI.GamePhaseでフェーズを参照して処理を分岐させる
    // Loading→Runへの変更について検討中
    public sealed class PhaseManager : ChildManager
    {
        public static PhaseManager GI { get; private set; }
        public GAME_PHASE GamePhase { get; private set; }

        protected override void OnInit()
        {
            GI = this;
            GamePhase = GAME_PHASE.Loading;
        }

        protected override void OnRun()
        {

        }
    }
}
