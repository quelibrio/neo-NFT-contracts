namespace NeoNftImplementation.NftContract.Models
{
    public class OperationResult
    {
        public bool IsComplete;
        public object Value;

        public OperationResult()
        {
            IsComplete = true;
        }
    }
}
