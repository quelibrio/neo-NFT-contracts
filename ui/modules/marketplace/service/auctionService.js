angular.module('marketplace').factory('auctionService',function($http) {
    let baseUrl = 'http://localhost:5001/api';
    var auctionService = {
        get () {
            return $http.get(`${baseUrl}/auction`).then(response => response.result).then(() => [
                {
                    "startDate":"2018-10-24 19:20:00.890000",
                    "endDate":"2018-10-24 20:40:00.890000",
                    "startPrice":1000.20,
                    "tokenId":112386,
                    "addressId":12562
                },{
                    "startDate":"2018-10-24 19:20:00.890000",
                    "endDate":"2018-10-24 20:40:00.890000",
                    "startPrice":561.3,
                    "tokenId":69541,
                    "addressId":2321687
                },{
                    "startDate":"2018-10-24 19:20:00.890000",
                    "endDate":"2018-10-24 20:40:00.890000",
                    "startPrice":3451.1,
                    "tokenId":18670936,
                    "addressId":25232
                },{
                    "startDate":"2018-10-24 19:20:00.890000",
                    "endDate":"2018-10-24 20:40:00.890000",
                    "startPrice":643.6,
                    "tokenId":1235089156,
                    "addressId":207877
                },{
                    "startDate":"2018-10-24 19:20:00.890000",
                    "endDate":"2018-10-24 20:40:00.890000",
                    "startPrice":744,
                    "tokenId":121587,
                    "addressId":2987
                },{
                    "startDate":"2018-10-24 19:20:00.890000",
                    "endDate":"2018-10-24 20:40:00.890000",
                    "startPrice":876,
                    "tokenId":175982117,
                    "addressId":2867
                },{
                    "startDate":"2018-10-24 19:20:00.890000",
                    "endDate":"2018-10-24 20:40:00.890000",
                    "startPrice":907,
                    "tokenId":62936123671,
                    "addressId":2756
                },{
                    "startDate":"2018-10-24 19:20:00.890000",
                    "endDate":"2018-10-24 20:40:00.890000",
                    "startPrice":32,
                    "tokenId":24755641,
                    "addressId":2235
                },{
                    "startDate":"2018-10-24 19:20:00.890000",
                    "endDate":"2018-10-24 20:40:00.890000",
                    "startPrice":124,
                    "tokenId":3,
                    "addressId":253329857
                }
                ]
            );
        },
        myTokens(address) {
            let otherAddress = window.neo.getByteArrayAddress(address);
            return $http.get(`${baseUrl}/ownedTokens?address=${otherAddress.value}`).then(response => response.result).then(() => [
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
                },{
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
                },{
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
                }]);
        }
    };

    return auctionService;
});
