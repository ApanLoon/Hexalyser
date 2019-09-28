using System;

namespace Hexalyser.Models
{
    public interface IModelService
    {
        void GetModel(Action<Model, Exception> callback);
    }
}
