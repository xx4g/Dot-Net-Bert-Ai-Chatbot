Project Overview
This C# console application uses a BERT (Bidirectional Encoder Representations from Transformers) ONNX model to predict answers based on user queries. The model compares user input to a predefined list of question-answer pairs, selecting the response with the highest probability. The program incorporates Python functionality to download and set up the BERT model if it doesn't exist, making it convenient for the user to work with ONNX models in a .NET environment.

Key Components
BERT Model: Uses a BERT ONNX model (bert-large-uncased-whole-word-masking-finetuned-squad.onnx) to predict text answers.
Python Integration: Uses the PythonExecutor to manage model downloads and exports if the model is not found in the local environment.
Predefined Question-Answer List: Contains a list of question-answer pairs, with responses based on the user's query.
Prediction and Comparison: The BERT model processes each question-answer pair to find the one with the highest probability that matches the user's query.
Usage
When the user runs the program, they are prompted to enter a query.
The program compares the user’s query against a predefined list of questions and retrieves the best answer based on the probability score calculated by the BERT model.
The program outputs the best-matching tokens and their probability.
The user can exit the program by typing "exit."
Prerequisites
.NET 6.0 SDK or newer
Python installed for the PythonExecutor to export the ONNX model
A directory where the models will be stored (created automatically if not present).
Setup
To run the application, ensure you have the required NuGet packages installed:

NuGet Packages
xml
Copy code
<PackageReference Include="Microsoft.ML.OnnxTransformer" Version="3.0.1" />
<PackageReference Include="Python.Deployment" Version="2.0.5" />
<PackageReference Include="Python.Included" Version="3.11.6" />
<PackageReference Include="System.Text.Encoding" Version="4.3.0" />
The Microsoft.ML.OnnxTransformer package provides the tools to load and run ONNX models within a .NET application.
The Python.Deployment and Python.Included packages help to integrate Python code execution in the .NET environment for tasks such as exporting ONNX models.
System.Text.Encoding handles the text encoding needed for the program’s string operations.
Sources
BERT Model: The BERT model used is bert-large-uncased-whole-word-masking-finetuned-squad.onnx, which is designed for question-answering tasks.
PythonExecutor: This tool is used to handle Python scripting within the C# application, specifically for managing ONNX model operations.





