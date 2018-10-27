using System.Linq;
using System.Numerics;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using NeoNftImplementation.MarketContract.Models;
using NeoNftImplementation.MarketContract.Infrastructure;
using dynamicAppCall = NeoNftImplementation.MarketContract.ContractMain.dynamicAppCall;

namespace NeoNftImplementation.MarketContract.Handlers
{
    public class MarketOperations : SmartContract
    {
        /// <summary>
        /// Attepts to handle the current operation
        /// </summary>
        public static OperationResult HandleMarketOperation(string operation, params object[] args)
        {
            var result = new OperationResult { IsComplete = true };

            if (operation == "getHasAlreadyCharged")
            {
                if (args.Length != 1)
                {
                    result.Value = 0;
                    return result;
                };

                byte[] txid = (byte[])args[0];

                result.Value = MarketOperations.HasAlreadyCharged(txid);
            }
            else if (operation == "getTotalExchargeCGas")
            {
                result.Value = MarketOperations.CGasBalance();
            }
            else if (operation == "getGenerationZeroPrice")
            {
                DataAccess.GetGenerationZeroPricesAsObjects();
            }
            else if (operation == "getAuctionRecord")
            {
                if (args.Length != 1)
                {
                    result.Value = 0;
                    return result;
                }

                byte[] txid = (byte[])args[0];

                result.Value = DataAccess.GetAuctionRecordAsObjects(txid);
            }
            else if (operation == "getAuctionById")
            {
                if (args.Length != 1)
                {
                    result.Value = 0;
                    return result;
                };

                BigInteger tokenId = (BigInteger)args[0];

                result.Value = MarketOperations.GetAuctionById(tokenId);
            }
            else if (operation == "getAuctionAllFee")
            {
                result.Value = MarketOperations.GetAccumulatedFees();
            }
            else if (operation == "getCGasScriptHash")
            {
                result.Value = DataAccess.GetCGasScriptHashAsBytes();
            }

            else if (operation == "createSaleAuction")
            {
                if (args.Length != 5)
                {
                    result.Value = 0;
                    return result;
                }

                byte[] tokenOwner = (byte[])args[0];
                BigInteger tokenId = (BigInteger)args[1];
                BigInteger startPrice = (BigInteger)args[2];
                BigInteger endPrice = (BigInteger)args[3];
                BigInteger duration = (BigInteger)args[4];

                result.Value = MarketOperations.CreateSaleAuction(tokenOwner, tokenId, startPrice, endPrice, duration);
            }
            else if (operation == "buyOnAuction")
            {
                if (args.Length != 2)
                {
                    result.Value = 0;
                    return result;
                }

                byte[] owner = (byte[])args[0];
                BigInteger tokenId = (BigInteger)args[1];

                result.Value = BuyOnAuction(owner, tokenId);
            }
            else if (operation == "cancelAuction")
            {
                if (args.Length != 2)
                {
                    result.Value = 0;
                    return result;
                };

                byte[] owner = (byte[])args[0];
                BigInteger tokenId = (BigInteger)args[1];

                result.Value = MarketOperations.CancelAuction(owner, tokenId);
            }

            else if (operation == "cloneOnAuction")
            {
                if (args.Length != 3)
                {
                    result.Value = 0;
                    return result;
                }

                byte[] sender = (byte[])args[0];
                BigInteger motherId = (BigInteger)args[1];
                BigInteger fatherId = (BigInteger)args[2];

                result.Value = MarketOperations.CloneOnAuction(sender, motherId, fatherId);
            }
            else if (operation == "createCloneAuction")
            {
                if (args.Length != 5)
                {
                    result.Value = 0;
                    return result;
                };

                byte[] owner = (byte[])args[0];
                BigInteger tokenId = (BigInteger)args[1];
                BigInteger startPrice = (BigInteger)args[2];
                BigInteger endPrice = (BigInteger)args[3];
                BigInteger duration = (BigInteger)args[4];

                result.Value = MarketOperations.CreateCloneAuction(owner, tokenId, startPrice, endPrice, duration);
            }

            else if (operation == "breedWithMy")
            {
                if (args.Length != 3)
                {
                    result.Value = 0;
                    return result;
                };

                byte[] owner = (byte[])args[0];
                BigInteger motherId = (BigInteger)args[1];
                BigInteger fatherId = (BigInteger)args[2];

                result.Value = MarketOperations.BreedWithMy(owner, motherId, fatherId);
            }
            else if (operation == "withdrawCGas")
            {
                if (args.Length != 2)
                {
                    result.Value = 0;
                    return result;
                };

                byte[] owner = (byte[])args[0];
                BigInteger count = (BigInteger)args[1];

                result.Value = MarketOperations.WithdrawCGas(owner, count);
            }
            else if (operation == "refreshBalance")
            {
                if (args.Length != 2)
                {
                    result.Value = 0;
                    return result;
                };

                byte[] owner = (byte[])args[0];
                byte[] txid = (byte[])args[1];

                result.Value = MarketOperations.RefreshBalance(owner, txid);
            }
            else
            {
                result.IsComplete = false;
            }

            return result;
        }

        /// <summary>
        /// Has the txid been recharged?
        /// </summary>
        private static bool HasAlreadyCharged(byte[] txid)
        {
            byte[] bytes = DataAccess.GetTransactionAsBytes(txid);
            return bytes.Length == 0;
        }

        /// <summary>
        /// Use txid to recharge
        /// </summary>
        private static bool RefreshBalance(byte[] owner, byte[] txid)
        {
            if (owner.Length != 20)
            {
                Runtime.Log("Owner error.");
                return false;
            }

            byte[] bytes = DataAccess.GetTransactionAsBytes(txid);
            if (bytes.Length > 0)
            {
                // Has been processed
                return false;
            }

            object[] args = new object[] { txid };

            byte[] cgasHash = DataAccess.GetCGasScriptHashAsBytes();
            dynamicAppCall dyncall = (dynamicAppCall)cgasHash.ToDelegate();

            object[] appCallResult = (object[])dyncall("getTxInfo", args);
            if (appCallResult.Length > 0)
            {
                byte[] from = (byte[])appCallResult[0];
                byte[] to = (byte[])appCallResult[1];
                BigInteger value = (BigInteger)appCallResult[2];

                if (from == owner)
                {
                    if (to == ExecutionEngine.ExecutingScriptHash)
                    {
                        Storage.Put(Storage.CurrentContext, Keys.TransactionKey(txid), value);

                        BigInteger newBalance = 0;
                        byte[] ownerBalanceKey = Keys.UserBalanceKey(owner);
                        byte[] ownerBalance = Storage.Get(Storage.CurrentContext, ownerBalanceKey);
                        if (ownerBalance.Length > 0)
                        {
                            newBalance = ownerBalance.AsBigInteger();
                        }

                        newBalance += value;

                        IncreaseCGasBalance(value);

                        Storage.Put(Storage.CurrentContext, ownerBalanceKey, newBalance.AsByteArray());

                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Coin
        /// </summary>
        private static bool WithdrawCGas(byte[] sender, BigInteger count)
        {
            if (sender.Length != 20)
            {
                Runtime.Log("Owner error.");
                return false;
            }

            if (Runtime.CheckWitness(sender))
            {
                BigInteger newBalance = 0;
                byte[] ownerBalanceBytes = DataAccess.GetUserBalanceAsBytes(sender);
                if (ownerBalanceBytes.Length > 0)
                {
                    newBalance = ownerBalanceBytes.AsBigInteger();
                }

                if (count <= 0 || count > newBalance)
                {
                    count = newBalance;
                }

                object[] args = new object[] 
                {
                    ExecutionEngine.ExecutingScriptHash,
                    sender,
                    count,
                    ExecutionEngine.ExecutingScriptHash
                };

                byte[] cgasHash = DataAccess.GetCGasScriptHashAsBytes();
                dynamicAppCall dyncall = (dynamicAppCall)cgasHash.ToDelegate();
                bool appCallSuccess = (bool)dyncall("transferAPP", args);
                if (!appCallSuccess)
                {
                    return false;
                }

                newBalance -= count;
                DecreaseCGasBalance(count);

                byte[] senderBalanceKey = Keys.UserBalanceKey(sender);
                if (newBalance > 0)
                {
                    Storage.Put(Storage.CurrentContext, senderBalanceKey, newBalance.AsByteArray());
                }
                else
                {
                    Storage.Delete(Storage.CurrentContext, senderBalanceKey);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Create an auction
        /// </summary>
        private static bool CreateSaleAuction(
            byte[] tokenOwner, BigInteger tokenId, 
            BigInteger beginPrice, BigInteger endPrice, 
            BigInteger duration) =>
                SaleItemWithType(tokenOwner, tokenId, beginPrice, endPrice, duration, 0);        

        /// <summary>
        /// Clone auction creation
        /// </summary>
        private static bool CreateCloneAuction(
            byte[] tokenOwner, BigInteger tokenId, 
            BigInteger beginPrice, BigInteger endPrice, 
            BigInteger duration) =>
                SaleItemWithType(tokenOwner, tokenId, beginPrice, endPrice, duration, 1);

        /// <summary>
        /// sellType:0 Auction 1 clone auction
        /// </summary>
        private static bool SaleItemWithType(
            byte[] tokenOwner, BigInteger tokenId, 
            BigInteger startPrice, BigInteger endPrice, 
            BigInteger duration, int sellType)
        {
            if (tokenOwner.Length != 20)
            {
                Runtime.Log("Owner error.");
                return false;
            }

            if (startPrice < 0 || endPrice < 0 || startPrice < endPrice)
            {
                return false;
            }

            if (endPrice < ContractMain.MinTxFee)
            {
                // End price cannot be lower than the minimum handling fee
                return false;
            }

            //if (Runtime.CheckWitness(tokenOwner))
            // Items are placed in the auction house
            object[] args = new object[3] 
            {
                tokenOwner,
                ExecutionEngine.ExecutingScriptHash,
                tokenId
            };

            bool appCallSuccess = (bool)ContractMain.NftContractCall("transferFrom_app", args);
            if (appCallSuccess)
            {
                var nowtime = Blockchain.GetHeader(Blockchain.GetHeight()).Timestamp;

                MarketAuction auction = new MarketAuction
                {
                    Owner = tokenOwner,
                    SellType = sellType,
                    SellTime = nowtime,
                    BeginPrice = startPrice,
                    EndPrice = endPrice,
                    Duration = duration
                };

                DataAccess.SetAuction(tokenId.AsByteArray(), auction);
                Events.RaiseAuctioned(tokenOwner, tokenId, startPrice, endPrice, duration, sellType, nowtime);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Buy from the auction house, put the money under the contract name, and give the goods to the buyer.
        /// </summary>
        private static bool BuyOnAuction(byte[] sender, BigInteger tokenId)
        {
            if (!Runtime.CheckWitness(sender))
            {
                return false;
            }

            object[] rawAuction = DataAccess.GetAuctionAsObjects(tokenId.AsByteArray());
            if (rawAuction.Length > 0)
            {
                MarketAuction info = (MarketAuction)(object)rawAuction;

                var nowtime = Blockchain.GetHeader(Blockchain.GetHeight()).Timestamp;
                var secondPass = nowtime - info.SellTime;

                //var secondPass = (nowtime - info.sellTime) / 1000;
                byte[] senderBalanceKey = Keys.UserBalanceKey(sender);
                byte[] ownerBalanceKey = Keys.UserBalanceKey(info.Owner);

                BigInteger senderBalance = Storage.Get(Storage.CurrentContext, senderBalanceKey).AsBigInteger();
                BigInteger currentPrice = ComputeCurrentPrice(info.BeginPrice, info.EndPrice, info.Duration, secondPass);

                var fee = currentPrice * 199 / 10000;
                if (fee < ContractMain.MinTxFee)
                {
                    fee = ContractMain.MinTxFee;
                }

                if (currentPrice < fee)
                {
                    currentPrice = fee;
                }

                if (senderBalance < currentPrice)
                {
                    // Not enough money
                    return false;
                }

                object[] args = new object[] 
                {
                    ExecutionEngine.ExecutingScriptHash,
                    sender,
                    tokenId
                };

                bool appCallSuccess = (bool)ContractMain.NftContractCall("transfer_app", args);
                if (!appCallSuccess)
                {
                    return false;
                }

                Storage.Put(Storage.CurrentContext, senderBalanceKey, senderBalance - currentPrice);

                BigInteger sellPrice = currentPrice - fee;
                DecreaseCGasBalance(fee);

                // The money is in the name of the seller/
                BigInteger newBalance = 0;
                byte[] sellerBalance = Storage.Get(Storage.CurrentContext, ownerBalanceKey);
                if (sellerBalance.Length > 0)
                {
                    newBalance = sellerBalance.AsBigInteger();
                }

                newBalance = newBalance + sellPrice;
                Storage.Put(Storage.CurrentContext, ownerBalanceKey, newBalance);

                Storage.Delete(Storage.CurrentContext, tokenId.AsByteArray());
                // transaction record
                /*AuctionRecord record = new AuctionRecord();
                record.tokenId = tokenId;
                record.seller = owner;
                record.buyer = sender;
                record.sellType = 0;
                record.sellPrice = curBuyPrice;
                record.sellTime = nowtime;

                _putAuctionRecord(tokenId.AsByteArray(), record);

                if(owner == ContractOwner)
                {
                    Gene0Record gene0Record;
                    byte[] v = (byte[])Storage.Get(Storage.CurrentContext, "gene0Record");
                    if(v.Length==0)
                    {
                        gene0Record = new Gene0Record();
                    }
                    else
                    {
                        object[] infoRec = (object[])Helper.Deserialize(v);
                        gene0Record = (Gene0Record)(object)infoRec;
                    }
                    int idx = (int)gene0Record.totalSellCount % 5;
                    if (idx == 0)
                    {
                        gene0Record.lastPrice0 = curBuyPrice;
                    }
                    else if (idx == 1)
                    {
                        gene0Record.lastPrice1 = curBuyPrice;
                    }
                    else if (idx == 2)
                    {
                        gene0Record.lastPrice1 = curBuyPrice;
                    }
                    else if (idx == 3)
                    {
                        gene0Record.lastPrice1 = curBuyPrice;
                    }
                    else if (idx == 4)
                    {
                        gene0Record.lastPrice1 = curBuyPrice;
                    }

                    gene0Record.totalSellCount += 1;

                    //
                    byte[] infoRec2 = Helper.Serialize(gene0Record);
                    Storage.Put(Storage.CurrentContext, "gene0Record", infoRec2);
                }*/

                Events.RaiseAuctionBuy(sender, tokenId, currentPrice, fee, nowtime);
                return true;

            }

            return false;
        }

        /// <summary>
        /// Buy auction clones
        /// </summary>
        private static bool CloneOnAuction(byte[] sender, BigInteger motherId, BigInteger fatherId)
        {
            if (!Runtime.CheckWitness(sender))
            {
                return false;
            }

            object[] rawFatherAuction = DataAccess.GetAuctionAsObjects(fatherId.AsByteArray());
            if (rawFatherAuction.Length > 0)
            {
                MarketAuction fatherAuction = (MarketAuction)(object)rawFatherAuction;
                byte[] owner = fatherAuction.Owner;

                if (fatherAuction.SellType == 1)
                {
                    byte[] senderBalanceKey = Keys.UserBalanceKey(sender);

                    var nowtime = Blockchain.GetHeader(Blockchain.GetHeight()).Timestamp;
                    var secondPass = nowtime - fatherAuction.SellTime;
                    //var secondPass = (nowtime - fatherInfo.sellTime) / 1000;

                    BigInteger senderBalance = Storage.Get(Storage.CurrentContext, senderBalanceKey).AsBigInteger();
                    BigInteger currentBuyPrice = ComputeCurrentPrice(
                        fatherAuction.BeginPrice, fatherAuction.EndPrice, fatherAuction.Duration, secondPass);

                    var fee = currentBuyPrice * 199 / 10000;
                    if (fee < ContractMain.MinTxFee)
                    {
                        fee = ContractMain.MinTxFee;
                    }

                    if (currentBuyPrice < fee)
                    {
                        currentBuyPrice = fee;
                    }

                    if (senderBalance < currentBuyPrice)
                    {
                        return false;
                    }

                    object[] args = new object[] 
                    {
                        sender,
                        motherId,
                        fatherId
                    };

                    bool appCallSuccess = (bool)ContractMain.NftContractCall("bidOnClone_app", args);
                    if (!appCallSuccess)
                    {
                        return false;
                    }

                    Storage.Put(Storage.CurrentContext, senderBalanceKey, senderBalance - currentBuyPrice);

                    currentBuyPrice -= fee;
                    DecreaseCGasBalance(fee);

                    BigInteger newBalance = 0;
                    byte[] ownerBalanceKey = Keys.UserBalanceKey(owner);
                    byte[] sellerBalance = Storage.Get(Storage.CurrentContext, ownerBalanceKey);
                    if (sellerBalance.Length > 0)
                    {
                        newBalance = sellerBalance.AsBigInteger();
                    }

                    newBalance = newBalance + currentBuyPrice;
                    Storage.Put(Storage.CurrentContext, ownerBalanceKey, newBalance);

                    //// Do not delete the auction record
                    //Storage.Delete(Storage.CurrentContext, tokenId.AsByteArray());

                    // transaction record
                    /*AuctionRecord record = new AuctionRecord();
                    record.tokenId = fatherGlaId;
                    record.seller = owner;
                    record.buyer = sender;
                    record.sellType = 1;
                    record.sellPrice = curBuyPrice + fee;
                    record.sellTime = nowtime;

                    _putAuctionRecord(fatherGlaId.AsByteArray(), record);*/

                    Events.RaiseAuctionClone(sender, motherId, fatherId, currentBuyPrice, fee, nowtime);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Clone with your own gladiator
        /// </summary>
        private static bool BreedWithMy(byte[] sender, BigInteger motherId, BigInteger fatherId)
        {
            if (!Runtime.CheckWitness(sender))
            {
                return false;
            }

            byte[] senderBalanceKey = Keys.UserBalanceKey(sender);
            BigInteger senderBalance = Storage.Get(Storage.CurrentContext, senderBalanceKey).AsBigInteger();

            var fee = ContractMain.MinTxFee;
            if (senderBalance < fee)
            {
                return false;
            }

            // Start cloning
            object[] args = new object[] 
            {
                sender,
                motherId,
                fatherId
            };

            bool appCallSuccess = (bool)ContractMain.NftContractCall("breedWithMy_app", args);
            if (appCallSuccess)
            {
                senderBalance -= fee;
                Storage.Put(Storage.CurrentContext, senderBalanceKey, senderBalance);

                DecreaseCGasBalance(fee);
                var nowtime = Blockchain.GetHeader(Blockchain.GetHeight()).Timestamp;

                Events.RaiseMyClone(sender, motherId, fatherId, fee, nowtime);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Cancel the auction
        /// </summary>
        private static bool CancelAuction(byte[] sender, BigInteger tokenId)
        {
            object[] rawAuction = DataAccess.GetAuctionAsObjects(tokenId.AsByteArray());
            if (rawAuction.Length > 0)
            {
                MarketAuction auction = (MarketAuction)(object)rawAuction;
                bool isAdmin = Runtime.CheckWitness(ContractMain.ContractOwner);
                if (sender != auction.Owner && isAdmin == false)
                {
                    return false;
                }

                if (Runtime.CheckWitness(sender) || isAdmin)
                {
                    object[] args = new object[] 
                    {
                        ExecutionEngine.ExecutingScriptHash,
                        auction.Owner,
                        tokenId
                    };

                    bool appCallSuccess = (bool)ContractMain.NftContractCall("transfer_app", args);
                    if (appCallSuccess)
                    {
                        DataAccess.DeleteAuction(tokenId.AsByteArray());
                        Events.RaiseCancelAuctioned(auction.Owner, tokenId);

                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Get auction information
        /// </summary>
        private static MarketAuction GetAuctionById(BigInteger tokenId)
        {
            object[] rawAuction = DataAccess.GetAuctionAsObjects(tokenId.AsByteArray());
            return (MarketAuction)(object)rawAuction;
        }
        
        private static BigInteger GetAccumulatedFees()
        {
            object[] args = new object[] { ExecutionEngine.ExecutingScriptHash };
            byte[] cgasScriptHash = DataAccess.GetCGasScriptHashAsBytes();
            dynamicAppCall cgasAppCall = (dynamicAppCall)cgasScriptHash.ToDelegate();

            BigInteger availableBalance = (BigInteger)cgasAppCall("balanceOf", args);
            BigInteger totalAvailableCGas = DataAccess.GetCGasBalanceAsBytes().AsBigInteger();

            BigInteger maxWithdrawAmount = availableBalance - totalAvailableCGas;
            return maxWithdrawAmount;
        }

        /// <summary>
        /// Store increased tokens
        /// </summary>
        private static void IncreaseCGasBalance(BigInteger amount)
        {
            BigInteger total = DataAccess.GetCGasBalanceAsBytes().AsBigInteger();
            total += amount;

            DataAccess.SetCGasBalance(total);
        }

        /// <summary>
        /// Total amount of tokens stored for reduction
        /// </summary>
        private static void DecreaseCGasBalance(BigInteger amount)
        {
            BigInteger balance = DataAccess.GetCGasBalanceAsBytes().AsBigInteger();
            balance -= amount;

            if (balance > 0)
            {
                DataAccess.SetCGasBalance(balance);
            }
            else
            {
                Storage.Delete(Storage.CurrentContext, Keys.CGasBalanceKey);
            }
        }

        /// <summary>
        /// All users have a token in the auction house, excluding the handling fee.
        /// </summary>
        private static BigInteger CGasBalance() =>
            DataAccess.GetCGasBalanceAsBytes().AsBigInteger();

        /// <summary>
        /// Computes the current price of an auction.
        /// </summary>
        private static BigInteger ComputeCurrentPrice(BigInteger beginPrice, BigInteger endingPrice, BigInteger duration, BigInteger secondsPassed)
        {
            if (duration < 1)
            {
                // Avoid being divided by 0
                duration = 1;
            }

            if (secondsPassed >= duration)
            {
                // We've reached the end of the dynamic pricing portion
                // of the auction, just return the end price.
                return endingPrice;
            }
            else
            {
                // Starting price can be higher than ending price (and often is!), so
                // this delta can be negative.
                //var totalPriceChange = endingPrice - beginPrice;

                // This multiplication can't overflow, _secondsPassed will easily fit within
                // 64-bits, and totalPriceChange will easily fit within 128-bits, their product
                // will always fit within 256-bits.
                //var currentPriceChange = totalPriceChange * secondsPassed / duration;
                //var currentPrice = beginPrice + (endingPrice - beginPrice) * secondsPassed / duration; 
                return beginPrice + (endingPrice - beginPrice) * secondsPassed / duration;
            }
        }
    }
}
