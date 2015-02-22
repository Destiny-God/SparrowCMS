﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SparrowCMS.Parsers
{
    public class LabelDescriptorParser
    {
        private static readonly Regex Regex = new Regex(@"{(?<name>[\w\.]+)(?<parameters>(\s+\w+\s*=\s*(""[^""]+""|[^\s\/]+|'[^']+'))*)\s*}(?<inner>[\s\S]*?){/\k<name>}|{(?<name>[\w.]+)(?<parameters>(\s+\w+\s*=\s*(""[^""]+""|[^\s\/]+|'[^']+'))*)\s*/}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static LabelDescriptor Parse(Match match)
        {
            if (match == null)
            {
                return null;
            }

            var labelName = match.Groups["name"].Value;
            //label类的名称默认是Default,比如有些labelName并不包含.符号 ,例如{Test /},则实际类名为Test.Default
            var className = "Default";
            var parameters = match.Groups["parameters"].Value;
            var dotIndex = labelName.LastIndexOf('.');
            if (dotIndex > -1)
            {
                className = labelName.Substring(dotIndex + 1);
                //labelName = labelName.Substring(0, dotIndex);
            }

            var desc = new LabelDescriptor
            {
                ID = Guid.NewGuid().ToString(),
                LabelName = labelName,
                ClassName = className,
                TemplateContent = match.Groups[0].Value,
                InnerHtml = match.Groups["inner"].Value,
            };

            LabelParameterParser.FindAll(desc, parameters);

            if (string.IsNullOrEmpty(desc.InnerHtml))
            {
                return desc;
            }

            FieldDescriptorParser.FindAll(desc, desc.InnerHtml);

            //desc.InnerLabelDescriptions = Parse(desc.InnerHtml);

            return desc;
        }

        /// <summary>
        /// 获取所有的LabelDescriptor
        /// </summary>
        /// <param name="templateContent"></param>
        /// <returns></returns>
        public static List<LabelDescriptor> FindAll(string templateContent)
        {
            var list = new List<LabelDescriptor>();
            foreach (Match m in Regex.Matches(templateContent))
            {
                list.Add(Parse(m));
            }

            return list;
        }

    }
}
