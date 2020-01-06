(function () {
    var controllerId = 'app.views.layout';
    angular.module('app').controller(controllerId, [
        '$rootScope','$scope', '$timeout', function ($rootScope, $scope, $timeout) {
            var vm = this;
            //Layout logic...

            abp.event.on('abp.notifications.received', function (userNotification) {
                //console.log(userNotification);
            });

            vm.activateLeftSideBar = function () {
                $timeout(function () {
                    $.AdminBSB.leftSideBar.activate();
                }, 2000);
            };

            vm.activateRightSideBar = function () {
                $timeout(function () {
                    $.AdminBSB.rightSideBar.activate();
                }, 2000);
            };


            vm.activateTopBar = function () {
                $.AdminBSB.search.activate();
                $.AdminBSB.navbar.activate();
            };

        }]);
})();