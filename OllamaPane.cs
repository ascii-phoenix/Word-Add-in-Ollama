using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.Extensions.AI;
using OllamaSharp;
using System.Threading.Tasks;
using System.IO;

namespace Ollama
{
    public partial class OllamaPane : UserControl
    {
        private IChatClient chatClient;
        private const int MaxInputChars = 6000;

        public OllamaPane(IChatClient client)
        {
            InitializeComponent();
            chatClient = client;
        }

        private async void buttonSend_Click(object sender, EventArgs e)
        {
            string input = textBoxInput.Text;
            if (string.IsNullOrWhiteSpace(input))
                return;

            var messages = new List<ChatMessage>
            {
                new ChatMessage(ChatRole.User, input)
            };

            textBoxOutput.Text = "";

            // Run in background thread
            var response = await Task.Run(() => chatClient.GetResponseAsync(messages));

            // Simulate typing effect
            foreach (char c in response.Text)
            {
                textBoxOutput.Invoke((Action)(() => textBoxOutput.AppendText(c.ToString())));
                await Task.Delay(20); // Adjust speed as needed
            }
        }

        private void InitializeComponent()
        {
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();

            this.textBoxInput.Location = new System.Drawing.Point(10, 10);
            this.textBoxInput.Size = new System.Drawing.Size(280, 26);
            this.textBoxInput.AllowDrop = true;
            this.textBoxInput.DragEnter += new DragEventHandler(this.textBoxInput_DragEnter);
            this.textBoxInput.DragDrop += new DragEventHandler(this.textBoxInput_DragDrop);

            this.buttonSend.Location = new System.Drawing.Point(10, 45);
            this.buttonSend.Size = new System.Drawing.Size(75, 30);
            this.buttonSend.Text = "Send";
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);

            this.textBoxOutput.Location = new System.Drawing.Point(10, 85);
            this.textBoxOutput.Size = new System.Drawing.Size(280, 200);
            this.textBoxOutput.Multiline = true;

            this.Controls.Add(this.textBoxInput);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textBoxOutput);
            this.Size = new System.Drawing.Size(300, 300);
            this.ResumeLayout(false);
        }

        private void textBoxInput_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.UnicodeText) ||
                e.Data.GetDataPresent(DataFormats.Text) ||
                e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private async void textBoxInput_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                // Text dropped directly
                if (e.Data.GetDataPresent(DataFormats.UnicodeText))
                {
                    var text = (string)e.Data.GetData(DataFormats.UnicodeText);
                    textBoxInput.Text = Truncate(text, MaxInputChars);
                    return;
                }
                if (e.Data.GetDataPresent(DataFormats.Text))
                {
                    var text = (string)e.Data.GetData(DataFormats.Text);
                    textBoxInput.Text = Truncate(text, MaxInputChars);
                    return;
                }

                // Files dropped
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    if (files == null || files.Length == 0) return;

                    var path = files[0];
                    var ext = Path.GetExtension(path)?.ToLowerInvariant();

                    if (ext == ".pdf")
                    {
                        // Extract PDF text off the UI thread
                        var extracted = await Task.Run(() => PdfHelper.ExtractText(path));
                        textBoxInput.Text = Truncate(extracted, MaxInputChars);
                    }
                    else if (ext == ".txt" || ext == ".md" || ext == ".csv" || ext == ".log")
                    {
                        var content = await Task.Run(() => File.ReadAllText(path));
                        textBoxInput.Text = Truncate(content, MaxInputChars);
                    }
                    else
                    {
                        // Fallback: put the file path into the input
                        textBoxInput.Text = path;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Drag-drop error: " + ex.Message);
            }
        }

        private static string Truncate(string value, int max)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= max ? value : value.Substring(0, max);
        }

        private TextBox textBoxInput;
        private Button buttonSend;
        private TextBox textBoxOutput;
    }
}
