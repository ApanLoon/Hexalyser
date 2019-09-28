
using System;

namespace Hexalyser.Models
{
    public class ModelService : IModelService
    {
        public void GetModel(Action<Model, Exception> callback)
        {
            // Build a model to use in the main application
            Model model = new Model();
            callback(model, null);
        }
    }
}
