using System.Numerics;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using NeoNftImplementation.MarketContract.Infrastructure;
using NeoNftImplementation.MarketContract.Models;
using Helper = Neo.SmartContract.Framework.Helper;
using dynamicAppCall = NeoNftImplementation.MarketContract.ContractMain.dynamicAppCall;

namespace NeoNftImplementation.MarketContract.Handlers
{
    public class AdminOperations : SmartContract
    {        
        public const ulong GenerationZeroMaxPrice = 1999000000;
        public const ulong GenerationZeroMinPrice = 9000000;
        public const ulong GenerationZeroAuctionDuration = 86400;

        public static OperationResult HandleAdminOperation(string operation, params object[] args)
        {
            var result = new OperationResult { IsComplete = true };

            if (operation == "setCgasScriptHash")
            {
                if (Runtime.CheckWitness(ContractMain.ContractOwner))
                {
                    Storage.Put(Storage.CurrentContext, Keys.SGasKey, (byte[])args[0]);
                    result.Value = new byte[] { 0x01 };
                    return result;
                }

                result.Value = new byte[] { 0x00 };
            }
            else if (operation == "setGenerationZeroPrice")
            {
                if (Runtime.CheckWitness(ContractMain.ContractOwner))
                {
                    BigInteger maxPrice = (BigInteger)args[0];
                    BigInteger minPrice = (BigInteger)args[1];
                    BigInteger duration = (BigInteger)args[2];
                    GenerationZeroPrice price = new GenerationZeroPrice
                    {
                        MaxPrice = maxPrice,
                        MinPrice = minPrice,
                        Duration = duration
                    };

                    result.Value = DataAccess.SetGenerationZeroPrice(price);
                    return result;
                }

                result.Value = false;
            }
            else if (operation == "createGenerationZeroAuction")
            {
                if (args.Length != 5)
                {
                    result.Value = 0;
                    return result;
                }

                byte strength = (byte)args[0];
                byte power = (byte)args[1];
                byte agile = (byte)args[2];
                byte speed = (byte)args[3];
                byte generation = (byte)args[4];

                result.Value = CreateGenerationZeroAuction(strength, power, agile, speed, generation);
            }
            else if (operation == "adminWithdraw")
            {
                if (args.Length != 2)
                {
                    result.Value = 0;
                    return result;
                };

                BigInteger flag = (BigInteger)args[0];
                BigInteger count = (BigInteger)args[1];

                result.Value = Withdraw(flag, count);
            }
            else
            {
                result.IsComplete = false;
            }

            return result;
        }

        /// <summary>
        /// Release 0 generation gladiators to the auction house
        /// </summary>
        private static bool CreateGenerationZeroAuction(byte strength, byte power, byte agile, byte speed, byte generation)
        {
            byte[] tokenOwner = ExecutionEngine.ExecutingScriptHash;
            if (Runtime.CheckWitness(ContractMain.MintOwner))
            {
                object[] args = new object[] { tokenOwner, strength, power, agile, speed, generation };
                BigInteger tokenId = (BigInteger)ContractMain.NftContractCall("createGenerationZeroFromAuction_app", args);
                if (tokenId == 0)
                {
                    return false;
                }

                BigInteger auctionStartPrice = GenerationZeroMaxPrice;
                BigInteger auctionEndPrice = GenerationZeroMinPrice;
                BigInteger auctionDuration = GenerationZeroAuctionDuration;

                object[] rawPrice = DataAccess.GetGenerationZeroPricesAsObjects();
                if (rawPrice.Length > 0)
                {
                    GenerationZeroPrice price = (GenerationZeroPrice)(object)rawPrice;
                    auctionStartPrice = price.MaxPrice;
                    auctionEndPrice = price.MinPrice;
                    auctionDuration = price.Duration;
                }

                return SaleGenerationZero(
                    ContractMain.ContractOwner, tokenId,
                    auctionStartPrice, auctionEndPrice,
                    auctionDuration, 0);
            }

            return false;
        }

        /// <summary>
        /// Withdrawal of income to contract owner
        /// </summary>
        private static bool Withdraw(BigInteger flag, BigInteger count)
        {
            if (Runtime.CheckWitness(ContractMain.ContractOwner))
            {
                object[] args = new object[] { ExecutionEngine.ExecutingScriptHash };
                byte[] cgasHash = DataAccess.GetCGasScriptHashAsBytes();
                dynamicAppCall dyncall = (dynamicAppCall)cgasHash.ToDelegate();

                BigInteger actualBalance = (BigInteger)dyncall("balanceOf", args);

                if (flag == 0)
                {
                    BigInteger localBalance = DataAccess.GetCGasBalanceAsBytes().AsBigInteger();
                    BigInteger maxWithdraw = actualBalance - localBalance;
                    if (count <= 0 || count > maxWithdraw)
                    {
                        count = maxWithdraw;
                    }
                }
                else
                {
                    count = actualBalance;
                    DataAccess.SetCGasBalance(0);
                }

                args = new object[]
                {
                    ExecutionEngine.ExecutingScriptHash,
                    ContractMain.ContractOwner,
                    count
                };

                dynamicAppCall dyncall2 = (dynamicAppCall)cgasHash.ToDelegate();
                bool appCallSuccess = (bool)dyncall2("transfer", args);
                if (!appCallSuccess)
                {
                    return false;
                }

                // booking cwt should not be accounted for here
                //_subTotal(count);
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// Added 0 generation gladiator
        /// </summary>
        private static bool SaleGenerationZero(
            byte[] tokenOwner, BigInteger tokenId,
            BigInteger startPrice, BigInteger endPrice,
            BigInteger duration, int sellType)
        {
            if (startPrice < 0 || endPrice < 0 || startPrice < endPrice)
            {
                return false;
            }

            if (endPrice < ContractMain.MinTxFee)
            {
                return false;
            }

            var nowtime = Blockchain.GetHeader(Blockchain.GetHeight()).Timestamp;

            MarketAuction info = new MarketAuction
            {
                Owner = tokenOwner,
                SellType = sellType,
                SellTime = nowtime,
                BeginPrice = startPrice,
                EndPrice = endPrice,
                Duration = duration
            };

            DataAccess.SetAuction(tokenId.AsByteArray(), info);
            Events.RaiseAuctioned(tokenOwner, tokenId, startPrice, endPrice, duration, sellType, nowtime);

            return true;
        }

        /// <summary>
        /// @dev Computes the next gen0 auction starting price, given
        /// * the average of the past 5 prices + 50%.
        /// </summary>
        private static BigInteger ComputeNextGen0Price()
        {
            BigInteger price = GenerationZeroMaxPrice;
            byte[] raw = Storage.Get(Storage.CurrentContext, "gene0Record");
            if (raw.Length != 0)
            {
                object[] rawRecord = (object[])Helper.Deserialize(raw);
                GenerationZeroRecord record = (GenerationZeroRecord)(object)rawRecord;
                BigInteger sum = record.LastPrice0 + record.LastPrice1 + record.LastPrice2 + record.LastPrice3 + record.LastPrice4;
                if (record.TotalSellCount >= 5)
                {
                    price = (sum / 5) * 3 / 2;
                    if (price < GenerationZeroMinPrice)
                    {
                        price = GenerationZeroMinPrice;
                    }
                }
            }

            return price;
        }
    }
}
