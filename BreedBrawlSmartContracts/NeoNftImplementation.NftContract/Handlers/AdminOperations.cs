using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using NeoNftImplementation.NftContract.Infrastructure;
using NeoNftImplementation.NftContract.Models;
using System.Numerics;

namespace NeoNftImplementation.NftContract.Handlers
{
    public class AdminOperations : SmartContract
    {
        public static OperationResult HandleAdminOperation(string operation, params object[] args)
        {
            var result = new OperationResult { IsComplete = true };

            if (operation == "setAttrConfig")
            {
                if (args.Length != 13)
                {
                    result.Value = 0;
                    return result;
                }

                int normalSkillIdMax = (int)args[0];
                int rareSkillIdMax = (int)args[1];
                int normalEquipMax = (int)args[2];
                int rareEquipMax = (int)args[3];

                int atr0Max = (int)args[4];
                int atr1Max = (int)args[5];
                int atr2Max = (int)args[6];
                int atr3Max = (int)args[7];
                int atr4Max = (int)args[8];
                int atr5Max = (int)args[9];
                int atr6Max = (int)args[10];
                int atr7Max = (int)args[11];
                int atr8Max = (int)args[12];

                result.Value = SetAttrConfig(
                    normalSkillIdMax, rareSkillIdMax, normalEquipMax, rareEquipMax,
                    atr0Max, atr1Max, atr2Max, atr3Max, atr4Max, atr5Max, atr6Max, atr7Max, atr8Max);
            }
            else if (operation == "setMarketAddress")
            {
                if (args.Length != 1)
                {
                    result.Value = 0;
                    return result;
                }

                byte[] addr = (byte[])args[0];

                result.Value = SetMarketAddress(addr);
            }
            else if (operation == "refreshToken")
            {
                if (args.Length != 1)
                {
                    result.Value = false;
                    return result;
                }

                BigInteger tokenId = (BigInteger)args[0];

                result.Value = RefreshToken(tokenId);
            }
            else
            {
                result.IsComplete = false;
            }

            return result;
        }

        /// <summary>
        /// Set the auction house address (add an operation to add the total amount of gladiator release here)
        /// </summary>
        private static bool SetMarketAddress(byte[] address)
        {
            if (Runtime.CheckWitness(ContractMain.ContractOwner))
            {
                Storage.Put(Storage.CurrentContext, Keys.MarketAddress, address);
                Storage.Put(Storage.CurrentContext, Keys.KeyAllSupply, NepOperations.MaxSupply);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Set skill weapon configuration parameters
        /// </summary>
        private static bool SetAttrConfig(
            int normalSkillIdMax, int rareSkillIdMax, int normalEquipMax, int rareEquipMax,
            int atr1Max, int atr2Max, int atr3Max, int atr4Max, int atr5Max, int atr6Max, int atr7Max, int atr8Max, int atr9Max)
        {
            if (Runtime.CheckWitness(ContractMain.ContractOwner))
            {
                AttributeConfig config = new AttributeConfig();
                config.NormalSkillIdMin = 1;
                config.RareSkillIdMin = 201;
                config.NormalEquipIdMin = 1;
                config.RareEquipIdMin = 201;

                config.NormalSkillIdMax = normalSkillIdMax;
                config.RareSkillIdMax = rareSkillIdMax;
                config.NormalEquipIdMax = normalEquipMax;
                config.RareEquipIdMax = rareEquipMax;

                config.Atr1Max = atr1Max;
                config.Atr2Max = atr2Max;
                config.Atr3Max = atr3Max;
                config.Atr4Max = atr4Max;
                config.Atr5Max = atr5Max;
                config.Atr6Max = atr6Max;
                config.Atr7Max = atr7Max;
                config.Atr8Max = atr8Max;
                config.Atr9Max = atr9Max;

                DataAccess.SetAttributeConfig(config);

                return true;
            }

            return false;
        }


        /// <summary>
        /// Special case background refresh Gladiator clone status
        /// </summary>
        private static bool RefreshToken(BigInteger id)
        {
            if (Runtime.CheckWitness(ContractMain.MintOwner))
            {
                var token = DataAccess.GetToken(id.AsByteArray());

                if (token == null)
                {
                    return false;
                }

                if (token.ApprovalExpiration < Blockchain.GetHeader(Blockchain.GetHeight()).Timestamp)
                {

                }

                token.CloneWithId = 0;
                token.CanBreedAfter = 0;
                token.IsPregnant = 0;

                DataAccess.SetToken(id.AsByteArray(), token);

                return true;
            }

            return false;
        }
    }
}
