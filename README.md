# BERT Question-Answering Console Application

This C# console application uses a BERT model in ONNX format for a simple question-answering system. It predicts answers based on user input by comparing it to predefined question-answer pairs and selecting the answer with the highest probability.

## Features

- **BERT Model**: Utilizes a pre-trained BERT model (`bert-large-uncased-whole-word-masking-finetuned-squad.onnx`) for generating predictions.
- **ONNX Runtime**: The model is loaded and executed using ONNX Runtime for inference.
- **Dynamic Model Download**: If the model does not exist locally, it is automatically downloaded and stored.
- **Python Integration**: PythonExecutor is used to manage Python dependencies for exporting ONNX models.

## Installation

### Prerequisites

- .NET 8.0 or higher
- Python 3.7 or higher (for PythonExecutor)
- ONNX Runtime

### NuGet Packages

The following NuGet packages are required for the project:

xml
```
<PackageReference Include="Microsoft.ML.OnnxTransformer" Version="3.0.1" />
<PackageReference Include="Python.Deployment" Version="2.0.5" />
<PackageReference Include="Python.Included" Version="3.11.6" />
<PackageReference Include="System.Text.Encoding" Version="4.3.0" />
```
## Setup
Clone this repository to your local machine:

git clone https://github.com/xx4g/Dot-Net-Bert-Ai-Chatbot.git
cd BertNlpApp
Ensure you have the necessary .NET and Python dependencies installed.

Build the project using .NET CLI:

bash

```
dotnet build
```
##Usage
Run the application:

bash

```
dotnet run
```
The application starts a loop asking for user input and returns the answer with the highest probability from a predefined set of question-answer pairs.

Exit the loop by typing exit.

Example:
```
Enter your input query:
what is the capital of the United States
Tokens: washington dc
Probability: 0.94
```
## Model Details
The application utilizes the bert-large-uncased-whole-word-masking-finetuned-squad.onnx model, which is fine-tuned for question-answering tasks. The model is downloaded if not found in the models directory.

Predefined Question-Answer Pairs
The application compares the user input to a predefined list of question-answer pairs. Some examples include:

```
"The Answer to 'hi' is 'hello'"
"The Answer to 'what is your name' is 'Brian'"
"The Answer to 'what is the capital of the United States' is 'Washington DC'"
"The Answer to 'what is the capital of Canada' is 'Ottawa'"
```
You can modify or extend these pairs by updating the questions list in the code.

## Code Overview

```
var model = new Bert("Assets\\Vocabularies\\base_uncased_large.txt", modelPath);

var questions = new List<string>
{
    "The Answer to 'hi ' is 'hello' [SEP] ",
    "The Answer to 'what is your name' is 'Brian' [SEP] ",
    "The Answer to 'what is the capital of the United States' is 'Washington DC' [SEP] ",
    "The Answer to 'what is the capital of Canada' is 'Ottawa' [SEP] "
    // Add more questions here
};

while (true)
{
    Console.WriteLine("Enter your input query:");
    string userInput = Console.ReadLine();
    if (userInput == "exit") break;

    var (tokens, probability) = model.Predict(question, userInput);

    Console.WriteLine($"{string.Join(" ", tokens)}");
    Console.WriteLine($"Probability: {probability}");
}
```

## License

```
This project is licensed under the MIT License. See the LICENSE file for more details.
```
