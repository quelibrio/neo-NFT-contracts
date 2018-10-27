using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using NeoNftImplementation.NftContract.Handlers;

namespace NeoNftImplementation.NftContract
{
    public class ContractMain : SmartContract
    {
        /// <summary>
        /// Contract owner, super administrator
        /// the owner, super admin address  AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y
        /// </summary>
        public static readonly byte[] ContractOwner = "AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y".ToScriptHash();

        /// <summary>
        /// Have permission to release 0 generation gladiator wallet address   AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y
        /// </summary>
        public static readonly byte[] MintOwner = "AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y".ToScriptHash();

        public static object Main(string operation, params object[] args)
        {
            if (Runtime.Trigger == TriggerType.Verification)
            {
                if (ContractOwner.Length == 20)
                {
                    // if param ContractOwner is script hash
                    //return Runtime.CheckWitness(ContractOwner);
                    return false;
                }
                else if (ContractOwner.Length == 33)
                {
                    // if param ContractOwner is public key
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

                result = TokenOperations.HandleTokenOperation(operation, args);
                if (result.IsComplete)
                {
                    return result.Value;
                }

                result = GameOperations.HandleGameOperation(operation, args);
                if (result.IsComplete)
                {
                    return result.Value;
                }
            }

            return false;
        }
    }
}
