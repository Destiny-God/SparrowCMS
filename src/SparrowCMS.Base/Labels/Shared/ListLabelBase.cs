﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core.Labels.Shared
{
    public abstract class ListLabelBase : ILabel
    {
        public IEnumerable<LabelParameter> Parameters { get; set; }

        public IEnumerable<Field> Fields { get; set; }

        private IEnumerable<Document> _rows = null;

        public abstract IEnumerable<Document> GetDatas();

        public int RecordCount { get; set; }

        public string PaginationId { get; set; }

        protected virtual void InitRows()
        {
            if (_rows == null)
            {
                _rows = GetDatas();
            }
        }

        protected virtual int GetRecordCount()
        {
            return RecordCount = RecordCount > 0 ? RecordCount : _rows.Count();
        }

        private void SavePaginationRecordCount()
        {
            if (!string.IsNullOrEmpty(PaginationId))
            {
                var recordCount = GetRecordCount();
                Context.Current.CurrentPage.ViewData[PaginationId] = recordCount;
            }
        }

        protected virtual string GetRepeatTemplate(string innerHtml)
        {
            return innerHtml;
        }

        public virtual string GetReplacedContent(string innerHtml)
        {
            InitRows();
            SavePaginationRecordCount();

            var repeatResult = string.Empty;
            if (_rows == null) return repeatResult;

            var rowTemplate = GetRepeatTemplate(innerHtml);
            foreach (var row in _rows)
            {
                repeatResult += this.GetRelacedModelContent(rowTemplate, row, Fields);

            }

            return innerHtml.Replace(rowTemplate, repeatResult);
        }
    }
}
