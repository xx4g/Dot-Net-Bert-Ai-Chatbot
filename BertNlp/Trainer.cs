using BertMlNet.MachineLearning.DataModel;
using Microsoft.ML;
using System.Collections.Generic;

namespace BertMlNet.MachineLearning
{
    public class Trainer
    {
        private readonly MLContext _mlContext;

        public Trainer()
        {
            _mlContext = new MLContext(11);
        }

        public ITransformer TrainModel(string bertModelPath, bool useGpu)
        {
            var pipeline = _mlContext.Transforms
                            .ApplyOnnxModel(
                                modelFile: bertModelPath,
                                outputColumnNames: new[] { "start_logits", "end_logits" }, // Match the model output names
                                inputColumnNames: new[] { "input_ids", "input_mask", "segment_ids" }, // Match the input names
                                gpuDeviceId: useGpu ? 0 : (int?)null);

            return pipeline.Fit(_mlContext.Data.LoadFromEnumerable(new List<BertInput>()));
        }

    }
}