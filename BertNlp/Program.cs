using BertMlNet;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;


class Program
{
    static async Task Main(string[] args)
    {

        // Define the path for the "models" folder within the current working directory
        string modelsPath = Path.Combine(Directory.GetCurrentDirectory(), "models");
        string modelPath = System.IO.Path.Combine(modelsPath, "bert-large-uncased-whole-word-masking-finetuned-squad.onnx");

        // Check if the "models" directory already exists
        if (!File.Exists(modelPath))
        {
            // If it does not exist, create it
            Directory.CreateDirectory(modelsPath);
            Console.WriteLine($"Created directory at: {modelsPath}");

            // Initialize PythonExecutor asynchronously
            PythonExecutor.PythonExecutor pythonExecutor = new PythonExecutor.PythonExecutor(new[] { "transformers", "numpy", "torch", "onnx", "onnxruntime" });

            try
            {
                await pythonExecutor.Initialize(); // Non-blocking, awaiting the initialization
                Console.WriteLine("PythonExecutor initialized successfully.");

                // Execute Python script to export ONNX model
                pythonExecutor.ExecuteScript(BertNlp.PythonExportOnnxModel.pythonScript);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PythonExecutor initialization failed: {ex.Message}");
            }
            finally
            {
                pythonExecutor.Shutdown();
            }
        }
        else
        {
            Console.WriteLine($"Model already exists at: {modelPath}");
        }
        // Initialize the BERT model
        var model = new Bert("Assets\\Vocabularies\\base_uncased_large.txt",
                           modelPath);
        while (true)
        {
            // Prompt user for input
            Console.WriteLine("Enter your input query:");
            string userInput = Console.ReadLine();
            if(userInput =="exit")
            {
                break;
            }
            // List of predefined questions
            List<string> questions = new List<string>
        { 
            "The Answer to 'hi ' is 'hello' [SEP] " ,
            "The Answer to 'what is your name' is 'Brian' [SEP] " +
            "The Answer to 'what is the capital of the United States' is 'Washington DC' [SEP] " +
            "The Answer to 'what is the capital of Canada' is 'Ottawa' [SEP]",

            "The Answer to 'hello ' is 'hello' [SEP] " +
            "The Answer to 'whats your name' is 'Brian' [SEP] " +
            "The Answer to 'what is the capital of England' is 'London' [SEP] " +
            "The Answer to 'what is the capital of Ireland' is 'Dublin' [SEP]",

            "The Answer to 'what is the capital of Spain' is 'Madrid' [SEP] " +
            "The Answer to 'who wrote Hamlet' is 'Shakespeare' [SEP] " +
            "The Answer to 'what is the capital of France' is 'Paris' [SEP] " +
            "The Answer to 'what is the capital of Germany' is 'Berlin' [SEP]",

            "The Answer to 'what is the speed of light' is '299792458 meters per second' [SEP] " +
            "The Answer to 'what is the largest planet' is 'Jupiter' [SEP] " +
            "The Answer to 'what is the boiling point of water' is '100 degrees Celsius' [SEP] " +
            "The Answer to 'who is the president of the United States' is 'Joe Biden' [SEP]",

            "The Answer to 'who invented the telephone' is 'Alexander Graham Bell' [SEP] " +
            "The Answer to 'what is the capital of Italy' is 'Rome' [SEP] " +
            "The Answer to 'what is the square root of 16' is '4' [SEP] " +
            "The Answer to 'who is the CEO of Tesla' is 'Elon Musk' [SEP]",

            "The Answer to 'what is the largest ocean' is 'Pacific Ocean' [SEP] " +
            "The Answer to 'who painted the Mona Lisa' is 'Leonardo da Vinci' [SEP] " +
            "The Answer to 'what is the chemical symbol for water' is 'H2O' [SEP] " +
            "The Answer to 'what is the capital of Russia' is 'Moscow' [SEP]",

            "The Answer to 'what is the capital of Spain' is 'Madrid' [SEP] " +
            "The Answer to 'who discovered America' is 'Christopher Columbus' [SEP] " +
            "The Answer to 'what is the capital of Japan' is 'Tokyo' [SEP] " +
            "The Answer to 'who is the author of Harry Potter' is 'J.K. Rowling' [SEP]",

            "The Answer to 'what is the tallest mountain' is 'Mount Everest' [SEP] " +
            "The Answer to 'what is the capital of Australia' is 'Canberra' [SEP] " +
            "The Answer to 'who won the World Cup in 2018' is 'France' [SEP] " +
            "The Answer to 'what is the atomic number of hydrogen' is '1' [SEP]",

            "The Answer to 'what is the longest river' is 'Nile' [SEP] " +
            "The Answer to 'what is the smallest country' is 'Vatican City' [SEP] " +
            "The Answer to 'what is the currency of Japan' is 'Yen' [SEP] " +
            "The Answer to 'who was the first man on the moon' is 'Neil Armstrong' [SEP]",

            "The Answer to 'what is the tallest building' is 'Burj Khalifa' [SEP] " +
            "The Answer to 'who is the author of The Odyssey' is 'Homer' [SEP] " +
            "The Answer to 'what is the distance to the moon' is '384400 km' [SEP] " +
            "The Answer to 'what is the largest desert' is 'Sahara Desert' [SEP]",

            "The Answer to 'who was the first president of the United States' is 'George Washington' [SEP] " +
            "The Answer to 'who invented the lightbulb' is 'Thomas Edison' [SEP] " +
            "The Answer to 'what is the capital of China' is 'Beijing' [SEP] " +
            "The Answer to 'what is the capital of Brazil' is 'Brasilia' [SEP]",

            "The Answer to 'what is the capital of India' is 'New Delhi' [SEP] " +
            "The Answer to 'what is the chemical symbol for gold' is 'Au' [SEP] " +
            "The Answer to 'who wrote The Great Gatsby' is 'F. Scott Fitzgerald' [SEP] " +
            "The Answer to 'what is the currency of the United States' is 'Dollar' [SEP]",

            "The Answer to 'who is the founder of Microsoft' is 'Bill Gates' [SEP] " +
            "The Answer to 'what is the square root of 64' is '8' [SEP] " +
            "The Answer to 'what is the chemical symbol for iron' is 'Fe' [SEP] " +
            "The Answer to 'what is the freezing point of water' is '0 degrees Celsius' [SEP]"
            // Add more questions here
        };

            // Initialize variables to track the best response
            string bestTokens = string.Empty;
            double highestProbability = 0;

            // Iterate over the list of questions and make predictions
            foreach (var question in questions)
            {
                var (tokens, probability) = model.Predict(question, userInput);

                // Compare to find the highest probability
                if (probability > highestProbability)
                {
                    highestProbability = probability;
                    bestTokens = string.Join(" ", tokens);
                }

            }

            // Display the best response with the highest probability
            Console.WriteLine($"{bestTokens}");
            
            Console.WriteLine($"Probability: {highestProbability}");
        }
    }
}
