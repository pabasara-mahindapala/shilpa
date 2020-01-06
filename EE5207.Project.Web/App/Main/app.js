(function () {
    'use strict';

    var app = angular.module('app', [
        'ngAnimate',
        'ngSanitize',
        'ngFileUpload',
        'ui.router',
        'ui.bootstrap',
        'ui.jq',
        'abp',
        'barcodeGenerator'
    ]);

    //Configuration for Angular UI routing.
    app.config([
        '$stateProvider', '$urlRouterProvider', '$locationProvider', '$qProvider',
        function ($stateProvider, $urlRouterProvider, $locationProvider, $qProvider) {
            $locationProvider.hashPrefix('');
            $urlRouterProvider.otherwise('/');
            $qProvider.errorOnUnhandledRejections(false);

            if (abp.auth.hasPermission('Pages.Users')) {
                $stateProvider
                    .state('users', {
                        url: '/users',
                        templateUrl: '/App/Main/views/users/index.cshtml',
                        menu: 'Users' //Matches to name of 'Users' menu in ProjectNavigationProvider
                    });
                $urlRouterProvider.otherwise('/users');
            }

            if (abp.auth.hasPermission('Pages.Roles')) {
                $stateProvider
                    .state('roles', {
                        url: '/roles',
                        templateUrl: '/App/Main/views/roles/index.cshtml',
                        menu: 'Roles' //Matches to name of 'Roles' menu in ProjectNavigationProvider
                    });
                $urlRouterProvider.otherwise('/roles');
            }

            if (abp.auth.hasPermission('Pages.Tenants')) {
                $stateProvider
                    .state('tenants', {
                        url: '/tenants',
                        templateUrl: '/App/Main/views/tenants/index.cshtml',
                        menu: 'Tenants' //Matches to name of 'Tenants' menu in ProjectNavigationProvider
                    });
                $urlRouterProvider.otherwise('/tenants');
            }

            $stateProvider
                .state('home', {
                    url: '/',
                    templateUrl: '/App/Main/views/home/home.cshtml',
                    menu: 'Home' //Matches to name of 'Home' menu in ProjectNavigationProvider
                })
                .state('publications', {
                    url: '/publications',
                    templateUrl: '/App/Main/views/publications/index.cshtml',
                    menu: 'Publications' //Matches to name of 'Publications' menu in ProjectNavigationProvider
                })
                .state('courses', {
                    url: '/courses',
                    templateUrl: '/App/Main/views/courses/index.cshtml',
                    menu: 'Courses' //Matches to name of 'Courses' menu in ProjectNavigationProvider
                })
                .state('teachers', {
                    url: '/teachers',
                    templateUrl: '/App/Main/views/teachers/index.cshtml',
                    menu: 'Teachers' //Matches to name of 'Publications' menu in ProjectNavigationProvider
                })
                .state('about', {
                    url: '/about',
                    templateUrl: '/App/Main/views/about/about.cshtml',
                    menu: 'About' //Matches to name of 'About' menu in ProjectNavigationProvider
                });
        }
    ]);

})();