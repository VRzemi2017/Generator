using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inspectorに二重で配列を表示するために使用
[System.Serializable]
public class ObjectList {
    //配置しやすいように空のGameObjectにしてあるがVector3でも可
    public List<GameObject> m_List = new List<GameObject>( );

    public ObjectList( List<GameObject> list ) {
        m_List = list;
    }
}


public class GemGanerator : MonoBehaviour {
    //--------Inspectorに表示される変数--------//
    //生成するGemPrefab
    [SerializeField]
    private GameObject m_GemPrefab;
    //Gemを生成する場所の配列
    [SerializeField]
    private List<ObjectList> m_GemMakePos = new List<ObjectList>( );
    //---------------------------------------//
    
    //--------メンバ変数--------//
    //ゲーム進行中に生成されるGemの配列
    private List<GameObject> m_gem_list;
    //生成されるGemの配列番号
    private int m_gem_num;
    //-------------------------//


    void Awake( ) {
    }

	void Start ( ) {
        m_gem_num = 0;
	}
	
	void Update ( ) {
		MainManager.GameState game_state = MainManager.CurrentState;
        switch( game_state ) {
            case MainManager.GameState.GAME_START:
                break;
            case MainManager.GameState.GAME_PLAYING:
                GenerateUpdate( );
                break;
            case MainManager.GameState.GAME_FINISH:
                break;
            default:
                break;
        }
	}

    //Gem生成の為の更新
    private void GenerateUpdate( ) {
        //もし全てのGemが出ているならreturn
        if ( IsAllGems( ) ) {
            return;
        }

        //現状のm_gem_listの更新
        ResetList( );
        //もしm_gem_listの中身が無かったら生成
        if ( IsNoneGem( ) ) {
            NextGems( );
            MakeGem( m_gem_num );
            return;
        }
    }
    //m_GemMakePos[ num ]のGemを生成
    private void MakeGem( int num ) {
        for ( int i = 0; i < m_GemMakePos[ num ].m_List.Count; i++ ) {
            Transform trans = m_GemMakePos[ num ].m_List[ i ].gameObject.transform;
            GameObject obj = Instantiate( m_GemPrefab, trans );
            m_gem_list.Add( obj );
        }
    }

    //現存しているGemがあるかないか
    //True :無
    //False:有
    private bool IsNoneGem( ) {
        return m_gem_list.Count == 0;
    }

    //外部からGemを廃棄されてしまう為、毎フレーム確認する必要がある
    private void ResetList( ) {
        for ( int  i = 0; i < m_gem_list.Count; i++ ) {
            if ( !m_gem_list[ i ] ) {
                m_gem_list.Remove( m_gem_list[ i ] );
            }
        }
    }

    //Gemの配列数の確認
    private bool IsAllGems( ) {
        return m_gem_num > m_gem_list.Count;
    }

    //Gemの組み合わせ更新
    private void NextGems( ) {
        m_gem_num++;
    }
}
