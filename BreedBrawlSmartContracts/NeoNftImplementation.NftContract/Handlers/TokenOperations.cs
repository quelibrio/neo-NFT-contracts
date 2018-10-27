using System.Linq;
using System.Numerics;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using NeoNftImplementation.NftContract.Infrastructure;
using NeoNftImplementation.NftContract.Models;

namespace NeoNftImplementation.NftContract.Handlers
{
    public class TokenOperations : SmartContract
    {
        /// <summary>
        /// Attepts to handle the current operation
        /// </summary>
        public static OperationResult HandleTokenOperation(string operation, params object[] args)
        {
            OperationResult result = new OperationResult { IsComplete = true };

            byte[] callingScript = ExecutionEngine.CallingScriptHash;
            if (operation == "getMarketAddress")
            {
                result.Value = GetMarketAddress();
            }
            else if (operation == "getAttrConfig")
            {
                result.Value = DataAccess.GetAttrConfigAsObjects();
            }
            else if (operation == "getTxInfo")
            {
                if (args.Length != 1)
                {
                    result.Value = 0;
                    return result;
                }

                byte[] transactionId = (byte[])args[0];

                result.Value = DataAccess.GetTransactionInfoAsObjects(transactionId);
            }
            else if (operation == "getTokenData")
            {
                BigInteger tokenId = (BigInteger)args[0];

                result.Value = DataAccess.GetToken(tokenId.AsByteArray());
            }
            else if (operation == "getTokenOfOwnerByIndex")
            {
                byte[] owner = (byte[])args[0];
                BigInteger index = (BigInteger)args[1];

                result.Value = DataAccess.GetOwnersTokenIdByIndexAsBytes(owner, index);
            }
            else if (operation == "getApprovedAddress")
            {
                BigInteger tokenId = (BigInteger)args[0];

                result.Value = GetApprovedAddress(tokenId);
            }

            else if (operation == "transferFrom")
            {
                if (args.Length != 3)
                {
                    result.Value = false;
                    return result;
                }

                byte[] from = (byte[])args[0];
                byte[] to = (byte[])args[1];
                BigInteger tokenId = (BigInteger)args[2];

                result.Value = TransferFrom(from, to, tokenId);
            }
            else if (operation == "mintToken")
            {
                if (args.Length != 10)
                {
                    result.Value = 0;
                    return result;
                }

                byte[] tokenOwner = (byte[])args[0];
                byte health = (byte)args[1];
                byte mana = (byte)args[2];
                byte agility = (byte)args[3];
                byte stamina = (byte)args[4];
                byte criticalStrike = (byte)args[5];
                byte attackSpeed = (byte)args[6];
                byte versatility = (byte)args[7];
                byte mastery = (byte)args[8];
                BigInteger level = (byte)args[9];

                result.Value = MintToken(tokenOwner, health, mana, agility, stamina,
                    criticalStrike,attackSpeed,versatility,mastery,level);
            }
            else if (operation == "approve")
            {
                byte[] approvedAddress = (byte[])args[0];
                BigInteger tokenId = (BigInteger)args[1];

                result.Value = Approve(approvedAddress, tokenId);
            }

            else if (operation == "transferFrom_app")
            {
                if (args.Length != 3)
                {
                    result.Value = false;
                    return result;
                }

                byte[] from = (byte[])args[0];
                byte[] to = (byte[])args[1];
                BigInteger tokenId = (BigInteger)args[2];

                if (!Runtime.CheckWitness(from))
                {
                    result.Value = false;
                    return result;
                }

                byte[] marketAddress = DataAccess.GetMarketAddressAsBytes();
                if (callingScript.AsBigInteger() != marketAddress.AsBigInteger())
                {
                    result.Value = false;
                    return result;
                }

                result.Value = NepOperations.Transfer(from, to, tokenId);
            }
            else if (operation == "transfer_app")
            {
                if (args.Length != 3)
                {
                    result.Value = false;
                    return result;
                }

                byte[] from = (byte[])args[0];
                byte[] to = (byte[])args[1];
                BigInteger tokenId = (BigInteger)args[2];

                if (from.AsBigInteger() != callingScript.AsBigInteger())
                {
                    result.Value = false;
                    return result;
                }

                result.Value = NepOperations.Transfer(from, to, tokenId);
            }
            else if (operation == "transfer_Syn")
            {
                if (args.Length != 4)
                {
                    result.Value = false;
                    return result;
                }

                byte[] from = (byte[])args[0];
                byte[] to = (byte[])args[1];
                BigInteger tokenId = (BigInteger)args[2];
                byte[] scHash = (byte[])args[3];

                if (scHash.AsBigInteger() != callingScript.AsBigInteger())
                {
                    result.Value = false;
                    return result;
                }

                result.Value = NepOperations.Transfer(from, to, tokenId);
            }
            else
            {
                result.IsComplete = false;
            }

            return result;
        }

        /// <summary>
        /// Get the auction house address
        /// </summary>
        private static byte[] GetMarketAddress() => DataAccess.GetMarketAddressAsBytes();
        
        /// <summary>
        /// After authorizing, transfer the author's gladiator assets to others
        /// </summary>
        private static bool TransferFrom(byte[] from, byte[] to, BigInteger tokenId)
        {
            if (from.Length != 20 || to.Length != 20)
            {
                return false;
            }

            if (from == to)
            {
                Runtime.Log("Transfer to self!");
                return true;
            }

            object[] rawToken = DataAccess.GetTokenAsObjects(tokenId.AsByteArray());
            if (rawToken.Length == 0)
            {
                return false;
            }

            TokenInfo token = (TokenInfo)(object)rawToken;
            if (token.Owner.Length != 20)
            {
                Runtime.Log("Token does not exist");
                return false;
            }

            if (from != token.Owner)
            {
                Runtime.Log("From address is not the owner of this token");
                return false;
            }

            byte[] approvedAddress = DataAccess.GetApprovedAddressAsBytes(tokenId.AsByteArray());
            if (approvedAddress.Length == 0)
            {
                Runtime.Log("No approval exists for this token");
                return false;
            }

            if (Runtime.CheckWitness(approvedAddress))
            {
                token.Owner = to;

                DataAccess.SetToken(tokenId.AsByteArray(), token);
                DataAccess.RemoveApproval(tokenId.AsByteArray());

                DataAccess.IncreaseAddressBalance(to);
                DataAccess.DecreaseAddressBalance(from);

                Runtime.Log("Transfer complete");

                DataAccess.SetTransactionInfo(from, to, tokenId);

                Events.RaiseTransfer(from, to, tokenId);

                return true;
            }

            Runtime.Log("Transfer by tx sender not approved by token owner");

            return false;
        }
        
        /// <summary>
        /// Check if a gladiator is authorized, not supported yet
        /// </summary>
        private static byte[] GetApprovedAddress(BigInteger tokenId) => 
            DataAccess.GetApprovedAddressAsBytes(tokenId.AsByteArray());


        /// <summary>
        /// Authorize someone to operate one of their own gladiators
        /// </summary>
        private static bool Approve(byte[] approvedAddress, BigInteger tokenId)
        {
            if (approvedAddress.Length != 20)
            {
                return false;
            }

            TokenInfo token = DataAccess.GetToken(tokenId.AsByteArray());
            if (token.Owner.Length != 20)
            {
                Runtime.Log("Token does not exist");

                return false;
            }

            if (Runtime.CheckWitness(token.Owner))
            {
                // only one third-party spender can be approved
                // at any given time for a specific token
                DataAccess.SetApprovedAddress(tokenId.AsByteArray(), approvedAddress);

                Events.RaiseApproved(token.Owner, approvedAddress, tokenId);

                return true;
            }

            Runtime.Log("Incorrect permission");

            return false;
        }

        /// <summary>
        /// Release Promotion Gladiator
        /// </summary>
        private static BigInteger MintToken(
            byte[] owner, byte health, byte mana, byte agility,
            byte stamina, byte criticalStrike, byte attackSpeed, byte versatility, byte mastery, BigInteger level) =>
                NepOperations.CreateToken(owner, health,  mana,  agility,
                    stamina,  criticalStrike,  attackSpeed,  versatility,  mastery, level);        
    }
}
