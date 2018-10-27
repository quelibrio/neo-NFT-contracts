using System.Numerics;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using NeoNftImplementation.NftContract.Infrastructure;
using NeoNftImplementation.NftContract.Models;

namespace NeoNftImplementation.NftContract.Handlers
{
    public class GameOperations : SmartContract
    {
        /// <summary>
        /// Attepts to handle the current operation
        /// </summary>
        public static OperationResult HandleGameOperation(string operation, params object[] args)
        {
            var callingScript = ExecutionEngine.CallingScriptHash;
            var result = new OperationResult { IsComplete = true };

            if (operation == "getIsPregnant")
            {
                if (args.Length != 1)
                {
                    result.Value = 0;
                    return result;
                }

                BigInteger tokenId = (BigInteger)args[0];
                result.Value = GameOperations.IsPregnant(tokenId);
            }
            else if (operation == "getCanBreedWithById")
            {
                if (args.Length != 2)
                {
                    result.Value = 0;
                    return result;
                }

                BigInteger motherId = (BigInteger)args[0];
                BigInteger fatherId = (BigInteger)args[1];
                result.Value = GameOperations.CanBreedWithById(motherId, fatherId);
            }
            else if (operation == "getIsCloneApplySucc")
            {
                if (args.Length != 1)
                {
                    result.Value = 0;
                    return result;
                }

                BigInteger tokenId = (BigInteger)args[0];
                result.Value = GameOperations.IsCloneApplySuccessful(tokenId);
            }
            else if (operation == "getIsReadyToBreed")
            {
                if (args.Length != 1)
                {
                    result.Value = 0;
                    return result;
                }

                BigInteger tokenId = (BigInteger)args[0];
                result.Value = GameOperations.IsReadyToBreed(tokenId);
            }

            else if (operation == "giveBirth")
            {
                if (args.Length != 1)
                {
                    result.Value = 0;
                    return result;
                }

                BigInteger motherId = (BigInteger)args[0];
                result.Value = GameOperations.GiveBirth(motherId);
            }

            else if (operation == "bidOnClone_app")
            {
                if (args.Length != 3)
                {
                    result.Value = 0;
                    return result;
                }

                byte[] owner = (byte[])args[0];
                BigInteger motherId = (BigInteger)args[1];
                BigInteger fatherId = (BigInteger)args[2];

                byte[] marketAddress = DataAccess.GetMarketAddressAsBytes();
                if (callingScript.AsBigInteger() != marketAddress.AsBigInteger())
                {
                    result.Value = false;
                    return result;
                }

                result.Value = GameOperations.BreedWithId_app(owner, motherId, fatherId);
            }

            else if (operation == "breedWithMy_app")
            {
                if (args.Length != 3)
                {
                    result.Value = 0;
                    return result;
                }

                byte[] owner = (byte[])args[0];
                BigInteger motherId = (BigInteger)args[1];
                BigInteger fatherId = (BigInteger)args[2];

                byte[] marketAddress = DataAccess.GetMarketAddressAsBytes();
                if (callingScript.AsBigInteger() != marketAddress.AsBigInteger())
                {
                    result.Value = false;
                    return result;
                }

                object[] rawFatherToken = DataAccess.GetTokenAsObjects(fatherId.AsByteArray());
                if (rawFatherToken.Length > 0)
                {
                    TokenInfo fatherInfo = (TokenInfo)(object)rawFatherToken;
                    if (fatherInfo.Owner.AsBigInteger() == owner.AsBigInteger())
                    {
                        result.Value = GameOperations.BreedWithId_app(owner, motherId, fatherId);
                        return result;
                    }
                }

                result.Value = false;
            }
            else if (operation == "createGenerationZeroFromAuction_app")
            {
                byte[] tokenOwner = (byte[])args[0];
                byte strength = (byte)args[1];
                byte power = (byte)args[2];
                byte agile = (byte)args[3];
                byte speed = (byte)args[4];
                BigInteger generation = (int)args[5];

                byte[] marketAddress = DataAccess.GetMarketAddressAsBytes();
                if (callingScript.AsBigInteger() != marketAddress.AsBigInteger())
                {
                    result.Value = false;
                    return result;
                }

                result.Value = NepOperations.CreateToken(tokenOwner, strength, power, agile, speed, generation);
            }
            else
            {
                result.IsComplete = false;
            }

            return result;
        }

        /// <summary>
        /// Clone with a gladiator for other contract calls
        /// </summary>
        private static bool BreedWithId_app(byte[] sender, BigInteger motherId, BigInteger fatherId)
        {
            if (!CanBreedWithById(motherId, fatherId))
            {
                return false;
            }

            object[] rawMotherToken = DataAccess.GetTokenAsObjects(motherId.AsByteArray());
            object[] rawFatherToken = DataAccess.GetTokenAsObjects(fatherId.AsByteArray());
            if (rawMotherToken.Length > 0 && rawFatherToken.Length > 0)
            {
                TokenInfo motherToken = (TokenInfo)(object)rawMotherToken;
                if (motherToken.Owner.AsBigInteger() == sender.AsBigInteger())
                {
                    return BreedWith(motherId, fatherId);
                }
            }

            return false;
        }

        /// <summary>
        /// Clone with another token
        /// </summary>
        private static bool BreedWith(BigInteger motherId, BigInteger fatherId)
        {
            TokenInfo motherToken = DataAccess.GetToken(motherId.AsByteArray());
            TokenInfo fatherToken = DataAccess.GetToken(fatherId.AsByteArray());

            BigInteger cooldownLevel = motherToken.CooldownLevel;
            if (cooldownLevel < fatherToken.CooldownLevel)
            {
                cooldownLevel = fatherToken.CooldownLevel;
            }
            
            uint nowtime = Blockchain.GetHeader(Blockchain.GetHeight()).Timestamp;
            BigInteger cooldown = GetCooldown(cooldownLevel);

            motherToken.IsPregnant = 1;
            motherToken.CloneWithId = fatherId;
            motherToken.CanBreedAfter = nowtime + cooldown;

            DataAccess.SetToken(motherId.AsByteArray(), motherToken);

            cooldown = GetCooldown(fatherToken.CooldownLevel);
            fatherToken.CanBreedAfter = nowtime + cooldown;

            DataAccess.SetToken(fatherId.AsByteArray(), fatherToken);
            Events.RaiseTokenCloned(motherToken.Owner, motherId, motherToken.CanBreedAfter, fatherId, fatherToken.CanBreedAfter);

            return true;
        }

        /// <summary>
        /// New token is created
        /// </summary>
        private static BigInteger GiveBirth(BigInteger motherId)
        {
            object[] rawMotherToken = DataAccess.GetTokenAsObjects(motherId.AsByteArray());
            if (rawMotherToken.Length > 0)
            {
                TokenInfo motherToken = (TokenInfo)(object)rawMotherToken;
                BigInteger fatherId = motherToken.CloneWithId;
                uint nowtime = Blockchain.GetHeader(Blockchain.GetHeight()).Timestamp;
                if (fatherId <= 0 || nowtime < motherToken.CanBreedAfter)
                {
                    return 0;
                }

                object[] rawFatherToken = DataAccess.GetTokenAsObjects(fatherId.AsByteArray());
                if (rawFatherToken.Length > 0)
                {
                    TokenInfo fatherToken = (TokenInfo)(object)rawFatherToken;
                    object[] rawToken = GetMixGene(motherToken, fatherToken);
                    if (rawToken.Length == 0)
                    {
                        return 0;
                    }

                    TokenInfo token = (TokenInfo)(object)rawToken;
                    token.FatherId = fatherId;
                    token.MotherId = motherId;

                    byte[] tokenId = DataAccess.GetTotalSupplyAsBytes();
                    BigInteger nextId = tokenId.AsBigInteger() + 1;
                    tokenId = nextId.AsByteArray();

                    DataAccess.SetToken(tokenId, token);
                    DataAccess.SetTotalSupply(tokenId);

                    motherToken.CloneWithId = 0;
                    motherToken.CanBreedAfter = 0;
                    motherToken.IsPregnant = 0;
                    motherToken.CooldownLevel += 1;
                    DataAccess.SetToken(motherId.AsByteArray(), motherToken);

                    fatherToken.CanBreedAfter = 0;
                    fatherToken.CooldownLevel += 1;
                    DataAccess.SetToken(fatherId.AsByteArray(), fatherToken);

                    Events.RaiseBirthed(tokenId.AsBigInteger(), token.Owner, token.IsPregnant, token.IsReady, token.CooldownLevel, token.CanBreedAfter,
                        token.CloneWithId, token.BirthTime, token.MotherId, token.FatherId, token.Generation);

                    return 1;
                }
            }

            return 0;
        }

        /// <summary>
        /// Determine if the gladiator is pregnant
        /// </summary>
        private static bool IsPregnant(BigInteger tokenId)
        {
            object[] rawToken = DataAccess.GetTokenAsObjects(tokenId.AsByteArray());
            if (rawToken.Length > 0)
            {
                TokenInfo token = (TokenInfo)(object)rawToken;
                if (token.CloneWithId > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Query whether the clone request is executed successfully
        /// </summary>
        private static bool IsCloneApplySuccessful(BigInteger tokenId)
        {
            object[] rawMotherToken = DataAccess.GetTokenAsObjects(tokenId.AsByteArray());
            if (rawMotherToken.Length > 0)
            {
                TokenInfo motherToken = (TokenInfo)(object)rawMotherToken;
                if (motherToken.CloneWithId > 0)
                {
                    object[] fatherTokenRaw = DataAccess.GetTokenAsObjects(motherToken.CloneWithId.AsByteArray());
                    if (fatherTokenRaw.Length > 0)
                    {
                        TokenInfo fatherToken = (TokenInfo)(object)fatherTokenRaw;
                        if (motherToken.CanBreedAfter > 0 && motherToken.IsPregnant == 1 && fatherToken.CanBreedAfter > 0)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Is the pregnant gladiator at the time of having a baby?
        /// </summary>
        private static bool IsReadyToBreed(BigInteger tokenId)
        {
            object[] rawToken = DataAccess.GetTokenAsObjects(tokenId.AsByteArray());
            if (rawToken.Length > 0)
            {
                TokenInfo token = (TokenInfo)(object)rawToken;
                if (token.CloneWithId <= 0)
                {
                    return false;
                }

                uint nowtime = Blockchain.GetHeader(Blockchain.GetHeight()).Timestamp;
                if (token.CanBreedAfter > 0 && nowtime >= token.CanBreedAfter)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determine if two gladiators can be cloned
        /// </summary>
        private static bool CanBreedWithById(BigInteger motherId, BigInteger fatherId)
        {
            object[] rawMotherToken = DataAccess.GetTokenAsObjects(motherId.AsByteArray());
            object[] rawFatherToken = DataAccess.GetTokenAsObjects(fatherId.AsByteArray());
            if (rawMotherToken.Length > 0 && rawFatherToken.Length > 0)
            {
                TokenInfo motherToken = (TokenInfo)(object)rawMotherToken;
                TokenInfo fatherToken = (TokenInfo)(object)rawFatherToken;
                if (motherToken.CloneWithId > 0 || fatherToken.CloneWithId > 0)
                {
                    return false;
                }

                uint nowtime = Blockchain.GetHeader(Blockchain.GetHeight()).Timestamp;

                if ((motherToken.CanBreedAfter == 0 || (motherToken.CloneWithId == 0 && nowtime > motherToken.CanBreedAfter)) &&
                    (fatherToken.CanBreedAfter == 0 || (fatherToken.CloneWithId == 0 && nowtime > fatherToken.CanBreedAfter)))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Get the cooling time in seconds
        /// </summary>
        private static BigInteger GetCooldown(BigInteger level)
        {
            BigInteger[] cooldownLevels = new BigInteger[] 
            {
                1, 10, 30, 60, 120, 240,
                489, 960, 1440, 2160, 3130,
                4380, 5900, 7670, 9600, 11500,
                13300, 14600, 15300
            };

            if (level > 18)
            {
                level = 18;
            }

            BigInteger cooldown = cooldownLevels[(int)level];
            cooldown *= 60;

            return cooldown;
        }

        /// <summary>
        /// Gene hybrid algorithm
        /// </summary>
        private static object[] GetMixGene(TokenInfo motherToken, TokenInfo fatherToken)
        {
            // In order to increase the fun of the game, the code of the gene mixture is not disclosed, and it needs to be completed according to the game plan.
            // In order to successfully compile, return an object without initial data

            TokenInfo token = new TokenInfo();
            return (object[])(object)token;
        }
    }
}
