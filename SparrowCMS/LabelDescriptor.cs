﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS
{
    public class LabelDescriptor
    {
        public LabelDescriptor()
        {
            ParameterDescriptors = new Dictionary<string, LabelParameterDescriptor>();
            FieldDescriptors = new List<FieldDescriptor>();
            InnerLabelDescriptors = new List<LabelDescriptor>();
        }

        public string ID { get; set; }

        public string LabelName { get; set; }

        public string ClassName { get; set; }

        public string PluginName { get; set; }

        public string TemplateContent { get; set; }

        public string InnerHtml { get; set; }

        public Dictionary<string, LabelParameterDescriptor> ParameterDescriptors { get; set; }

        public List<FieldDescriptor> FieldDescriptors { get; set; }

        public List<LabelDescriptor> InnerLabelDescriptors { get; set; }

        public string GetLabelClassFullName()
        {
            if (LabelName.EndsWith(ClassName))
            {
                return LabelName;
            }
            return LabelName + "." + ClassName;
        }

        public ILabel GetLabel()
        {
            return LabelBuilder.Build(this);
        }
    }
}
