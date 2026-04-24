using System.IO;
using System.Collections;
using System.Collections.Generic;
//using System;
using System.Text;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class create : MonoBehaviour
{
    public List<string[]> _csvData = new List<string[]>();  //CSVファイルの中身を入れるリスト
    public int[] number = new int[9]; // 配列の宣言
    private int stageNum;  // 現在のステージ数
    private List<int> numbers = new List<int>();
    public int[] ransu = new int[10]; // 配列の宣言

    void Start()
    {
        Fileopen script = gameObject.GetComponent<Fileopen>(); // Fileopenをインスタンス化
        _csvData = script._csvData; // Fileopenから_csvDataを取得

        stageNum = PlayerPrefs.GetInt("stageNum"); // "stageNum"から現在のステージ数を取得

        for (int i = 1; i <= 9; i++) // 難易度ごとにランダムに空欄を生成
        {
            numbers.Add(i);
        }
        int n = 0;
        //Debug.Log("count" + numbers.Count);
        //0++ < ステージ数の間 格納
        string load = SceneManager.GetActiveScene().name;
            if (load == "hard") // ステージ数が9で"extra"へシーン遷移
            {
                while (n < 9)
                {   
                int index = Random.Range(0, numbers.Count);
                ransu[n] = numbers[index];
                n++;
                numbers.RemoveAt(index);
                }
            }else{
                while (n < stageNum && n < 9)
                {   
                int index = Random.Range(0, numbers.Count);
                ransu[n] = numbers[index];
                n++;
                numbers.RemoveAt(index);
                }
            }
        
        numbers.Clear();
        //床キューブ生成処理
        Randcubecreate();
    }

    void Update()
    {
        
    }

    void Randcubecreate()
    {
        int flag = 0;
        //ランダム変数生成
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; ransu[j] != 0; j++)
            {
                //Debug.Log(j+"回通過");
                //一回でも同じならフラッグ代入
                // Debug.Log("ransu: "+ransu[j]);
                // Debug.Log("i+1: "+ (i+1));
                if (ransu[j] == i + 1)
                {
                    flag = 1;
                }
            }
            if (flag == 1)
            {
                //Debug.Log("生成処理実行");
                number[i] = UnityEngine.Random.Range(0, 6);
                int num = int.Parse(_csvData[i][0]);
                while (number[i] == num)
                {
                    number[i] = UnityEngine.Random.Range(0, 6);// ※ 1～5の範囲でランダムな整数値が返る
                }
                flag = 0;
            }
            else
            {
                number[i] = 99;
            }
        }
        // Cubeプレハブを元に、インスタンスを生成
        int nn = 0;
        GameObject obj = (GameObject)Resources.Load("cube0");
        double x = -0.5, y = -1.5, z = 1.5;
        for (int n = 0; n < 3; n++)
        {
            for (int j = 0; j < 3; j++)
            {
                switch (number[nn])
                {
                    case 0:
                        obj = (GameObject)Resources.Load("cube0");
                        //リネーム
                        obj.name = $"cubenumber{nn}";
                        break;
                    case 1:
                        obj = (GameObject)Resources.Load("cube1");
                        //リネーム
                        obj.name = $"cubenumber{nn}";
                        break;
                    case 2:
                        obj = (GameObject)Resources.Load("cube2");
                        //リネーム
                        obj.name = $"cubenumber{nn}";
                        break;
                    case 3:
                        obj = (GameObject)Resources.Load("cube3");
                        //リネーム
                        obj.name = $"cubenumber{nn}";
                        break;
                    case 4:
                        obj = (GameObject)Resources.Load("cube4");
                        //リネーム
                        obj.name = $"cubenumber{nn}";
                        break;
                    case 5:
                        obj = (GameObject)Resources.Load("cube5");
                        //リネーム
                        obj.name = $"cubenumber{nn}";
                        break;
                    case 99:
                        obj = (GameObject)Resources.Load("cube99");
                        break;
                    default:
                        break;
                }
                Instantiate(obj, new Vector3((float)x, (float)y, (float)z), Quaternion.Euler(-90, 90, 0));
                x++;
                nn++;
            }
            x = -0.5;
            z--;
        }
    }
}
