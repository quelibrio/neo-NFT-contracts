using System.Numerics;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using NeoNftImplementation.NftContract.Infrastructure;
using NeoNftImplementation.NftContract.Models;

namespace NeoNftImplementation.NftContract.Handlers
{
    public class NepOperations : SmartContract
    {
        /// <summary>
        /// Total amount
        /// </summary>
        public const ulong MaxSupply = 4320;
                
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
            else if (operation == "symbol")
            {
                result.Value = NepOperations.Symbol();
            }
            else if (operation == "totalSupply")
            {
                result.Value = NepOperations.TotalSupply();
            }
            else if (operation == "decimals")
            {
                result.Value = 0;
            }
            else if (operation == "ownerOf")
            {
                BigInteger tokenId = (BigInteger)args[0];

                result.Value = NepOperations.OwnerOf(tokenId);
            }
            else if (operation == "tokenURI")
            {
                BigInteger tokenId = (BigInteger)args[0];

                result.Value = NepOperations.TokenURI(tokenId);
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
            else if (operation == "tokensOfOwner")
            {
                byte[] owner = (byte[])args[0];

                result.Value = NepOperations.TokensOfOwner(owner);
            }

            else if (operation == "transfer")
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

                var callingScript = ExecutionEngine.CallingScriptHash;
                if (ExecutionEngine.EntryScriptHash.AsBigInteger() != callingScript.AsBigInteger())
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
        /// Name
        /// </summary>
        private static string Name() => "BreedOrBrawl";

        /// <summary>
        /// Symbol
        /// </summary>
        private static string Symbol() => "BOB";
        
        /// <summary>
        /// Version
        /// </summary>
        private static string Version() => "0.0.1";

        /// <summary>
        /// Get the total number of tokens that have been issued
        /// </summary>
        private static BigInteger TotalSupply() =>
            DataAccess.GetTotalSupplyAsBytes().AsBigInteger();

        /// <summary>
        /// Token URI
        /// </summary>
        private static string TokenURI(BigInteger tokenId) => "uri/" + tokenId;

        /// <summary>
        /// The number of tokens owned by an address
        /// </summary>
        private static BigInteger BalanceOf(byte[] owner) =>
            Storage.Get(Storage.CurrentContext, Keys.AddressBalanceKey(owner)).AsBigInteger();

        /// <summary>
        /// Get the token owner address
        /// </summary>
        private static byte[] OwnerOf(BigInteger tokenId)
        {
            var token = DataAccess.GetToken(tokenId.AsByteArray());
            if (token != null && token.Owner.Length > 0)
            {
                return token.Owner;
            }
            else
            {
                return new byte[] { };
            }
        }

        /// <summary>
        /// Get all gladiator ids owned by an address, not currently supported
        /// </summary>
        private static BigInteger[] TokensOfOwner(byte[] owner)
        {
            BigInteger tokenCount = NepOperations.BalanceOf(owner);
            BigInteger[] result = new BigInteger[(int)tokenCount];

            if (tokenCount == 0)
            {
                return result;
            }
            else
            {
                // We count on the fact that all NFTInfo have IDs starting at 1 and increasing
                // sequentially up to the totalCat count.
                for (BigInteger idx = 1; idx < tokenCount + 1; idx += 1)
                {
                    var tokenId = DataAccess.GetOwnersTokenIdByIndexAsBytes(owner, idx);
                    result[(int)idx - 1] = tokenId.AsBigInteger();
                }

                return result;
            }
        }

        /// <summary>
        /// Generate new token data and record
        /// </summary>
        public static BigInteger CreateToken(byte[] owner, byte health, byte criticalStrike, byte agility, byte attackSpeed, BigInteger generation)
        {
            if (owner.Length != 20)
            {
                return 0;
            }

            if (!Runtime.CheckWitness(ContractMain.MintOwner))
            {
                Runtime.Log("Only the contract owner may mint new tokens.");
                return 0;
            }

            //Determine whether the total amount is exceeded
            byte[] tokenaId = Storage.Get(Storage.CurrentContext, Keys.KeyAllSupply);

            byte[] tokenId = DataAccess.GetTotalSupplyAsBytes();
            BigInteger nextTokenId = tokenId.AsBigInteger() + 1;
            tokenId = nextTokenId.AsByteArray();

            TokenInfo token = new TokenInfo();
            token.Owner = owner;
            token.IsPregnant = 0;
            token.IsReady = 0;
            token.CanBreedAfter = 0;
            token.CloneWithId = 0;
            token.BirthTime = Blockchain.GetHeader(Blockchain.GetHeight()).Timestamp;
            token.MotherId = 0;
            token.FatherId = 0;
            token.Generation = generation;
            token.CooldownLevel = token.Generation;
            token.Agility = agility;
            token.AttackSpeed = attackSpeed;
            token.CriticalStrike = criticalStrike;



            DataAccess.SetToken(tokenId, token);
            DataAccess.SetTotalSupply(tokenId);
            DataAccess.IncreaseAddressBalance(owner);
            
            Events.RaiseBirthed(
                tokenId.AsBigInteger(), token.Owner, token.Agility, token.AttackSpeed,
                token.CriticalStrike, token.CanBreedAfter, token.CloneWithId, token.BirthTime, 
                token.MotherId, token.FatherId, token.Generation);

            return tokenId.AsBigInteger();
        }

        /// <summary>
        /// Transfer the token ownership
        /// </summary>
        public static bool Transfer(byte[] from, byte[] to, BigInteger tokenId)
        {
            if (from.Length != 20 || to.Length != 20)
            {
                return false;
            }

            if (from == to)
            {
                return true;
            }

            var token = DataAccess.GetToken(tokenId.AsByteArray());
            if (token == null)
            {
                return false;
            }

            if (from != token.Owner)
            {
                return false;
            }

            token.Owner = to;

            DataAccess.SetToken(tokenId.AsByteArray(), token);
            DataAccess.RemoveApproval(tokenId.AsByteArray());
            DataAccess.SetTransactionInfo(from, to, tokenId);

            DataAccess.IncreaseAddressBalance(to);
            DataAccess.DecreaseAddressBalance(from);

            Events.RaiseTransfer(from, to, tokenId);

            return true;
        }
    }
}
