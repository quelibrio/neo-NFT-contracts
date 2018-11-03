using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using NeoNftImplementation.MarketContract.Handlers;

namespace NeoNftImplementation.MarketContract
{
    public class ContractMain : SmartContract
    {
        public const ulong MinTxFee = 5000000;

        /// <summary>
        /// The owner, super admin address  AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y
        /// </summary>
        public static readonly byte[] ContractOwner = "AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y".ToScriptHash();

        /// <summary>
        /// The wallet address that has permission to issue 0-generation contract AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y
        /// </summary>
        public static readonly byte[] MintOwner = "AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y".ToScriptHash();

        /// <summary>
        /// 
        /// </summary>
        [Appcall("ae0e436c61d4568a0cb8c37997bcdc3cab99f6f2")]
        public static extern object NftContractCall(string operation, object[] args);

        /// <summary>
        /// 
        /// </summary>
        public delegate object dynamicAppCall(string operation, object[] args);

        public static object Main(string operation, params object[] args)
        {
            if (Runtime.Trigger == TriggerType.Verification)
            {
                if (ContractOwner.Length == 20)
                {
                    return false;
                }
                else if (ContractOwner.Length == 33)
                {
                    byte[] signature = operation.AsByteArray();
                    return VerifySignature(signature, ContractOwner);
                }
            }
            else if (Runtime.Trigger == TriggerType.VerificationR)
            {
                return true;
            }
            else if (Runtime.Trigger == TriggerType.Application)
            {
                var result = AdminOperations.HandleAdminOperation(operation, args);
                if (result.IsComplete)
                {
                    return result.Value;
                }

                result = NepOperations.HandleNepOperation(operation, args);
                if (result.IsComplete)
                {
                    return result.Value;
                }

                result = MarketOperations.HandleMarketOperation(operation, args);
                if (result.IsComplete)
                {
                    return result.Value;
                }
            }

            return false;
        }
    }
}
