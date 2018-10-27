using System.Numerics;
using Neo.SmartContract.Framework;
using NeoNftImplementation.MarketContract.Infrastructure;
using NeoNftImplementation.MarketContract.Models;

namespace NeoNftImplementation.MarketContract.Handlers
{
    public class NepOperations : SmartContract
    {        
        /// <summary>
        /// Attepts to handle the current operation
        /// </summary>
        public static OperationResult HandleNepOperation(string operation, params object[] args)
        {
            var result = new OperationResult { IsComplete = true };

            if (operation == "version")
            {
                result.Value = NepOperations.Version();
            }
            else if (operation == "name")
            {
                result.Value = NepOperations.Name();
            }
            else if (operation == "balanceOf")
            {
                if (args.Length != 1)
                {
                    result.Value = 0;
                    return result;
                }

                byte[] account = (byte[])args[0];

                result.Value = NepOperations.BalanceOf(account);
            }
            else
            {
                result.IsComplete = false;
            }

            return result;
        }

        /// <summary>
        /// Name
        /// </summary>
        private static string Name() => "CrazyGladiatorAuction";

        /// <summary>
        /// Version
        /// </summary>
        private static string Version() => "1.2.6";

        /// <summary>
        /// The token stored by the user at the auction
        /// </summary>
        private static BigInteger BalanceOf(byte[] address) =>
            DataAccess.GetUserBalanceAsBytes(address).AsBigInteger();
    }
}
