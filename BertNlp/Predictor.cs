﻿using BertMlNet.MachineLearning.DataModel;
using Microsoft.ML;

namespace BertMlNet.MachineLearning
{
    public class Predictor
    {
        private MLContext _mLContext;
        private PredictionEngine<BertInput, BertPredictions> _predictionEngine;

        public Predictor(ITransformer trainedModel)
        {
            _mLContext = new MLContext();
            _predictionEngine = _mLContext.Model
                                          .CreatePredictionEngine<BertInput, BertPredictions>(trainedModel);
        }

        public BertPredictions Predict(BertInput encodedInput)
        {
            return _predictionEngine.Predict(encodedInput);
        }
    }
}