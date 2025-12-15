# Word Add-in Ollama

**Word Add-in Ollama** is a powerful plugin for Microsoft Word that integrates AI-driven chat functionality directly into your Word documents. This add-in helps streamline your document-related workflows and provides an interactive chat pane for users to ask questions, extract text, or engage in conversations tailored to the content of imported documents.

## Features

- **Interactive Chat Pane**: Provides a user-friendly, built-in message interface where users can interact with an AI-based chat service powered by `Ollama`.
- **PDF Text Extraction**: Easily drag and drop a PDF file into the add-in to extract and process the text for conversations or analysis.
- **File and Text Import Support**: Support for importing multiple file formats such as `.txt`, `.md`, `.csv`, `.log`, and `.pdf`.
- **AI-Powered Assistance**: Utilizes `OllamaSharp` and Microsoft.Extensions.AI for handling document-related inquiries and generating responses using the pre-configured `gemma3:4b` model.
- **Seamless Word Integration**: Built on the `Microsoft.Office.Interop.Word` and `Microsoft.Office.Tools.Word` libraries, the add-in integrates tightly with Word's user interface and capabilities.
- **Drag-and-Drop Functionality**: Drag and drop files or text directly into the chat pane to load input effortlessly.

## Installation and Requirements

### Prerequisites

- **Operating System**: Windows with Microsoft Word installed.
- **Development Environment**: Visual Studio 2017 or later.
- **Framework**: Targets .NET Framework 4.8.
- **Dependencies**:
  - [OllamaSharp](https://www.nuget.org/packages/OllamaSharp/)
  - [UglyToad.PdfPig](https://www.nuget.org/packages/PdfPig/)
  - Various Microsoft.Extensions packages (for Dependency Injection, Logging, AI, etc.).

### Build and Installation

1. **Clone the Repository**:
   ```
   git clone https://github.com/ascii-phoenix/Word-Add-in-Ollama.git
   cd Word-Add-in-Ollama
   ```

2. **Restore NuGet Packages**:
   Open the solution file `Ollama.sln` in Visual Studio and let the NuGet Package Manager restore all dependencies.

3. **Build the Solution**:
   Use Visual Studio's build tools to compile the solution in either debug or release mode.

4. **Deploy the Add-in**:
   After building, the plugin will generate in `bin\Debug\`. Follow Microsoft's Word Add-in deployment guidelines to register and activate the plugin in Word.

5. **Run the Add-in**:
   Open Microsoft Word and activate `Ollama` from the Add-ins menu.

## Usage

Once installed, the add-in provides an interactive chat pane alongside your document in Microsoft Word:

1. **Sending Messages**:
   - Type a query into the input box and press the `Send` button.
   - The AI-based chat client will respond inline within the pane.

2. **Drag and Drop Support**:
   - Drag a file (e.g., `.pdf`, `.txt`) or paste a block of text directly into the input field.
   - The add-in will process the content and enable you to ask questions about it.

3. **PDF Document Question Answering**:
   - The add-in supports loading and querying content from PDF files using the `PdfHelper` utility.

## Architecture and Key Components

### Core Files

- **`Ollama.csproj`**: Defines the .NET Framework target, dependencies, and build settings for the add-in.
- **`ThisAddIn.cs`**: Manages the lifecycle of the Word add-in, initializing the `OllamaPane` chat interface, and setting up the `OllamaApiClient` for AI interactions.
- **`OllamaPane.cs`**: Implements the interactive chat pane with drag-and-drop support and typing effects for displaying responses.
- **`PdfHelper.cs`**: Handles text extraction from PDF files.
- **`Inputform1.resx`**: Provides localization and resource settings for the add-in.
- **`Ollama.sln`**: The Visual Studio Solution file for managing all project components.

### Libraries and External Integrations

- **OllamaSharp**: A high-performance library for interacting with the Ollama service.
- **UglyToad.PdfPig**: Facilitates PDF parsing and text extraction.
- **Microsoft.Extensions.AI**: Adds AI service integration and utilities.

## Contribution

Contributions are welcome! If you would like to report an issue or suggest new features, please open an issue or a pull request in this repository.

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.

---

Enjoy using **Word Add-in Ollama** to enhance your document workflows!
