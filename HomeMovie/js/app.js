var app=angular.module('MyApp',['ngRoute'])
            .config(['$routeProvider', function($routeProvider){
                $routeProvider
                .when('/Home',{
                	templateUrl: './views/Home.html',
                	controller:'HomeCtrl'
                })
                .when('/Mainland',{
                	templateUrl: './views/Mainland.html',
                	controller:'MainlandCtrl'
                })
                .when('/Hongkong',{
                	templateUrl: './views/Hongkong.html',
                	controller:'HongkongCtrl'
                })
                .when('/America',{
                	templateUrl: './views/America.html',
                	controller:'AmericaCtrl'
                })
                .when('/Cartoon',{
                	templateUrl: './views/Cartoon.html',
                	controller:'CartoonCtrl'
                })
                .otherwise({redirectTo:'/Home'});
            }]);