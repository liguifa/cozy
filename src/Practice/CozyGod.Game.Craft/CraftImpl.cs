﻿using CozyGod.Game.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyGod.Model;
using System.IO;

namespace CozyGod.Game.Craft
{
    public class CraftImpl : ICraft
    {
        const string filePath = "CraftTableConfig.txt";

        List<CraftTable> m_CraftTableList = null;
        private ICardLibrary mCL;

        public bool init()
        {
            bool bRet = true;
            LoadCraftTable();
            if (m_CraftTableList==null)
            {
                bRet = false;
            }

            return bRet;
        }

        class CraftTable
        {
            internal List<string> costCardList = new List<string>();
            internal List<string> resultCardItem = new List<string>();
        }

        public void SetCardLibrary(ICardLibrary cl)
        {
            mCL = cl;
        }

        public Card Craft(Card a, Card b)
        {
            Card cardRet = null;
            for (int i = 0; i < m_CraftTableList.Count; i++)
            {
                if (m_CraftTableList[i].costCardList.Count == 2)
                {
                    if (m_CraftTableList[i].costCardList[0] == a.Name && b.Name == m_CraftTableList[i].costCardList[1])
                    {
                        cardRet = mCL.FindCardByName(m_CraftTableList[i].resultCardItem[0]);
                        break;
                    }
                    if (m_CraftTableList[i].costCardList[1] == a.Name && b.Name == m_CraftTableList[i].costCardList[0])
                    {
                        cardRet = mCL.FindCardByName(m_CraftTableList[i].resultCardItem[0]);
                        break;
                    }
                }
            }
            return cardRet;
        }

        public bool TryCraft(Card a, Card b)
        {
            bool bRet = false;

            for (int i = 0; i < m_CraftTableList.Count; i++)
            {
                if (m_CraftTableList[i].costCardList.Count == 2)
                {
                    if (m_CraftTableList[i].costCardList[0] == a.Name&& b.Name == m_CraftTableList[i].costCardList[1])
                    {
                        bRet = true;
                        break;
                    }
                    if (m_CraftTableList[i].costCardList[1] == a.Name && b.Name == m_CraftTableList[i].costCardList[0])
                    {
                        bRet = true;
                        break;
                    }
                }
            }

            return bRet;
        }
        private void LoadCraftTable()
        {
            using (var fileStm = new FileStream(filePath, FileMode.Open))
            {
                StreamReader sr = new StreamReader(fileStm);

                List<string[]> strList = new List<string[]>();

                string str = sr.ReadLine();

                while (str != null)
                {
                    string[] strCraft = ParseStr(str);
                    if (strCraft != null)
                    {
                        string[] costCard = strCraft[0].Split(',');
                        CraftTable CraftTableTemp = new CraftTable();
                        for (int i = 0; i < costCard.Length; i++)
                        {
                            CraftTableTemp.costCardList.Add(costCard[i]);
                        }

                        string[] resultCard = strCraft[1].Split(',');
                        for (int i = 0; i < resultCard.Length; i++)
                        {
                            CraftTableTemp.resultCardItem.Add(resultCard[i]);
                        }

                        if (m_CraftTableList == null)
                        {
                            m_CraftTableList = new List<CraftTable>();
                        }
                        m_CraftTableList.Add(CraftTableTemp);
                    }

                    str = sr.ReadLine();
                }
                sr.Close();
            }

        }

        static private string[] ParseStr(string _line)
        {
            if (_line[0] != '/' &&
                _line[1] != '/')
            {
                string[] _list = _line.Split('\t');

                return _list;
            }

            return null;
        }
    }
}