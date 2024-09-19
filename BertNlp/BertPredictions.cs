using Microsoft.ML.Data;

namespace BertMlNet.MachineLearning.DataModel
{
    public class BertPredictions
    {
        [ColumnName("end_logits")] // Matches 'end_logits' from the model output names
        public float[] EndLogits { get; set; }

        [ColumnName("start_logits")] // Matches 'start_logits' from the model output names
        public float[] StartLogits { get; set; }

        [ColumnName("unique_ids")] // Ensure this matches the unique identifier used in the model
        public long[] UniqueIds { get; set; }
    }
}
