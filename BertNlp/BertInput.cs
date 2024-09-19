using Microsoft.ML.Data;

namespace BertMlNet.MachineLearning.DataModel
{

    public class BertInput
    {
        [ColumnName("input_ids")] // Matches 'input_ids' from the model input names
        public long[] InputIds { get; set; }

        [ColumnName("input_mask")] // Matches 'input_mask' from the model input names
        public long[] InputMask { get; set; }

        [ColumnName("segment_ids")] // Matches 'segment_ids' from the model input names
        public long[] SegmentIds { get; set; }

        // You can add more properties if needed for other inputs or unique IDs
    }
}