using System;

namespace Hexalyser.Models
{
    public class DesignModelService : IModelService
    {
        public void GetModel(Action<Model, Exception> callback)
        {
            // Build a model to use in blend/xaml designer
            Model model = new Model();
            model.Documents.Add(new Document("C:\\Temp\\File1.bin"));
            model.Documents.Add(new Document("C:\\Temp\\File2.bin"));
            callback(model, null);
        }
    }
}
