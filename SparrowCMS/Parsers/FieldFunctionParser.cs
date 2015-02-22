﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using SparrowCMS.Managers;

namespace SparrowCMS.Parsers
{
    public class FieldFunctionParser
    {
        private static readonly Regex Regex = new Regex(@"(?<name>\w+)\s*=\s*(""(?<value>[^""]+)""|'(?<value>[^']+)'|(?<value>[^\s)(]+))", RegexOptions.Compiled);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="labelName"></param>
        /// <param name="attributesTemplateContent">format="some text $this" dateformat="yyyy/MM/dd"</param>
        /// <returns></returns>
        public static List<FieldFunction> FindAll(FieldDescriptor descriptor, string attributesTemplateContent)
        {
            var result = new List<FieldFunction>();
            if (!string.IsNullOrEmpty(attributesTemplateContent))
            {
                foreach (Match m in Regex.Matches(attributesTemplateContent))
                {
                    var func = Parse(descriptor, m);
                    if (func != null)
                    {
                        result.Add(func);
                    }
                }
            }
            return result;
        }

        private static FieldFunction Parse(FieldDescriptor descriptor, Match m)
        {
            var name = m.Groups["name"].Value;
            var value = m.Groups["value"].Value;

            var factories = FactoryManager.GetInstance().GetFunctionFactories();
            foreach (var factory in factories)
            {
                var attribute = factory.CreateFunction<FieldFunction>(descriptor.LabelDescriptor.PluginName, descriptor.LabelDescriptor.LabelName, name);
                if (attribute != null)
                {
                    attribute.Name = name;
                    attribute.Value = value;
                }
                return attribute;
            }
            return null;

        }
    }
}
