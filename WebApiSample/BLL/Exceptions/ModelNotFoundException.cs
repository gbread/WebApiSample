namespace WebApiSample.BLL.Exceptions
{
    public class ModelNotFoundException : Exception
    {
        public ModelNotFoundException(string name, object key) :
            base($"Not found key {key} for entity {name}")
        {
        }
    }
}
