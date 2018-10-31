angular.module('marketplace').factory('auctionService', function ($http) {
    let baseUrl = 'http://localhost:5001/api';
    var auctionService = {
        get() {
            return $http.get(`${baseUrl}/auction`).then(response => response.data).then(() => [
                    {
                        stats: {
                            health: 5790,
                            mana: 1010,
                            agility: 100,
                            stamina: 123,
                            criticalStrike: 41,
                            attackSpeed: 34,
                            versatility: 6,
                            mastery: 78,
                            gen: 0
                        },
                        "startDate": "2018-10-28 13:20:00.890000",
                        "endDate": "2018-10-29 20:40:00.890000",
                        "startPrice": 471000000000,
                        "tokenId": 12,
                        isMine: true
                    }, {
                        stats: {
                            health: 3790,
                            mana: 5010,
                            agility: 600,
                            stamina: 23,
                            criticalStrike: 1,
                            attackSpeed: 39,
                            versatility: 61,
                            mastery: 18,
                            gen: 0
                        },
                        "startDate": "2018-10-24 12:20:00.890000",
                        "endDate": "2018-10-24 20:40:00.890000",
                        "startPrice": 790900090000,
                        "tokenId": 11,
                        isMine: true
                    }, {
                        stats: {
                            health: 1790,
                            mana: 1010,
                            agility: 600,
                            stamina: 23,
                            criticalStrike: 1,
                            attackSpeed: 39,
                            versatility: 61,
                            mastery: 8,
                            gen: 0
                        },
                        "startDate": "2018-10-24 11:53:00.890000",
                        "endDate": "2018-10-24 20:40:00.890000",
                        "startPrice": 6900000000,
                        "tokenId": 19,
                        "addressId": 25232
                    }, {
                        stats: {
                            health: 541,
                            mana: 1010,
                            agility: 600,
                            stamina: 23,
                            criticalStrike: 1,
                            attackSpeed: 39,
                            versatility: 61,
                            mastery: 8,
                        },
                        "startDate": "2018-10-24 11:53:00.890000",
                        "endDate": "2018-10-24 20:40:00.890000",
                        "startPrice": 6900000000,
                        "tokenId": 19,
                        "addressId": 25232
                    }, {
                        stats: {
                            health: 790,
                            mana: 1010,
                            agility: 600,
                            stamina: 23,
                            criticalStrike: 1,
                            attackSpeed: 39,
                            versatility: 61,
                            mastery: 8,
                        },
                        "startDate": "2018-10-24 11:53:00.890000",
                        "endDate": "2018-10-24 20:40:00.890000",
                        "startPrice": 6900000000,
                        "tokenId": 21,
                        "addressId": 25232
                    }
                ]
            );
        },
        myTokens(address) {
            let otherAddress = window.neo.getByteArrayAddress(address);
            return $http.get(`${baseUrl}/ownedTokens?address=${otherAddress.value}`)
                .catch(err => console.error(err))
                .then(response => response.data)/*.then(() => [
             {
             "id": 1,
             "nickname": "SuperPalavVVVVVVVV",
             "txId": "51c2d176b7e1be634c94525f5fbd1fbe",
             "health": 110,
             "mana": 100,
             "agility": 200,
             "stamina": 100,
             "criticalStrike": 1,
             "attackSpeed": 5,
             "mastery": 10,
             "versatility": 5,
             "imageUrl": "https://tylerhollywood.files.wordpress.com/2010/01/hero.jpg",
             "addressId": 1,
             "experience": 100,
             "level": 50,
             "isBreeding": false,
             "isBrawling": false,
             "address": null,
             "auctions": []
             },
             {
             "id": 1,
             "nickname": "KKKKKK",
             "txId": "51c2d176b7e1be634c94525f5fbd1ebe",
             "health": 110,
             "mana": 100,
             "agility": 200,
             "stamina": 100,
             "criticalStrike": 1,
             "attackSpeed": 5,
             "mastery": 10,
             "versatility": 5,
             "imageUrl": "https://tylerhollywood.files.wordpress.com/2010/01/hero.jpg",
             "addressId": 1,
             "experience": 100,
             "level": 50,
             "isBreeding": false,
             "isBrawling": false,
             "address": null,
             "auctions": []
             }, {
             "id": 1,
             "nickname": "SuperPalavVVVVVVVV#3",
             "txId": "51c2d176b7e1be634c94525f5fbd1fbe",
             "health": 110,
             "mana": 100,
             "agility": 200,
             "stamina": 100,
             "criticalStrike": 1,
             "attackSpeed": 5,
             "mastery": 10,
             "versatility": 5,
             "imageUrl": "https://tylerhollywood.files.wordpress.com/2010/01/hero.jpg",
             "addressId": 1,
             "experience": 100,
             "level": 50,
             "isBreeding": false,
             "isBrawling": false,
             "address": null,
             "auctions": []
             },
             {
             "id": 1,
             "nickname": "SuperPalavVVVVVVVV#4",
             "txId": "51c2d176b7e1be634c94525f5fbd1fbe",
             "health": 110,
             "mana": 100,
             "agility": 200,
             "stamina": 100,
             "criticalStrike": 1,
             "attackSpeed": 5,
             "mastery": 10,
             "versatility": 5,
             "imageUrl": "https://tylerhollywood.files.wordpress.com/2010/01/hero.jpg",
             "addressId": 1,
             "experience": 100,
             "level": 50,
             "isBreeding": false,
             "isBrawling": false,
             "address": null,
             "auctions": []
             }, {
             "id": 1,
             "nickname": "SuperPalavVVVVVVVV#5",
             "txId": "51c2d176b7e1be634c94525f5fbd1fbe",
             "health": 110,
             "mana": 100,
             "agility": 200,
             "stamina": 100,
             "criticalStrike": 1,
             "attackSpeed": 5,
             "mastery": 10,
             "versatility": 5,
             "imageUrl": "https://tylerhollywood.files.wordpress.com/2010/01/hero.jpg",
             "addressId": 1,
             "experience": 100,
             "level": 50,
             "isBreeding": false,
             "isBrawling": false,
             "address": null,
             "auctions": []
             }])*/;
        }
    };

    return auctionService;
});
