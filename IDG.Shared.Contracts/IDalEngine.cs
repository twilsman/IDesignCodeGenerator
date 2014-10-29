using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IDG
{
    [ServiceContract]
    public interface IDalEngine
    {
        [OperationContract]
        void GenerateCodeFile(string templatesPath, string templateFile, string outputPath, string outputFile);
        
        [OperationContract]
        void GenerateCodeFile(string templatesPath, string templateFile, string outputPath, string outputFile, object model);
    }
}
