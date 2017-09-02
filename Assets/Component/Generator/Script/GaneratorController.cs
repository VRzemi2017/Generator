using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inspectorに二重で配列を表示するために使用
[System.Serializable]
public struct WaveData {
    public bool IsWarpViewOnTime;
    public bool IsWarpViewOnGemNum;
    public bool IsAddMode;
    public float TimeOnWarpView;
    public int GemOnWarpView;
    public int GemNumOnView;
    public float TimeOnView;
    public int WarpWave;
}


public class GaneratorController : MonoBehaviour {
    

    [SerializeField] GemGanerator m_gemGanerator;
    [SerializeField] EnemyGanrator m_enemyGanerator;
    [SerializeField] List<WaveData> m_WaveList;

    public int m_totalGem;
    public int m_nowWaveNum;
    public float m_totalTime;

    private void Start() {
        m_totalGem = 0;
        m_nowWaveNum = 0;
    }

    public void IsGetGem( ) {
        m_totalGem++;
    }

    private void Update() {
        MainManager.GameState game_state = MainManager.CurrentState;
        UpdateWave();

        switch (game_state) {
            case MainManager.GameState.GAME_START:
                m_totalTime = 0;
                break;
            case MainManager.GameState.GAME_PLAYING:
                m_totalTime += Time.deltaTime;
                UpdateWave();
                break;
            case MainManager.GameState.GAME_FINISH:
                break;
            default:
                break;
        }
    }

    private void UpdateWave() {
        if (m_WaveList[m_nowWaveNum].IsWarpViewOnGemNum) {
            if (m_totalGem >= m_WaveList[m_nowWaveNum].GemOnWarpView) {
                m_nowWaveNum = m_WaveList[m_nowWaveNum].WarpWave;
                m_gemGanerator.SetWave(m_WaveList[m_nowWaveNum].IsAddMode, m_nowWaveNum);
                return;
            }
        }

        if (m_WaveList[m_nowWaveNum].IsWarpViewOnTime) {
            if (m_totalTime >= m_WaveList[m_nowWaveNum].TimeOnWarpView) {
                m_nowWaveNum = m_WaveList[m_nowWaveNum].WarpWave;
                m_gemGanerator.SetWave(m_WaveList[m_nowWaveNum].IsAddMode, m_nowWaveNum);
                return;
            }
        }

        if (m_totalTime >= m_WaveList[m_nowWaveNum].TimeOnView) {
            m_nowWaveNum++;
            m_gemGanerator.SetWave(m_WaveList[m_nowWaveNum].IsAddMode, m_nowWaveNum);
            return;
        }

        if (m_totalGem >= m_WaveList[m_nowWaveNum].GemNumOnView) {
            m_nowWaveNum++;
            m_gemGanerator.SetWave(m_WaveList[m_nowWaveNum].IsAddMode, m_nowWaveNum);
            return;
        }

    }
    
}
