using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace fox.spider
{
    /// <summary>
    /// DocumentParser Utilities
    /// </summary>
    public class DocumentParserUtilities
    {
        /// <summary>
        /// convenient function to create a CellBasedDocumentParser
        /// </summary>
        /// <param name="row">row content parser</param>
        /// <param name="table">data table</param>
        /// <param name="rel">relation processor, this may be null</param>
        /// <param name="rowFirst">first row path which will be added sequentially.</param>
        /// <param name="rowNext">next row path which will be added sequentially.</param>
        /// <param name="cellFirst">first cell path which will be added sequentially.</param>
        /// <param name="cellNext">next cell path which will be added sequentially.</param>
        /// <returns></returns>
        public static CellBasedDocumentParser createCellBasedDocumentParser(IElementRowContentParser row,DataTable table , IRelationProcessor rel,
            string[] rowFirst, string[] rowNext, string[] cellFirst, string[] cellNext)
        {
            CellBasedDocumentParser oReturn = new CellBasedDocumentParser();
            oReturn.setDataTable(table);
            oReturn.RowContentParser = row;
            oReturn.setRelationProcessor(rel);

            for (int i = 0; i < rowFirst.Length; i++)
            {
                oReturn.addRowFirstPath(rowFirst[i]);
            }

            for (int i = 0; i < rowNext.Length; i++)
            {
                oReturn.addRowNextPath(rowNext[i]);
            }

            for (int i = 0; i < cellFirst.Length; i++)
            {
                oReturn.addCellFirstPath(cellFirst[i]);
            }

            for (int i = 0; i < cellNext.Length; i++)
            {
                oReturn.addCellNextPath(cellNext[i]);
            }

            return oReturn;
        }
        /// <summary>
        /// convenient function to create a CellBasedDocumentParser
        /// </summary>
        /// <param name="row"></param>
        /// <param name="table"></param>
        /// <param name="rel"></param>
        /// <param name="rowFirst"></param>
        /// <param name="rowNext"></param>
        /// <param name="cellFirst"></param>
        /// <param name="cellNext"></param>
        /// <returns></returns>
        public static CellBasedDocumentParser createCellBasedDocumentParser(IElementRowContentParser row, DataTable table, IRelationProcessor rel,
            string rowFirst, string rowNext, string cellFirst, string cellNext)
        {
            return createCellBasedDocumentParser(row, table, rel, new string[] { rowFirst }, new string[] { rowNext },
                new string[] { cellFirst }, new string[] { cellNext });
        }
        /// <summary>
        /// convenient function to create a RepeaterContentParser
        /// </summary>
        /// <param name="row"></param>
        /// <param name="table"></param>
        /// <param name="rel"></param>
        /// <param name="first"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public static RepeaterContentParser createRepeaterContentParser(IElementRowContentParser row, DataTable table, IRelationProcessor rel,
            string[] first, string[] next)
        {
            RepeaterContentParser oReturn = new RepeaterContentParser();
            oReturn.setDataTable(table);
            oReturn.RowContentParser = row;
            oReturn.setRelationProcessor(rel);
            for (int i = 0; i < first.Length; i++)
            {
                oReturn.addFirstItemPath(first[i]);
            }

            for (int i = 0; i < next.Length; i++)
            {
                oReturn.addNextItemPath(next[i]);
            }

            return oReturn;
        }
        /// <summary>
        /// convenient function to create a RepeaterContentParser
        /// </summary>
        /// <param name="row"></param>
        /// <param name="table"></param>
        /// <param name="rel"></param>
        /// <param name="first"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public static RepeaterContentParser createRepeaterContentParser(IElementRowContentParser row, DataTable table, IRelationProcessor rel,
            string first, string next)
        {
            return createRepeaterContentParser(row, table, rel, new string[] { first }, new string[] { next });
        }
        /// <summary>
        /// convenient function to create a TableExtractor
        /// </summary>
        /// <param name="row"></param>
        /// <param name="table"></param>
        /// <param name="rel"></param>
        /// <param name="tablePath"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static TableExtractor createTableExtractor(IElementRowContentParser row, DataTable table, IRelationProcessor rel,
            string tablePath, int start, int end)
        {
            TableExtractor oReturn = new TableExtractor();
            oReturn.setDataTable(table);
            oReturn.setRelationProcessor(rel);
            oReturn.RowContentParser = row;
            oReturn.TablePath = tablePath;
            oReturn.StartRowIndex = start;
            oReturn.EndRowIndex = end;
            return oReturn;
        }
        /// <summary>
        /// convenient function to create a TableExtractor, the end will be int.MaxValue
        /// </summary>
        /// <param name="row"></param>
        /// <param name="table"></param>
        /// <param name="rel"></param>
        /// <param name="tablePath"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static TableExtractor createTableExtractor(IElementRowContentParser row, DataTable table, IRelationProcessor rel,
            string tablePath, int start)
        {
            return createTableExtractor(row, table, rel, tablePath, start, int.MaxValue);
        }
        /// <summary>
        /// convenient function to create a TableExtractor, the start will be 0 and the end will be int.MaxValue
        /// </summary>
        /// <param name="row"></param>
        /// <param name="table"></param>
        /// <param name="rel"></param>
        /// <param name="tablePath"></param>
        /// <returns></returns>
        public static TableExtractor createTableExtractor(IElementRowContentParser row, DataTable table, IRelationProcessor rel,
            string tablePath)
        {
            return createTableExtractor(row, table, rel, tablePath, 0);
        }

    }
}
