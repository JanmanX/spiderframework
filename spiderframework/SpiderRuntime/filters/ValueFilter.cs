using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using fox.spider.runtime.interfaces;

namespace fox.spider.runtime.filter
{
    /// <summary>
    /// A ValueFilter provides Regular Expression and Not Null constraints to filter a DataRow. It 
    /// checks if the specified field of a DataRow is mathed by a regular expression or not nul.
    /// </summary>
    public class ValueFilter : IRowFilter
    {
        private List<string> m_NotNullFields = new List<string>();
        private Dictionary<string, string> m_RegexFields = new Dictionary<string, string>();
        /// <summary>
        /// add not null field name.
        /// 增加 not null 的欄位名稱。
        /// </summary>
        /// <param name="field"></param>
        public void addNotNullFields(params string[] field)
        {
            for (int i = 0; i < field.Length; i++)
            {
                m_NotNullFields.Add(field[i]);
            }
        }
        /// <summary>
        /// add regular expression and field name which needs to match.
        /// 增加 regular expression 的欄位及 pattern 。
        /// </summary>
        /// <param name="f">field name, 欄位名稱</param>
        /// <param name="p">Regular Expression Pattern</param>
        public void addRegexField(string f, string p)
        {
            m_RegexFields.Add(f, p);
        }

        private bool checkNotNull(DataRow row)
        {
            foreach (string s in m_NotNullFields)
            {
                if (row.Table.Columns.Contains(s))
                {
                    if (DBNull.Value.Equals(row[s]) || null==row[s])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool checkRegexp(DataRow row)
        {
            foreach (string s in m_RegexFields.Keys)
            {
                if (row.Table.Columns.Contains(s))
                {
                    if (Regex.IsMatch(row[s].ToString(), m_RegexFields[s]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        #region IRowFilter Members
        /// <summary>
        /// do check.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool filter(object sender, DataRow row)
        {

            return checkNotNull(row) && checkRegexp(row);
        }

        #endregion
    }
}
