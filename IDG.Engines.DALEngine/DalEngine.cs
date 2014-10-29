using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RazorEngine;
using System.IO;

namespace IDG.Engines
{
    public class DalEngine : IDalEngine
    {
        public void GenerateCodeFile(string templatesPath, string templateFile, string outputPath, string outputFile)
        {
            parse(templatesPath, templateFile, outputPath, outputFile, null);
        }

        public void GenerateCodeFile(string templatesPath, string templateFile, string outputPath, string outputFile, object model)
        {
            parse(templatesPath, templateFile, outputPath, outputFile, model);
        }

        private void parse(string templatesPath, string templateFile, string outputPath, string outputFile, object model)
        {
            string template = File.ReadAllText(string.Format(@"{0}\{1}", templatesPath, templateFile));

            string code;
            if(model == null)
            {
                code = Razor.Parse(template);
            }
            else
            {
                code = Razor.Parse(template, model);
            }

            File.WriteAllText(string.Format(@"{0}{1}.cs", outputPath, outputFile), code);
        }
    }
}
