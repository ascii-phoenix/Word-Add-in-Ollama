using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.Extensions.AI;
using UglyToad.PdfPig;

public static class PdfHelper
{
    public static string ExtractText(string pdfPath)
    {
        var sb = new StringBuilder();
        using (var doc = PdfDocument.Open(pdfPath))
        {
            foreach (var page in doc.GetPages())
                sb.AppendLine(page.Text);
        }
        return sb.ToString();
    }

    public static async System.Threading.Tasks.Task<string> AskAboutPdfAsync(IChatClient chatClient, string pdfPath, string question)
    {
        var text = ExtractText(pdfPath);
        var max = Math.Min(text.Length, 6000);
        var doc = text.Substring(0, max);

        var messages = new List<ChatMessage>
        {
            new ChatMessage(ChatRole.System, "Answer using only the provided document content."),
            new ChatMessage(ChatRole.User, $"Document:\n{doc}\n\nQuestion: {question}")
        };

        var resp = await chatClient.GetResponseAsync(messages);
        return resp.Text;
    }
}