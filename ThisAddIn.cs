using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using OllamaSharp;
using Microsoft.Extensions.AI;

namespace Ollama
{
    
    public partial class ThisAddIn
    {
        public const string Model = "gemma3:4b";
        OllamaApiClient chatClient;
        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            chatClient = new OllamaApiClient(new Uri("http://localhost:11434/"), Model);

            var paneControl = new OllamaPane(chatClient);
            var taskPane = this.CustomTaskPanes.Add(paneControl, "Ollama Chat");
            taskPane.Visible = true;
        }



        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            chatClient.Dispose();
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
