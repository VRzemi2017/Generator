using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inspectorに二重で配列を表示するために使用
[System.Serializable]
public class EnmeyWaveList
{
    public List<int> m_List = new List<int>();
    public bool m_DisposeOnNewWave;

    public EnmeyWaveList(List<int> list)
    {
        m_List = list;
    }
}

public class EnemyGanrator : MonoBehaviour {
    //一番外側のサイズはWave数を設定する
    //Element一つ一つの中のサイズが各Waveの敵の数をする
    //最後のElementは敵の番号を設定する
    [SerializeField]
    private List<EnmeyWaveList> m_EnemyWave = new List<EnmeyWaveList>();
    //Waveの間隔時間を設定する、最初のElementは開始してから敵が1Wave目の敵が出現するまでの時間,
    //配列の数がWave数よりも少ない場合は最初のElementの時間を参照して敵を出現させる
    [SerializeField]
    private List<float> m_EnemyWaveViewtime = new List<float>();
    //敵のマネージャー
    [SerializeField]
    private CharaManager m_charaManager;

    //現在のWave数
    private int m_now_wave;
    //Wave開始からの時間
    private float m_total_time;
    //次のWaveが表示されるまでの時間
    private float m_now_wave_view_time;


    private void Start() {
        m_now_wave = 0;
        m_total_time = 0;
        m_charaManager.SetEnemyAllActive(false);
        CheckNowWaveViewTime();
    }

    private void Update() {
        MainManager.GameState game_state = MainManager.CurrentState;
        switch (game_state) {
            case MainManager.GameState.GAME_START:
                break;
            case MainManager.GameState.GAME_PLAYING:
                CheckWaveView();
                break;
            case MainManager.GameState.GAME_FINISH:
                break;
            default:
                break;
        }
        DebugCode();
    }
    private void DebugCode() {
        CheckWaveView();
    }
    private void CheckWaveView() {
        m_total_time += Time.deltaTime;
        if (m_total_time >= m_now_wave_view_time) {
            m_total_time = 0;
            //もしDisPoseがTrueであればそのWaveのEnemyをすべて消す
            if (m_EnemyWave[m_now_wave].m_DisposeOnNewWave && m_now_wave != 0) {
                foreach (int index in m_EnemyWave[m_now_wave].m_List){
                    m_charaManager.SetEnemyActive(index, false);
                }
            }
            //次のWaveのEnemyを登場させる
            foreach (int index in m_EnemyWave[m_now_wave].m_List) {
                m_charaManager.SetEnemyActive(index, true);
            }
            m_now_wave++;
            CheckNowWaveViewTime();
        }
    }

    private void CheckNowWaveViewTime() {
        m_now_wave_view_time = m_EnemyWaveViewtime[0];
        if (m_EnemyWaveViewtime.Count > m_now_wave) {
            m_now_wave_view_time = m_EnemyWaveViewtime[m_now_wave];
        }
    }

}
