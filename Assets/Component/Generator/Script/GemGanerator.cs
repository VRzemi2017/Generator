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
    //GemParenの配列
    [SerializeField]
    private GameObject m_GemParent;
    //Gemを生成する場所の配列
    [SerializeField]
    private List<ObjectList> m_GemMakePos = new List<ObjectList>( );
    //GemParentが生成される場所
    [SerializeField]
    private Transform m_GemParents;
    //---------------------------------------//
    
    //--------メンバ変数--------//
    //生成されるGemの配列番号
    private int m_gem_parent_num;
    //GemParentを管理するList
    private List<Transform> m_gem_parent;
    //-------------------------//


    void Awake( ) {
    }

	void Start ( ) {
        m_gem_parent_num = 0;

        //最初に全てのGemを生成する
        for (int i = 0; i < m_GemMakePos.Count; i++) {
            Transform gem_parent = Instantiate(m_GemParent).transform;
            gem_parent.SetParent(m_GemParents);
            m_gem_parent.Add(gem_parent);
            for (int j = 0; j < m_GemMakePos[i].m_List.Count; j++) {
                Transform trans = m_GemMakePos[i].m_List[j].gameObject.transform;
                GameObject gem = Instantiate(m_GemPrefab, trans);
                //Parentをセットすることで、管理する。
                gem.transform.SetParent(gem_parent, false);
            }
            m_gem_parent[i].gameObject.SetActive(false);
        }
        //表示するべきGemをActive化させる
        m_gem_parent[m_gem_parent_num].gameObject.SetActive(true);

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

        //もしm_gem_listの中身が次のものを表示させる
        if ( IsNoneGem( ) ) {
            //前のものの表示を消す
            m_gem_parent[m_gem_parent_num].gameObject.SetActive(false);
            NextGems( );
            m_gem_parent[m_gem_parent_num].gameObject.SetActive(true);
            return;
        }
    }

    //現存しているGemがあるかないか
    //True :無
    //False:有
    private bool IsNoneGem( ) {
        foreach ( Transform gem in m_gem_parent[m_gem_parent_num]) {
            if (gem) {
                return false;
            }
        }
        return true;
    }

    //GemParentの更新の配列数の確認
    private bool IsAllGems( ) {
        return m_gem_parent_num > m_gem_parent.Count;
    }

    //GemParentの更新
    private void NextGems( ) {
        m_gem_parent_num++;
    }
}
