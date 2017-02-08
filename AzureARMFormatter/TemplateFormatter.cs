using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AzureARMFormatter
{
    public class TemplateFormatter
    {
        public string TemplatePath { get; set; }
        public TemplateFormatter(string fullFilePath)
        {
            TemplatePath = fullFilePath;
        }

        public string FormatText()
        {
            var bubbleTypeJson = BubbleTypeNodeToTop();
            return RemoveExtraSpace(bubbleTypeJson);
        }

        public string BubbleTypeNodeToTop()
        {
            // read file into a string and deserialize JSON to a type
            JObject deployJSON = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(TemplatePath));

            //Check for resource group 
            var resources = deployJSON["resources"];
            if (resources != null)
            {
                foreach (var resource in resources)
                    if (resource is JObject)
                        BubbleTypeProperty((JObject)resource);
            }
            else throw new InvalidDataException("Cannot find 'resources' node template");

            return deployJSON.ToString();
        }

        public string RemoveExtraSpace(string jsonasstring)
        {
            return Regex.Replace(jsonasstring, @"^(\s+)\{\s+""type""", "$1{ \"type\"", RegexOptions.Multiline);
        }

        void BubbleTypeProperty(JObject resource)
        {
            var typeprop = resource.Properties().Where(p => p.Name == "type").First();
            if (typeprop != null)
            {
                typeprop.Remove();
            }
            resource.AddFirst(typeprop);
        }

        void Sort(JObject jObj)
        {
            var props = jObj.Properties().ToList();
            foreach (var prop in props)
            {
                prop.Remove();
            }

            foreach (var prop in props.OrderBy(p => p.Name))
            {
                jObj.Add(prop);
                if (prop.Value is JObject)
                    Sort((JObject)prop.Value);
            }
        }
    }
}
